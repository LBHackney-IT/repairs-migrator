using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core
{
    public class PropertyBag : Dictionary<string, object>
    {
        private readonly IList<string> errors;

        public PropertyBag()
        {
            this.errors = new List<string>();
        }

        internal T To<T>()
            where T : class, new()
        {
            var model = new T();
            Type type = typeof(T);
            bool shouldMapWholeClassFromName = ShouldMapFromPropName(type);

            foreach (var prop in type.GetProperties())
            {
                if (shouldMapWholeClassFromName && TryGetValue(prop.Name, out var value))
                {
                    prop.SetValue(model, value);
                }
                else if (TryGetPropBagKey(prop, out var key)
                    && TryGetValue(key, out var value2))
                {
                    prop.SetValue(model, value2);
                }
            }

            AttachErrors(model);

            return model;
        }

        private void AttachErrors<T>(T model) where T : class, new()
        {
            if (model is IHasErrors withErrors) withErrors.Errors = this.errors;
        }

        internal static PropertyBag From<T>(T inModel)
             where T : class
        {
            return From(inModel, typeof(T));
        }

        internal static PropertyBag From(object inModel, Type inType)
        {
            var bag = new PropertyBag();
            bag.Apply(inModel, inType);

            return bag;
        }
        
        internal void Apply<T>(T model)
            where T : class
        {
            Apply(model, typeof(T));
        }

        internal void Apply(object model, Type type)
        {
            bool shouldMapWholeClassFromName = ShouldMapFromPropName(type);
            foreach (var prop in type.GetProperties())
            {
                if (shouldMapWholeClassFromName)
                {
                    this[prop.Name] = prop.GetValue(model);
                }
                else
                {
                    foreach (var key in GetPropBagKeys(prop))
                    {
                        this[key] = prop.GetValue(model);
                    }
                }
            }
        }

        internal void AddError(string error)
        {
            this.errors.Add(error);
        }

        private static bool TryGetPropBagKey(MemberInfo prop, out string key)
        {
            if (Attribute.GetCustomAttribute(prop, typeof(PropertyKeyAttribute)) is PropertyKeyAttribute attr)
            {
                key = attr.Key;
                return true;
            }

            if (ShouldMapFromPropName(prop))
            {
                key = prop.Name;
                return true;
            }

            key = string.Empty;
            return false;
        }

        private static IEnumerable<string> GetPropBagKeys(MemberInfo prop)
        {
            foreach (var attr in Attribute.GetCustomAttributes(prop, typeof(PropertyKeyAttribute)).OfType<PropertyKeyAttribute>())
            {
                yield return attr.Key;
            }

            if (ShouldMapFromPropName(prop))
            {
                yield return prop.Name;
            }
        }

        private static bool ShouldMapFromPropName(MemberInfo prop)
        {
            return Attribute.GetCustomAttribute(prop, typeof(MapPropNameAttribute), true) is MapPropNameAttribute;
        }

        private static bool ShouldMapFromPropName(Type type)
        {
            return Attribute.GetCustomAttribute(type, typeof(MapPropNameAttribute), true) is MapPropNameAttribute;
        }
    }
}
