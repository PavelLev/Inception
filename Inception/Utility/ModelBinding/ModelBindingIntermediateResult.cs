using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Inception.Utility.ModelBinding
{
    public class ModelBindingIntermediateResult
    {
        public ModelBindingIntermediateResult(dynamic deserializedObject)
        {
            DeserializedObject = deserializedObject;

            BindingCount = 0;
        }

        public void IncrementBindingCount()
        {
            BindingCount++;
        }

        public dynamic DeserializedObject { get; }
        public int BindingCount { get; private set; }
    }
}