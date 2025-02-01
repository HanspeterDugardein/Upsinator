using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Upsinator.Models
{
    internal class UpsiSettingsModel : INotifyPropertyChanged
    {
        private string _filePathUpsi;

        private UpsiDataModel _upsi;

        private UpsiDataArgModel _newUpsiArg;

        public UpsiSettingsModel()
        {
            this._filePathUpsi = "";

            this._upsi = new UpsiDataModel();

            this._newUpsiArg = new UpsiDataArgModel();
        }

        public string FilePathUpsi
        {
            get
            {
                return this._filePathUpsi;
            }
            set
            {
                this._filePathUpsi = value;

                this.NotifyPropertyChanged(nameof(this.FilePathUpsi));
            }
        }

        public UpsiDataModel Upsi
        {
            get
            {
                return this._upsi;
            }
            set
            {
                this._upsi = value;

                this.NotifyPropertyChanged(nameof(this.Upsi));
            }
        }

        public UpsiDataArgModel NewUpsiArg
        {
            get
            {
                return this._newUpsiArg;
            }
            set
            {
                this._newUpsiArg = value;

                this.NotifyPropertyChanged(nameof(this.NewUpsiArg));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
