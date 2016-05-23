using MI4T.Common.Configuration.Interface;
using MI4T.Common.Configuration;
using MI4T.Common.Logging;
using MI4T.IndexService.DataContracts;
using MI4T.IndexService.DataHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using MI4T.Common.Services.DataContracts;
using MI4T.Common.Services;
using MI4T.Common.Services.Helper;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using MI4T.IndexService.BAL.ContentMapper;
using MI4T.IndexService.BAL.AsyncData;


namespace MI4T.IndexService.BAL
{
    public class MI4TIndexManager
    {
        private IPropertyConfiguration propertyConfiguration;
        private static IPropertyConfiguration propConfiguration;
        private static object containerLock;

        public object ServiceConstants { get; private set; }

        /// <summary>
        /// Singleton MI4TIndexManager static constructor
        /// </summary>
        static MI4TIndexManager()
        {
            try
            {
                string MongoDBIndexConfigPath = Utility.GetConfigurationValue("SearchIndexServiceConfig");
                propConfiguration = ConfigurationManager.GetInstance().GetConfiguration(MongoDBIndexConfigPath)
                    as IPropertyConfiguration;
                containerLock = new object();
                MI4TLogger.WriteLog(ELogLevel.DEBUG, "Config Path: " + MongoDBIndexConfigPath);
            }
            catch (Exception ex)
            {
                MI4TLogger.WriteLog(ELogLevel.ERROR, ex.Message + ex.StackTrace);
                throw ex;
            }
            MI4TLogger.WriteLog(ELogLevel.DEBUG, "Exiting MI4TIndexManager.MI4TIndexManager()");
        }
        public MI4TIndexManager(string Langauge)
        {
           
            MI4TLogger.WriteLog(ELogLevel.INFO, "Entering MI4TIndexManager:" +
            Langauge);
            try
            {
                string MongoDBURL = propConfiguration.GetString(MI4TServicesConstants.mongoDB_URL);
                MI4TLogger.WriteLog(ELogLevel.INFO, "Mongo URL: " + MongoDBURL);
            }
            catch (Exception ex)
            {
                MI4TLogger.WriteLog(ELogLevel.ERROR, ex.Message + ex.StackTrace);
                throw;
            }
        }
        public IndexResponse AddDocument(IndexRequest query)
        {
             MI4TLogger.WriteLog(ELogLevel.INFO,
            "Entering MI4TIndexManager.AddDocument for TCM URI: " +
            query.ItemURI);
            
            IndexResponse response = new IndexResponse();

            OperationResult result = OperationResult.Failure;
            try
            {
                XmlDocument doc = new XmlDocument();
                string ID = string.Empty;           
                doc.LoadXml(Utility.UpdateContentTypeXML(Regex.Replace(query.DCP.ToString(), @"\b'\b", "")));                
                var bln = Deserialize<MongoDBModel>(doc);
                                
                var conString = propConfiguration.GetString(MI4TServicesConstants.mongoDB_URL);
                MI4TLogger.WriteLog(ELogLevel.INFO, "conString: " +
           conString);
                /// creating MongoClient
                var client = new MongoClient(conString);

                ///Get the database
                var DB = client.GetDatabase(propConfiguration.GetString(MI4TServicesConstants.dbName));
                MI4TLogger.WriteLog(ELogLevel.INFO, "dbName: " +
         propConfiguration.GetString(MI4TServicesConstants.dbName));

                ///Get the collcetion from the DB in which you want to insert the data                
                var collection = DB.GetCollection<MongoDBModel>(propConfiguration.GetString(MI4TServicesConstants.tableName));
                MI4TLogger.WriteLog(ELogLevel.INFO, "tableName: " +
                    propConfiguration.GetString(MI4TServicesConstants.tableName));
                //var filter = Builders<MongoDBModel>.Filter.Eq("ItemURI", bln.ItemURI);
                //var result11 =  collection.Find(filter).ToListAsync();

                ///Insert data in to MongoDB
                collection.InsertOne(bln);
                result = OperationResult.Success;
                
            }
            catch (Exception ex)
            {
                string logString = MI4TServiceConstants.LOG_MESSAGE + Environment.NewLine;

                logString = string.Concat(logString,
                                          Environment.NewLine,
                                          string.Format("{0}{1}", ex.Message, ex.StackTrace));

                MI4TLogger.WriteLog(ELogLevel.ERROR, logString);
               result = OperationResult.Failure;
            }
            response.Result = (int)result;
           MI4TLogger.WriteLog(ELogLevel.INFO,
                                 "Exiting MI4TIndexManager.AddDocument, Result: " +
                                  result.ToString());

            return response;
        }
        
        /// <summary>
        /// This method removes an index from Mongo 
        /// </summary>
        /// <param name="query">IndexRequest containing delete criteria</param>
        /// <returns>IndexResponse indicating success or failure</returns>
        public IndexResponse RemoveDocument(IndexRequest query)
        {
            MI4TLogger.WriteLog(ELogLevel.INFO, "Entering MI4TIndexManager.RemoveDocument for TCM URI: " +
                                 query.ItemURI);
            IndexResponse response = new IndexResponse();

            OperationResult result = OperationResult.Failure;
            try
            {
                
                MongoServerSettings settings = new MongoServerSettings();
                settings.Server = new MongoServerAddress("localhost", 27017);
                // Create server object to communicate with our server
                MongoServer server = new MongoServer(settings);

                MongoDatabase myDB = server.GetDatabase("customerDatabase");

                MongoCollection<BsonDocument> records = myDB.GetCollection<BsonDocument>("article");
                var query1 = Query.EQ("ItemURI", query.ItemURI);
                records.Remove(query1);
                
                result = OperationResult.Success;
                MI4TLogger.WriteLog(ELogLevel.INFO, "Exit MI4TIndexManager.RemoveDocument for TCM URI: " +
                                 query.ItemURI +" result "+ result);
            }
            catch (Exception ex)
            {
                string logString = MI4TServiceConstants.LOG_MESSAGE + Environment.NewLine;
                logString = string.Concat(logString,
                                          string.Format("Item URI : {0}", query.ItemURI),
                                          Environment.NewLine, string.Format("{0}{1}", ex.Message, ex.StackTrace));
               MI4TLogger.WriteLog(ELogLevel.ERROR, logString);
               result = OperationResult.Failure;
            }

            response.Result = (int)result;

            MI4TLogger.WriteLog(ELogLevel.INFO,
                                  "Exiting MI4TIndexManager.RemoveDocument, Result: " +
                                  result.ToString());

            return response;
        }
        public static T Deserialize<T>(XmlDocument xmlDocument)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));

            StringReader reader = new StringReader(xmlDocument.InnerXml);
            XmlReader xmlReader = new XmlTextReader(reader);
            //Deserialize the object.
            return (T)ser.Deserialize(xmlReader);
        }
    }
    
}
