using System.IO;

namespace DemoApp.Business.Services.Abstractions
{
    public interface IFileService
    {
        void Save(string location, string filename, Stream fileStream);
    }
}
