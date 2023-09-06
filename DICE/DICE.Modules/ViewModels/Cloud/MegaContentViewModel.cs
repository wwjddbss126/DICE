using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.DataAnnotations;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System;
using DICE.Common;
using DICE.Modules.ViewModels.Cloud.Mega;
using System.IO;
using log4net;
using System.Reflection;
using System.Windows.Forms;
using System.Windows;
using DICE.Modules.Views.Cloud;
using MessageBox = System.Windows.Forms.MessageBox;
using Newtonsoft.Json;
using DataGrid = System.Windows.Controls.DataGrid;

namespace DICE.Modules.ViewModels.Cloud
{
	public enum MegaLayoutMode
	{
		Normal,
		Flipped
	}
	public class HistoryItem
	{
		public string Type { get; set; }
		public string Name { get; set; }
		public string Extension { get; set; }
		public string Size { get; set; }
		public string CreationDate { get; set; }
		public string ModificationDate { get; set; }
		public string Owner { get; set; }
		public string Id { get; set; }
		public string ParentId { get; set; }
		public string Fingerprint { get; set; }
		public string Favorite { get; set; }
		public string Label { get; set; }
		public string DownloadURL { get; set; }
		public string SharingType { get; set; }
		public string SharedUserId { get; set; }
		public string Path { get; set; }
		public string Hash { get; set; }

	}
	public class MegaContentViewModel : MegaContentViewModelBase<MegaDetailViewModel>
	{
		public static MegaContentViewModel Create()
		{
			return ViewModelSource.Create(() => new MegaContentViewModel());
		}

		MegaICloudFolderDescription currentFolder;
		IEnumerable<MegaICloudFolderDescription> CloudFolders;
		public virtual MegaLayoutMode MegaLayoutMode { get; set; }
		protected MegaContentViewModel() : base(ModuleType.MegaCloud) { }
		public virtual MegaDetailViewModel CurrentMessage { get; set; }
		public virtual MegaDetailViewModel NewFilelist { get; set; }

		public void CustomColumnDisplayText(DevExpress.Xpf.Grid.CustomColumnDisplayTextEventArgs e)
		{
			if (e.Column.FieldName == "Subject")
			{
				MegaDetailViewModel row = e.Row as MegaDetailViewModel;
				e.DisplayText = row != null ? row.SubjectDisplayText : e.Value.ToString();
			}
		}

		[Command(false)]
		public void AssignMailFolders(IEnumerable<MegaICloudFolderDescription> folders)
		{
			CloudFolders = folders;
		}

		[Command(false)]
		public void SetCurrentFolder(MegaICloudFolderDescription folder)
		{
			currentFolder = folder;
			UpdateItemsSource();
		}

		void ShowMessageWindow()
		{
			this.GetService<IWindowService>().Show(this);
		}

		static INode Mtgt;
		static JToken Mobj;
		public static readonly ILog Mega_Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public void CheckFile(object param)
		{
			foreach (var item in param as ObservableCollection<object>)
			{
				MegaDetailViewModel itemViewModel = item as MegaDetailViewModel;
				MegaManager mm = new MegaManager();

				foreach (INode node in InLabViewModel.mnodes)
				{
					if (node.Id == itemViewModel.Id)
					{
						Mtgt = node;
					}
				}

				mm.DownloadFile(Mtgt, Mtgt.Name);

				Console.WriteLine("★ File Downloaded: " + Configurations.CloudWorkingFolder + "\\" + Mtgt.Name);
				Console.WriteLine("★ Downloadable Link: " + mm.GetDownloadLink(Mtgt));
				Mega_Log.Info("Success collecting file " + Mtgt.Name);
			}
		}

		public void MegaSearch_UpdateItemsSource(object param)
		{
			List<MegaDetailViewModel> datalist = new List<MegaDetailViewModel>();
			MegaDetailViewModel data = new MegaDetailViewModel();

			foreach (var node in InLabViewModel.mnodes)
			{
				if (node.Type == 0 && !node.Name.Contains("failed"))
				{
					if (node.Name.ToString().Contains(param.ToString()))
					{
						data.MType = node.Type.ToString();
						data.Name = node.Name.ToString();
						data.Size = node.Size.ToString();
						data.Extension = node.Name.ToString();
						data.CreationDate = node.CreationDate.ToString();
						data.ModificationDate = node.ModificationDate.ToString();
						data.Owner = node.Owner.ToString();
						data.Id = node.Id.ToString();
						data.ParentId = node.ParentId.ToString();
						data.Fingerprint = node.Fingerprint.ToString();
						data.Favorite = node.Fav.ToString();
						data.Label = node.Lbl.ToString();
						data.FileAttributes = node.FileAttributes.ToString();
						data.SharingType = node.SharingType.ToString();
						data.SharedUserId = node.SharedUserId.ToString();
					}
				}
				datalist.Add(data);
			}
			datalist = datalist.Distinct().ToList();
			datalist.Clear();

			IEnumerable<MegaDetailViewModel> newItems = datalist;
			ItemsSource = new ObservableCollection<MegaDetailViewModel>(newItems);
		}

