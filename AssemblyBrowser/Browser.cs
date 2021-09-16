using System;
using AssemblyBrowser.Entity.Tree;

namespace AssemblyBrowser
{
    public class Browser : AbstractBrowser
    {
        
        public Browser(string assemblyRoot) : base(assemblyRoot)
        {
        }


        public override AssemblyTree GetAssemblyStructure()
        {
            throw new NotImplementedException();
        }
    }
}