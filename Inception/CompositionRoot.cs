using DryIoc;
using Inception.Utility.ModelBinding;

namespace Inception
{
    public class CompositionRoot
    {
        public CompositionRoot(IRegistrator registrator)
        {
            registrator.Register<ICustomModelBinder, CustomModelBinder>(Reuse.Singleton);

            registrator.Register<ICustomModelBinderProvider, CustomModelBinderProvider>(Reuse.Singleton);
        }
    }
}