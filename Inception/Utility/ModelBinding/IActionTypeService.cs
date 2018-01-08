using System;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace Inception.Utility.ModelBinding
{
    public interface IActionTypeService
    {
        Type GetType(ActionDescriptor actionDescriptor);
    }
}