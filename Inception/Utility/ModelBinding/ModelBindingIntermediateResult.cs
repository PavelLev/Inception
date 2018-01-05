using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Inception.Utility.ModelBinding
{
    public class ModelBindingIntermediateResult
    {
        public ModelBindingIntermediateResult(object deserializedObject)
        {
            DeserializedObject = deserializedObject;

            BindingCount = 0;
        }

        public void IncrementBindingCount()
        {
            BindingCount++;
        }

        public object DeserializedObject { get; }
        public int BindingCount { get; private set; }
    }
}