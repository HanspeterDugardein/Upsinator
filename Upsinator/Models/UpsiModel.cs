using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Upsinator.Models
{
    internal class UpsiModel : INotifyPropertyChanged
    {
        private UpsiDataModel _model;

        public string FilePathUpsi { get; set; }

        public UpsiDataModel Upsi
        {
            get
            {
                return this._model;
            }
            set
            {
                this._model = value;

                this.NotifyPropertyChanged(nameof(this.Upsi));
            }
        }

        public UpsiModel()
        {
            this.FilePathUpsi = "Uninitialized_filepath";

            this._model = new UpsiDataModel();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
