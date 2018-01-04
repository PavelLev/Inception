using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Inception.Utility.ModelBinding
{
    public class ModelBindingIntermediateResult
    {
        public Dictionary<string, object> ActionParameters { get; } = new Dictionary<string, object>();
        public int BindingCount { get; set; } = 0;
    }
}