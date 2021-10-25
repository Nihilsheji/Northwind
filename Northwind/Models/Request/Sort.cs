using Northwind.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Northwind.Models.Request
{
    public class Sort
    {
        public string Property { get; set; }
        public string Type { get; set; }
        public bool Ascending { get; set; }


        public object GetExpression<T>(ref Type t) where T : IExpAccess<T>
        {
            object res = null;

            var properties = typeof(T).GetProperties();

            var property = properties.FirstOrDefault(x => x.Name == Property);

            if (property == null) return null;

            t = property.PropertyType;

            T obj = (T)typeof(T).GetMethod("GetInstance").Invoke(null, null);

            switch(t.Name)
            {
                case nameof(String):
                    res = obj.GetStringPropertyExp(property.Name);
                    break;
                case nameof(Int32):
                    res = obj.GetIntPropertyExp(property.Name);
                    break;
                default:
                    res = null;
                    break;
            }

            return res;
        }

        public static Type GetType(string type) => type switch
        {
            "string" => typeof(string),
            "int" => typeof(int),
            "boolean" => typeof(bool),
            "decimal" => typeof(decimal),
            "datetime" => typeof(DateTime),
            (_) => null
        };
    }
}