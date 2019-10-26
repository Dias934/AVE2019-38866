using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsonzai
{
	public class JsonPropertyAttribute : Attribute
	{
		public string Name { get; }


		public JsonPropertyAttribute(string Name)
		{
			this.Name = Name;
		}
	}
}
