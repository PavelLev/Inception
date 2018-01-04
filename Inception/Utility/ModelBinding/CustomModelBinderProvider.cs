using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Inception.Utility.ModelBinding
{
    public class CustomModelBinderProvider: ICustomModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext modelBinderProviderContext)
        {
            if (modelBinderProviderContext == null)
            {
                throw new ArgumentNullException(nameof(modelBinderProviderContext));
            }

            return CustomModelBinder.Instance;
        }
    }
}