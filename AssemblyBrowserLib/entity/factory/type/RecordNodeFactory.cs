using System.Linq;
using System.Reflection;
using AssemblyBrowserLib.Entity.Type;

namespace AssemblyBrowserLib.Entity.Factory.Type
{
    public class RecordNodeFactory : StructNodeFactory
    {
        public override bool CanProduce(TypeInfo typeInfo)
        {
            return typeInfo.GetMethods().Any(m => m.Name == "<Clone>$");
        }

        public override AssemblyNode Produce(TypeInfo typeInfo)
        {
            AssemblyNode assemblyNode = base.Produce(typeInfo);
            Record @record = new Record(typeInfo.Name);
            
            assemblyNode.Children.ToList()
                .ForEach(child => record.Children.Add(child));
            
            return record;
        }
    }
}