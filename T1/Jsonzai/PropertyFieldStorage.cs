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
			foreach (PropertyInfo p in props)
				this.props.Add(CheckAttributeName(p,p.Name), p);
			foreach (FieldInfo f in fields)
				this.fields.Add(CheckAttributeName(f,f.Name), f);
			
		}

		private string CheckAttributeName(MemberInfo m,string name)
		{
			if (m.GetCustomAttribute(typeof(JsonPropertyAttribute)) != null)
				return m.GetCustomAttribute<JsonPropertyAttribute>().Name;
			return name;
		}

		public bool ContainsMember(string s){
			return props.ContainsKey(s) | fields.ContainsKey(s);
		}

		internal void SetValue(object target, string aux, object val)
		{
			if (props.ContainsKey(aux))
				props[aux].SetValue(target, CheckAttributeName(props[aux],val));
			else
				fields[aux].SetValue(target, CheckAttributeName(fields[aux], val));
		}
		private object CheckAttributeName(MemberInfo m, object val)
		{
			if (m.GetCustomAttribute(typeof(JsonConvertAttribute)) != null)
				return m.GetCustomAttribute<JsonConvertAttribute>().Convert(val.ToString());
			return val;
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
