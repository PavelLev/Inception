using System.Net.Http;
using DryIoc;
using HtmlAgilityPack;
using Inception.Repository.Utility.Extensions;
using Inception.Testing;
using Inception.Testing.Results;
using Inception.Utility;
using Inception.Utility.Exceptions;
using Inception.Utility.ModelBinding;
using Inception.Utility.ModelBinding.ActionConstraint;
using Inception.Utility.Serialization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Inception
{
    public class CompositionRoot
    {
        public CompositionRoot(IContainer container)
        {
            RegisterConfiguration(container);

            RegisterUtility(container);

            RegisterReporitory(container);

            RegisterTesting(container);

            RegisterDomainName(container);
        }



        private void RegisterConfiguration(IContainer container)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("InceptionConfiguration.json");

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



        private void RegisterReporitory(IContainer container)
        {
            container.RegisterCompositionRoot<Repository.CompositionRoot>();
        }



        private void RegisterTesting(IContainer container)
        {
            container.Register<ITestingService, TestingService>(Reuse.Singleton);
        }



        private void RegisterDomainName(IContainer container)
        {
            container.Register<IDomainNameService, DomainNameService>(Reuse.InWebRequest);
        }
    }
}