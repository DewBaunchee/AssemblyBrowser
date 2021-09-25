using System.Linq;
using System.Reflection;
using AssemblyBrowserLib.Entity.Factory.Member;
using AssemblyBrowserLib.Entity.Member;
using AssemblyBrowserLib.Entity.Type;

namespace AssemblyBrowserLib.Entity.Factory.Type
{
    public class StructNodeFactory : IAssemblyNodeFactory
    {
        private readonly IFactory<FieldInfo, Field> _fieldNodeFactory;
        private readonly IFactory<PropertyInfo, Property> _propertyNodeFactory;

        public StructNodeFactory()
        {
            _fieldNodeFactory = new FieldNodeFactory();
            _propertyNodeFactory = new PropertyNodeFactory();
        }
        public virtual bool CanProduce(TypeInfo typeInfo)
        {
            return typeInfo.IsValueType && !typeInfo.IsPrimitive && !typeInfo.IsEnum;
        }

        public virtual AssemblyNode Produce(TypeInfo typeInfo)
        {
            Struct @struct = new Struct(typeInfo.Name);
            
            typeInfo.DeclaredFields.ToList()
                .ForEach(field => @struct.Children.Add(_fieldNodeFactory.Produce(field)));
            typeInfo.DeclaredProperties.ToList()
                .ForEach(property => @struct.Children.Add(_propertyNodeFactory.Produce(property)));
            
            return @struct;
        }
    }
}