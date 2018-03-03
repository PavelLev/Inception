namespace Inception.Utility.Exceptions
{
    public interface IBusinessExceptionProvider
    {
        BusinessException Create(BusinessError businessError, string errorDescription);
    }
}
