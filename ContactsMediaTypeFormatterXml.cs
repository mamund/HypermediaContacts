using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Xml;
using Microsoft.ApplicationServer.Http;

namespace ContactsHypermedia
{
  public class ContactsMediaTypeFormatterXml : MediaTypeFormatter
  {
    string baseUri;

    public ContactsMediaTypeFormatterXml(string baseUri)
    {
      this.baseUri = baseUri;
      this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/contoso.contacts+xml"));
    }

    public override object OnReadFromStream(Type type, System.IO.Stream stream, HttpContentHeaders contentHeaders)
    {
      throw new NotImplementedException();
    }

    public override void OnWriteToStream(Type type, object value, System.IO.Stream stream, HttpContentHeaders contentHeaders, System.Net.TransportContext context)
    {
      var contacts = (IEnumerable<Contact>)value;

      var doc = new XmlDocument();
      var root = doc.AddElement("Root");

      // always emit the contacts collection, even if it's empty (we need the URI!)
      var contactsXml = root.AddElement("Contacts");
      contactsXml.SetAttribute("href", string.Format("{0}/contacts", this.baseUri));

      if (contacts.Count() > 0)
      {
        foreach (var contact in contacts)
        {
          HandleContact(contactsXml, contact);
        }
      }

      // always emit the queries, too!
      HandleQueries(root);

      doc.Save(stream);
    }

    void HandleContact(XmlElement contactsXml, Contact contact)
    {
      var contactXml = contactsXml.AddElement("Contact");
      contactXml.SetAttribute("href", string.Format("{0}/contacts/{1}", this.baseUri, contact.ContactId));
      contactXml.AddElement("Name", contact.Name);
      contactXml.AddElement("Address", contact.Address);
      contactXml.AddElement("City", contact.City);
      contactXml.AddElement("State", contact.State);
      contactXml.AddElement("Zip", contact.Zip);
      contactXml.AddElement("Email", contact.Email);
      HandleTwitter(contactXml, contact);
    }

    void HandleTwitter(XmlElement contactXml, Contact contact)
    {
      if (contact.Twitter != null)
      {
        var twitterXml = contactXml.AddElement("Twitter");
        twitterXml.SetAttribute("user", "gblock"); // SB logged-in user

        var tweetsXml = twitterXml.AddElement("Tweets");
        tweetsXml.SetAttribute("href", string.Format("{0}/contacts/{1}/tweets", this.baseUri, contact.ContactId));

        var dmXml = twitterXml.AddElement("DM");
        var field = dmXml.AddElement("Field", "Message");
        dmXml.SetAttribute("href", string.Format("{0}/contacts/{1}/dm", this.baseUri, contact.ContactId)); ;

        var mentionXml = twitterXml.AddElement("Mention");
        field = mentionXml.AddElement("Field", "Message");
        mentionXml.SetAttribute("href", string.Format("{0}/contacts/{1}/mention", this.baseUri, contact.ContactId));
      }
    }

    void HandleQueries(XmlElement root)
    {
      var queriesXml = root.AddElement("Queries");

      var twitterContactsXml = queriesXml.AddElement("Query");
      twitterContactsXml.SetAttribute("name", "query-twitter-contacts");
      twitterContactsXml.SetAttribute("href", string.Format("{0}/contacts/twitter", this.baseUri));

      var searchXml = queriesXml.AddElement("Query");
      searchXml.SetAttribute("name", "query-search");
      searchXml.SetAttribute("href", string.Format("{0}/contacts/search", this.baseUri));
      searchXml.AddElement("Field", "Name");
    }
  }
}