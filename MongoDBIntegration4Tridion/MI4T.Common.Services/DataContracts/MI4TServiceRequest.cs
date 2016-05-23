using System.Runtime.Serialization;


namespace MI4T.Common.Services.DataContracts
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T">Service specific request payload data contact</typeparam>
    [DataContract]
    public class MI4TServiceRequest<T>
    {
        public MI4TServiceRequest()
        {
        }

        private T servicePayload;

        /// <summary>
        /// The service payload is a generic implementation for any service which would hold the service
        /// request data send by the consumer
        /// </summary>
        [DataMember]
        public T ServicePayload
        {
            get { return servicePayload; }
            set { servicePayload = value; }
        }

    }
}
