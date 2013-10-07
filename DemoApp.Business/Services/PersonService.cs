using System.Collections.Generic;
using System.Linq;
using DemoApp.Business.Models;
using DemoApp.Repositories;
using DemoApp.Core;

namespace DemoApp.Business.Services
{
    public class PersonService : IPersonService
    {
        private IContentObjectRepository _repository;
        private SimpleMapper _mapper;

        public PersonService(IContentObjectRepository repository)
        {
            _repository = repository;
            _mapper = new SimpleMapper();

            _mapper.AddMapper<Data.Entities.Person, Person>((Data.Entities.Person model) => new Person
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Photo = model.Photo,
                BirthDate = model.BirthDate,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = (Gender)model.Gender,
                Type = "person"
            });
            _mapper.AddMapper((Person model) => new Data.Entities.Person
            {
                Id = model.Id,
                Name = model.FirstName + " " + model.LastName,
                Description = model.Description,
                BirthDate = model.BirthDate,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = (int)model.Gender
            });
        }

        public IEnumerable<Person> GetAll()
        {
            return _repository.Get().OfType<Data.Entities.Person>().Map(_mapper.Map<Data.Entities.Person, Person>);
        }

        public Person Get(int id)
        {
            return _mapper.Map<Data.Entities.Person, Person>(_repository.Get().OfType<Data.Entities.Person>().SingleOrDefault(x => x.Id == id));
        }

        public Person Add(Person model)
        {
            using (var tran = _repository.BeginTransaction())
            {
                var entity = _repository.Create(_mapper.Map<Person, Data.Entities.Person>(model));
                model.Id = entity.Id;
                tran.Commit();
            }
            return model;
        }

        public Person Save(Person model)
        {
            using (var tran = _repository.BeginTransaction())
            {
                var entity = _repository.Get().OfType<Data.Entities.Person>().Single(x => x.Id == model.Id);
                entity.BirthDate = model.BirthDate;
                entity.Description = model.Description;
                entity.FirstName = model.FirstName;
                entity.Gender = (int)model.Gender;
                entity.LastName = model.LastName;
                entity.Name = string.Format("{0} {1}", model.FirstName, model.LastName);
                entity = (Data.Entities.Person)_repository.Save(entity);
                tran.Commit();
                return _mapper.Map<Data.Entities.Person, Person>(entity);
            }
        }

        public void Delete(int id)
        {
            _repository.Delete(_repository.Get().OfType<Data.Entities.Person>().SingleOrDefault(x => x.Id == id));
        }
    }
}
