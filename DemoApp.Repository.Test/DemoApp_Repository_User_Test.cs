using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DemoApp.DataModel.Entities;
using DemoApp.DataModel.Interfaces;
using Moq;
using DemoApp.Repositories;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace DemoApp.Repository.Test
{
	[TestClass]
	public class DemoApp_Repository_User_Test
	{
		private IRepository<User> _repository;
		private Mock<IDbContext> mockContext;

		[TestInitialize]
		public void Start()
		{
			mockContext = new Mock<IDbContext>();

			_repository = new UserRepository(mockContext.Object);
		}

		[TestMethod]
		public void DemoApp_Repository_User_Find()
		{
			var mockSet = GetList();
			mockContext.Setup(x => x.Set<User>()).Returns(mockSet);

			var res = _repository.Find();
			mockContext.Verify(x => x.Set<User>());
			Assert.AreEqual(mockSet.Count(), res.Count());
			for (var i = 0; i < res.Count(); i++)
				Assert.AreEqual(res.ElementAt(i), mockSet.ElementAt(i));
		}

		[TestMethod]
		public void DemoApp_Repository_User_Find_With_Filter()
		{
			var filterById = 1;
			var mockSet = GetList();
			mockContext.Setup(x => x.Set<User>()).Returns(mockSet);

			var elementById = _repository.Find(x => x.Id == filterById).SingleOrDefault();
			mockContext.Verify(x => x.Set<User>());
			Assert.AreEqual(elementById, mockSet.SingleOrDefault(x => x.Id == filterById));
		}

		[TestMethod]
		public void DemoApp_Repository_User_Find_With_Order()
		{

			var mockSet = GetList();
			mockContext.Setup(x => x.Set<User>()).Returns(mockSet);

			var res = _repository.Find(orderBy: m => m.OrderByDescending(x => x.DateCreated));

			mockContext.Verify(x => x.Set<User>());
			var list = mockSet.OrderByDescending(x => x.DateCreated);

			Assert.AreEqual(list.Count(), res.Count());
			for (var i = 0; i < res.Count(); i++)
				Assert.AreEqual(res.ElementAt(i), list.ElementAt(i));
		}

		[TestMethod]
		public void DemoApp_Repository_User_Add()
		{
			var user = new User
			{
				Id = 99,
				DateCreated= DateTime.Now,
				Password = "testc",
				PasswordSalt = "test",
				Username = "UserName"
			};
			var mockSet = new Mock<DbSet<User>>();
			mockSet.Setup(x => x.Add(user)).Returns(user);
			mockContext.Setup(x => x.Set<User>()).Returns(mockSet.Object);

			_repository.Insert(user);
			mockContext.Verify(x => x.Set<User>());
			mockSet.Verify(x => x.Add(user));
		}

		private FakeDbSet<User> GetList()
		{
			return new FakeDbSet<User>{
				new User{
					Id = 1,
					DateCreated= DateTime.Now.AddMonths(3),
					Password = "test1",
					PasswordSalt = "test",
					Username = "UserName1"
				},
				new User{
					Id = 2,
					DateCreated= DateTime.Now.AddMinutes(-7),
					Password = "test2",
					PasswordSalt = "test",
					Username = "UserName2"
				},
				new User{
					Id = 3,
					DateCreated= DateTime.Now.AddYears(1),
					Password = "test3",
					PasswordSalt = "test",
					Username = "UserName3"
				},
				new User{
					Id = 4,
					DateCreated= DateTime.Now,
					Password = "test4",
					PasswordSalt = "test",
					Username = "UserName4"
				},

			};
		}
	}
}
