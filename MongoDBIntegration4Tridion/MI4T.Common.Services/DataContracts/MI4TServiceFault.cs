using System.Runtime.Serialization;

namespace MI4T.Common.Services.DataContracts
{
    /// <summary>
    /// The service fault class is a data contract which would hold the error code and error messages
    /// corresponding to the error(s) occurred during service method execution
    /// </summary>
    [DataContract]
    public class MI4TServiceFault
    {
        private string code;
        /// <summary>
        /// Fault Code of the error
        /// </summary>
        [DataMember]
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private string message;

        /// <summary>
        /// Fault Message corresponding to the fault code
        /// </summary>
        [DataMember]
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }
}
