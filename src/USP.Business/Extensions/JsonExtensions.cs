using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace USP.Business.Extensions
{
    public static class JsonExtensionMethods
    {
        public static String ToJson(this object model, bool ignoreNull = false)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = (ignoreNull) ? NullValueHandling.Ignore : NullValueHandling.Include,
                ContractResolver = (ignoreNull) ? new CamelCasePropertyNamesContractResolver() : new SubstituteNullWithEmptyStringContractResolver(),
            };
            return JsonConvert.SerializeObject(model, Formatting.Indented, settings);
        }

        public static T FromJson<T>(this string model)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            };
            return JsonConvert.DeserializeObject<T>(model, settings);
        }
    }

    public class SubstituteNullWithEmptyStringContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (property.PropertyType == typeof(string))
            {
                // Wrap value provider supplied by Json.NET.
                property.ValueProvider = new NullToEmptyStringValueProvider(property.ValueProvider);
            }

            return property;
        }

        sealed class NullToEmptyStringValueProvider : IValueProvider
        {
            private readonly IValueProvider _provider;

            public NullToEmptyStringValueProvider(IValueProvider provider)
            {
                if (provider == null) throw new ArgumentNullException("provider");

                _provider = provider;
            }

            public object GetValue(object target)
            {
                return _provider.GetValue(target) ?? "";
            }

            public void SetValue(object target, object value)
            {
                _provider.SetValue(target, value);
            }
        }
    }

}