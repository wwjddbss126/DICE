using System.Collections.Generic;
using System;
using System.Windows;
using DICE.Modules.ViewModels.Cloud.Mega;
using DICE.Modules.ViewModels.Cloud;
using System.Linq;
using static DICE.Modules.ViewModels.Cloud.Mega.MegaManager;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using DevExpress.Pdf.ContentGeneration;
using HandyControl.Controls;
using System.ComponentModel;
using System.Security.RightsManagement;

namespace DICE.Modules.Views.CollectConfigViews
{
    public partial class CredentialInputMegaView
    {
		public CredentialInputMegaView()
        {
            InitializeComponent();
        }
        private void OK(object sender, RoutedEventArgs e)
		{
			Close();
        }

		class Credential
		{
			public string userid;
			public string password;
		}

		public static string al_i;
		public static string al_p;
		public static string al_email;
		public static string al_hash;
		public static string al_pak;
		public static string al_mk;

		public void Mega_Login(object sender, RoutedEventArgs e)
		{
			Credential credential = new Credential
			{
				userid = UserIdInput.Text,
				password = PasswordInput.Text
			};

			MegaManager mm = new MegaManager();

			al_i = credential.userid;
			al_p = credential.password;

			al_email = mm.GenerateAuthInfos(credential.userid, credential.password).Email;
			al_hash = mm.GenerateAuthInfos(credential.userid, credential.password).Hash;
			al_mk = mm.GenerateAuthInfos(credential.userid, credential.password).MFAKey;
			al_pak = System.Text.Encoding.Default.GetString(mm.GenerateAuthInfos(credential.userid, credential.password).PasswordAesKey);

			Close();

			//if (mm.IsLoggedIn)
			//{
			//	Console.WriteLine("MEGA Login Succeed\nsessionID: " + mm._sessionId);

			//	Close();
			//}

			//else if (mm.IsLoggedIn == false)
			//{
			//	Console.WriteLine("MEGA Login Failed.");
			//}

		}
		private static void DisplayNodesRecursive(IEnumerable<INode> nodes, INode parent, int level = 0)
		{
			IEnumerable<INode> children = nodes.Where(x => x.ParentId == parent.Id);
			foreach (INode child in children)
			{
				Console.WriteLine($"{child.Name} {child.Size} bytes {child.CreationDate} {child.ModificationDate}");
				if (child.Type == NodeType.Directory)
				{
					DisplayNodesRecursive(nodes, child, level + 1);
				}
			}
		}
	}


}
