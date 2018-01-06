using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace Inception.Utility.ModelBinding
{
    public class CustomModelBinderProvider: ICustomModelBinderProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public CustomModelBinderProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IModelBinder GetBinder(ModelBinderProviderContext modelBinderProviderContext)
        {
            if (modelBinderProviderContext == null)
            {
                throw new ArgumentNullException(nameof(modelBinderProviderContext));
            }

            return _serviceProvider.GetRequiredService<ICustomModelBinder>();
        }
    }
}