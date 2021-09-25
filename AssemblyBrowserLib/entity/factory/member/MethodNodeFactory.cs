using System.Linq;
using System.Reflection;
using AssemblyBrowserLib.Entity.Member;

namespace AssemblyBrowserLib.Entity.Factory.Member
{
    public class MethodNodeFactory : IFactory<MethodInfo, Method>
    {
        public Method Produce(MethodInfo methodInfo)
        {
            return new Method(methodInfo.Name)
            {
                AccessModifier = GetAccessModifier(methodInfo),
                ReturnType = methodInfo.ReturnType.Name,
                Args = methodInfo.GetParameters().Select(info => info.ParameterType.Name + " " + info.Name).ToArray()
            };
        }

        private string GetAccessModifier(MethodInfo methodInfo)
        {
            if (methodInfo.IsPrivate) return "private";
            if (methodInfo.IsPublic) return "public";
            if (methodInfo.IsFamily) return "protected";
            return null;
        }
    }
}