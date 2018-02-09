using DryIoc;
using Inception.Repository.Utility.Extensions;
using Inception.Utility.ModelBinding;
using Inception.Utility.ModelBinding.ActionConstraint;

namespace Inception
{
    public class CompositionRoot
    {
        public CompositionRoot(IContainer container)
        {
            RegisterUtility(container);

            RegisterReporitory(container);
        }



        private void RegisterUtility(IRegistrator registrator)
        {
            registrator.Register<ICustomActionConstraint, CustomActionConstraint>(Reuse.Singleton);

            registrator.Register<ICustomActionModelConvention, CustomActionModelConvention>(Reuse.Singleton);


            registrator.Register<IActionTypeService, ActionTypeService>(Reuse.Singleton);

            registrator.Register<ICustomModelBinder, CustomModelBinder>(Reuse.Singleton);

            registrator.Register<ICustomModelBinderProvider, CustomModelBinderProvider>(Reuse.Singleton);

            registrator.Register<IPostActionModelDeserializer, PostActionModelDeserializer>(Reuse.Singleton);
        }



        private void RegisterReporitory(IContainer container)
        {
            container.RegisterCompositionRoot<Repository.CompositionRoot>();
        }
    }
}