		public void CheckHistory(object param)
		{
			foreach (var item in (param as ObservableCollection<object>))
			{				
				MegaDetailViewModel itemViewModel = item as MegaDetailViewModel;

				foreach (var obj in InLabViewModel.mobj)
				{
					if ((string)obj["Id"] == itemViewModel.Id)
					{
						Mobj = obj;
					}
				}
				if (Mobj["History"] == null)
				{
					MessageBox.Show("No History found for the selected file", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					try
					{
						string json = Mobj["History"].ToString();

						JArray jsonArray = JArray.Parse(json);
						List<JObject> modifiedElements = new List<JObject>();

						foreach (JObject element in jsonArray)
						{
							if (element["History"] != null)
							{
								JArray historyArray = (JArray)element["History"];
								element.Remove("History");
								modifiedElements.Add(element);
								modifiedElements.AddRange(historyArray.Values<JObject>());
							}
							else
							{
								modifiedElements.Add(element);
							}
						}

						JArray modifiedJsonArray = new JArray(modifiedElements);

						string modifiedJsonString = modifiedJsonArray.ToString(Formatting.Indented);

						List<HistoryItem> historyItems = JsonConvert.DeserializeObject<List<HistoryItem>>(modifiedJsonString);
						
						MegaHistoryView history = new MegaHistoryView
						{
							WindowStartupLocation = WindowStartupLocation.CenterScreen
						};
						DataGrid dataGrid = new DataGrid
						{
							ItemsSource = historyItems
						};

						history.Content = dataGrid;
						history.ShowDialog();						
					}
					catch (Exception ex)
					{
						MessageBox.Show("Something wrong with parsing JSON: Check your Console" + ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

						Console.WriteLine("★ File History for " + Mobj["Name"] + "-----------------------start");
						Console.WriteLine(Mobj["History"]);
						Console.WriteLine("★ File History for " + Mobj["Name"] + "-------------------------end");
					}
				}
			}
		}

		public void OpenMessage()
		{
			MThumbnailSource = "";
			MDataSource = "";
			NewFilelist = MegaDetailViewModel.Create();

			if (CurrentMessage != null)
			{
				NewFilelist.Assign(CurrentMessage);

				var Thumb_path = Path.Combine(new string[]
					{
						Configurations.CloudWorkingFolder,
						"Mega",
						".thumbnail",
					});

				DirectoryInfo di = new DirectoryInfo(Thumb_path);
				bool thumb_flag = false;

				foreach (var file in di.GetFiles())
				{
					if (Path.GetFileNameWithoutExtension(file.ToString()) == Path.GetFileNameWithoutExtension(CurrentMessage.Name))
					{
						thumb_flag = true;
						break;
					}
				}

				if (!thumb_flag)
				{
					MThumbnailSource = "/DICE.Modules;component/Views/Assets/noimg.jpg";
				}
				else
				{
					MThumbnailSource = Path.Combine(new string[]
					{
						Configurations.CloudWorkingFolder,
						"Mega",
						".thumbnail",
						Path.GetFileNameWithoutExtension(CurrentMessage.Name) + ".png"
					});
				}

				MDataSource += "Favorite: " + CurrentMessage.Favorite + "\n";
				MDataSource += "Label: " + CurrentMessage.Label + "\n";
				MDataSource += "Name: " + Path.GetFileNameWithoutExtension(CurrentMessage.Name) + "\n";
				MDataSource += "Extension: " + CurrentMessage.Extension + "\n";
				MDataSource += "Path: " + CurrentMessage.Path + "\n";
				MDataSource += "Size: " + CurrentMessage.Size + "\n";
				MDataSource += "Creation Date: " + CurrentMessage.CreationDate + "\n";
				MDataSource += "Modification Date: " + CurrentMessage.ModificationDate + "\n";
				MDataSource += "Sharing Type: " + CurrentMessage.SharingType + "\n";
				MDataSource += "Shared User (ID): " + CurrentMessage.SharedUserId + "\n";
				MDataSource += "Owner (ID): " + CurrentMessage.Owner + "\n";
				MDataSource += "File ID: " + CurrentMessage.Id + "\n";
				MDataSource += "Parent ID: " + CurrentMessage.ParentId + "\n";
				MDataSource += "Fingerprint: " + CurrentMessage.Fingerprint + "\n";
				// MDataSource += "FileAttributes: " + CurrentMessage.FileAttributes + "\n";
				MDataSource += "DownloadURL: " + CurrentMessage.DownloadURL + "\n";
				var trashed = CurrentMessage.ParentId == InLabViewModel.mRubbishId ? "True" : "False";
				MDataSource += "Trashed: " + trashed + "\n";
				MDataSource += "Hash (MD5): " + CurrentMessage.Hash;
			}
		}
		[Command(false)]
		public void FlipLayout()
		{
			MegaLayoutMode = MegaLayoutMode == MegaLayoutMode.Normal ? MegaLayoutMode.Flipped : MegaLayoutMode.Normal;
		}

		protected override void UpdateItemsSource()
		{
			if (currentFolder == null)
				return;

			IEnumerable<MegaDetailViewModel> newItems = null;
			MegaFolderViewModel folder = (currentFolder as MegaFolderViewModel);

			if (folder.AdditionalData == null)
			{
				bool isRootFolder = currentFolder.MegaFolder == MegaFolderName.Root;
				newItems = Items.Where(x => x.Type == currentFolder.MegaType && (isRootFolder || x.Folder == currentFolder.MegaFolder));
			}
			else
			{				
				string folderId = ((currentFolder as MegaFolderViewModel).AdditionalData as JToken)["Id"].ToString();
				newItems = Items.Where((x) => x.ParentId == folderId && x.Type == currentFolder.MegaType);				
			}

			ItemsSource = new ObservableCollection<MegaDetailViewModel>(newItems);
		}

		public virtual string MThumbnailSource
		{
			get;
			set;
		}
		public virtual string MDataSource
		{
			get;
			set;
		}
	}
}
