using DICE.Common;
using DevExpress.Mvvm;
using DevExpress.Mvvm.ModuleInjection;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.Xpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;

namespace DICE.Main.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
		public MainViewModel()
        {
			Configurations.OutputFolder = Directory.GetCurrentDirectory();
			Configurations.CacheFolder = Path.Combine(Configurations.OutputFolder, ".cache");
			MakeDirectory(Configurations.CacheFolder);
			Configurations.CloudWorkingFolder = Path.Combine(Configurations.OutputFolder, "Cloud");
			MakeDirectory(Configurations.CloudWorkingFolder);
			ModuleManager.DefaultManager.Navigate(Regions.MainControl, "MainTool");
        }

        public void MainTool()
        {		
			ModuleManager.DefaultManager.Navigate(Regions.MainControl, "MainTool");
        }

        public static MainViewModel Create()
        {
            return ViewModelSource.Create(() => new MainViewModel());
        }
        public void InLab()
        {
			ModuleManager.DefaultManager.Navigate(Regions.MainControl, "InLab");
        }

        private void MakeDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}

