using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AssemblyBrowserLib.Entity;
using AssemblyBrowserLib.Entity.Factory;
using AssemblyBrowserLib.Entity.Factory.Type;
using AssemblyBrowserLib.Entity.Namespace;
using AssemblyBrowserLib.Entity.Tree;
using Assembly = AssemblyBrowserLib.Entity.Tree.Assembly;

namespace AssemblyBrowserLib
{
    public class AssemblyBrowser : AbstractBrowser
    {
        private Dictionary<string, Namespace> _namespaces;
        private Assembly _loaded;
        private System.Reflection.Assembly _loadedAssembly;
        private List<IAssemblyNodeFactory> _nodeFactories;

        public AssemblyBrowser(string assemblyRoot) : base(assemblyRoot)
        {
            _nodeFactories = new List<IAssemblyNodeFactory>
            {
                new ClassNodeFactory(),
                new EnumNodeFactory(),
                new InterfaceNodeFactory(),
                new RecordNodeFactory(),
                new StructNodeFactory()
            };
        }

        public override void LoadAssembly()
        {
            InitLoader();
            FillNamespaces();
            BuildTree();
        }

        private void FillNamespaces()
        {
            foreach (var type in _loadedAssembly.DefinedTypes)
            {
                if (!_namespaces.ContainsKey(type.Namespace))
                {
                    _namespaces[type.Namespace] = new Namespace(type.Namespace);
                }
                _namespaces[type.Namespace].Children.Add(LoadType(type));
            }
        }

        private AssemblyNode LoadType(TypeInfo type)
        {
            IAssemblyNodeFactory factory = _nodeFactories.Find(factory => factory.CanProduce(type));
            if (factory == null)
                throw new Exception("Cannot produce: " + type.FullName);
            return factory.Produce(type);
        }

        private void InitLoader()
        {
            _loadedAssembly = System.Reflection.Assembly.LoadFrom(AssemblyRoot);
            if (_loadedAssembly == null)
                throw new Exception("Assembly not found on path: " + AssemblyRoot);
            _namespaces = new Dictionary<string, Namespace>();
            _loaded = new Assembly(AssemblyRoot);
        }

        private void BuildTree()
        {
            _namespaces.Values.ToList().ForEach(value => _loaded.Children.Add(value));
        }

        public override Assembly GetAssemblyStructure()
        {
            return _loaded;
        }
    }
}