using System.Collections.Generic;
using System.Linq;
using DemoApp.Business.Models;
using DemoApp.Repositories;
using DemoApp.Core;

namespace DemoApp.Business.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;
        private SimpleMapper _mapper;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
            _mapper = new SimpleMapper();

            _mapper.AddMapper<Data.Entities.User, User>((Data.Entities.User model) => new User
            {
                Id = model.Id,
                DateCreated = model.DateCreated,
                Username = model.Username,
                AuthToken = model.AuthToken,
                Password = model.Password,
                PasswordSalt = model.PasswordSalt
            });
            _mapper.AddMapper((User model) => new Data.Entities.User
            {
                Id = model.Id,
                DateCreated = model.DateCreated,
                Username = model.Username,
                AuthToken = model.AuthToken,
                Password = model.Password,
                PasswordSalt = model.PasswordSalt
            });
        }

        public IEnumerable<User> GetAll()
        {
            return _repository.Get().OfType<Data.Entities.User>().Map(_mapper.Map<Data.Entities.User, User>);
        }

        public User Get(int id)
        {
            return _mapper.Map<Data.Entities.User, User>(_repository.Get().OfType<Data.Entities.User>().SingleOrDefault(x => x.Id == id));
        }

        public User Get(string username)
        {
            var user = _repository.Get().OfType<Data.Entities.User>().SingleOrDefault(x => x.Username == username);
            if (user == null) return null;
            return _mapper.Map<Data.Entities.User, User>(user);
        }

        public User Add(User model)
        {
            using (var tran = _repository.BeginTransaction())
            {
                var entity = _repository.Create(_mapper.Map<User, Data.Entities.User>(model));
                model.Id = entity.Id;
                tran.Commit();
            }
            return model;
        }

        public User Save(User model)
        {
            using (var tran = _repository.BeginTransaction())
            {
                var entity = _repository.Get().OfType<Data.Entities.User>().Single(x => x.Id == model.Id);
                entity.Username = model.Username;
                entity.AuthToken = model.AuthToken;
                entity.DateCreated = model.DateCreated;
                entity.PasswordSalt = model.PasswordSalt;
                entity.Password = model.Password;

                entity = (Data.Entities.User)_repository.Save(entity);
                tran.Commit();
                return _mapper.Map<Data.Entities.User, User>(entity);
            }
        }

        public void Delete(int id)
        {
            _repository.Delete(_repository.Get().OfType<Data.Entities.User>().SingleOrDefault(x => x.Id == id));
        }
    }
}
