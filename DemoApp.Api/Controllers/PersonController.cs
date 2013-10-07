using DemoApp.Business.Models;
using DemoApp.Business.Services;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace DemoApp.Api.Controllers
{
    public class PersonController : ApiController
    {
        private IPersonService PersonService { get; set; }

        public PersonController(IPersonService service)
        {
            PersonService = service;
        }

        public IEnumerable<Person> Get()
        {
            return PersonService.GetAll();
        }

        public Person Get(int id)
        {
            return PersonService.Get(id);
        }

        public Person Post(Person model)
        {
            return PersonService.Add(model);
        }

        public Person Put(int id, Person model)
        {
            var value = PersonService.Get(id);
            if (value == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return PersonService.Save(model);
        }

        public void Delete(int id)
        {
            var model = PersonService.Get(id);
            if (model == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            PersonService.Delete(model.Id);
        }
    }
}
