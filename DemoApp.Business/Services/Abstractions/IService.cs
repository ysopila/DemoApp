using System.Collections.Generic;

namespace DemoApp.Business.Services.Abstractions
{
    public interface IService<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Add(T model);
        T Save(T model);
        void Delete(int id);
    }
}
