using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Newtonsoft.Json.Linq;
using DICE.Modules.ViewModels.Cloud;
using DICE.Modules.ViewModels;
using System.IO;

namespace DICE.Modules.Cloud.DataProvider
{
    public class MegaDataProvider : MegaDataProviderBase
	{
        DataSet cloudDataSet;
        int initItemsCount = 0;

        public MegaDataProvider()
        {
            cloudDataSet = InitDataSet();
        }

        public JObject Mobj { get; set; }

        void ReleaseDataIfNeeded()
        {
            if (++initItemsCount == 4)
            {
                cloudDataSet.Dispose();
                cloudDataSet = null;
            }
        }

        protected override IList<MegaDetailViewModel> FillMessages()
        {
            IList<MegaDetailViewModel> result = new List<MegaDetailViewModel>();
            DataTable cloudTable = cloudDataSet.Tables["MegaCloud"];
            if (cloudTable != null && cloudTable.Rows.Count > 0)
            {
                foreach (DataRow row in cloudTable.Rows)
				{
					result.Add(CreateMegaCloudDetail(row));
				}
            }
            ReleaseDataIfNeeded();
            return result;
        }

        static DataSet InitDataSet()
        {
            DataSet ds = new DataSet("MegaCloud");
            DataTable dt = new DataTable("MegaCloud");

			dt.Columns.Add("Type");
			dt.Columns.Add("Name");
			dt.Columns.Add("Extension");
			dt.Columns.Add("Size");
			dt.Columns.Add("CreationDate");
			dt.Columns.Add("ModificationDate");
			dt.Columns.Add("Owner");
			dt.Columns.Add("Id");
			dt.Columns.Add("ParentId");
			dt.Columns.Add("Fingerprint");
			dt.Columns.Add("Favorite");
			dt.Columns.Add("Label");
			dt.Columns.Add("DownloadURL");

			dt.Columns.Add("SharingType");
			dt.Columns.Add("SharedUserId");

			dt.Columns.Add("Path");
			dt.Columns.Add("Hash");
			dt.Columns.Add("History");

			foreach (var item in InLabViewModel.mobj)
			{
				if (item["Type"].ToString() == "0")
				{
					DataRow row = dt.NewRow();

					row["Type"] = item["Type"];
					row["Name"] = item["Name"];
					row["Extension"] = item["Extension"];
					row["Size"] = item["Size"];
					row["CreationDate"] = item["CreationDate"];
					row["ModificationDate"] = item["ModificationDate"];
					row["Owner"] = item["Owner"];
					row["Id"] = item["Id"];
					row["ParentId"] = item["ParentId"];
					row["Fingerprint"] = item["Fingerprint"];
					row["DownloadURL"] = item["DownloadURL"];
					row["Path"] = item["Path"];
					row["Hash"] = item["Hash"];
					row["History"] = item["History"];

					switch (item["Favorite"].ToString())
					{
						case "1":
							row["Favorite"] = "True";
							break;
						default:
							row["Favorite"] = "False";
							break;
					}

					switch (item["Label"].ToString())
					{
						case "1":
							row["Label"] = "Red";
							break;
						case "2":
							row["Label"] = "Yellow";
							break;
						case "3":
							row["Label"] = "Green";
							break;
						case "4":
							row["Label"] = "Blue";
							break;
						case "5":
							row["Label"] = "Purple";
							break;
						case "6":
							row["Label"] = "Gray";
							break;
						default:
							row["Label"] = "N/A";
							break;
					}

					switch (item["SharingType"].ToString())
					{						
						case "1":
							row["SharingType"] = "Incoming";
							break;
						default:
							row["SharingType"] = "N/A or Outgoing";
							break;
					}

					if (item["SharedUserId"].ToString() != "")
					{
						row["SharedUserId"] = item["SharedUserId"];
					}
					else
					{
						row["SharedUserId"] = "N/A or Outgoing";
					}

					dt.Rows.Add(row);
				}
			}


			foreach (var item in InLabViewModel.mobj_fav)
			{
				if (item["Type"].ToString() == "0")
				{
					DataRow row = dt.NewRow();

					row["Type"] = item["Type"];
					row["Name"] = item["Name"];
					row["Extension"] = item["Extension"];
					row["Size"] = item["Size"];
					row["CreationDate"] = item["CreationDate"];
					row["ModificationDate"] = item["ModificationDate"];
					row["Owner"] = item["Owner"];
					row["Id"] = item["Id"];
					row["ParentId"] = item["ParentId"];
					row["Fingerprint"] = item["Fingerprint"];
					row["DownloadURL"] = item["DownloadURL"];
					row["Favorite"] = "Favorite";
					row["Path"] = item["Path"];
					row["Hash"] = item["Hash"];
					row["History"] = item["History"];

					switch (item["Label"].ToString())
					{
						case "1":
							row["Label"] = "Red";
							break;
						case "2":
							row["Label"] = "Yellow";
							break;
						case "3":
							row["Label"] = "Green";
							break;
						case "4":
							row["Label"] = "Blue";
							break;
						case "5":
							row["Label"] = "Purple";
							break;
						case "6":
							row["Label"] = "Gray";
							break;
						default:
							row["Label"] = "N/A";
							break;
					}

					switch (item["SharingType"].ToString())
					{
						case "1":
							row["SharingType"] = "Incoming";
							break;
						default:
							row["SharingType"] = "N/A or Outgoing";
							break;
					}

					if (item["SharedUserId"].ToString() != "")
					{
						row["SharedUserId"] = item["SharedUserId"];
					}
					else
					{
						row["SharedUserId"] = "N/A or Outgoing\"";
					}

					dt.Rows.Add(row);
				}
			}

			ds.Tables.Add(dt);

            return ds;
        }

