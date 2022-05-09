using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ServerApp.Tests
{
    public static class Extensions
    {
        public static class PropertyCache<T>
        {
            private static readonly Lazy<IReadOnlyCollection<PropertyInfo>> publicPropertiesLazy
                = new Lazy<IReadOnlyCollection<PropertyInfo>>(() => typeof(T).GetProperties());

            public static IReadOnlyCollection<PropertyInfo> PublicProperties => PropertyCache<T>.publicPropertiesLazy.Value;
        }
        public static bool ArePropertiesNotNull<T>(this T obj)
        {
            return PropertyCache<T>.PublicProperties.All(propertyInfo => propertyInfo.GetValue(obj) != null);
        }
    }
}
