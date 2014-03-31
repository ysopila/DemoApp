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
	public class DemoApp_Repository_Person_Test
	{
		private IRepository<Person> _repository;
		private Mock<IDbContext> mockContext;

		[TestInitialize]
		public void Start()
		{
			mockContext = new Mock<IDbContext>();

			_repository = new PersonRepository(mockContext.Object);
		}

		[TestMethod]
		public void DemoApp_Repository_Test_Person_Find()
		{
			var mockSet = GetList();
			mockContext.Setup(x => x.Set<Person>()).Returns(mockSet);

			var res = _repository.Find();
			mockContext.Verify(x => x.Set<Person>());
			Assert.AreEqual(mockSet.Count(), res.Count());
			for (var i = 0; i < res.Count(); i++)
				Assert.AreEqual(res.ElementAt(i), mockSet.ElementAt(i));
		}

		[TestMethod]
		public void DemoApp_Repository_Test_Person_Find_With_Filter()
		{
			var filterById = 1;
			var mockSet = GetList();
			mockContext.Setup(x => x.Set<Person>()).Returns(mockSet);

			var elementById = _repository.Find(x => x.Id == filterById).SingleOrDefault();
			mockContext.Verify(x => x.Set<Person>());
			Assert.AreEqual(elementById, mockSet.SingleOrDefault(x => x.Id == filterById));

			var filterByDescription = "description 2";

			var elementsByDescription = _repository.Find(x => x.Description == filterByDescription);
			mockContext.Verify(x => x.Set<Person>());
			var list = mockSet.Where(x => x.Description == filterByDescription);

			Assert.AreEqual(list.Count(), elementsByDescription.Count());
			for (var i = 0; i < elementsByDescription.Count(); i++)
				Assert.AreEqual(elementsByDescription.ElementAt(i), list.ElementAt(i));

		}

		[TestMethod]
		public void DemoApp_Repository_Test_Person_Find_With_Order()
		{

			var mockSet = GetList();
			mockContext.Setup(x => x.Set<Person>()).Returns(mockSet);

			var res = _repository.Find(orderBy: m => m.OrderByDescending(x => x.BirthDate));

			mockContext.Verify(x => x.Set<Person>());
			var list = mockSet.OrderByDescending(x => x.BirthDate);

			Assert.AreEqual(list.Count(), res.Count());
			for (var i = 0; i < res.Count(); i++)
				Assert.AreEqual(res.ElementAt(i), list.ElementAt(i));
		}

		[TestMethod]
		public void DemoApp_Repository_Test_Person_Add()
		{
			var person = new Person
			{
				Id = 99,
				Description = "Description",
				Name = "Name",
				Photo = "Photo",
				BirthDate = DateTime.Now,
				FirstName = "FirstName",
				LastName = "LastName"
			};
			var mockSet = new Mock<DbSet<Person>>();
			mockSet.Setup(x => x.Add(person)).Returns(person);
			mockContext.Setup(x => x.Set<Person>()).Returns(mockSet.Object);

			_repository.Insert(person);
			mockContext.Verify(x => x.Set<Person>());
			mockSet.Verify(x => x.Add(person));
		}

		private FakeDbSet<Person> GetList()
		{
			return new FakeDbSet<Person>{
				new Person{
					Id = 1,
					Name = "name 1",
					Photo = "photo1.png",
					Description="description 1",
					BirthDate = DateTime.Now.AddMinutes(15),
					FirstName = "FirstName",
					LastName = "LastName"
				},
				new Person{
					Id = 2,
					Name = "name 2",
					Photo = "photo2.png",
					Description="description 2",
					BirthDate = DateTime.Now.AddMinutes(1),
					FirstName = "FirstName",
					LastName = "LastName"
				},
				new Person{
					Id = 3,
					Name = "name 3",
					Photo = "photo3.png",
					Description="description 3 and description 2",
					BirthDate = DateTime.Now.AddMinutes(-3),
					FirstName = "FirstName",
					LastName = "LastName"
				},
				new Person{
					Id = 4,
					Name = "name 4",
					Photo = "photo4.png",
					Description="description 4",
					BirthDate = DateTime.Now.AddMinutes(9),
				},

			};
		}
	}
}
