using System;

namespace Inception.Utility.Exceptions
{
    public class BusinessException : Exception
    {
        private readonly string _messageFormat;



        public BusinessException(MiscellaneousConfiguration miscellaneousConfiguration, BusinessError error, string description)
        {
            _messageFormat = miscellaneousConfiguration.BusinessExceptionMessageFormat;

            Error = error;
            Description = description;
        }



        public BusinessError Error
        {
            get;
        }



        public string Description
        {
            get;
        }


        public override string Message =>
            string.Format(_messageFormat, Error, Description);
    }
}
