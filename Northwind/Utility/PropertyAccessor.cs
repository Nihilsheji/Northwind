using Northwind.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Northwind.Utility
{
    public static class PropertyAccessor<T>
    {
        private static Dictionary<Type, int> TupleTypeMap = new Dictionary<Type, int>() {
            { typeof(Category), 1 },
            { typeof(Order), 2 }
        };

        public static Tuple<
            Dictionary<string, LambdaExpression>,
            Dictionary<string, LambdaExpression>> PropAccessors
                = new Tuple<
                    Dictionary<string, LambdaExpression>,
                    Dictionary<string, LambdaExpression>>(
                        Category.PropDictionary,
                        Order.PropDictionary
                    );

        public static Tuple<
            Dictionary<string, PropertyInfo>,
            Dictionary<string, PropertyInfo>> PropInfoAccessors = new Tuple<Dictionary<string, PropertyInfo>, Dictionary<string, PropertyInfo>>
            (
                Category.PropInfoDictionary,
                Order.PropInfoDictionary
            );


        public static LambdaExpression GetPropertyExpression(string propName) {
            var i = TupleTypeMap.GetValueOrDefault(typeof(T));

            if (i == 0) return null;

            LambdaExpression exp;

            switch(i)
            {
                case 1:
                    exp =  PropAccessors.Item1.GetValueOrDefault(propName);
                    break;
                case 2:
                    exp = PropAccessors.Item2.GetValueOrDefault(propName);
                    break;                     
                default:
                    return null;
            }

            return exp;
        }

        public static PropertyInfo GetPropertyInfo(string propName)
        {
            var i = TupleTypeMap.GetValueOrDefault(typeof(T));

            if (i == 0) return null;

            PropertyInfo info;

            switch (i)
            {
                case 1:
                    info = PropInfoAccessors.Item1.GetValueOrDefault(propName);
                    break;
                case 2:
                    info = PropInfoAccessors.Item2.GetValueOrDefault(propName);
                    break;
                default:
                    return null;
            }

            return info;
        }
    }
}
