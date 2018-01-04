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



        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var descriptor = bindingContext.ActionContext.ActionDescriptor;

            var parameters = descriptor.Parameters.ToList();

            if (!_typeByActionDescriptor.ContainsKey(descriptor))
            {
                _typeByActionDescriptor[descriptor] = GenerateType(descriptor, parameters);
            }


            var stream = bindingContext.HttpContext.Request.Body;
            
            if (!_modelBindingIntermediateResultByStream.ContainsKey(stream))
            {
                _modelBindingIntermediateResultByStream[stream] = ParseBody(parameters, stream);
            }


            var modelBindingIntermediateResult = _modelBindingIntermediateResultByStream[stream];

            var parameterName = bindingContext.FieldName;

            var model = modelBindingIntermediateResult.ActionParameters[parameterName];

            bindingContext.Result = ModelBindingResult.Success(model);


            modelBindingIntermediateResult.BindingCount++;

            if (modelBindingIntermediateResult.BindingCount == parameters.Count)
            {
                _modelBindingIntermediateResultByStream.Remove(stream);
            }


            return Task.CompletedTask;
        }

        private Type GenerateType(ActionDescriptor descriptor, List<ParameterDescriptor> parameters)
        {
            var assemblyName = new AssemblyName(descriptor.Id);


        }


        private ModelBindingIntermediateResult ParseBody(List<ParameterDescriptor> parameters, Stream stream)
        {
            var modelBindingIntermediateResult = new ModelBindingIntermediateResult();

            var jsonSerialiser = new JsonSerializer();

            using (var streamReader = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                // Start object
                jsonTextReader.Read();

                while (parameters.Count > 0)
                {
                    jsonTextReader.Read();
                    if (jsonTextReader.TokenType != JsonToken.PropertyName)
                    {
                        throw new JsonSerializationException(
                            $"Expected property name at line {jsonTextReader.LineNumber}, position {jsonTextReader.LinePosition}");
                    }


                    var parameterToMatch = parameters.First(parameter => parameter.Name == jsonTextReader.Path);


                    object value = null;

                    if (
                        parameterToMatch.ParameterType.IsPrimitive
                        ||
                        parameterToMatch.ParameterType == typeof(decimal)
                        ||
                        parameterToMatch.ParameterType == typeof(string)
                        ||
                        parameterToMatch.ParameterType == typeof(DateTime)
                    )
                    {
                        jsonTextReader.Read();

                        ;

                    }

                    if (value.GetType() != parameterToMatch.ParameterType)
                    {
                        throw new JsonSerializationException(
                            $"Expected value of type {parameterToMatch.ParameterType}, actual value has type {value.GetType()} at line {jsonTextReader.LineNumber}, position {jsonTextReader.LinePosition}");
                    }

                    modelBindingIntermediateResult.ActionParameters[parameterToMatch.Name] = value;

                    parameters.Remove(parameterToMatch);
                }


                // End object
                jsonTextReader.Read();

                if (jsonTextReader.TokenType != JsonToken.EndObject)
                {
                    throw new JsonSerializationException(
                        $"Expected end object at line {jsonTextReader.LineNumber}, position {jsonTextReader.LinePosition}");
                }
            }

            return modelBindingIntermediateResult;
        }
    }
}