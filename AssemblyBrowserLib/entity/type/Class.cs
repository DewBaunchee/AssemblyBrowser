namespace AssemblyBrowserLib.Entity.Type
{
    public class Class : AssemblyNode
    {

        private readonly bool _isAbstract;
        
        public Class(string name, bool isAbstract) : base(name)
        {
            _isAbstract = isAbstract;
        }

        public override string ToString()
        {
            return GetType().Name + ": "
                                  + (_isAbstract ? "abstract " : " ")
                                  + Name;
        }
    }
}