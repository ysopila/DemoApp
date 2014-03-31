using System.Collections.Generic;
using System.Linq;
using DemoApp.Business.Models;
using DemoApp.Core;
using DemoApp.Business.Services.Abstractions;
using DemoApp.DataModel.Interfaces;
using DemoApp.Core.Mapper;
using Entity = DemoApp.DataModel.Entities;

namespace DemoApp.Business.Services.Implementations
{
	public class PersonService : IPersonService
	{
		private IUnitOfWork _unitOfWork;
		private ISimpleMapper _mapper;

		public PersonService(IUnitOfWork unitOfWork, ISimpleMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public IEnumerable<Person> GetAll()
		{
			return _unitOfWork.PersonRepository.Find().Map<Entity.Person, Person>(_mapper.Map<Entity.Person, Person>);
		}

		public IEnumerable<Person> GetAll(string authorName)
		{
			return _unitOfWork.PersonRepository.Find(filter: x => x.Name.Contains(authorName))
				.Map<Entity.Person, Person>(_mapper.Map<Entity.Person, Person>);
		}

		public Person Get(int id)
		{
			return _mapper.Map<Entity.Person, Person>(_unitOfWork.PersonRepository.Find(filter: x => x.Id == id).SingleOrDefault());
		}

		public Person Add(Person model)
		{
			var entity = _mapper.Map<Person, Entity.Person>(model);
			_unitOfWork.PersonRepository.Insert(entity);
			_unitOfWork.Save();
			return _mapper.Map<Entity.Person, Person>(entity);
		}

		public Person Save(Person model)
		{
			var entity = _unitOfWork.PersonRepository.Find(filter: x => x.Id == model.Id).SingleOrDefault();
			entity.BirthDate = model.BirthDate;
			entity.Description = model.Description;
			entity.FirstName = model.FirstName;
			entity.Gender = (int)model.Gender;
			entity.LastName = model.LastName;
			entity.Name = string.Format("{0} {1}", model.FirstName, model.LastName);
			_unitOfWork.PersonRepository.Update(entity);
			_unitOfWork.Save();
			return _mapper.Map<Entity.Person, Person>(entity);
		}

		public void Delete(int id)
		{
			var entity = _unitOfWork.PersonRepository.Find(filter: x => x.Id == id).SingleOrDefault();
			_unitOfWork.PersonRepository.Delete(entity);
			_unitOfWork.Save();
		}

	}
}
