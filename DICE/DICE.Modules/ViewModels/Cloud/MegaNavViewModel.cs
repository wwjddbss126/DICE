using DevExpress.Mvvm.POCO;
using System.Collections.Generic;
using DICE.Modules.Cloud.Helpers;
using DICE.Modules.ViewModels.Cloud;
using static DICE.Modules.ViewModels.Cloud.MegaTestDatasetViewModel;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using DevExpress.Mvvm.Xpf;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Printing.Core.PdfExport.Metafile;
using System;
using DICE.Modules.ViewModels.Cloud.Mega;
using System.Linq;
using DevExpress.DocumentServices.ServiceModel.DataContracts;
using static IronPython.Modules._ast;

namespace DICE.Modules.ViewModels.Cloud
{
    public class MegaNavViewModel : NavigationViewModelBase
    {
        public static MegaNavViewModel Create()
        {
            return ViewModelSource.Create(() => new MegaNavViewModel());
        }

		public IList<MegaICloudFolderDescription> Folders { get; private set; }

		public IList<MegaICloudFolderDescription> Files { get; private set; }

		public virtual MegaICloudFolderDescription CurrentFolder { get; set; }

		protected MegaNavViewModel() : base(ModuleType.MegaCloud)
        {
            HeaderViewModel = HeaderViewModel.Create();
            HeaderViewModel.Init(this);
        }

        [Command]
        public void ProcessListDoubleClick(NodeClickArgs args)
        {
			MegaFolderViewModel NNode = (MegaFolderViewModel)args.Item;
            JToken item = ((JToken)NNode.AdditionalData);

            List<string> c = new List<string>();

            if (ContentViewModel != null)
            {
                ((MegaContentViewModel)ContentViewModel).SetCurrentFolder(NNode);
            }
        }

        public MegaTestDatasetViewModel listitems;

        protected override void Initialize()
        {
            this.listitems = new MegaTestDatasetViewModel();
            Header = "Mega";
            Icon = FilePathHelper.GetDXImageUri("Outlook Inspired/Glyph_Mail");
            FillFolders();
        }
        protected override void InitializeInDesignMode()
        {
            ContentViewModel = MegaContentViewModel.Create();
        }
        protected override void OnContentViewModelChanged(object oldValue)
        {
            if (oldValue != null)
                (oldValue as MegaContentViewModel).AssignMailFolders(null);
            (ContentViewModel as MegaContentViewModel).SetCurrentFolder(CurrentFolder);
            (ContentViewModel as MegaContentViewModel).AssignMailFolders(Folders);
        }
        protected void OnCurrentFolderChanged()
        {
            if (ContentViewModel == null)
                return;

            (ContentViewModel as MegaContentViewModel).SetCurrentFolder(CurrentFolder);
        }

		// CLOUD DRIVE
		void RecursiveFolderCd(TreeNode<string> root, MegaFolderViewModel mainFolder) 
        {
			MegaFolderViewModel sub = MegaFolderViewModel.Create(
						 root.Data,
						 FilePathHelper.GetAppImageUri(""),
						 (root.Data == "root") ? MegaFolderName.Root : MegaFolderName.Folder1, MegaFolderType.Cd
					 );
			sub.AdditionalData = root.AdditionalData;
			sub.ImageSource =
			   ((root.AdditionalData as JToken)["Type"].ToString() == "0")
				? "/DICE.Modules;component/Views/Assets/file.png"
				: "/DICE.Modules;component/Views/Assets/folder.png";

			foreach (TreeNode<string> child in root.Children)
			{
				RecursiveFolderCd(child, sub);
			}
			mainFolder.MegaSubFolders.Add(sub);
        }

		void RecursiveFolderFav(TreeNode<string> root, MegaFolderViewModel mainFolder)
		{
			MegaFolderViewModel sub = MegaFolderViewModel.Create(
				root.Data,
				FilePathHelper.GetAppImageUri(""),
				(root.Data == "Favorite") ? MegaFolderName.Root : MegaFolderName.Folder1, MegaFolderType.Fav
			);
			
			sub.AdditionalData = root.AdditionalData;
			sub.ImageSource =
				((root.AdditionalData as JToken)["Type"].ToString() == "0")
				? "/DICE.Modules;component/Views/Assets/file.png"
				: "/DICE.Modules;component/Views/Assets/folder.png";
			
			foreach (TreeNode<string> child in root.Children)
			{
				RecursiveFolderFav(child, sub);
			}
			mainFolder.MegaSubFolders.Add(sub);
		}

