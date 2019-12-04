using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummies
{
	public class DummyLibSetterStudentGrade : ISetter
	{
		private Type t = typeof(string);
		
		public string GetName()
		{
			return "Grade";
		}

		public Type GetPropertyType()
		{
			return typeof(double);
		}

		public object SetValue(object target, object value)
		{
			((Student)target).Grade = (double)value;
			return target;
		}
	}
}
