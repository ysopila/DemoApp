using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoApp.Core.Mapper
{
	public class MapperNotFoundException : Exception
	{
		public MapperNotFoundException() { }
		public MapperNotFoundException(string message) : base(message) { }
	}
}
