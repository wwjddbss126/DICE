namespace DICE.Modules.ViewModels.Cloud.Mega
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Threading;

	public partial interface IMegaManager
	{
		event EventHandler<ApiRequestFailedEventArgs> ApiRequestFailed;

		bool IsLoggedIn { get; }

		MegaManager.LogonSessionToken Login(string email, string password);

		MegaManager.LogonSessionToken Login(string email, string password, string mfaKey);

		MegaManager.LogonSessionToken Login(MegaManager.AuthInfos authInfos);

		void Login(MegaManager.LogonSessionToken logonSessionToken);

		void Login();

		void LoginAnonymous();

		void Logout();

		string GetRecoveryKey();

		IAccountInformation GetAccountInformation();

		IEnumerable<ISession> GetSessionsHistory();

		IEnumerable<INode> GetNodes();

		IEnumerable<INode> GetNodes(INode parent);

		void Delete(INode node, bool moveToTrash = true);

		INode CreateFolder(string name, INode parent);

		Uri GetDownloadLink(INode node);

		void DownloadFile(INode node, string outputFile, CancellationToken? cancellationToken = null);

		void DownloadFile(Uri uri, string outputFile, CancellationToken? cancellationToken = null);

		Stream Download(INode node, CancellationToken? cancellationToken = null);

		Stream Download(Uri uri, CancellationToken? cancellationToken = null);

		INode GetNodeFromLink(Uri uri);

		IEnumerable<INode> GetNodesFromLink(Uri uri);

		INode UploadFile(string filename, INode parent, CancellationToken? cancellationToken = null);

		INode Upload(Stream stream, string name, INode parent, DateTime? modificationDate = null, CancellationToken? cancellationToken = null);

		INode Move(INode node, INode destinationParentNode);

		INode Rename(INode node, string newName);

		MegaManager.AuthInfos GenerateAuthInfos(string email, string password, string mfaKey = null);

		Stream DownloadFileAttribute(INode node, FileAttributeType fileAttributeType, CancellationToken? cancellationToken = null);
	}
}
