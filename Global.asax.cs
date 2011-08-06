using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using ContactsHypermedia.APIs;
using Microsoft.ApplicationServer.Http;
using Microsoft.ApplicationServer.Http.Activation;
using Microsoft.ApplicationServer.Http.Description;

namespace ContactsHypermedia
{
  public class Global : System.Web.HttpApplication
  {

    protected void Application_Start(object sender, EventArgs e)
    {
      var config = new HttpHostConfiguration();
      config.AddFormatters(new ContactsMediaTypeFormatterXml("http://localhost:62192"), new ContactsMediaTypeFormatterJson("http://localhost:62192"));
      RouteTable.Routes.MapServiceRoute<ContactsApi>("contacts", config);
    }

  }
}