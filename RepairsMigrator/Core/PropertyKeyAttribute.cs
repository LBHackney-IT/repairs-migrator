using System;

namespace Core
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PropertyKeyAttribute : Attribute
    {
        public string Key { get; }

        public PropertyKeyAttribute(string key)
        {
            Key = key;
        }
    }
}
