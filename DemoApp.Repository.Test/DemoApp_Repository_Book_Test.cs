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
	public class DemoApp_Repository_Book_Test
	{
		private IRepository<Book> _repository;
		private Mock<IDbContext> mockContext;

		[TestInitialize]
		public void Start()
		{
			mockContext = new Mock<IDbContext>();

			_repository = new BookRepository(mockContext.Object);
		}

		[TestMethod]
		public void DemoApp_Repository_Test_Book_Find()
		{
			var mockSet = GetList();
			mockContext.Setup(x => x.Set<Book>()).Returns(mockSet);

			var res = _repository.Find();
			mockContext.Verify(x => x.Set<Book>());
			Assert.AreEqual(mockSet.Count(), res.Count());
			for (var i = 0; i < res.Count(); i++)
				Assert.AreEqual(res.ElementAt(i), mockSet.ElementAt(i));
		}

		[TestMethod]
		public void DemoApp_Repository_Test_Book_Find_With_Filter()
		{
			var filterById = 1;
			var mockSet = GetList();
			mockContext.Setup(x => x.Set<Book>()).Returns(mockSet);

			var elementById = _repository.Find(x => x.Id == filterById).SingleOrDefault();
			mockContext.Verify(x => x.Set<Book>());
			Assert.AreEqual(elementById, mockSet.SingleOrDefault(x => x.Id == filterById));

			var filterByDescription = "description 2";

			var elementsByDescription = _repository.Find(x => x.Description == filterByDescription);
			mockContext.Verify(x => x.Set<Book>());
			var list = mockSet.Where(x => x.Description == filterByDescription);

			Assert.AreEqual(list.Count(), elementsByDescription.Count());
			for (var i = 0; i < elementsByDescription.Count(); i++)
				Assert.AreEqual(elementsByDescription.ElementAt(i), list.ElementAt(i));

		}

		[TestMethod]
		public void DemoApp_Repository_Test_Book_Find_With_Order()
		{

			var mockSet = GetList();
			mockContext.Setup(x => x.Set<Book>()).Returns(mockSet);

			var res = _repository.Find(orderBy: m => m.OrderByDescending(x => x.Published));

			mockContext.Verify(x => x.Set<Book>());
			var list = mockSet.OrderByDescending(x => x.Published);

			Assert.AreEqual(list.Count(), res.Count());
			for (var i = 0; i < res.Count(); i++)
				Assert.AreEqual(res.ElementAt(i), list.ElementAt(i));
		}

		[TestMethod]
		public void DemoApp_Repository_Test_Book_Add()
		{
			var book = new Book
			{
				Id = 99,
				AuthorId = 99,
				Copyright = "Copyright",
				Description = "Description",
				Name = "Name",
				Photo = "Photo",
				Published = DateTime.Now
			};
			var mockSet = new Mock<DbSet<Book>>();
			mockSet.Setup(x => x.Add(book)).Returns(book);
			mockContext.Setup(x => x.Set<Book>()).Returns(mockSet.Object);

			_repository.Insert(book);
			mockContext.Verify(x => x.Set<Book>());
			mockSet.Verify(x => x.Add(book));
		}

		private FakeDbSet<Book> GetList()
		{
			return new FakeDbSet<Book>{
				new Book{
					Id = 1,
					Name = "name 1",
					Photo = "photo1.png",
					Published = DateTime.Now,
					Description="description 1",
					Copyright = "copyright 1",
					AuthorId = 1
				},
				new Book{
					Id = 2,
					Name = "name 2",
					Photo = "photo2.png",
					Published = DateTime.Now.AddDays(-2),
					Description="description 2",
					Copyright = "copyright 2",
					AuthorId = 2
				},
				new Book{
					Id = 3,
					Name = "name 3",
					Photo = "photo3.png",
					Published = DateTime.Now.AddMinutes(7),
					Description="description 3 and description 2",
					Copyright = "copyright 3",
					AuthorId = 3
				},
				new Book{
					Id = 4,
					Name = "name 4",
					Photo = "photo4.png",
					Published = DateTime.Now.AddSeconds(-30),
					Description="description 4",
					Copyright = "copyright 4",
					AuthorId = 4
				},

			};
		}
	}
}
