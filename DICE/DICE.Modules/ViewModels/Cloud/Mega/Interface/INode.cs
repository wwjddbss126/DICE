namespace DICE.Modules.ViewModels.Cloud.Mega
{
	using System;

  public interface INode : IEquatable<INode>
  {
    string Id { get; }

    NodeType Type { get; }

    string Name { get; }

    long Size { get; }

    DateTime? ModificationDate { get; }

    string Fingerprint { get; }

    string ParentId { get; }

    DateTime? CreationDate { get; }

    string Owner { get; }

    IFileAttribute[] FileAttributes { get; }

	string Fav { get; }
	string Lbl { get; }
	string SharedUserId { get; }
	string SharingType { get; }
	}

  internal interface INodeCrypto
  {
    byte[] Key { get; }

    byte[] SharedKey { get; }

    byte[] Iv { get; }

    byte[] MetaMac { get; }

    byte[] FullKey { get; }
  }

  public interface IFileAttribute
  {
    int Id { get; }

    FileAttributeType Type { get; }

    string Handle { get; }
  }

  public enum FileAttributeType
  {
    Thumbnail = 0,
    Preview = 1
  }

  public enum NodeType
  {
    File = 0,
    Directory,
    Root,
    Inbox,
    Trash
  }
}
