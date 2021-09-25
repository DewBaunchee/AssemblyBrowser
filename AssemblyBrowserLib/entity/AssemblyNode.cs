using System;
using System.Collections.ObjectModel;

namespace AssemblyBrowserLib.Entity
{
    public class AssemblyNode
    {
        public string Name { get; }
        public ObservableCollection<AssemblyNode> Children { get; }

        public AssemblyNode(string name)
        {
            Name = name;
            Children = new ObservableCollection<AssemblyNode>();
        }

        public override string ToString()
        {
            return GetType().Name + ": " + Name;
        }
    }
}