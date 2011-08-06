using System.Xml;

namespace ContactsHypermedia
{
  static class XmlExtensions
  {
    public static XmlElement AddElement(this XmlNode node, string name, string value = null)
    {
      XmlElement element;

      if (node is XmlDocument)
      {
        var document = node as XmlDocument;
        element = document.CreateElement(name);
      }
      else
      {
        element = node.OwnerDocument.CreateElement(name);
      }
      node.AppendChild(element);
      if (value != null)
        element.InnerText = value;
      return element;
    }
  }
}