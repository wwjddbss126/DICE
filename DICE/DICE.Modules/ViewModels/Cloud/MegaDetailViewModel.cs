using DevExpress.Mvvm.POCO;
using System;		
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Xpf;
using DICE.Modules.ViewModels.Cloud.Mega;

namespace DICE.Modules.ViewModels.Cloud
{
	public enum MegaCategories { Root = 1, Folder1 = 2, Folder2 = 3, Subject = 4 };
	public enum MegaFolderName { Root, Folder1, Folder2, Subject };
	public enum MegaFolderType { Cd, Sha, Fav, Rub};
	public enum MegaFileType { Normal, Deleted, FileHistory, Thumbnail };
	
	// MEGA-CLOUD DRIVE (mcd)
	// MEGA-FAVORITE (mf)
	// MEGA-SHARED (ms)
	// MEGA-RECYCLE BIN (mrb)

	[POCOViewModel(ImplementIDataErrorInfo = true)]

	public class MegaDetailViewModel
	{
		public static MegaDetailViewModel Create()
		{
			return ViewModelSource.Create(() => new MegaDetailViewModel());
		}
		public MegaFolderName Folder { get; set; }
		public MegaFolderType Type { get; set; }
		public string SubjectDisplayText { get { return string.Format("{1}{0}", MegaSubject, MegaIsReply ? "Re: " : ""); } }
		public virtual string MType { get; set; }
		public virtual string Name { get; set; }
		public virtual string Size { get; set; }
		public virtual string Extension { get; set; }
		public virtual string CreationDate { get; set; }
		public virtual string ModificationDate { get; set; }
		public virtual string Owner { get; set; }
		public virtual string Id { get; set; }
		public virtual string ParentId { get; set; }
		public virtual string Fingerprint { get; set; }
		public virtual string FileAttributes { get; set; }
		public virtual string DownloadURL { get; set; }
		public virtual string Favorite { get; set; }
		public virtual string Label { get; set; }
		public virtual string SharedUserId { get; set; }
		public virtual string SharingType { get; set; }
		public virtual string Path { get; set; }
		public virtual string Hash { get; set; }
		public virtual string History { get; set; }

		public virtual string MegaSubject { get; set; }
		public virtual bool MegaIsReply { get; set; }

		[Command(false)]
		public void Assign(MegaDetailViewModel message)
		{
			MType = message.MType;
			Name = message.Name;
			Size = message.Size;
			Extension = message.Extension;
			CreationDate = message.CreationDate;
			ModificationDate = message.ModificationDate;	
			Owner = message.Owner;
			Id = message.Id;
			ParentId = message.ParentId;
			Fingerprint = message.Fingerprint;
			FileAttributes = message.FileAttributes;
			Favorite = message.Favorite;
			Label = message.Label;
			DownloadURL = message.DownloadURL;
			SharingType = message.SharingType;
			SharedUserId = message.SharedUserId;
			Path = message.Path;
			Hash = message.Hash;
			History= message.History;
		}
	}
}
