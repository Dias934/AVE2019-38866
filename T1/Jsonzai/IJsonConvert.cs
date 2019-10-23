using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsonzai
{

	interface IJsonConvert
	{
		object Convert(string s);
	}
	public class JsonToDateTime:IJsonConvert
	{
		
		public JsonToDateTime(){}

		public object Convert(string s){
			return DateTime.Parse(s);
		}
	}
}
