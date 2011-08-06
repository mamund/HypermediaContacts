// <copyright>
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
using System;

namespace ContactsHypermedia
{
  using System.Collections.Generic;
  using System.Linq;

  public class ContactRepository : IContactRepository
  {
    private static IList<Contact> contacts;

    private static int nextContactID;

    public ContactRepository(IList<Contact> contacts)
    {
      Initialize(contacts);
    }

    public static void Initialize(IList<Contact> contacts)
    {
      ContactRepository.contacts = contacts;
      nextContactID = contacts.Count + 1;
    }

    public ContactRepository()
    {
    }

    static ContactRepository()
    {
      contacts = new List<Contact>();
      contacts.Add(new Contact { Name = "Glenn Block", Address = "1 Microsoft Way", City = "Redmond", State = "Washington", Zip = "98112", Email = "gblock@microsoft.com", Twitter = "gblock" });
      contacts.Add(new Contact { Name = "Howard Dierking", Address = "1 Microsoft Way", City = "Redmond", State = "Washington", Zip = "98112", Email = "howard@microsoft.com", Twitter = "howard_dierking" });
      contacts.Add(new Contact { Name = "Yavor Georgiev", Address = "1 Microsoft Way", City = "Redmond", State = "Washington", Zip = "98112", Email = "yavorg@microsoft.com", Twitter = "digthepony" });
      contacts.Add(new Contact { Name = "Jeff Handley", Address = "1 Microsoft Way", City = "Redmond", State = "Washington", Zip = "98112", Email = "jeff.handley@microsoft.com", Twitter = "jeffhandley" });
      contacts.Add(new Contact { Name = "Deepesh Mohnani", Address = "1 Microsoft Way", City = "Redmond", State = "Washington", Zip = "98112", Email = "deepm@microsoft.com", Twitter = "deepeshm" });
      contacts.Add(new Contact { Name = "Brad Olenick", Address = "1 Microsoft Way", City = "Redmond", State = "Washington", Zip = "98112", Email = "brado@microsoft.com", Twitter = "brado_23" });
      contacts.Add(new Contact { Name = "Ron Jacobs", Address = "1 Microsoft Way", City = "Redmond", State = "Washington", Zip = "98112", Email = "rojacobs@microsoft.com", Twitter = "ronljacobs" });
      nextContactID = contacts.Count + 1;
    }

    public void Update(Contact updatedContact)
    {
      var contact = this.Get(updatedContact.ContactId);
      contact.Name = updatedContact.Name;
      contact.Address = updatedContact.Address;
      contact.City = updatedContact.City;
      contact.State = updatedContact.State;
      contact.Zip = updatedContact.Zip;
      contact.Email = updatedContact.Email;
      contact.Twitter = updatedContact.Twitter;
    }

    public Contact Get(Guid id)
    {
      return contacts.SingleOrDefault(c => c.ContactId == id);
    }

    public List<Contact> GetAll()
    {
      return contacts.ToList();
    }

    public List<Contact> GetOne(Guid id)
    {
      return contacts.Where(c => c.ContactId == id).ToList();
    }

    public void Post(Contact contact)
    {
      contacts.Add(contact);
    }

    public void Delete(Guid id)
    {
      var contact = this.Get(id);
      contacts.Remove(contact);
    }

    public List<Contact> Search(string name)
    {
      return contacts.Where(c => c.Name.Contains(name)).ToList();
    }

    public List<Contact> HasTwitter()
    {
      return contacts.Where(c => c.Twitter!=null).ToList();
    }
  }
}