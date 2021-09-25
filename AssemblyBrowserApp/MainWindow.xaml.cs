using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using AssemblyBrowserLib;
using AssemblyBrowserLib.Entity;

namespace AssemblyBrowserApp
{
    public partial class MainWindow
    {

        private Thread _assembliesLoadingThread;
        private string _root;
        private bool IsLoading { get; set; }
        private string Root
        {
            get => _root;
            set
            {
                if (value == _root) return;
                
                _root = value;
                OnPropertyChanged(nameof(Root));
            }
        }
        private ObservableCollection<AssemblyNode> Nodes { get; set; }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            PropertyChanged += (_, e) =>
            {
                if(e.PropertyName == "Root")
                    LoadAssemblies();
            };
            Root = "/University/SSP/AssemblyBrowserLab";
        }

        private void LoadAssemblies()
        {
            _assembliesLoadingThread?.Join();
            _assembliesLoadingThread = new Thread(() =>
            {
                Nodes = new ObservableCollection<AssemblyNode>();
                List<string> assemblies = AssemblyFinder.FindAssemblies(Root);
                Dispatcher.Invoke(() =>
                {
                    IsLoading = true;
                    AssemblyView.ItemsSource = Nodes;
                });

                assemblies.ForEach(path =>
                {
                    var browser = new AssemblyBrowser(path);
                    browser.LoadAssembly();
                    Dispatcher.Invoke(() => Nodes.Add(browser.GetAssemblyStructure()));
                });

                Dispatcher.Invoke(() =>
                {
                    IsLoading = false;
                });
            });
            _assembliesLoadingThread.Start();
        }
    }
}