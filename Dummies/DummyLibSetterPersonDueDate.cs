using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummies
{
	public class DummyLibSetterPersonDueDate : ISetter
	{
		private Type t = typeof(string);
		
		public string GetName()
		{
			return "DueDate";
		}

		public Type GetPropertyType()
		{
			return typeof(DateTime);
		}

		public object SetValue(object target, object value)
		{
			((Person)target).DueDate = (DateTime)(value);
			return target;
		}
	}
}
