using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using AssemblyBrowserLib.Entity;

namespace AssemblyBrowserApp
{
    public class AssemblyBrowserViewModel : INotifyPropertyChanged
    {
        public Thread AssembliesLoadingThread { get; set; }

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

        public ObservableCollection<AssemblyNode> Nodes { get; set; }
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}