using System.Linq;
using System.Reflection;
using AssemblyBrowserLib.Entity.Type;

namespace AssemblyBrowserLib.Entity.Factory.Type
{
    public class InterfaceNodeFactory : ClassNodeFactory
    {
        public override bool CanProduce(TypeInfo typeInfo)
        {
            return typeInfo.IsInterface;
        }

        public override AssemblyNode Produce(TypeInfo typeInfo)
        {
            AssemblyNode assemblyNode = base.Produce(typeInfo);
            Interface @interface = new Interface(typeInfo.Name);
            
            assemblyNode.Children.ToList()
                .ForEach(child => @interface.Children.Add(child));

            return @interface;
        }
    }
}