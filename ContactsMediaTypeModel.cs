using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ContactsHypermedia
{
  public class ContactsMediaTypeModel
  {
    public ContactsMediaTypeModel()
    {
      Contacts = new List<Contact>();
    }
    public List<Contact> Contacts { get; set; }
  }

  public class Contact
  {
    public Contact()
    {
      ContactId = Guid.NewGuid();
    }
    public Guid ContactId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public string Email { get; set; }
    public string Twitter { get; set; }
  }
}