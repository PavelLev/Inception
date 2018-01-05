using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Inception.Utility.ModelBinding
{
    public class CustomModelBinder: ICustomModelBinder
    {
        public static CustomModelBinder Instance { get; } = new CustomModelBinder();

        private readonly Dictionary<Stream, ModelBindingIntermediateResult> _modelBindingIntermediateResultByStream =
            new Dictionary<Stream, ModelBindingIntermediateResult>();

        private readonly Dictionary<ActionDescriptor, Type> _typeByActionDescriptor =
            new Dictionary<ActionDescriptor, Type>();


        private readonly AssemblyBuilder _assemblyBuilder;

        private readonly ModuleBuilder _moduleBuilder;

        public CustomModelBinder()
        {
            var assemblyName = new AssemblyName(Guid.NewGuid().ToString());

            _assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);

            _moduleBuilder = _assemblyBuilder.DefineDynamicModule("MainModule");
        }


        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var descriptor = bindingContext.ActionContext.ActionDescriptor;

            var parameterDescriptors = descriptor.Parameters.ToList();

            if (!_typeByActionDescriptor.ContainsKey(descriptor))
            {
                _typeByActionDescriptor[descriptor] = GenerateType(descriptor, parameterDescriptors);
            }


            var stream = bindingContext.HttpContext.Request.Body;
            
            if (!_modelBindingIntermediateResultByStream.ContainsKey(stream))
            {
                _modelBindingIntermediateResultByStream[stream] = ParseBody(stream, _typeByActionDescriptor[descriptor]);
            }


            var modelBindingIntermediateResult = _modelBindingIntermediateResultByStream[stream];

            var parameterName = bindingContext.FieldName;

            var model = modelBindingIntermediateResult.DeserializedObject[parameterName];

            bindingContext.Result = ModelBindingResult.Success(model);


            modelBindingIntermediateResult.IncrementBindingCount();

            if (modelBindingIntermediateResult.BindingCount == parameterDescriptors.Count)
            {
                _modelBindingIntermediateResultByStream.Remove(stream);
            }


            return Task.CompletedTask;
        }

        private Type GenerateType(ActionDescriptor descriptor, IEnumerable<ParameterDescriptor> parameterDescriptors)
        {
            var typeBuilder = _moduleBuilder.DefineType(descriptor.Id, TypeAttributes.Public | TypeAttributes.Class);

            foreach (var parameterDescriptor in parameterDescriptors)
            {
                typeBuilder.DefineField(parameterDescriptor.Name, parameterDescriptor.ParameterType,
                    FieldAttributes.Public);
            }

            return typeBuilder.CreateType();
        }


        private ModelBindingIntermediateResult ParseBody(Stream stream, Type type)
        {
            var jsonSerialiser = new JsonSerializer();

            using (var streamReader = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                var deserializedObject = jsonSerialiser.Deserialize(jsonTextReader, type);

                var modelBindingIntermediateResult = new ModelBindingIntermediateResult(deserializedObject);

                return modelBindingIntermediateResult;
            }
        }
    }
}