using DryIoc;

namespace Inception.Repository.Utility.Extensions
{
    public static class ContainerExtensions
    {
        public static void LoadCompositionRoot<T>(this IContainer container)
        {
            container.Register<ICompositionRootLoader, CompositionRootLoader>(Reuse.Singleton, ifAlreadyRegistered:IfAlreadyRegistered.Keep);


            var compositionRootLoader = container.Resolve<ICompositionRootLoader>();

            compositionRootLoader.Load<T>();
        }
    }
}
