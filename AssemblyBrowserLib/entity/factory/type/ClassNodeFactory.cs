using System.Linq;
using System.Reflection;
using AssemblyBrowserLib.Entity.Factory.Member;
using AssemblyBrowserLib.Entity.Member;
using AssemblyBrowserLib.Entity.Type;

namespace AssemblyBrowserLib.Entity.Factory.Type
{
    public class ClassNodeFactory : StructNodeFactory
    {

        private readonly IFactory<MethodInfo, Method> _methodNodeFactory;

        public ClassNodeFactory()
        {
            _methodNodeFactory = new MethodNodeFactory();
        }
        
        public override bool CanProduce(TypeInfo typeInfo)
        {
            return typeInfo.IsClass;
        }

        public override AssemblyNode Produce(TypeInfo typeInfo)
        {
            AssemblyNode assemblyNode = base.Produce(typeInfo);
            Class @class = new Class(typeInfo.Name, typeInfo.IsAbstract);
            
            assemblyNode.Children.ToList()
                .ForEach(child => @class.Children.Add(child));
            typeInfo.DeclaredMethods.ToList()
                .ForEach(method => @class.Children.Add(_methodNodeFactory.Produce(method)));
            
            return @class;
        }
    }
}