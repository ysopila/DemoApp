using DemoApp.Business.Models;
using System.Collections.Generic;

namespace DemoApp.Business.Services.Abstractions
{
	public interface IPersonService : IService<Person>
	{
		IEnumerable<Person> GetAll(string authorName);
	}
}
