using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace Inception.Utility.ModelBinding
{
    public class ActionTypeService : IActionTypeService
    {
        private readonly Dictionary<ActionDescriptor, Type> _typeByActionDescriptor =
            new Dictionary<ActionDescriptor, Type>();


        private readonly ModuleBuilder _moduleBuilder;



        public ActionTypeService()
        {
            var assemblyName = new AssemblyName(Guid.NewGuid().ToString());

            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);

            _moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
        }



        public Type GetType(ActionDescriptor actionDescriptor)
        {
            if (!_typeByActionDescriptor.ContainsKey(actionDescriptor))
            {
                _typeByActionDescriptor[actionDescriptor] = GenerateType(actionDescriptor);
            }

            var type = _typeByActionDescriptor[actionDescriptor];

            return type;
        }



        private Type GenerateType(ActionDescriptor actionDescriptor)
        {
            var typeName = DisplayNameToTypeName();

            var typeBuilder = _moduleBuilder.DefineType(typeName, TypeAttributes.Public | TypeAttributes.Class);

            foreach (var parameterDescriptor in actionDescriptor.Parameters)
            {
                typeBuilder.DefineField(parameterDescriptor.Name, parameterDescriptor.ParameterType, FieldAttributes.Public);
            }

            return typeBuilder.CreateType();
        }



        private string DisplayNameToTypeName()
        {
            var typeName = Guid.NewGuid().ToString();

            return typeName;
        }
    }
}