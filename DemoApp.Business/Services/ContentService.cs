using System.Collections.Generic;
using System.Linq;
using DemoApp.Business.Models;
using DemoApp.Repositories;
using DemoApp.Core;

namespace DemoApp.Business.Services
{
    public class ContentService : IContentService
    {
        private IContentObjectRepository _repository;
        private SimpleMapper _mapper;

        public ContentService(IContentObjectRepository repository)
        {
            _repository = repository;
            _mapper = new SimpleMapper();

            _mapper.AddMapper<Data.Entities.ContentObject, Content>((model) =>
                new Content
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    Photo = model.Photo,
                    Type = model is Data.Entities.Book ? "book" : "person"
                });
            _mapper.AddMapper<Content, Data.Entities.ContentObject>((model) =>
                new Data.Entities.ContentObject
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description
                });
        }

        public IEnumerable<Content> GetAll()
        {
            return _repository.Get().Map(_mapper.Map<Data.Entities.ContentObject, Content>);
        }

        public Content Get(int id)
        {
            return _mapper.Map<Data.Entities.ContentObject, Content>(_repository.Get().SingleOrDefault(x => x.Id == id));
        }

        public Content Add(Content model)
        {
            using (var tran = _repository.BeginTransaction())
            {
                var entity = _repository.Create(_mapper.Map<Content, Data.Entities.ContentObject>(model));
                model.Id = entity.Id;
                tran.Commit();
            }
            return model;
        }

        public Content Save(Content model)
        {
            using (var tran = _repository.BeginTransaction())
            {
                var entity = _repository.Get().SingleOrDefault(x => x.Id == model.Id);
                entity.Description = model.Description;
                entity.Name = model.Name;
                entity.Photo = model.Photo;
                entity = _repository.Save(entity);
                tran.Commit();
                return _mapper.Map<Data.Entities.ContentObject, Content>(entity);
            }
        }

        public void Delete(int id)
        {
            _repository.Delete(_repository.Get().Single(x => x.Id == id));
        }
    }
}
