using System.Reflection;
using AssemblyBrowserLib.Entity.Member;

namespace AssemblyBrowserLib.Entity.Factory.Member
{
    public class PropertyNodeFactory : IFactory<PropertyInfo, Property>
    {
        public Property Produce(PropertyInfo propertyInfo)
        {
            return new Property(propertyInfo.Name)
            {
                Type = propertyInfo.PropertyType.Name,
                CanRead = propertyInfo.CanRead,
                CanWrite = propertyInfo.CanWrite
            };
        }
    }
}