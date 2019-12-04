using Jsonzai;
using System;


namespace Jsonzai.Dummies
{
    public class JsonToDateTime : IJsonTypeConverter
    {
        public JsonToDateTime() { }

        public object Convert(string dateTimeStr) {
            return DateTime.Parse(dateTimeStr);
        }
    }
}
