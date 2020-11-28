using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;
using System.Xml;

namespace lab3
{
    public class XmlParser : IParsable
    {
        public string filePath;
        public XmlParser(string filePath)
        {
            this.filePath = filePath;
        }
        public T GetConfig<T>()
        {
            var obj = (T)Activator.CreateInstance(typeof(T));
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            var objectProperties = typeof(T).GetProperties();

            foreach (var prop in objectProperties)
            {
                var root = xmlDoc.DocumentElement;
                XmlNode node = null;
                FindNode(prop, root, ref node);

                if (node != null)
                {
                    prop.SetValue(obj, Convert.ChangeType(node.InnerText,
                                                prop.PropertyType));

                }
            }
            return obj;
        }
        private void FindNode(PropertyInfo prop, XmlNode root, ref XmlNode resNode)
        {
            foreach (XmlNode node in root.ChildNodes)
            {
                if (node.Name == prop.Name)
                {
                    resNode = node;
                }
                FindNode(prop, node, ref resNode);
            }


        }

    }

}
