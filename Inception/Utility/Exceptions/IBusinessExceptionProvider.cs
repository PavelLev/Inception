namespace Inception.Utility.Exceptions
{
    public interface IBusinessExceptionProvider
    {
        BusinessException Create(BusinessError error, string description);
    }
}
