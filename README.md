# ЛР 3. Корзун Адам 953505

### Options  
Два класса - TransferOptions, EncryptOptions. В TransferOptions находятся все необходимые переменные - TargetPath, SourcePath, Компрессия, шифрование.
```
private bool encryption;
private bool compress;
private string encryptionKey;
private string sourceFilePath;
private string targetFilePath;
```
EncryptOptions для теста, персеров для любого класса.
```
var xmlConfig = manager.GetConfig<eEncryptOptions>();
```
### Parsers
Интерфейс IParsable c методом GetConfig и 2 парсера, реализющие GetConfig: <br />
JsonParser - использует System.Text.Json
```
return JsonSerializer.Deserialize<T>(jsonString);
```
XmlParser - ищет рекурсивно в xml файле параметры класса, для которого мы вызывааем GetConfig
```
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
```
### Configs
Одинковые кофиги в json и xml форматах, схема для валидации xml
### ConfigManager
Выбирает парсер в зависимости от расширения в переданном пути конфигурационного файла
### Изменений второй ЛР:
##### Валидация xml файла
```
private void ValidateXml(string xsdFilePath, string xmlFilePath)
    {
        var schema = new XmlSchemaSet();
        schema.Add(string.Empty, xsdFilePath);
        XDocument doc = XDocument.Load(xmlFilePath);

        doc.Validate(schema, ValidationCallBack);
    }
```
##### Получение конфигурации из файлов
```
var manager = new ConfigManager(@"C:\Users\admin\Desktop\c#sem3\labs\lab2Dir\config.xml");
var xmlConfig = manager.GetConfig<TransferOptions>();
manager = new ConfigManager(@"C:\Users\admin\Desktop\c#sem3\labs\lab2Dir\config.json");
var jsonConfig = manager.GetConfig<TransferOptions>();
```
##### Архивация и шифрование файла были hard coded, теперь они определяются исходя из параметров, переданных в функцию 
```
public static void SendFile(string path, string targetPath,
                                  bool encrypt, string key,
                                  bool compress = false)
```