		void RecursiveFolderRub(TreeNode<string> root, MegaFolderViewModel mainFolder)
        {
			MegaFolderViewModel sub = MegaFolderViewModel.Create(
                root.Data,
                FilePathHelper.GetAppImageUri(""),
                (root.Data == "Rubbish bin") ? MegaFolderName.Root : MegaFolderName.Folder2, MegaFolderType.Rub
            );
			sub.AdditionalData = root.AdditionalData;
			sub.ImageSource =
			   ((root.AdditionalData as JToken)["Type"].ToString() == "0")
				? "/DICE.Modules;component/Views/Assets/file.png"
				: "/DICE.Modules;component/Views/Assets/folder.png";

			foreach (TreeNode<string> child in root.Children)
            {
				RecursiveFolderRub(child, sub);
            }
            mainFolder.MegaSubFolders.Add(sub);
        }

		void RecursiveFolderSha(TreeNode<string> root, MegaFolderViewModel mainFolder)
		{
			MegaFolderViewModel sub = MegaFolderViewModel.Create(
				root.Data,
				FilePathHelper.GetAppImageUri(""),
				(root.Data == "Shared") ? MegaFolderName.Root : MegaFolderName.Folder1, MegaFolderType.Sha
			);
			sub.AdditionalData = root.AdditionalData;
			sub.ImageSource =
			   ((root.AdditionalData as JToken)["Type"].ToString() == "0")
				? "/DICE.Modules;component/Views/Assets/file.png"
				: "/DICE.Modules;component/Views/Assets/folder.png";

			foreach (TreeNode<string> child in root.Children)
			{
				RecursiveFolderSha(child, sub);
			}
			mainFolder.MegaSubFolders.Add(sub);
		}

