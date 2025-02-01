using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Upsinator.Logic;

namespace Upsinator.CustomControls
{
    /// <summary>
    /// Interaction logic for FolderPathPicker.xaml
    /// </summary>
    public partial class FolderPathPicker : UserControl
    {
        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register(nameof(Path),
                typeof(string),
                typeof(FolderPathPicker),
                new PropertyMetadata(string.Empty));

        public string Path
        {
            get
            {
                return (string)this.GetValue(FolderPathPicker.PathProperty);
            }
            set
            {
                this.SetValue(PathProperty, value);

                this.NotifyPropertyChanged(nameof(this.Path));
            }
        }

        public ICommand BrowseCmd { get; set; }

        public FolderPathPicker()
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
                InitialDirectory = this.InitialDirectory,
            };

            // This will halt code until the OpenFolder dialog closes
            ofd.ShowDialog();

            var selectedPath = ofd.FolderName;

            // OpenFolderDialog.FolderName == "" when canceled without selecting a folder
            if (!String.IsNullOrEmpty(selectedPath))
            {
                // Check if the folder exists too
                if (!Directory.Exists(selectedPath))
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
                // Hack: Doing this always ensures a folder,
                // either directly selected folder, or the folder of the somehow selected file
                // Little code-smell as this seems to allow files to for a FOLDER-selector tool but whatevers
                //return Path.GetDirectoryName(this.FolderPath);
                return System.IO.Path.GetDirectoryName(this.Path);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
