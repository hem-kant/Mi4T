
// <author>Hem Kant</author>
// <date>22-May-2016</date>
using System.ServiceModel;
using MI4T.Common.Services.DataContracts;
using MI4T.IndexService.DataContracts;

namespace MongoDBIndexService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IIndexService
    {

        /// <summary>
        /// operation contract to create index
        /// </summary>
        /// <param name="query">An object of type IndexRequest need to be passed </param>
        /// <returns>IndexResponse object which will have Result as 1 for failure and 0 for success</returns>       
        [OperationContract]
        MI4TServiceResponse<IndexResponse> AddDocument(MI4TServiceRequest<IndexRequest> query);

        /// <summary>
        /// operation contract to delete index
        /// </summary>
        /// <param name="query">An object of type IndexRequest need to be passed  </param>
        /// <returns>IndexResponse object which will have Result as 0 for failure and 1 for success</returns>
        [OperationContract]
        MI4TServiceResponse<IndexResponse> RemoveDocument(MI4TServiceRequest<IndexRequest> query);

        
    }
}
