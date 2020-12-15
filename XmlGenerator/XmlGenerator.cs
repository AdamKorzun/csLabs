using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Schema;
using Models;
using System.Xml;
using System.IO;
namespace XMLGenerator
{
    public class XmlGenerator
    {
        public void ConvertToXmlFile(FileModel obj, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(FileModel));
            using (var files = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(files, obj);
            }
            
            XmlReader reader = new XmlReader.Create(filePath);
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            XmlSchemaInference schema = new XmlSchemaInference();
            schemaSet = schema.InferSchema(reader);
            filePath = Path.ChangeExtension(filePath, ".xsd");
            foreach (XmlSchema s in schemaSet.Schemas())
            {
                using (var sw = new StringWriter()) 
                {
                    using (var writer= XmlWriter.Create(sw))
                    {
                        s.Write(writer);
                    }
                    File.WriteAllText(filePath, sw.ToString());
                }
            }
            
            
        }
      
    }
}
