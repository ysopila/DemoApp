using System.Collections.Generic;
using System.Linq;
using DemoApp.Business.Models;
using DemoApp.Core;
using DemoApp.Repositories;

namespace DemoApp.Business.Services
{
    public class BookService : IBookService
    {
        private IContentObjectRepository _repository;
        private SimpleMapper _mapper;

        public BookService(IContentObjectRepository repository)
        {
            _repository = repository;
            _mapper = new SimpleMapper();

            _mapper.AddMapper<Data.Entities.Book, Book>((model) => new Book
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Photo = model.Photo,
                Author = _mapper.Map<Data.Entities.Person, Person>(model.Author),
                Copyright = model.Copyright,
                Published = model.Published,
                Type = "book"
            });
            _mapper.AddMapper<Book, Data.Entities.Book>((Book model) => new Data.Entities.Book
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                AuthorId = model.Author.Id,
                Author = _mapper.Map<Person, Data.Entities.Person>(model.Author),
                Copyright = model.Copyright,
                Published = model.Published,
            });
            _mapper.AddMapper<Data.Entities.Person, Person>((model) => new Person
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                BirthDate = model.BirthDate,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = (Gender)model.Gender,
                Type = "person"
            });
            _mapper.AddMapper<Person, Data.Entities.Person>((Person model) => new Data.Entities.Person
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                BirthDate = model.BirthDate,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = (int)model.Gender
            });
        }

        public IEnumerable<Book> GetAll()
        {
            return _repository.Get().OfType<Data.Entities.Book>().Map(_mapper.Map<Data.Entities.Book, Book>);
        }

        public Book Get(int id)
        {
            return _mapper.Map<Data.Entities.Book, Book>(_repository.Get().OfType<Data.Entities.Book>().SingleOrDefault(x => x.Id == id));
        }

        public Book Add(Book model)
        {
            using (var tran = _repository.BeginTransaction())
            {
                var entity = _repository.Create(_mapper.Map<Book, Data.Entities.Book>(model));
                model.Id = entity.Id;
                tran.Commit();
            }
            return model;
        }

        public Book Save(Book model)
        {
            using (var tran = _repository.BeginTransaction())
            {
                var entity = _repository.Get().OfType<Data.Entities.Book>().SingleOrDefault(x => x.Id == model.Id);
                if (model.Author != null)
                {
                    entity.AuthorId = model.Author.Id;
                    entity.Author = _repository.Get().OfType<Data.Entities.Person>().SingleOrDefault(x => x.Id == model.Author.Id);
                }
                entity.Copyright = model.Copyright;
                entity.Description = model.Description;
                entity.Name = model.Name;
                entity.Published = model.Published;
                entity = (Data.Entities.Book)_repository.Save(entity);
                tran.Commit();
            }
            return model;
        }

        public void Delete(int id)
        {
            _repository.Delete(_repository.Get().OfType<Data.Entities.Book>().SingleOrDefault(x => x.Id == id));
        }
    }
}
