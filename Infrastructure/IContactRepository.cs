// <copyright>
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using System;

namespace ContactsHypermedia
{
  using System.Collections.Generic;

  public interface IContactRepository
  {
    void Update(Contact updatedContact);

    Contact Get(Guid id);

    List<Contact> GetAll();

    void Post(Contact contact);

    void Delete(Guid id);
  }
}