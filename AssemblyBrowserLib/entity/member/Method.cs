namespace AssemblyBrowserLib.Entity.Member
{
    public class Method : AssemblyNode
    {
        public string AccessModifier { get; set; }
        public string ReturnType { get; set; }
        public string[] Args { get; set; }

        public Method(string name) : base(name)
        {
        }

        public override string ToString()
        {
            return AccessModifier + " " + ReturnType + " " + Name + "(" + string.Join(", ", Args) + ")";
        }
    }
}