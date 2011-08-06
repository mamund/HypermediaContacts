using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;
using System.Net.Http;
using Microsoft.ApplicationServer.Http;

namespace ContactsHypermedia.APIs
{
  [ServiceContract]
  public class TweetsApi
  {
    [WebInvoke(UriTemplate = "dm", Method = "POST")]
    public void DM(JsonValue request)
    {
      dynamic content = request;
    }

    [WebInvoke(UriTemplate = "mention", Method = "POST")]
    public void Mention(JsonValue request)
    {
      dynamic content = request;
    }
  }
}