using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoApp.Core.Mapper
{
	public interface ISimpleMapper
	{
		V Map<T, V>(T aObject);
	}
}
