using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummies {
	public class DummyLibSetterDateYear : ISetter {
		public string GetName() {
			return "Year";
		}

		public Type GetPropertyType() {
			return typeof(int);
		}

		public object SetValue(object target, object value) {
			Date d = (Date)target;
			d.Year = (int)value;
			return d;
		}
	}
}
