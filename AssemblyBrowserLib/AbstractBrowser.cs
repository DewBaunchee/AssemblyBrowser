using AssemblyBrowserLib.Entity.Tree;

namespace AssemblyBrowserLib
{
    public abstract class AbstractBrowser
    {
        public string AssemblyRoot { get; }

        protected AbstractBrowser(string assemblyRoot)
        {
            AssemblyRoot = assemblyRoot;
        }
        
        public abstract void LoadAssembly();
        public abstract Assembly GetAssemblyStructure();
    }
}