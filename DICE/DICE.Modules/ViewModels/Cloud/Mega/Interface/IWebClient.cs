﻿namespace DICE.Modules.ViewModels.Cloud.Mega
{
	using System;
  using System.IO;

  public interface IWebClient
  {
    int BufferSize { get; set; }

    string PostRequestJson(Uri url, string jsonData);

    string PostRequestRaw(Uri url, Stream dataStream);

    Stream PostRequestRawAsStream(Uri url, Stream dataStream);

    Stream GetRequestRaw(Uri url);
  }
}