		static MegaDetailViewModel CreateMegaCloudDetail(DataRow row)
        {
			MegaDetailViewModel file = MegaDetailViewModel.Create();
		
			file.MType = string.Format("{0}", row["Type"]);			
			file.Size = string.Format("{0}", row["Size"]);

			file.Name = string.Format("{0}", row["Name"]);
			file.Extension = string.Format("{0}", row["Extension"]);

			file.CreationDate = string.Format("{0}", row["CreationDate"]);
			file.ModificationDate = string.Format("{0}", row["ModificationDate"]);
			file.Owner = string.Format("{0}", row["Owner"]);
			file.Id = string.Format("{0}", row["Id"]);
			file.ParentId = string.Format("{0}", row["ParentId"]);
			file.Fingerprint = string.Format("{0}", row["Fingerprint"]);
			file.DownloadURL = string.Format("{0}", row["DownloadURL"]);	
			file.Label = string.Format("{0}", row["Label"]);
			file.Favorite = string.Format("{0}", row["Favorite"]);
			file.SharedUserId = string.Format("{0}", row["SharedUserId"]);
			file.SharingType = string.Format("{0}", row["SharingType"]);
			file.Path = string.Format("{0}", row["Path"]);
			file.Hash = string.Format("{0}", row["Hash"]);
			file.History = string.Format("{0}", row["History"]);

			if (row["Favorite"].ToString() == "Favorite")
			{
				file.Type = MegaFolderType.Fav;
			}
			else
			{
				if (InLabViewModel.mSharedId.Contains(row["ParentId"].ToString()))
				{
					file.Type = MegaFolderType.Sha;
				}
				else if (row["ParentId"].ToString() == InLabViewModel.mRubbishId)
				{
					file.Type = MegaFolderType.Rub;
				}
				else
				{
					file.Type = MegaFolderType.Cd;
				}
			}
			return file;
        }

        static MegaFolderName MegaGetFolder(DataRow row)
        {
            object Megacategory = row["CategoryID"];
            string MegacategoryString = string.Format("{0}", (MegaCategories)(Megacategory == DBNull.Value ? 1 : (int)Megacategory));
            if (string.IsNullOrEmpty(MegacategoryString)) return MegaFolderName.Root;
            return (MegaFolderName)Enum.Parse(typeof(MegaFolderName), MegacategoryString.Replace(" ", ""), true);
        }
    }
}
