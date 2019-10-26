using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jsonzai
{
	public class ReflectionCache
	{
		private readonly Dictionary<Type, MetadataTypeStorage> _typeCache= new Dictionary<Type, MetadataTypeStorage>();

		public ReflectionCache(){}

		public void CheckAndAddType(Type t, params MemberInfo[] list)
		{
			if(!_typeCache.ContainsKey(t))
				_typeCache.Add(t, new MetadataTypeStorage(list));
		}

		public bool ContainsType(Type t)
		{
			return _typeCache.ContainsKey(t);
		}

		public bool TypeHasThisMember(Type t, string member)
		{
			return _typeCache[t].ContainsMember(member);
		}

		public object GetValueFromMethod(Type t, string name, string val)
		{
			return _typeCache[t].GetValueFromMethod(name, val);
		}

		public void SetValueInThisType(Type t, object target, string name, object val)
		{
			_typeCache[t].SetValue(target, name, val);
		}

		public Type GetTypeOfMember(Type t, string name)
		{
			return _typeCache[t].GetTypeOfMember(name);
		}
	}

	class MetadataTypeStorage
	{
		private readonly IDictionary<string, MemberInfo> _storage = new Dictionary<string, MemberInfo>();
	
		public MetadataTypeStorage(params MemberInfo [] list)
		{
			foreach (MemberInfo mi in list)
				_storage.Add(CheckAttributeName(mi), mi);
		}

		private string CheckAttributeName(MemberInfo m)
		{
			if (m.GetCustomAttribute(typeof(JsonPropertyAttribute)) != null)
				return m.GetCustomAttribute<JsonPropertyAttribute>().Name;
			return m.Name;
		}

		public bool ContainsMember(string s)
		{
			return _storage.ContainsKey(s);
		}

		public object GetValueFromMethod(string name, string val)
		{
			return ((MethodInfo)_storage[name]).Invoke(null, new string[] { val });
		}

		public void SetValue(object target, string name, object val)
		{
			if (_storage.ContainsKey(name)) 
			{
				Type t = GetMemberInfoType(_storage[name]);
				if(t.Equals(typeof(PropertyInfo)))
					((PropertyInfo)_storage[name]).SetValue(target, CheckAttributeObject(_storage[name], val));
				else
					((FieldInfo)_storage[name]).SetValue(target, CheckAttributeObject(_storage[name], val));

			}				
		}

		private object CheckAttributeObject(MemberInfo m, object val)
		{
			if (m.GetCustomAttribute(typeof(JsonConvertAttribute)) != null)
				return m.GetCustomAttribute<JsonConvertAttribute>().Convert(val.ToString());
			return val;
		}

		public Type GetTypeOfMember(string name)
		{
			Type t=GetMemberInfoType(_storage[name]);
			if (t.Equals(typeof(PropertyInfo))) 
				t=((PropertyInfo)_storage[name]).PropertyType;
			else
				t = ((FieldInfo)_storage[name]).FieldType;
			if (t.IsArray) t = t.GetElementType();
			return t;
		}

		private Type GetMemberInfoType(MemberInfo member)
		{
			switch (member.MemberType)
			{
				case MemberTypes.Field:
					return typeof(FieldInfo);
				case MemberTypes.Property:
					return typeof(PropertyInfo);
				default:
					throw new ArgumentException("MemberInfo must be if type FieldInfo, PropertyInfo or EventInfo", "member");
			}
		}
	}
}
