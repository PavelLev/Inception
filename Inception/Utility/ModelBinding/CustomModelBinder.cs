using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Inception.Utility.ModelBinding
{
    public class CustomModelBinder: ICustomModelBinder
    {
        private readonly IPostActionModelDeserializer _postActionModelDeserializer;

        private readonly Dictionary<HttpRequest, ModelBindingIntermediateResult> _modelBindingIntermediateResultByHttpRequest =
            new Dictionary<HttpRequest, ModelBindingIntermediateResult>();



        public CustomModelBinder(IPostActionModelDeserializer postActionModelDeserializer)
        {
            _postActionModelDeserializer = postActionModelDeserializer;
        }



        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var httpRequest = bindingContext.HttpContext.Request;
            
            if (!_modelBindingIntermediateResultByHttpRequest.ContainsKey(httpRequest))
            {
                var deserializedObject = _postActionModelDeserializer.GetDeserializedObject(httpRequest);

                _modelBindingIntermediateResultByHttpRequest[httpRequest] = new ModelBindingIntermediateResult(deserializedObject);
            }


            var modelBindingIntermediateResult = _modelBindingIntermediateResultByHttpRequest[httpRequest];

            var parameterName = bindingContext.FieldName;

            var model = modelBindingIntermediateResult.DeserializedObject.GetType()
                .GetField(parameterName)
                .GetValue(modelBindingIntermediateResult.DeserializedObject);

            bindingContext.Result = ModelBindingResult.Success(model);


            modelBindingIntermediateResult.IncrementBindingCount();

            if (modelBindingIntermediateResult.BindingCount == bindingContext.ActionContext.ActionDescriptor.Parameters.Count)
            {
                _modelBindingIntermediateResultByHttpRequest.Remove(httpRequest);

                _postActionModelDeserializer.RemoveDeserializedObject(httpRequest);
            }

            return Task.CompletedTask;
        }
    }
}