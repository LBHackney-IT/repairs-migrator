using System;
using System.Collections.Generic;

namespace Core
{
    public class PropertyBag : Dictionary<string, object>
    {
        internal T To<T>()
            where T : class, new()
        {
            var model = new T();
            Type type = typeof(T);

            foreach (var prop in type.GetProperties())
            {
                if (Attribute.GetCustomAttribute(prop, typeof(PropertyKeyAttribute)) is PropertyKeyAttribute attr 
                    && this.TryGetValue(attr.Key, out var value))
                {
                    prop.SetValue(model, value);
                }
            }

            return model;
        }

        internal static PropertyBag From<T>(T inModel)
             where T : class
        {
            var bag = new PropertyBag();
            bag.Apply(inModel);

            return bag;
        }

        internal void Apply<T>(T model)
            where T : class
        {
            Type type = typeof(T);

            foreach (var prop in type.GetProperties())
            {
                if (Attribute.GetCustomAttribute(prop, typeof(PropertyKeyAttribute)) is PropertyKeyAttribute attr)
                {
                    this[attr.Key] = prop.GetValue(model);
                }
            }
        }
    }
}
