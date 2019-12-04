using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummies

{
	public interface ISetter
	{
		object SetValue(object target, object value);
		string GetName();

		Type GetPropertyType();
	}
}
