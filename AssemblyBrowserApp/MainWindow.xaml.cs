using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using AssemblyBrowserLib;
using AssemblyBrowserLib.Entity;

namespace AssemblyBrowserApp
{
    public partial class MainWindow
    {

        public AssemblyBrowserViewModel ViewModel { get; }
        
        public MainWindow()
        {
            ViewModel = new AssemblyBrowserViewModel();
            InitializeComponent();
            ViewModel.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == "Root")
                    LoadAssemblies();
            };
        }

        private void LoadAssemblies()
        {
            ViewModel.AssembliesLoadingThread?.Interrupt();
            ChangeProcessMessage("");
            
            if (!Directory.Exists(ViewModel.Root) && !File.Exists(ViewModel.Root))
                return;
            
            ViewModel.AssembliesLoadingThread = new Thread(() =>
            {
                ViewModel.Nodes = new ObservableCollection<AssemblyNode>();
                Dispatcher.Invoke(() => AssemblyView.ItemsSource = ViewModel.Nodes);
                ChangeProcessMessage("Searching assemblies...");
                List<string> assemblies = AssemblyFinder.FindAssemblies(ViewModel.Root);

                assemblies.ForEach(path =>
                {
                    ChangeProcessMessage("Loading assembly: " + path +"...");
                    var browser = new AssemblyBrowser(path);
                    browser.LoadAssembly();
                    Dispatcher.Invoke(() => ViewModel.Nodes.Add(browser.GetAssemblyStructure()));
                });

                ChangeProcessMessage("");
            });
            
            ViewModel.AssembliesLoadingThread.Start();
        }

        private void ChangeProcessMessage(string message)
        {
            Dispatcher.Invoke(() => ViewModel.ProcessMessage = message);
        }
    }
}