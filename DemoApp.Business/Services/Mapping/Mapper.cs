using DemoApp.Business.Models;
using DemoApp.Core.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity = DemoApp.DataModel.Entities;

namespace DemoApp.Business.Services.Mapping
{
	public class Mapper : SimpleMapper, ISimpleMapper
	{
		public Mapper()
		{
			this.AddMapper<Entity.User, User>((Entity.User model) => model == null ? null : new User
			{
				Id = model.Id,
				DateCreated = model.DateCreated,
				Username = model.Username,
				AuthToken = model.AuthToken,
				Password = model.Password,
				PasswordSalt = model.PasswordSalt
			});
			this.AddMapper((User model) => model == null ? null : new Entity.User
			{
				Id = model.Id,
				DateCreated = model.DateCreated,
				Username = model.Username,
				AuthToken = model.AuthToken,
				Password = model.Password,
				PasswordSalt = model.PasswordSalt
			});
			this.AddMapper<Entity.Person, Person>((Entity.Person model) => model == null ? null : new Person
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
			this.AddMapper((Person model) => model == null ? null : new Entity.Person
			{
				Id = model.Id,
				Name = model.FirstName + " " + model.LastName,
				Description = model.Description,
				BirthDate = model.BirthDate,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Gender = (int)model.Gender
			});
			this.AddMapper<Entity.ContentObject, Content>((model) => model == null ? null
				: new Content
				{
					Id = model.Id,
					Name = model.Name,
					Description = model.Description,
					Photo = model.Photo,
					Type = model is Entity.Book ? "book" : "person"
				});
			this.AddMapper<Content, Entity.ContentObject>((model) => model == null ? null
				: new Entity.ContentObject
				{
					Id = model.Id,
					Name = model.Name,
					Description = model.Description
				});
			this.AddMapper<Entity.Book, Book>((model) => model == null ? null : new Book
			{
				Id = model.Id,
				Name = model.Name,
				Description = model.Description,
				Photo = model.Photo,
				Author = this.Map<Entity.Person, Person>(model.Author),
				Copyright = model.Copyright,
				Published = model.Published,
				Type = "book"
			});
			this.AddMapper<Book, Entity.Book>((Book model) => model == null ? null : new Entity.Book
			{
				Id = model.Id,
				Name = model.Name,
				Description = model.Description,
				AuthorId = model.Author.Id,
				Author = this.Map<Person, Entity.Person>(model.Author),
				Copyright = model.Copyright,
				Published = model.Published,
			});
		}
	}
}
