namespace AssemblyBrowserLib.Entity.Member
{
    public class Property : AssemblyNode
    {
        public string Type { get; set; }
        public bool CanRead { get; set; }
        public bool CanWrite { get; set; }

        public Property(string name) : base(name)
        {
        }

        public override string ToString()
        {
            return Type + " " + Name + "{ " + ((CanRead ? "get; " : "") + (CanWrite ? "set;" : "")).Trim() + " }";
        }
    }
}