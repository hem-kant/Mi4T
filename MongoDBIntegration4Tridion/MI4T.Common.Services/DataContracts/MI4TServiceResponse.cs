 
using System.Runtime.Serialization;


namespace MI4T.Common.Services.DataContracts
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T">Service specific response payload data contact</typeparam>
    [DataContract]
    public class MI4TServiceResponse<T>
    {
        public MI4TServiceResponse()
        {
            ResponseContext = new MI4TResponseContext();
        }
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

        private MI4TResponseContext responseContext;


        /// <summary>
        /// The response context would hold the data related to response which would be populated by  
        /// underlying service/service client
        /// </summary>
        [DataMember]
        public MI4TResponseContext ResponseContext
        {
            get { return responseContext; }
            set { responseContext = value; }
        }
    }
}
