using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MI4T.Common.Logging;
using MI4T.Common.Services.DataContracts;

namespace MI4T.Common.ExceptionManagement
{
    public class ExceptionHelper
    {
        private static void LogException(MI4TIndexingException ampException)
        {
            MI4TLogger.WriteLog(ELogLevel.ERROR, ampException.Message + ", Code " + ampException.Code);
        }
        public static void HandleException(Exception exception)
        {
            MI4TIndexingException ampException = exception as MI4TIndexingException;
            if (ampException != null)
            {
                LogException(ampException);
            }
        }
        public static void HandleException(Exception exception, out MI4TServiceFault fault)
        {
            MI4TIndexingException ampException = exception as MI4TIndexingException;
            fault = new MI4TServiceFault();
            if (ampException != null)
            {
                fault.Code = ampException.Code;
                fault.Message = ampException.Message;
            }
            else
            {
                fault.Code = MI4T.Common.Services.MI4TServiceConstants.ServiceFault.UNKNOWN_EXCEPTION_CODE;
                fault.Message = MI4T.Common.Services.MI4TServiceConstants.ServiceFault.UNKNOWN_EXCEPTION_MESSAGE;
            }
        }

        public static void HandleCustomException(Exception ex, string LogMessage)
        {
            MI4TIndexingException ampEx = ex as MI4TIndexingException;
            if (ampEx != null)
            {
                MI4TLogger.WriteLog(ELogLevel.WARN, LogMessage);
            }
            else
            {
                MI4TLogger.WriteLog(ELogLevel.ERROR, LogMessage);
            }
        }
    }
}
