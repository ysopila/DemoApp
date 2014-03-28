using DemoApp.Business.Models;
using DemoApp.Business.Services.Abstractions;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace DemoApp.Api.Controllers
{
    public class BookController : ApiController
    {
        private IBookService BookService { get; set; }

        public BookController(IBookService service)
        {
            BookService = service;
        }

        public IEnumerable<Book> Get()
        {
            return BookService.GetAll();
        }

        public Book Get(int id)
        {
            return BookService.Get(id);
        }

        public Book Post(Book model)
        {
            return BookService.Add(model);
        }

        public Book Put(int id, Book model)
        {
            var value = BookService.Get(id);
            if (value == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return BookService.Save(model);
        }

        public void Delete(int id)
        {
            var model = BookService.Get(id);
            if (model == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            BookService.Delete(model.Id);
        }
    }
}