		void FillFolders()
		{
			TreeNode<string> root = null;
			List<TreeNode<string>> favRoot = new List<TreeNode<string>>();
			TreeNode<string> rubbishRoot = null;
			List<TreeNode<string>> sharedRoots = new List<TreeNode<string>>();

			Folders = new List<MegaICloudFolderDescription>();

			MegaFolderViewModel mainfolder_clouddrive = MegaFolderViewModel.Create("CLOUD DRIVE", FilePathHelper.GetAppImageUri(""), MegaFolderName.Root, MegaFolderType.Cd);
			mainfolder_clouddrive.ImageSource = "../Assets/mainfolder.png";

			MegaFolderViewModel mainfolder_fav = MegaFolderViewModel.Create("FAVORITE", FilePathHelper.GetAppImageUri(""), MegaFolderName.Root, MegaFolderType.Fav);
			mainfolder_fav.ImageSource = "../Assets/star.png";

			MegaFolderViewModel mainfolder_shared = MegaFolderViewModel.Create("SHARED", FilePathHelper.GetAppImageUri(""), MegaFolderName.Root, MegaFolderType.Sha);
			mainfolder_shared.ImageSource = "../Assets/share.png";

			MegaFolderViewModel mainfoder_recyclebin = MegaFolderViewModel.Create("RUBBISH BIN", FilePathHelper.GetAppImageUri(""), MegaFolderName.Root, MegaFolderType.Rub);
			mainfoder_recyclebin.ImageSource = "../Assets/trash.png";

			foreach (JToken item in InLabViewModel.mobj)
			{
				Console.WriteLine(item);

				if (item["Type"].ToString() == "2")
				{
					root = new TreeNode<string>() { Data = "root", AdditionalData = item };
				}
				else if (item["Type"].ToString() == "3")
				{
				}
				else if (item["Type"].ToString() == "4")
				{
					rubbishRoot = new TreeNode<string>() { Data = "Rubbish bin", AdditionalData = item };
				}
				else if (InLabViewModel.mSharedId.Contains(item["Id"].ToString()))
				{
					sharedRoots.Add(new TreeNode<string>() { Data = item["Id"].ToString(), AdditionalData = item });
				}
				else
				{
					if (item["Favorite"].ToString() == "1")
					{
						favRoot.Add(new TreeNode<string>() { Data = item["Name"].ToString(), AdditionalData = item });
					}
					if (item["ParentId"].ToString() == InLabViewModel.mRubbishId)
					{
						rubbishRoot.Children.Add(new TreeNode<string>() { Data = item["Name"].ToString(), AdditionalData = item });
					}
					else if (item["ParentId"].ToString() == InLabViewModel.mRootId)
					{
						root.Children.Add(new TreeNode<string>() { Data = item["Name"].ToString(), AdditionalData = item });
					}
					else if (InLabViewModel.mSharedId.Contains(item["ParentId"].ToString())) // Shared child
					{
						InLabViewModel.mSharedId[Array.IndexOf(InLabViewModel.mSharedId, null)] = item["Id"].ToString();
						JToken parentIdToken = item["ParentId"];

						if (parentIdToken != null)
						{
							string parentId = parentIdToken.ToString();
							foreach (var sharedRoot in sharedRoots)
							{
								TreeNode<string> parentNode = FindNodeById(sharedRoot, parentId);

								if (parentNode != null)
								{
									parentNode.Children.Add(new TreeNode<string>() { Data = item["Name"].ToString(), AdditionalData = item });
								}
							}
						}
						else
						{
							foreach (var sharedRoot in sharedRoots)
							{
								sharedRoot.Children.Add(new TreeNode<string>() { Data = item["Name"].ToString(), AdditionalData = item });
							}
						}
					}
					else
					{
						JToken parentIdToken = item["ParentId"];

						if (parentIdToken != null)
						{
							string parentId = parentIdToken.ToString();
							TreeNode<string> parentNode = FindNodeById(root, parentId);

							if (parentNode != null)
							{
								parentNode.Children.Add(new TreeNode<string>() { Data = item["Name"].ToString(), AdditionalData = item });
							}
						}
						else
						{
							root.Children.Add(new TreeNode<string>() { Data = item["Name"].ToString(), AdditionalData = item });
						}
					}
				}				
			}

			// MEGA-CLOUD DRIVE
			RecursiveFolderCd(root, mainfolder_clouddrive);
			Folders.Add(mainfolder_clouddrive);

			// MEGA-FAVORITE
			foreach (var fR in favRoot)
			{
				RecursiveFolderFav(fR, mainfolder_fav);
			}
			Folders.Add(mainfolder_fav);

			// MEGA-SHARED (4/4)
			foreach (var sharedRoot in sharedRoots)
			{
				RecursiveFolderSha(sharedRoot, mainfolder_shared);
			}
			Folders.Add(mainfolder_shared);

			// MEGA-RECYCLE BIN
			RecursiveFolderRub(rubbishRoot, mainfoder_recyclebin);
			Folders.Add(mainfoder_recyclebin);
		}


		TreeNode<string> FindSharedRoot(string id)
		{
			foreach (MegaFolderViewModel folder in Folders)
			{
				if (folder.MegaName == "SHARED")
				{
					foreach (MegaFolderViewModel subFolder in folder.MegaSubFolders)
					{
						if (subFolder.MegaType == MegaFolderType.Sha && subFolder.MegaName == id)
						{
							return new TreeNode<string>() { Data = id, AdditionalData = subFolder.AdditionalData };
						}
					}
				}
			}
			return null;
		}


		private static TreeNode<string> FindNodeById(TreeNode<string> node, string id)
		{
			if (node.AdditionalData != null && ((JToken)node.AdditionalData)["Id"].ToString() == id)
			{
				return node;
			}

			foreach (TreeNode<string> childNode in node.Children)
			{
				TreeNode<string> foundNode = FindNodeById(childNode, id);

				if (foundNode != null)
				{
					return foundNode;
				}
			}

			return null;
		}



		MegaICloudFolderDescription GetFolderByFolderDescription(MegaFolderName name, MegaFolderType type, IEnumerable<MegaICloudFolderDescription> folders)
        {
            foreach (MegaICloudFolderDescription folder in folders)
            {
                if ((folder.MegaFolder == name) && (folder.MegaType == type))
                    return folder;

                if (folder.MegaGetSubFolders() != null)
                {
					MegaICloudFolderDescription subFolder = GetFolderByFolderDescription(name, type, folder.MegaGetSubFolders());
                    if (subFolder != null)
                        return subFolder;
                }
            }
            return null;
        }
    }

    class MegaNode
	{
        public int data;
        public MegaNode left;
        public MegaNode right;

        public MegaNode(int data)
        {
            this.data = data;
            right = null;
            left = null;
        }
    }
}

