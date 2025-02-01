using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Upsinator.Models
{
    internal class UpsiDataArgModel : INotifyPropertyChanged
    {
        private int _orderId;

        private string _label;

        private string _value;

        private bool _isMandatory;

        private ArgTypeEnum _argType;

        public UpsiDataArgModel()
        {
            this._orderId = -1;

            this._label = "";

            this._value = "";

            this._isMandatory = false;

            this._argType = ArgTypeEnum.String;
        }

        public int Id
        {
            get
            {
                return this._orderId;
            }
            set
            {
                this._orderId = value;

                this.NotifyPropertyChanged(nameof(this.Id));
                this.NotifyPropertyChanged(nameof(this.DisplayTitle));
            }
        }

        public string Label
        {
            get
            {
                return this._label;
            }
            set
            {
                this._label = value;

                this.NotifyPropertyChanged(nameof(this.Label));
            }
        }

        /// <summary>
        /// Save the user value here associated with this arg.
        /// This can be preset for example
        /// </summary>
        public string Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;

                this.NotifyPropertyChanged(nameof(this.Value));
            }
        }

        public bool IsMandatory
        {
            get
            {
                return this._isMandatory;
            }
            set
            {
                this._isMandatory = value;

                this.NotifyPropertyChanged(nameof(this.IsMandatory));
            }
        }

        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public ArgTypeEnum ArgType
        {
            get
            {
                return this._argType;
            }
            set
            {
                this._argType = value;

                this.NotifyPropertyChanged(nameof(this.ArgType));
            }
        }

        [JsonIgnore]
        public string DisplayTitle
        {
            get
            {
                return $"Argument {this.Id}";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            var arg = (UpsiDataArgModel) obj;

            if (arg.Id == this.Id)
            {
                return true;
            }

            return false;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }

    public enum ArgTypeEnum
    {
        String,
        Folder,
        File
    }
}
