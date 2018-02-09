using DryIoc;
using Inception.Repository.Utility.Extensions;

namespace Inception.Repository
{
    internal class StaticContainer
    {
        private static IResolverContext _instance;

        public static IResolverContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    var container = new Container();

                    container.RegisterCompositionRoot<CompositionRoot>();

                    _instance = container.OpenScope(Reuse.WebRequestScopeName);
                }

                return _instance;
            }
        }
    }
}
