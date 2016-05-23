 
using System.Runtime.Serialization;
using MI4T.Common.Services.DataContracts;


namespace MI4T.IndexService.DataContracts
{
    [DataContract]
    public class IndexResponse
    {
        [DataMember]
        public int Result { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }
    }
}
