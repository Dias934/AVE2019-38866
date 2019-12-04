using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsonzai.Dummies
{
    public class JsonConvertAttribute : Attribute
    {
        public Type ConverterType { get; set; }
        public JsonConvertAttribute(Type jsonConvertAttribute)
        {
            ConverterType = jsonConvertAttribute;
        }
    }
}
