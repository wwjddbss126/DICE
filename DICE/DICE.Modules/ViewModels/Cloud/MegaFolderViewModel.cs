using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Grid;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DICE.Modules.ViewModels.Cloud
{
    public interface MegaICloudFolderDescription
    {
        int MegaCount { get; set; }
		MegaFolderName MegaFolder { get; }
		MegaFolderType MegaType { get; }
        IEnumerable<MegaICloudFolderDescription> MegaGetSubFolders();
    }
    public class MegaFolderViewModel : MegaICloudFolderDescription
    {
        public static MegaFolderViewModel Create(string name, Uri icon, MegaFolderName folder, MegaFolderType type)
        {
            return ViewModelSource.Create(() => new MegaFolderViewModel()
            {
				MegaName = name,
				MegaIcon = icon,
				MegaFolder = folder,
				MegaType = type
            });
        }
		public string MegaName { get; set; }
        public List<MegaFolderViewModel> MegaSubFolders { get; set; }
		public Uri MegaIcon { get; set; }
        public MegaFolderName MegaFolder { get; set; }
        public MegaFolderType MegaType { get; set; }
        public virtual int MegaCount { get; set; }
        public object AdditionalData { get; set; }
        public string ImageSource { get; set; }
        protected MegaFolderViewModel()
        {
			MegaSubFolders = new List<MegaFolderViewModel> ();
			MegaType = MegaFolderType.Cd;
        }
        IEnumerable<MegaICloudFolderDescription> MegaICloudFolderDescription.MegaGetSubFolders()
        {
            return MegaSubFolders;
        }
    }

    public class MegaCloudFoldersChildSelector : IChildNodesSelector
    {
        IEnumerable IChildNodesSelector.SelectChildren(object item)
        {
            if (item is MegaICloudFolderDescription)
                return (item as MegaICloudFolderDescription).MegaGetSubFolders();

            return null;
        }
    }
}
