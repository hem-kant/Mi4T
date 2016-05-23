
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using MI4T.Common.Services.DataContracts;


namespace MI4T.Common.Services.DataContracts
{
    /// <summary>
    /// GenericIndexingReposneContext class holds the information about the service response context
    /// like faults(error) information and other useful information related to service response
    /// </summary>
    [DataContract]
    public  class MI4TResponseContext
    {
        public MI4TResponseContext()
        {
            FaultCollection = new Collection<MI4TServiceFault>();
        }

        //Contains the Environment Context of the service response
        [DataMember]
        public MI4TEnvironmentContext EnvironmentContext { get; set; }


        private Collection<MI4TServiceFault> faultCollection;

        /// <summary>
        /// Fault Collection, a collection of GenericIndexingServiceFault
        /// </summary>
        [DataMember]
        public Collection<MI4TServiceFault> FaultCollection
        {
            get { return faultCollection; }
            set { faultCollection = value; }
        }

        /// <summary>
        /// public to check if response got any fault
        /// </summary>
        public bool IsFault
        {
            get { return faultCollection.Count > 0; }
            set { }
        }
    }
}
