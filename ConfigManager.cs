using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace lab3
{
    public class ConfigManager
    {
        IParsable parser;
        public ConfigManager(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new ArgumentException("Couldn't find file");
            }
            if (Path.GetExtension(filePath) == ".json")
            {
                parser = new JsonParser(filePath);
            }
            else if (Path.GetExtension(filePath) == ".xml")
            {
                parser = new XmlParser(filePath);
            }
        }
        public T GetConfig<T>() => parser.GetConfig<T>();
    }

}
