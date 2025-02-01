using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Upsinator.Models
{
    internal class UpsiUseModel : INotifyPropertyChanged
    {
        private UpsiDataModel _upsi;

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

        public UpsiUseModel()
        {
            this._upsi = new UpsiDataModel();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
