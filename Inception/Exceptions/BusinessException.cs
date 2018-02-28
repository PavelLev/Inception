using System;
using Inception.Utility;

namespace Inception.Exceptions
{
    public class BusinessException : Exception
    {
        private readonly string _messageFormat;



        public BusinessException(MiscellaneousConfiguration miscellaneousConfiguration)
        {
            _messageFormat = miscellaneousConfiguration.BusinessExceptionMessageFormat;
        }



        public BusinessError BusinessError
        {
            get;
            set;
        }



        public string ErrorDescription
        {
            get;
            set;
        }


        public override string Message =>
            string.Format(_messageFormat, BusinessError, ErrorDescription);
    }
}
