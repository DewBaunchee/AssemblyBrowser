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

        public MainWindowViewModel ViewModel { get; }
        
        public MainWindow()
        {
            ViewModel = new MainWindowViewModel(Dispatcher);
            InitializeComponent();
            AssemblyView.ItemsSource = ViewModel.Nodes;
        }
    }
}