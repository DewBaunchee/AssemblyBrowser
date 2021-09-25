namespace AssemblyBrowserLib.Entity.Member
{
    public class Field : AssemblyNode
    {
        
        public string AccessModifier { get; set; }
        public string Type { get; set; }
        
        public Field(string name) : base(name)
        {
        }

        public override string ToString()
        {
            return AccessModifier + " " + Type + " " + Name;
        }
    }
}