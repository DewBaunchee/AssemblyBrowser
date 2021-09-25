using System.Reflection;
using AssemblyBrowserLib.Entity.Member;

namespace AssemblyBrowserLib.Entity.Factory.Member
{
    public class FieldNodeFactory : IFactory<FieldInfo, Field>
    {
        public Field Produce(FieldInfo fieldInfo)
        {
            return new Field(fieldInfo.Name)
            {
                AccessModifier = GetAccessModifier(fieldInfo),
                Type = fieldInfo.FieldType.Name
            };
        }

        private string GetAccessModifier(FieldInfo fieldInfo)
        {
            if (fieldInfo.IsPrivate) return "private";
            if (fieldInfo.IsPublic) return "public";
            if (fieldInfo.IsFamily) return "protected";
            return null;
        }
    }
}