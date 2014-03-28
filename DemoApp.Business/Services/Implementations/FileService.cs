using DemoApp.Business.Services.Abstractions;
using System;
using System.IO;

namespace DemoApp.Business.Services.Implementations
{
    public class FileService : IFileService
    {
        private string CreateDirectory(string directory)
        {
            var fullPath = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, directory);

            if (Directory.Exists(directory)) return fullPath;

            return Directory.CreateDirectory(fullPath).FullName;
        }
        public void Save(string location, string filename, Stream fileStream)
        {
            var fullPath = string.Format(@"{0}\{1}", CreateDirectory(location), filename);
            using (var newFileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                fileStream.CopyTo(newFileStream);
                fileStream.Flush();
                fileStream.Close();
            }
        }
    }
}
