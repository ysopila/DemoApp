using System.Collections.Generic;
using System.Linq;
using DemoApp.Business.Models;
using DemoApp.Core;
using DemoApp.Business.Services.Abstractions;
using DemoApp.Core.Mapper;
using DemoApp.DataModel.Interfaces;
using Entity = DemoApp.DataModel.Entities;
namespace DemoApp.Business.Services.Implementations
{
	public class UserService : IUserService
	{
		private IUnitOfWork _unitOfWork;
		private ISimpleMapper _mapper;

		public UserService(IUnitOfWork unitOfWork, ISimpleMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;

		}

		public IEnumerable<User> GetAll()
		{
			return _unitOfWork.UserRepository.Find().Map<Entity.User, User>(_mapper.Map<Entity.User, User>);
		}

		public User Get(int id)
		{
			return _mapper.Map<Entity.User, User>(_unitOfWork.UserRepository.Find(filter: x=>x.Id == id).SingleOrDefault());
		}

		public User Get(string username)
		{
			return _mapper.Map<Entity.User, User>(_unitOfWork.UserRepository.Find(filter: x => x.Username == username).SingleOrDefault());
		}

		public User Add(User model)
		{
			var entity = _mapper.Map<User, Entity.User>(model);
			_unitOfWork.UserRepository.Insert(entity);
			_unitOfWork.Save();
			return _mapper.Map<Entity.User, User>(entity);
		}

		public User Save(User model)
		{
			var entity = _unitOfWork.UserRepository.Find(filter: x => x.Id == model.Id).SingleOrDefault();
			entity.Username = model.Username;
			entity.AuthToken = model.AuthToken;
			entity.DateCreated = model.DateCreated;
			entity.PasswordSalt = model.PasswordSalt;
			entity.Password = model.Password;
			_unitOfWork.UserRepository.Update(entity);
			_unitOfWork.Save();

			return _mapper.Map<Entity.User, User>(entity);
		}

		public void Delete(int id)
		{
			var entity = _unitOfWork.UserRepository.Find(filter: x => x.Id == id).SingleOrDefault();
			if (entity != null)
			{
				_unitOfWork.UserRepository.Delete(entity);
				_unitOfWork.Save();
			}
		}
	}
}
