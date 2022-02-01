using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Shared
{
    public interface IFileService
    {
        void SaveAsXmlFile(string filePathName, object item);

        object ReadXmlFile(string filePathName, Type type);

        bool DirectoryExists(string path);

        void CreateDirectory(string path);

        string[] GetDirectoryFiles(string path);

        string[] GetDirectoryFiles(string path, string searchPattern);
    }
}
