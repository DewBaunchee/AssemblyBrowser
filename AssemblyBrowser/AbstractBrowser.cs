using AssemblyBrowser.Entity.Tree;

namespace AssemblyBrowser
{
    public abstract class AbstractBrowser
    {
        public string AssemblyRoot { get; }

        protected AbstractBrowser(string assemblyRoot)
        {
            AssemblyRoot = assemblyRoot;
        }
        
        public abstract AssemblyTree GetAssemblyStructure();
    }
}