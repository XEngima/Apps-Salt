using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Salt.Shared
{
    public class FileService : IFileService
    {
        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public string[] GetDirectoryFiles(string path)
        {
            return Directory.GetFiles(path);
        }

        public string[] GetDirectoryFiles(string path, string searchPattern)
        {
            return Directory.GetFiles(path, searchPattern);
        }

        public object ReadXmlFile(string filePathName, Type type)
        {
            XmlSerializer serializer = new XmlSerializer(type);

            // Create a new StreamWriter
            var reader = new StreamReader(filePathName);

            // Serialize the file
            object result = serializer.Deserialize(reader);

            // Close the writer
            reader.Close();

            return result;
        }

        public void SaveAsXmlFile(string filePathName, object item)
        {
            var serializer = new XmlSerializer(item.GetType());

            // Create a new StreamWriter
            TextWriter writer = new StreamWriter(Path.Combine(filePathName));

            // Serialize the file
            serializer.Serialize(writer, item);

            // Close the writer
            writer.Close();
        }
    }
}
