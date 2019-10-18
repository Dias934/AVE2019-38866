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
		static IDictionary<Type, PropertyFieldStorage> _cache=new Dictionary<Type, PropertyFieldStorage>(); //cache

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
			if(klass.Equals(typeof(decimal))) return Convert.ToDecimal(word);
			if (klass.Equals(typeof(float))) return Convert.ToSingle(word);
			if (klass.Equals(typeof(double))) return Convert.ToDouble(word);
			if (klass.Equals(typeof(short))) return Convert.ToInt16(word);
			if (klass.Equals(typeof(int))) return Convert.ToInt32(word);
			if (klass.Equals(typeof(long))) return Convert.ToInt64(word);
			if (klass.Equals(typeof(ushort))) return Convert.ToUInt16(word);
			if (klass.Equals(typeof(uint))) return Convert.ToUInt32(word);
			if (klass.Equals(typeof(ulong))) return Convert.ToUInt64(word);
			throw new NotImplementedException();
		}

        private static object ParseObject(JsonTokens tokens, Type klass)
        {
			if (!_cache.ContainsKey(klass))
				_cache.Add(klass, new PropertyFieldStorage(klass.GetProperties(), klass.GetFields()));
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
				if (name.Length>0 && name[0] > 'a' && name[0] < 'z') name=name.ToUpper().Substring(0,1)+name.Substring(1,name.Length);
				if (_cache[t].ContainsMember(name))
					_cache[t].SetValue(target,name,JsonParser.Parse(tokens, _cache[t].GetTypeOfMember(name)));
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
				if (tokens.Current == JsonTokens.OBJECT_OPEN) list.Add(JsonParser.Parse(tokens, klass));
				if (tokens.Current == JsonTokens.COMMA) tokens.MoveNext();
				if (tokens.Current == ' ') tokens.Trim();
			}
            tokens.Pop(JsonTokens.ARRAY_END); // Discard square bracket ] ARRAY_END

            return list.ToArray(klass);
        }
    }
}
