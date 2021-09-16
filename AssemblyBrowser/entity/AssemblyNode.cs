using System.Collections.ObjectModel;

namespace AssemblyBrowser.Entity
{
    public class AssemblyNode
    {
        public string Name { set; get; }
        public ObservableCollection<AssemblyNode> Nodes { set; get; }
    }
}