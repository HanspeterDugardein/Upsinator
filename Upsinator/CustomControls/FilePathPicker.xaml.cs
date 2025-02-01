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
    /// Interaction logic for FilePathPicker.xaml
    /// </summary>
    public partial class FilePathPicker : UserControl
    {
        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register(nameof(Path),
                typeof(string),
                typeof(FilePathPicker),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty FileExtensionFilterProperty =
            DependencyProperty.Register(nameof(FileExtensionFilter),
                typeof(string),
                typeof(FilePathPicker),
                new PropertyMetadata("All files (*.*)|*.*"));

        public string Path
        {
            get
            {
                return (string)this.GetValue(FilePathPicker.PathProperty);
            }
            set
            {
                this.SetValue(PathProperty, value);

                this.NotifyPropertyChanged(nameof(this.Path));
            }
        }

        public string FileExtensionFilter
        {
            get
            {
                return (string)this.GetValue(FilePathPicker.FileExtensionFilterProperty);
            }
            set
            {
                this.SetValue(FileExtensionFilterProperty, value);
            }
        }

        public ICommand BrowseCmd { get; set; }

        public FilePathPicker()
        {
            this.InitializeComponent();

            this.RootUiElement.DataContext = this;

            this.BrowseCmd = new RelayCommand(new Action<object>(this.Browse));
        }

        private void Browse(object param)
        {
            var ofd = new OpenFileDialog()
            {
                Title = "Select file",
                Multiselect = false,
                InitialDirectory = this.InitialDirectory,
                Filter = this.FileExtensionFilter
            };

            ofd.ShowDialog();

            var selectedFile = ofd.FileName;

            if (!String.IsNullOrEmpty(selectedFile))
            {
                if (!File.Exists(selectedFile))
                {
                    this.Path = selectedFile;
                }
                else
                {
                    // Hoping this logs to somewhere? First time using it
                    // Not very interested either atm
                    Console.Error.WriteLine($"Selected file {selectedFile} not found.");
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
