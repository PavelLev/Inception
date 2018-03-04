using System.Net.Http;
using DryIoc;
using HtmlAgilityPack;
using Inception.Repository.Utility.Extensions;
using Inception.Testing;
using Inception.Utility;
using Inception.Utility.ModelBinding;
using Inception.Utility.ModelBinding.ActionConstraint;
using Microsoft.Extensions.Configuration;

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

        }



        private void RegisterUtility(IContainer container)
        {
            container.Register<ICustomActionConstraint, CustomActionConstraint>(Reuse.Singleton);

            container.Register<ICustomActionModelConvention, CustomActionModelConvention>(Reuse.Singleton);


            container.Register<IActionTypeService, ActionTypeService>(Reuse.Singleton);

            container.Register<ICustomModelBinder, CustomModelBinder>(Reuse.Singleton);

            container.Register<ICustomModelBinderProvider, CustomModelBinderProvider>(Reuse.Singleton);

            container.Register<IPostActionModelDeserializer, PostActionModelDeserializer>(Reuse.Singleton);


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