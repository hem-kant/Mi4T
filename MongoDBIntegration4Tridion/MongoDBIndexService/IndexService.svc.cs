
// <author>Hem Kant</author>
// <date>22-May-2016</date>
// <summary>Class representing MongoDBIndexService to AddDcoument and RemoveDocument from MongoDB</summary>
using System; 
using System.ServiceModel;
using System.ServiceModel.Web; 
using System.ServiceModel.Activation; 
using System.Xml.Serialization;
using MI4T.IndexService.BAL;
using MI4T.IndexService.DataContracts;
using MI4T.Common.Services.Helper;
using MI4T.Common.Logging;
using MI4T.Common.Services.DataContracts;
using MI4T.Common.Services;
using MI4T.Common.ExceptionManagement;

namespace MongoDBIndexService
{
    /// <summary>
    /// This class exposes the MongoDB Index Service methods
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]

    public class IndexService : IIndexService
    {
        /// <summary>
        /// This method creates an index in Mongo 
        /// </summary>
        /// <param name="query">An object of IndexRequest need to be passed</param>
        /// <returns>Object of type IndexResponse is returned which has field Result as 0 for success and 1 for failure</returns>
        [WebInvoke(UriTemplate = "/AddDocument/", Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        public MI4TServiceResponse<IndexResponse> AddDocument(MI4TServiceRequest<IndexRequest> query)
        {

            MI4TLogger.WriteLog(ELogLevel.INFO, "Enter into method IndexService.AddDocumnet()");          
            MI4TServiceResponse<IndexResponse> serviceResponse = new MI4TServiceResponse<IndexResponse>();            
            try
            {
               
                string language = query.ServicePayload.LanguageInRequest;
                IndexResponse resultValue;

                MI4TIndexManager indexManager = new MI4TIndexManager(language);  
                resultValue = indexManager.AddDocument(query.ServicePayload);                              
                MI4TLogger.WriteLog(ELogLevel.INFO, "AddDocumnet is called publish is true");                
                serviceResponse.ServicePayload = resultValue;
            }
            catch (Exception ex)
            {

                serviceResponse.ServicePayload = new IndexResponse();
                serviceResponse.ServicePayload.Result = 1;
                serviceResponse.ServicePayload.ErrorMessage = "AddDocumnet is not called ispublish is false";
                MI4TLogger.WriteLog(ELogLevel.INFO, "AddDocumnet is not called ispublish is false" + ex.Message);  
            }
            return serviceResponse;
        }

        /// <summary>
        /// This method removes an index from MongoDB
        /// </summary>
        /// <param name="query">An object of IndexRequest need to be passed</param>
        /// <returns>Object of type IndexResponse is returned which has field Result as 0 for success and 1 for failure</returns>
        [WebInvoke(UriTemplate = "/RemoveDocument/", Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        public MI4TServiceResponse<IndexResponse> RemoveDocument(MI4TServiceRequest<IndexRequest> query)
        {
            MI4TLogger.WriteLog(ELogLevel.INFO, "Entering into method IndexService.RemoveDocument");
            MI4TServiceResponse<IndexResponse> serviceResponse = new MI4TServiceResponse<IndexResponse>();
            try
            {
                //serviceResponse.ServicePayload = new IndexResponse();
                //serviceResponse.ServicePayload.Result = 1;
                MI4TLogger.WriteLog(ELogLevel.INFO, "RemoveDocument is  called publish is true1 ");
                 
                IndexResponse resultValue;
                MI4TIndexManager indexManager = new MI4TIndexManager(query.ServicePayload.LanguageInRequest);
                resultValue = indexManager.RemoveDocument(query.ServicePayload);
                serviceResponse.ServicePayload = resultValue;
                MI4TLogger.WriteLog(ELogLevel.INFO, "RemoveDocument is  called publish is true 2");
            }
            catch (Exception ex)
            {
                serviceResponse.ServicePayload = new IndexResponse();
                serviceResponse.ServicePayload.Result = 0;
                serviceResponse.ServicePayload.ErrorMessage = "RemoveDocument is not called ispublish is false";
               MI4TLogger.WriteLog(ELogLevel.INFO, "RemoveDocument is not called ispublish is false");
                string logString = MI4TServiceConstants.LOG_MESSAGE + Environment.NewLine;
                string request = query != null ? query.ToJSONText() : "Request = NULL";
                logString = string.Concat(logString, string.Format("Service Request: {0}", request),
                                            Environment.NewLine, string.Format("{0}{1}", ex.Message, ex.StackTrace));
                MI4TLogger.WriteLog(ELogLevel.ERROR, logString);
                CatchException<IndexResponse>(ex, serviceResponse);
            }
           MI4TLogger.WriteLog(ELogLevel.INFO, "Exiting from method IndexService.RemoveDocument");
            return serviceResponse;
        } 

        private void CatchException<T>(Exception ex, MI4TServiceResponse<T> serviceResponse)
        {
            MI4TServiceFault fault = new MI4TServiceFault();
            ExceptionHelper.HandleException(ex, out fault);
            serviceResponse.ResponseContext.FaultCollection.Add(fault);
        }
    }

     
}
