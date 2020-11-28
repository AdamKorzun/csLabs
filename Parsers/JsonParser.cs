using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
namespace lab3
{
    class JsonParser : IParsable
    {
        private readonly string filePath;
        public JsonParser(string filePath)
        {
            this.filePath = filePath;
        }
        public T GetConfig<T>()
        {
            string jsonString;
            using (var reader = new StreamReader(filePath))
            {
                jsonString = reader.ReadToEnd();
            }

            return JsonSerializer.Deserialize<T>(jsonString);

        }
    }
}
