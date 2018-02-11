using System.Collections.Generic;
using DryIoc;
using Inception.Repository.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Inception.Repository
{
    public class CompositionRoot
    {
        public CompositionRoot(IContainer container)
        {
            RegisterConfiguration(container);

            RegisterDbContextLogging(container);

            RegisterInceptionDbContext(container);

            RegisterRepositories(container);
        }



        private void RegisterConfiguration(IContainer container)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("InceptionRepositoryConfiguration.json");

            var configuration = builder.Build();


            container.Configure<DbContextConfiguration>(configuration.GetSection("DbContext"));
        }



        private void RegisterDbContextLogging(IContainer container)
        {
            container.Register(Made.Of(() => new LoggerFactory(Arg.Of<IEnumerable<ILoggerProvider>>())), Reuse.Singleton);

            container.UseInstance<ILoggerProvider>(new ConsoleLoggerProvider((_, __) => true, true));
        }



        private void RegisterInceptionDbContext(IContainer container)
        { 
            container.RegisterDbContext((IResolver resolverContext, DbContextOptionsBuilder<InceptionDbContext> optionsBuilder) =>
            {
                var connectionString = resolverContext.Resolve<DbContextConfiguration>().ConnectionString;

                optionsBuilder.UseSqlServer(connectionString);


                optionsBuilder.UseLoggerFactory
                    (
                    resolverContext.Resolve<LoggerFactory>()
                    );
            });
        }



        private void RegisterRepositories(IContainer container)
        {
            container.Register(typeof(IGenericRepository<>), typeof(GenericRepository<>), Reuse.InWebRequest);
        }
    }
}
