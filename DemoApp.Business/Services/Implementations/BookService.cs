using System.Collections.Generic;
using System.Linq;
using DemoApp.Business.Models;
using DemoApp.Core;
using DemoApp.Business.Services.Abstractions;
using DemoApp.DataModel.Interfaces;
using Entity = DemoApp.DataModel.Entities;
using DemoApp.Core.Mapper;

namespace DemoApp.Business.Services.Implementations
{
	public class BookService : IBookService
	{
		private IUnitOfWork _unitOfWork;
		private ISimpleMapper _mapper;

		public BookService(IUnitOfWork unitOfWork, ISimpleMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public IEnumerable<Book> GetAll()
		{
			return _unitOfWork.BookRepository.Find().Map(_mapper.Map<Entity.Book, Book>);
		}

		public Book Get(int id)
		{
			return _mapper.Map<Entity.Book, Book>(_unitOfWork.BookRepository.Find(filter: x => x.Id == id).SingleOrDefault());
		}

		public Book Add(Book model)
		{
			var entity = _mapper.Map<Book, Entity.Book>(model);
			_unitOfWork.BookRepository.Insert(entity);
			_unitOfWork.Save();
			return _mapper.Map<Entity.Book, Book>(entity);
		}

		public Book Save(Book model)
		{
			var entity = _unitOfWork.BookRepository.Find(filter: x => x.Id == model.Id).SingleOrDefault();
			if (model.Author != null)
			{
				entity.AuthorId = model.Author.Id;
				entity.Author = _unitOfWork.PersonRepository.Find(filter: x => x.Id == model.Author.Id).SingleOrDefault();
			}
			entity.Copyright = model.Copyright;
			entity.Description = model.Description;
			entity.Name = model.Name;
			entity.Published = model.Published;

			_unitOfWork.BookRepository.Update(entity);
			_unitOfWork.Save();
			return model;
		}

		public void Delete(int id)
		{
			var entity = _unitOfWork.BookRepository.Find(filter: x => x.Id == id).SingleOrDefault();
			if (entity != null)
			{
				_unitOfWork.BookRepository.Delete(entity);
				_unitOfWork.Save();
			}
		}
	}
}
