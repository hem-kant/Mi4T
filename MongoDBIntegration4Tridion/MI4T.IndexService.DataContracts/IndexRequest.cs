using System.Runtime.Serialization;
using MI4T.Common.Services.DataContracts;
using System.Collections.Generic;
using System;

namespace MI4T.IndexService.DataContracts
{
    [DataContract]
    public class IndexRequest
    {
        [DataMember]
        public string ItemURI { get; set; }

        [DataMember]
        public string DCP { get; set; }

        [DataMember]
        public string ContentType { get; set; }

        [DataMember]
        public string LanguageInRequest { get; set; }
         
    }
}
