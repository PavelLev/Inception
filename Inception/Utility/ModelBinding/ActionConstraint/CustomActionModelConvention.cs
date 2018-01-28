using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;

namespace Inception.Utility.ModelBinding.ActionConstraint
{
    public class CustomActionModelConvention : ICustomActionModelConvention
    {
        private readonly IServiceProvider _serviceProvider;

        public CustomActionModelConvention(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Apply(ActionModel action)
        {
            if (!action.Parameters.Any())
            {
                return;
            }


            foreach (var actionSelectorModel in action.Selectors)
            {
                actionSelectorModel.ActionConstraints.Add(_serviceProvider.GetService<ICustomActionConstraint>());
            }
        }
    }
}