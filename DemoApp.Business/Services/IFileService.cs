using System.IO;

namespace DemoApp.Business.Services
{
    public interface IFileService
    {
        void Save(string location, string filename, Stream fileStream);
    }
}
