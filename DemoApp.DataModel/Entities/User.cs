using System;

namespace DemoApp.DataModel.Entities
{
	public class User
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string PasswordSalt { get; set; }
		public string AuthToken { get; set; }
		public DateTime DateCreated { get; set; }
	}
}
