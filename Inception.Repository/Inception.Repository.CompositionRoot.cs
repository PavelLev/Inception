using System;
using DryIoc;
using Inception.Repository.Utility;
using Inception.Repository.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Inception.Repository
{
    /// <summary>
    /// This class is used to manage registrations of DryIoc container
    /// All registrations should be in constructor or called by constructor methods
    /// To apply registrations use <see cref="ContainerExtensions.LoadCompositionRoot{T}(IContainer)"/>
    /// </summary>
    public class CompositionRoot
    {
        public CompositionRoot(IContainer container)
        {
            RegisterConfiguration(container);

            RegisterInceptionDbContext(container);

            RegisterRepositories(container);
        }



        private void RegisterConfiguration(IContainer container)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("Inception.Repository.Configuration.json");

            var configuration = builder.Build();


            container.Configure<DbContextConfiguration>(configuration.GetSection("DbContext"));
        }



        private void RegisterInceptionDbContext(IContainer container)
        { 
            container.RegisterDbContext((IResolver resolverContext, DbContextOptionsBuilder<InceptionDbContext> optionsBuilder) =>
            {
                var connectionString = resolverContext.Resolve<DbContextConfiguration>().ConnectionString;

                optionsBuilder.UseSqlServer(connectionString);


                //optionsBuilder.UseLoggerFactory
                //    (
                //    resolverContext.Resolve<LoggerFactory>()
                //    );
            });
        }



        private void RegisterRepositories(IContainer container)
        {
            container.Register(typeof(IGenericRepository<>), typeof(GenericRepository<>), setup: Setup.With(useParentReuse: true));
        }
    }
}
