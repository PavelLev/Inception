using System;
using Microsoft.AspNetCore.Http;

namespace Inception.Utility.ModelBinding
{
    public interface IPostActionModelDeserializer
    {
        void ParseBody(HttpRequest stream, Type type);

        object GetDeserializedObject(HttpRequest stream);

        void RemoveDeserializedObject(HttpRequest stream);
    }
}