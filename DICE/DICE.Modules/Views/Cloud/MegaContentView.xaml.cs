using System;
using System.Windows;
using System.Windows.Controls;

namespace DICE.Modules.Views.Cloud
{
	public partial class MegaContentView : UserControl
	{
		public MegaContentView()
		{
			InitializeComponent();
		}

		public class Data
		{
			public string Name { get; set; }
			public string Size { get; set; }
			public string Extension { get; set; }
			public string CreationDate { get; set; }
			public string ModificationDate { get; set; }
			public string Owner { get; set; }
			public string Id { get; set; }
			public string ParentId { get; set; }
			public string Fingerprint { get; set; }
			public string FileAttributes { get; set; }
			public string DownloadURL { get; set; }
			public string Path { get; set; }
			public string Fav { get; set; }
			public string Lbl { get; set; }
			public string Hash { get; set; }
			public string SharedUserId { get; set; }
			public string SharingType { get; set; }
			public string History { get; set; }

			public Data(string Name, string Size, string Path, string CreationDate, string ModificationDate, string Owner, string Id, string ParentId, string Fingerprint, string FileAttributes, string DownloadURL,  string Fav, string Lbl, string SharedUserId, string SharingType, string Hash, string History)
			{
				this.Name = System.IO.Path.GetFileNameWithoutExtension(Name);
				if (System.IO.Path.GetExtension(Name) != null && System.IO.Path.GetExtension(Name).Length > 0 && System.IO.Path.GetExtension(Name)[0] == '.') // null 체크
				{
					this.Extension = System.IO.Path.GetExtension(Name).Substring(1);
				}
				else
				{
					this.Extension = System.IO.Path.GetExtension(Name);
				}

				this.Size = Size;
				this.CreationDate = CreationDate;
				this.ModificationDate = ModificationDate;
				this.Owner = Owner;
				this.Id = Id;
				this.Path = Path;
				this.ParentId = ParentId;
				this.Fingerprint = Fingerprint;
				this.FileAttributes = FileAttributes;
				this.DownloadURL = DownloadURL;
				this.Fav = Fav;	
				this.Lbl = Lbl;
				this.SharingType = SharingType;
				this.SharedUserId = SharedUserId;
				this.Hash = Hash;
				this.History = History;
			}
		}
	}
}
