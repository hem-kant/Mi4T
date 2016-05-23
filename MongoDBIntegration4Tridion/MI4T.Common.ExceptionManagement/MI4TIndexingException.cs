using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MI4T.Common.ExceptionManagement
{
    public class MI4TIndexingException :ApplicationException
    {
        public MI4TIndexingException() : base()
        {
        }
        public string Code { get; set; }

        public MI4TIndexingException(string code, string errorMessage)
			: base(errorMessage)
		{
            Code = code;
        }

        public MI4TIndexingException(string msg, Exception ex)
            : base(msg, ex)
        {
        }

        public MI4TIndexingException(string msg)
            : base(msg)
        {
        }
    }
}
