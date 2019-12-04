using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsonzai.Dummies
{
	public class DummyLibSetterStudentName : ISetter
	{
		private Type t = typeof(string);
		
		public string GetName()
		{
			return "Name";
		}

		public Type GetPropertyType()
		{
			return typeof(string);
		}

		public void SetValue(object target, object value)
		{
			((Student)target).Name = (string)value;
		}
	}
}
