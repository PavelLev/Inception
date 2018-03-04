using System;

namespace Inception.Utility.Exceptions
{
    public class BusinessExceptionProvider : IBusinessExceptionProvider
    {
        private readonly Func<BusinessError, string, BusinessException> _createBusinessExceptionFunc;



        public BusinessExceptionProvider(Func<BusinessError, string, BusinessException> createBusinessExceptionFunc)
        {
            _createBusinessExceptionFunc = createBusinessExceptionFunc;
        }



        public BusinessException Create(BusinessError businessError, string errorDescription) =>
            _createBusinessExceptionFunc(businessError, errorDescription);
    }
}