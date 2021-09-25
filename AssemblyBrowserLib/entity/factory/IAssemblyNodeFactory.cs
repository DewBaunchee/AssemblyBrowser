using System.Reflection;

namespace AssemblyBrowserLib.Entity.Factory
{
    public interface IAssemblyNodeFactory : IFactory<TypeInfo, AssemblyNode>
    {
        bool CanProduce(TypeInfo typeInfo);
    }
}