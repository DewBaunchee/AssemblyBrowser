using System.Linq;
using System.Reflection;
using AssemblyBrowserLib.Entity.Type;

namespace AssemblyBrowserLib.Entity.Factory.Type
{
    public class EnumNodeFactory : ClassNodeFactory
    {
        public override bool CanProduce(TypeInfo typeInfo)
        {
            return typeInfo.IsEnum;
        }

        public override AssemblyNode Produce(TypeInfo typeInfo)
        {
            AssemblyNode assemblyNode = base.Produce(typeInfo);
            Enum @enum = new Enum(typeInfo.Name);
            
            assemblyNode.Children.ToList()
                .ForEach(child => @enum.Children.Add(child));

            return @enum;
        }
    }
}