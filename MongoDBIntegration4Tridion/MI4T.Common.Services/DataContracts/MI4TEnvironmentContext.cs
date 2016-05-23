using System.Runtime.Serialization;


namespace MI4T.Common.Services.DataContracts
{
    [DataContract]
    public class MI4TEnvironmentContext
    {
        /// <summary>
        /// This is the base path of the binary file storage location
        /// </summary>
        [DataMember]
        public string BinaryFileBasePath { get; set; }
    }
}
