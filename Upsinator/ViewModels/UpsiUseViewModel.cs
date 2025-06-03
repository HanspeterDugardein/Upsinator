using System;
using System.IO;
using System.Text.Json;
using System.Windows.Input;
using Upsinator.Logic;
using Upsinator.Models;

namespace Upsinator.ViewModels
{
    internal class UpsiUseViewModel
    {
        public UpsiUseModel Model { get; set; }

        public ICommand ExecuteCmd { get; set; }

        public UpsiUseViewModel()
        {
            this.Model = new UpsiUseModel();

            this.ExecuteCmd = new RelayCommand(new Action<object>(this.Execute));
        }

        /// <summary>
        /// Execute the PowerShell command here
        /// </summary>
        /// <param name="param"></param>
        private void Execute(object _param)
        {
            Console.WriteLine(this.Model.Upsi.Args[2].Value);
        }
    }
}
