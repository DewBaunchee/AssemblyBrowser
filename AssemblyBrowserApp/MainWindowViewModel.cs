using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Threading;
using AssemblyBrowserLib;
using AssemblyBrowserLib.Entity;

namespace AssemblyBrowserApp
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private CancellationTokenSource _cancellationTokenSource;

        private string _processMessage;

        public string ProcessMessage
        {
            get => _processMessage;
            set
            {
                if (value == _processMessage) return;

                _processMessage = value;
                OnPropertyChanged(nameof(ProcessMessage));
            }
        }

        private string _root;

        public string Root
        {
            get => _root;
            set
            {
                if (value == _root) return;

                _root = value;
                OnPropertyChanged(nameof(Root));
            }
        }

        public ObservableCollection<AssemblyNode> Nodes { get; }

        private readonly Dispatcher _dispatcher;

        public MainWindowViewModel(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            Nodes = new ObservableCollection<AssemblyNode>();
            PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == "Root")
                    LoadAssemblies();
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadAssemblies()
        {
            if (_cancellationTokenSource != null)
                _cancellationTokenSource.Cancel();

            _cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = _cancellationTokenSource.Token;
            ChangeProcessMessage("");

            if (!Directory.Exists(Root) && !File.Exists(Root))
                return;

            new Thread(() =>
            {
                _dispatcher.Invoke(() => Nodes.Clear());
                ChangeProcessMessage("Searching assemblies...");
                List<string> assemblies = AssemblyFinder.FindAssemblies(Root);

                assemblies.ForEach(path =>
                {
                    token.ThrowIfCancellationRequested();

                    ChangeProcessMessage("Loading assembly: " + path + "...");
                    var browser = new AssemblyBrowser(path);
                    try
                    {
                        browser.LoadAssembly();
                        if (!token.IsCancellationRequested)
                            _dispatcher.Invoke(() => Nodes.Add(browser.GetAssemblyStructure()));
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                });

                ChangeProcessMessage("");
            }).Start();
        }

        private void ChangeProcessMessage(string message)
        {
            _dispatcher.Invoke(() => ProcessMessage = message);
        }
    }
}