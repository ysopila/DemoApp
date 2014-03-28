using System;

namespace DemoApp.DataModel.Entities
{
	public class Person : ContentObject
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime BirthDate { get; set; }
		public int Gender { get; set; }
	}
}
