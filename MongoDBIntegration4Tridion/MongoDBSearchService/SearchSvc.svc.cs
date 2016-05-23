// <author>Hem Kant</author>
// <date>22-May-2016</date>
// <summary>Class representing MongoDBSearchService to GetContentFromMongoDB from MongoDB</summary>

using MI4T.Common.Configuration;
using MI4T.Common.Configuration.Interface;
using MI4T.Common.Logging;
using MI4T.Common.Services.DataContracts;
using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using MI4T.Common.Services.Helper;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using System.Collections;
using System.Collections.Generic;
using MI4TSearching.SearchService.BAL.Model;
using MI4T.IndexService.BAL.ContentMapper;
using System.Threading.Tasks;

namespace MongoDBSearchService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class SearchSvc : IMongoDBSearchService
    {
        private IPropertyConfiguration propertyConfiguration;
        private static IPropertyConfiguration propConfiguration;
        private static object containerLock;

        [WebInvoke(UriTemplate = "/GetContentFromMongoDB/", Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        public Stream GetContentFromMongoDB(MI4TServiceRequest<SearchRequest> request)
        {
            string resultJSON = string.Empty;
            MI4TLogger.WriteLog(ELogLevel.INFO, "Entering into GetContentFromMongoDB");
            try
            {
                if (request != null && request.ServicePayload != null)
                {
                    string MongoDBIndexConfigPath = Utility.GetConfigurationValue("IndexServiceConfig");
                    propConfiguration = ConfigurationManager.GetInstance().GetConfiguration(MongoDBIndexConfigPath)
                        as IPropertyConfiguration;
                    containerLock = new object();
                    string result = string.Empty;
                    var connectionString = propConfiguration.GetString(MI4TServicesConstants.mongoDB_URL);
                    var client = new MongoClient(connectionString);
                    var server = client.GetServer();
                    var database = server.GetDatabase(propConfiguration.GetString(MI4TServicesConstants.dbName));
                    var collection = database.GetCollection<MongoDBModelSearch>(propConfiguration.GetString(MI4TServicesConstants.tableName));

                    var andList = new List<IMongoQuery>();
                    foreach (DictionaryEntry entry in request.ServicePayload.Filters)
                    {
                        MI4TLogger.WriteLog(ELogLevel.INFO, "Reading request.ServicePayload.Filters");
                        switch (request.ServicePayload.QueryType.ToUpper())
                        {
                            case "AND":
                                andList.Add(Query.EQ(entry.Key.ToString(), entry.Value.ToString()));
                                break;
                            case "OR":
                                andList.Add(Query.Or(Query.EQ(entry.Key.ToString(), entry.Value.ToString())));
                                break;
                            default:
                                andList.Add(Query.Not(Query.EQ(entry.Key.ToString(), entry.Value.ToString())));
                                break;
                        }
                        
                    }
                    var query = Query.And(andList);
                    MI4TLogger.WriteLog(ELogLevel.INFO, "Query generated");
                    //Map/Reduce            
                    var map =
                        "function() {" +
                        "    for (var key in this) {" +
                        "        emit(key, { count : 1 });" +
                        "    }" +
                        "}";

                    var reduce =
                        "function(key, emits) {" +
                        "    total = 0;" +
                        "    for (var i in emits) {" +
                        "        total += emits[i].count;" +
                        "    }" +
                        "    return { count : total };" +
                        "}";

                    var mr = collection.MapReduce(map, reduce);
                    foreach (var document in mr.GetResults())
                    {
                        document.ToJson();
                    }
                 
                    MI4TLogger.WriteLog(ELogLevel.INFO, "Calling collection.FindOne(query)");
                  //  var entity = collection.Find(query).ToListAsync();
                    var result1 = collection.FindAs<MongoDBModelSearch>(query);
                    resultJSON = result1.ToJson();
                    
                    MI4TLogger.WriteLog(ELogLevel.INFO, "OUTPUT: " + resultJSON);
                }
            }
            catch (Exception ex)
            {
                MI4TLogger.WriteLog(ELogLevel.ERROR, "ERROR: " + ex.Message + ex.StackTrace);
            }
            return new MemoryStream(Encoding.UTF8.GetBytes(resultJSON));
        }

       
    }
}
