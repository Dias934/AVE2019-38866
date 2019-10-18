using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsonzai
{

	interface IJsonConvert
	{
		object Convert();
	}
	public class JsonToDateTime:IJsonConvert
	{

		private string _DateTime;

		public JsonToDateTime(String s)
		{
			_DateTime = s;
		}

		public object Convert(){
			return DateTime.Parse(_DateTime);
		}
	}
}
