using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jsonzai
{
	public class JsonParser
	{
		static readonly Type[] PARSE_ARGUMENTS_TYPES = { typeof(string) };
		
		static readonly ReflectionCache _cache = new ReflectionCache();

        public static object Parse(String source, Type klass)
        {
            return Parse(new JsonTokens(source), klass);
        }

        static object Parse(JsonTokens tokens, Type klass) {
            switch (tokens.Current) {
                case JsonTokens.OBJECT_OPEN:
                    return ParseObject(tokens, klass);
                case JsonTokens.ARRAY_OPEN:
                    return ParseArray(tokens, klass);
                case JsonTokens.DOUBLE_QUOTES:
                    return ParseString(tokens);
                default:
                    return ParsePrimitive(tokens, klass);
            }
        }

        private static string ParseString(JsonTokens tokens)
        {
            tokens.Pop(JsonTokens.DOUBLE_QUOTES); // Discard double quotes "
            return tokens.PopWordFinishedWith(JsonTokens.DOUBLE_QUOTES);
        }

        private static object ParsePrimitive(JsonTokens tokens, Type klass)
        {
            string word = tokens.popWordPrimitive();
            if (!klass.IsPrimitive || typeof(string).IsAssignableFrom(klass))
                if (word.ToLower().Equals("null"))
                    return null;
                else
                    throw new InvalidOperationException("Looking for a primitive but requires instance of " + klass);
			_cache.CheckAndAddType(klass, klass.GetMethod("Parse",new Type[] {typeof(string) })); //mete em cache o método Parse do tipo primitivo do argumento klass
			return _cache.GetValueFromMethod(klass, "Parse", word); //pede para receber um valor do método Parse 
		}

        private static object ParseObject(JsonTokens tokens, Type klass)
        {
			if (!_cache.ContainsType(klass))
			{
				PropertyInfo [] p = klass.GetProperties();
				FieldInfo[] f = klass.GetFields();
				MemberInfo[] m = new MemberInfo[p.Length+f.Length];
				Array.Copy(p, m, p.Length);
				Array.Copy(f, 0, m, p.Length, f.Length);
				_cache.CheckAndAddType(klass, m);
			}
			tokens.Pop(JsonTokens.OBJECT_OPEN); // Discard bracket { OBJECT_OPEN
            object target = Activator.CreateInstance(klass);
            return FillObject(tokens, target);
        }

        private static object FillObject(JsonTokens tokens, object target)
        {
			Type t = target.GetType();
			string name;
            while (tokens.Current != JsonTokens.OBJECT_END)
            {
				name = tokens.PopWordFinishedWith(':');
				if (_cache.TypeHasThisMember(t,name))
					_cache.SetValueInThisType(t,target,name,Parse(tokens, _cache.GetTypeOfMember(t,name)));
				else throw new ArgumentException("Wrong Field/Property passed on argument");
				if (tokens.Current == JsonTokens.COMMA) tokens.MoveNext();
				if (tokens.Current == JsonTokens.ARRAY_END) throw new InvalidOperationException("Wrong Array End character detected");
            }
            tokens.Pop(JsonTokens.OBJECT_END); // Discard bracket } OBJECT_END
            return target;
        }

        private static object ParseArray(JsonTokens tokens, Type klass)
        {

            ArrayList list = new ArrayList();
            tokens.Pop(JsonTokens.ARRAY_OPEN); // Discard square brackets [ ARRAY_OPEN
            while (tokens.Current != JsonTokens.ARRAY_END)
            {
				if (tokens.Current == JsonTokens.OBJECT_OPEN) list.Add(Parse(tokens, klass));
				if (tokens.Current == JsonTokens.COMMA) tokens.MoveNext();
				if (tokens.Current == ' ') tokens.Trim();
			}
            tokens.Pop(JsonTokens.ARRAY_END); // Discard square bracket ] ARRAY_END

            return list.ToArray(klass);
        }
    }
}
