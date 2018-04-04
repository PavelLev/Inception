using System;
using System.Net.Http;
using DryIoc;
using HtmlAgilityPack;
using Inception.Repository.Utility.Extensions;
using Inception.Testing;
using Inception.Utility;
using Inception.Utility.Exceptions;
using Inception.Utility.ModelBinding;
using Inception.Utility.ModelBinding.ActionConstraint;
using Inception.Utility.Parallel;
using Inception.Utility.Serialization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Inception
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

            RegisterUtility(container);

            RegisterRepository(container);

            RegisterTesting(container);

            RegisterDomainName(container);
        }



        private void RegisterConfiguration(IContainer container)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("Inception.Configuration.json");

            var configuration = builder.Build();


            container.Configure<TestingConfiguration>(configuration.GetSection("Testing"));


            container.Configure<DomainNameConfiguration>(configuration.GetSection("DomainName"));


            container.Configure<MiscellaneousConfiguration>(configuration.GetSection("Miscellaneous"));

        }



        private void RegisterUtility(IContainer container)
        {
            container.Register<ICustomActionConstraint, CustomActionConstraint>(Reuse.Singleton);

            container.Register<ICustomActionModelConvention, CustomActionModelConvention>(Reuse.Singleton);


            container.Register<BusinessException>();

            container.Register<IBusinessExceptionProvider, BusinessExceptionProvider>(Reuse.Singleton);

            container.Register<IExceptionFilter, ExceptionFilter>(Reuse.Singleton);


            container.Register<IActionTypeService, ActionTypeService>(Reuse.Singleton);

            container.Register<ICustomModelBinder, CustomModelBinder>(Reuse.Singleton);

            container.Register<ICustomModelBinderProvider, CustomModelBinderProvider>(Reuse.Singleton);

            container.Register<IPostActionModelDeserializer, PostActionModelDeserializer>(Reuse.Singleton);


            container.Register(typeof(ICluster<>), typeof(Cluster<>));


            container.Register<IContractResolver, RequireObjectPropertiesContractResolver>
                (
                Reuse.Singleton,
                setup: Setup.With(condition: IsParentIPostActionModelDeserializer)
                );

            container.Register<JsonSerializer>
                (
                Reuse.Singleton, 
                Made.Of
                    (
                    () => new JsonSerializer
                    {
                        ContractResolver = Arg.Of<IContractResolver>(),
                        MissingMemberHandling = MissingMemberHandling.Error
                    }
                    ),
                Setup.With(condition: IsParentIPostActionModelDeserializer)
                );


            bool IsParentIPostActionModelDeserializer(Request request) =>
                request.Parent.ServiceType == typeof(IPostActionModelDeserializer);


            container.Register<HttpClient>(Reuse.Singleton, Made.Of(() => new HttpClient()));

            container.Register<HtmlDocument>();


            container.Register<IHtmlParser, HtmlParser>(Reuse.Singleton);

            container.Register<IUriService, UriService>(Reuse.Singleton);
        }



        private void RegisterRepository(IContainer container)
        {
            container.LoadCompositionRoot<Repository.CompositionRootToken>();
        }



        private void RegisterTesting(IContainer container)
        {
            container.Register<ISiteTestingService, ClusterSiteTestingService>(Reuse.Singleton);

            container.Register<ILinkTestingService, ClusterLinkTestingService>(Reuse.Singleton);
        }



        private void RegisterDomainName(IContainer container)
        {
            container.Register<IDomainNameService, DomainNameService>(Reuse.InWebRequest);
        }
    }
}