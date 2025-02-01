using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using Upsinator.Logic;
using Upsinator.Models;

namespace Upsinator.ViewModels
{
    internal class UpsiSettingsViewModel
    {
        public UpsiDataModel UpsiDataModel { get; set; }

        public UpsiSettingsModel Model { get; set; }

        public UpsiDataArgModel NewArg { get; set; }

        public ICommand RemoveArgumentCmd { get; set; }

        public ICommand AddNewArgumentCmd { get; set; }

        public ICommand SaveSettingsCmd { get; set; }

        public ICommand ResetSettingsCmd { get; set; }

        public string FileExtensionFilter
        {
            get
            {
                return "PowerShell Files|*.ps1|All files|*.*";
            }
        }

        public UpsiSettingsViewModel()
        {
            this.UpsiDataModel = new UpsiDataModel();

            this.Model = new UpsiSettingsModel();

            this.NewArg = new UpsiDataArgModel();

            this.RemoveArgumentCmd = new RelayCommand(new Action<object>(this.RemoveArg));

            this.AddNewArgumentCmd = new RelayCommand(new Action<object>(this.AddNewArg));

            this.SaveSettingsCmd = new RelayCommand(new Action<object>(this.SaveSettings));

            this.ResetSettingsCmd = new RelayCommand(new Action<object>(this.ResetSettings));
        }

        public void SetFilePathScript(string filePathScript)
        {
            this.Model.FilePathUpsi = filePathScript;
        }

        /// <summary>
        /// Sets the Data Model of the UPSI file for the SettingsView
        /// </summary>
        /// <param name="dataModel">DataModel to get the UPSI file data from</param>
        public void SetDataModel(UpsiDataModel dataModel)
        {
            // First save the reference in Memory. After saving, this can be used to update it
            // for the whole application, or for resetting unwanted changes
            this.UpsiDataModel = dataModel;

            // Create actual new variables of the dataModel for editing;
            //  because of 'complex' objects, it is saved by reference instead of
            //  by value, so new copy!
            var newDataModel = new UpsiDataModel();

            newDataModel.Title = dataModel.Title;
            newDataModel.PowerShellScript = dataModel.PowerShellScript;
            
            var newArgs = new ObservableCollection<UpsiDataArgModel>();

            foreach (var arg in dataModel.Args)
            {
                newArgs.Add(new UpsiDataArgModel()
                {
                    Label = arg.Label,
                    Value = arg.Value,
                    Id = arg.Id,
                    IsMandatory = arg.IsMandatory,
                    ArgType = arg.ArgType,
                });
            }

            newDataModel.Args = newArgs;

            this.Model.Upsi = newDataModel;
        }

        private void RemoveArg(object _param)
        {
            int idToRemove = -1;

            if (_param != null && _param is int)
            {
                idToRemove = (int) _param;
            }

            // Remove the selected argument from the list.
            this.Model.Upsi.Args.Remove(new UpsiDataArgModel() { Id = idToRemove });

            // Now iterate the list and update the Ids
            var i = 1;

            // Surely there is some code-ninja shit to do this in one line, but this works
            foreach (var obj in this.Model.Upsi.Args.OrderBy(arg => arg.Id))
            {
                obj.Id = i;

                i++;
            }
        }

        private void AddNewArg(object _param)
        {
            // Calculate the new Id, keep it simple,
            // get highest current Id and +1 it
            var currentLastArgs = this.Model.Upsi.Args.MaxBy(arg => arg.Id);

            var newId = 1;

            if (currentLastArgs != null)
            {
                newId = currentLastArgs.Id + 1;
            }

            // Get a reference to the to-create arg, to keep code clean
            var newArgForm = this.Model.NewUpsiArg;

            // Now build the new Arg with the data we got
            var newArg = new UpsiDataArgModel()
            {
                Id = newId,
                Label = newArgForm.Label,
                Value = newArgForm.Value,
                ArgType = newArgForm.ArgType,
                IsMandatory = newArgForm.IsMandatory
            };

            this.Model.Upsi.Args.Add(newArg);

            // Finally, clean the FormNewArg;
            newArgForm.Id = -1;
            newArgForm.Label = "";
            newArgForm.Value = "";
            newArgForm.ArgType = ArgTypeEnum.String;
            newArgForm.IsMandatory = false;
        }

        private void SaveSettings(object _param)
        {
            // 1. Overwrite the current UPSI model in mem, or create if new
            //  Apparently, i have to do this property by property to trigger the OnNotifyPropertyChanged, oh well
            this.UpsiDataModel.Title = this.Model.Upsi.Title;
            this.UpsiDataModel.PowerShellScript = this.Model.Upsi.PowerShellScript;
            this.UpsiDataModel.Args = this.Model.Upsi.Args;

            this.SetDataModel(this.UpsiDataModel);

            // 2. Save to file
            JsonFileWriter.WriteObjectToFile(this.Model.Upsi, this.Model.FilePathUpsi);
        }

        private void ResetSettings(object _param)
        {
            this.SetDataModel(this.UpsiDataModel);
        }
    }
}
