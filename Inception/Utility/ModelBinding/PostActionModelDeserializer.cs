using System;
using System.Collections.Generic;
using System.IO;
using Inception.Utility.Serialization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Inception.Utility.ModelBinding
{
    public class PostActionModelDeserializer : IPostActionModelDeserializer
    {
        private readonly Dictionary<HttpRequest, JObject> _jObjectByHttpRequest = new Dictionary<HttpRequest, JObject>();
        
        private readonly Dictionary<HttpRequest, object> _objectByHttpRequest = new Dictionary<HttpRequest, object>();



        public void ParseBody(HttpRequest httpRequest, Type type)
        {
            JObject jObject;

            if (_jObjectByHttpRequest.ContainsKey(httpRequest))
            {
                jObject = _jObjectByHttpRequest[httpRequest];
            }
            else
            {
                using (var streamReader = new StreamReader(httpRequest.Body))
                using (var jsonTextReader = new JsonTextReader(streamReader))
                {
                    jObject = JObject.Load(jsonTextReader);
                }

                _jObjectByHttpRequest[httpRequest] = jObject;
            }

            
            if (!_objectByHttpRequest.ContainsKey(httpRequest))
            {
                var jsonSerialiser = new JsonSerializer
                {
                    MissingMemberHandling = MissingMemberHandling.Error,
                    ContractResolver = new RequireObjectPropertiesContractResolver()
                };

                var @object = jObject.ToObject(type, jsonSerialiser);

                _objectByHttpRequest[httpRequest] = @object;
            }
            else
            {
                throw new InvalidOperationException("Already parsed this body");
            }
        }



        public object GetDeserializedObject(HttpRequest httpRequest)
        {
            var @object = _objectByHttpRequest[httpRequest];

            return @object;
        }



        public void RemoveDeserializedObject(HttpRequest httpRequest)
        {
            _jObjectByHttpRequest.Remove(httpRequest);
            _objectByHttpRequest.Remove(httpRequest);
        }
    }
}