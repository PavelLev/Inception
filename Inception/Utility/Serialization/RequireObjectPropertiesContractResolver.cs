﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Inception.Utility.Serialization
{
    public class RequireObjectPropertiesContractResolver: DefaultContractResolver
    {
        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            var contract = base.CreateObjectContract(objectType);
            contract.ItemRequired = Required.Always;
            return contract;
        }
    }
}