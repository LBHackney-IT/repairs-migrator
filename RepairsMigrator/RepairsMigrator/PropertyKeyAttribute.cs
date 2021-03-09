using System;

namespace RepairsMigrator
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyKeyAttribute : Attribute
    {
        public string Key { get; }

        public PropertyKeyAttribute(string key)
        {
            Key = key;
        }
    }
}
