using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jsonzai
{
	class PropertyFieldStorage
	{
		IDictionary<string, PropertyInfo> props = new Dictionary<string, PropertyInfo>();
		IDictionary<string, FieldInfo> fields = new Dictionary<string, FieldInfo>();

		public PropertyFieldStorage() { }
		public PropertyFieldStorage(PropertyInfo[] props, FieldInfo[] fields) { 
			foreach(PropertyInfo p in props)
			{
				this.props.Add(p.Name, p);
			}
			foreach (FieldInfo f in fields)
			{
				this.fields.Add(f.Name, f);
			}
		}

		public bool ContainsMember(string s){
			return props.ContainsKey(s) | fields.ContainsKey(s);
		}

		internal void SetValue(object target, string aux, object val)
		{
			if (props.ContainsKey(aux))
				props[aux].SetValue(target, val);
			else
				fields[aux].SetValue(target, val);
		}

		internal Type GetTypeOfMember(string aux){
			Type t;
			if (props.ContainsKey(aux))
				t= props[aux].PropertyType;
			else
				t= fields[aux].FieldType;
			if (t.IsArray) t = t.GetElementType();
			return t;
		}
	}
}
