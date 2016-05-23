using System;
using System.Runtime.Serialization;

namespace MI4TIndexing.Services.DataContracts.DataContracts
{
    [DataContract]
    public class MI4TIndexingSearchRequest<T>
    {
        public MI4TIndexingSearchRequest() { }

        private T servicePayload;

        /// <summary>
        /// The service payload is a generic implementation for any service which would hold the service
        /// response data send to the consumer
        /// </summary>
        [DataMember]
        public T ServicePayload
        {
            get { return servicePayload; }
            set { servicePayload = value; }
        }
    }
}
