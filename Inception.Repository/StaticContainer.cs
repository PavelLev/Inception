using System.Collections.Generic;
using DryIoc;
using Inception.Repository.Utility.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

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

                    container.Register<ILoggerFactory, LoggerFactory>(Made.Of(() => new LoggerFactory(Arg.Of<IEnumerable<ILoggerProvider>>())), Reuse.Singleton);

                    container.UseInstance<ILoggerProvider>(new ConsoleLoggerProvider((_, __) => true, true));

                    container.LoadCompositionRoot<CompositionRootToken>();

                    _instance = container.OpenScope(Reuse.WebRequestScopeName);
                }

                return _instance;
            }
        }
    }
}
