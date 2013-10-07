using DemoApp.Data.Entities;
using System.Data.Objects;

namespace DemoApp.Repositories
{
    public class ContentObjectRepository : Repository<ContentObject>, IContentObjectRepository
    {
        public ContentObjectRepository(ObjectContext context)
            : base(context)
        {
        }
    }
}
