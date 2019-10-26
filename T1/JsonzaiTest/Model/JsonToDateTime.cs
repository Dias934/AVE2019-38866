using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jsonzai;

namespace Jsonzai.Test.Model
{
	public class JsonToDateTime : IJsonConvert
	{

		public JsonToDateTime() { }

		public object Convert(string s)
		{
			return DateTime.Parse(s);
		}
	}
}
