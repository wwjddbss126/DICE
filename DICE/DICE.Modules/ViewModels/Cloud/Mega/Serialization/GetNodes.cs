﻿namespace DICE.Modules.ViewModels.Cloud.Mega.Serialization
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Runtime.Serialization;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;

  internal class GetNodesRequest : RequestBase
  {
    public GetNodesRequest(string shareId = null)
      : base("f")
    {
			C = 1;
			R = 1;

      if (shareId != null)
      {
        QueryArguments["n"] = shareId;

        // Retrieve all nodes in all subfolders
        R = 1;
      }
    }

    [JsonProperty("c")]
    public int C { get; private set; }

    [JsonProperty("r")]
    public int R { get; private set; }
  }

  internal class GetNodesResponse
  {
    private readonly byte[] _masterKey;
    private List<SharedKey> _sharedKeys;

    public GetNodesResponse(byte[] masterKey)
    {
      _masterKey = masterKey;
    }

    public Node[] Nodes { get; private set; }

    public Node[] UnsupportedNodes { get; private set; }

	public Node[] OldNodes { get; private set; }

    public Node[] OldUnsupportedNodes { get; private set; }

    [JsonProperty("f")]
    public JRaw NodesSerialized { get; private set; }

	[JsonProperty("f2")]
    public JRaw OldNodesSerialized { get; private set; }

    [JsonProperty("ph")]
    public JRaw FileHandles { get; private set; }

    [JsonProperty("u")]
    public JRaw SharedNodes { get; private set; }

    [JsonProperty("ok")]
    public List<SharedKey> SharedKeys
    {
      get => _sharedKeys;
      private set => _sharedKeys = value;
    }

    [OnDeserialized]
    public void OnDeserialized(StreamingContext ctx)
    {
		var tempNodes = JsonConvert.DeserializeObject<Node[]>(NodesSerialized.ToString(), new NodeConverter(_masterKey, ref _sharedKeys));
		UnsupportedNodes = tempNodes.Where(x => x.EmptyKey).ToArray();
		Nodes = tempNodes.Where(x => !x.EmptyKey).ToArray();


		if (OldNodesSerialized != null)
		{
			var oldTempNodes = JsonConvert.DeserializeObject<Node[]>(OldNodesSerialized.ToString(), new NodeConverter(_masterKey, ref _sharedKeys));
			OldUnsupportedNodes = oldTempNodes.Where(x => x.EmptyKey).ToArray();
			OldNodes = oldTempNodes.Where(x => !x.EmptyKey).ToArray();
		}
	}
  }
}
