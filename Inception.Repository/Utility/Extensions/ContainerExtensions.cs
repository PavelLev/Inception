using DryIoc;

namespace Inception.Repository.Utility.Extensions
{
    public static class ContainerExtensions
    {
        public static void RegisterCompositionRoot<TCompositionRoot>(this IContainer container)
        {
            container.Register<TCompositionRoot>(Reuse.Singleton);

            container.Resolve<TCompositionRoot>();
        }
    }
}
