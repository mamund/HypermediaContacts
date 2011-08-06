using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;
using Microsoft.ApplicationServer.Http;
using Microsoft.ApplicationServer.Http.Dispatcher;

// 2011-08-05 (mca)
// added HttpResponseMessage to return 404 errors
// tried using HttpRequestMessage<Contact> for POST
// but my contact object is always null!
//
// 2011-08-04 (mca)
// added search support
// mod search to only support name (for today)
// added support for 201 for POST and 204 for DELETE
//
// 2011-08-03 (mca)
// CRUD is up and running!
namespace ContactsHypermedia.APIs
{
  [ServiceContract]
  public class ContactsApi
  {
    [WebGet(UriTemplate = "")]
    public List<Contact> GetContacts()
    {
      var repo = new ContactRepository();
      return repo.GetAll();
    }

    [WebGet(UriTemplate = "{id}")]
    public List<Contact> Get(Guid id)
    {
      var repo = new ContactRepository();

      var c = repo.Get(id);
      if (c == null)
      {
        throw new HttpResponseException(new HttpResponseMessage<Contact>(System.Net.HttpStatusCode.NotFound));
      }
      return repo.GetOne(id);
    }

    // NB: form arg names should not be case-sensitive
    [WebInvoke(UriTemplate = "", Method = "POST")]
    public HttpResponseMessage<string> Post(Contact contact)
    {
      string baseUri = "http://localhost:62192/contacts/";
      var repo = new ContactRepository();
      repo.Post(contact);

      var rsp = new HttpResponseMessage<string>("");
      rsp.Headers.Add("Location", string.Format("{0}/{1}", baseUri, contact.ContactId));
      rsp.StatusCode = System.Net.HttpStatusCode.Created;
      rsp.Content = null;

      return rsp;
    }

    // NB: form arg names should not be case-sensitive
    /* *** this returns an empty contact!!!
    [WebInvoke(UriTemplate = "", Method = "POST")]
    public HttpResponseMessage<string> Post(HttpRequestMessage<Contact> req)
    {
      var content = req.Content;
      Contact contact = content.ReadAs<Contact>();
      var repo = new ContactRepository();
      repo.Post(contact);

      var rsp = new HttpResponseMessage<string>("");
      rsp.Headers.Add("Location", string.Format("{0}/{1}",req.RequestUri,contact.ContactId));
      rsp.StatusCode = System.Net.HttpStatusCode.Created;
      rsp.Content = null;

      return rsp;
    }
    */

    // NB: form arg names should not be case-sensitive
    [WebInvoke(UriTemplate = "{id}", Method = "PUT")]
    public List<Contact> Put(Contact contact, Guid id)
    {
      var repo = new ContactRepository();
      
      var c = repo.Get(id);
      if (c == null)
      {
        throw new HttpResponseException(new HttpResponseMessage<Contact>(System.Net.HttpStatusCode.NotFound));
      }

      contact.ContactId = id;
      repo.Update(contact);

      return repo.GetOne(id);
    }

    [WebInvoke(UriTemplate = "{id}", Method = "DELETE")]
    public HttpResponseMessage<string> delete(Guid id)
    {
      var repo = new ContactRepository();

      var c = repo.Get(id);
      if (c == null)
      {
        throw new HttpResponseException(new HttpResponseMessage<Contact>(System.Net.HttpStatusCode.NotFound));
      }

      repo.Delete(id);

      var rsp = new HttpResponseMessage<string>("");
      rsp.StatusCode = System.Net.HttpStatusCode.NoContent;
      rsp.Content = null;

      return rsp; 
    }
    
    // Q: how do i write a template where 
    // multiple args can appear in any order?
    // NB: query string arg names should not be case-sensitive
    [WebGet(UriTemplate = "search?name={n}")]
    public List<Contact> Search(string name)
    {
      var repo = new ContactRepository();
      return repo.Search(name);
    }

    [WebGet(UriTemplate = "twitter")]
    public List<Contact> HasTwitter()
    {
      var repo = new ContactRepository();
      return repo.HasTwitter();
    }
  }
}