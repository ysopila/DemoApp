using System;

namespace DemoApp.DataModel.Entities
{
	public class Book : ContentObject
	{
		public int AuthorId { get; set; }
		public DateTime Published { get; set; }
		public string Copyright { get; set; }

		public virtual Person Author { get; set; }
	}
}