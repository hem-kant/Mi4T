// <author>Hem Kant</author>
// <date>22-May-2016</date>
// <summary>Interface IMongoDBSearchService with OperationContract GetContentFromMongoDB</summary>
using MI4T.Common.Services.DataContracts; 
using MI4TSearching.SearchService.BAL.Model; 
using System.IO; 
using System.ServiceModel;
 

namespace MongoDBSearchService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IMongoDBSearchService
    {
        [OperationContract]
        Stream GetContentFromMongoDB(MI4TServiceRequest<SearchRequest> request);
    }
     
}
