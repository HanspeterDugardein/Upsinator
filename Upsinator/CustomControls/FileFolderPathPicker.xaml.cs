using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Upsinator.Logic;

namespace Upsinator.CustomControls
{
    /// <summary>
    /// Interaction logic for FileFolderPathPicker.xaml
    /// </summary>
    public partial class FileFolderPathPicker : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register(nameof(Path),
                typeof(string),
                typeof(FileFolderPathPicker),
                new PropertyMetadata(string.Empty));

        public string Path
        {
            get
            {
                return (string)this.GetValue(FileFolderPathPicker.PathProperty);
            }
            set
            {
                this.SetValue(PathProperty, value);

                this.NotifyPropertyChanged(nameof(this.Path));
            }
        }

        public ICommand BrowseCmd { get; set; }

        public FileFolderPathPicker()
        {
            this.InitializeComponent();

            this.RootUiElement.DataContext = this;

            this.BrowseCmd = new RelayCommand(new Action<object>(this.Browse));
        }

        private void Browse(object param)
        {
            var ofd = new OpenFolderDialog()
            {
                Title = "Select folder",
                Multiselect = false,
                // This seems to default to last selected folder IF the path does not exist
                // or it's value is defaulted, so just put the current FolderPath in and it will lead somewhere useful.
                InitialDirectory = this.InitialDirectory
            };

            // This will halt code until the OpenFolder dialog closes
            ofd.ShowDialog();

            var selectedPath = ofd.FolderName;

            // OpenFolderDialog.FolderName == "" when canceled without selecting a folder
            if (!String.IsNullOrEmpty(selectedPath))
            {
                // Check if the folder exists too
                if (Directory.Exists(selectedPath))
                {
                    this.Path = selectedPath;
                }
                else
                {
                    // Hoping this logs to somewhere? First time using it
                    // Not very interested either atm
                    Console.Error.WriteLine($"Selected folder {selectedPath} not found.");
                }
            }
        }

        private string InitialDirectory
        {
            get
            {
                return System.IO.Path.GetDirectoryName(this.Path) ?? string.Empty;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
