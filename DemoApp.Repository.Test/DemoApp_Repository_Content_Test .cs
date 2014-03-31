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
	public class DemoApp_Repository_Content_Test
	{
		private IRepository<ContentObject> _repository;
		private Mock<IDbContext> mockContext;

		[TestInitialize]
		public void Start()
		{
			mockContext = new Mock<IDbContext>();

			_repository = new ContentRepository(mockContext.Object);
		}

		[TestMethod]
		public void DemoApp_Repository_Content_Find()
		{
			var mockSet = GetList();
			mockContext.Setup(x => x.Set<ContentObject>()).Returns(mockSet);

			var res = _repository.Find();
			mockContext.Verify(x => x.Set<ContentObject>());
			Assert.AreEqual(mockSet.Count(), res.Count());
			for (var i = 0; i < res.Count(); i++)
				Assert.AreEqual(res.ElementAt(i), mockSet.ElementAt(i));
		}

		[TestMethod]
		public void DemoApp_Repository_Content_Find_With_Filter()
		{
			var filterById = 1;
			var mockSet = GetList();
			mockContext.Setup(x => x.Set<ContentObject>()).Returns(mockSet);

			var elementById = _repository.Find(x => x.Id == filterById).SingleOrDefault();
			mockContext.Verify(x => x.Set<ContentObject>());
			Assert.AreEqual(elementById, mockSet.SingleOrDefault(x => x.Id == filterById));
		}

		[TestMethod]
		public void DemoApp_Repository_Content_Find_With_Order()
		{

			var mockSet = GetList();
			mockContext.Setup(x => x.Set<ContentObject>()).Returns(mockSet);

			var res = _repository.Find(orderBy: m => m.OrderByDescending(x => x.Id));

			mockContext.Verify(x => x.Set<ContentObject>());
			var list = mockSet.OrderByDescending(x => x.Id);

			Assert.AreEqual(list.Count(), res.Count());
			for (var i = 0; i < res.Count(); i++)
				Assert.AreEqual(res.ElementAt(i), list.ElementAt(i));

			var filterByDescription = "description 1";

			var elementsByDescription = _repository.Find(x => x.Description == filterByDescription);
			mockContext.Verify(x => x.Set<ContentObject>());
			var list2 = mockSet.Where(x => x.Description == filterByDescription);

			Assert.AreEqual(list2.Count(), elementsByDescription.Count());
			for (var i = 0; i < elementsByDescription.Count(); i++)
				Assert.AreEqual(elementsByDescription.ElementAt(i), list2.ElementAt(i));
		}

		[TestMethod]
		public void DemoApp_Repository_Content_Add()
		{
			var content = new ContentObject
			{
				Id = 99,
				Description = "description",
				Name = "Name",
				Photo = "Photo"
			};
			var mockSet = new Mock<DbSet<ContentObject>>();
			mockSet.Setup(x => x.Add(content)).Returns(content);
			mockContext.Setup(x => x.Set<ContentObject>()).Returns(mockSet.Object);

			_repository.Insert(content);
			mockContext.Verify(x => x.Set<ContentObject>());
			mockSet.Verify(x => x.Add(content));
		}

		private FakeDbSet<ContentObject> GetList()
		{
			return new FakeDbSet<ContentObject>{
				new ContentObject{
					Id = 1,
					Description = "description 1",
					Name = "Name1",
					Photo = "Photo1"
				},
				new ContentObject{
					Id = 2,
					Description = "description 2",
					Name = "Name2",
					Photo = "Photo2"
				},
				new ContentObject{
					Id = 3,
					Description = "description 3",
					Name = "Name3",
					Photo = "Photo3"
				},
				new ContentObject{
					Id = 4,
					Description = "description 1",
					Name = "Name4",
					Photo = "Photo4"
				},

			};
		}
	}
}
