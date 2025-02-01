using Newtonsoft.Json;
using System;
using System.IO;
using Upsinator.Models;

namespace Upsinator.ViewModels
{
    internal class UpsiViewModel
    {
        public UpsiViewModel()
        {
            this.Model = new UpsiModel();

            this.UseViewModel = new UpsiUseViewModel();

            this.SettingsViewModel = new UpsiSettingsViewModel();
        }

        public UpsiModel Model { get; set; }

        public UpsiUseViewModel UseViewModel { get; set; }

        public UpsiSettingsViewModel SettingsViewModel { get; set; }

        public void SetFilePath(string filePath)
        {
            this.Model.FilePathUpsi = filePath;

            this.InitModel();
        }

        private void InitModel()
        {
            if (this.ValidateUpsiFile())
            {
                using (var sr = new StreamReader(this.Model.FilePathUpsi))
                {
                    var text = sr.ReadToEnd();

                    if (!String.IsNullOrEmpty(text))
                    {
                        var obj = JsonConvert.DeserializeObject<UpsiDataModel>(text);

                        if (obj != null)
                        {
                            // Just for simplifying further logic,
                            // iterate the arguments in order of the file,
                            // and link that to the argument
                            if (obj.Args != null)
                            {
                                for (int i = 0; i < obj.Args.Count; i++)
                                {
                                    // The index in our program is 1-based, so i+1
                                    obj.Args[i].Id = i + 1;
                                }
                            }

                            this.Model.Upsi = obj;
                        }
                    }
                }

                this.InitUseView();

                this.InitSettingsView();
            }
        }

        private bool ValidateUpsiFile()
        {
            if (!File.Exists(this.Model.FilePathUpsi))
            {
                return false;
                //throw new Exception($"The file '{this.Model.FilePathUpsi}' was not found");
            }

            return true;
        }

        private void InitUseView()
        {
            this.UseViewModel.Model.Upsi = this.Model.Upsi;
        }

        private void InitSettingsView()
        {
            this.SettingsViewModel.SetFilePathScript(this.Model.FilePathUpsi);

            this.SettingsViewModel.SetDataModel(this.Model.Upsi);
        }
    }
}
