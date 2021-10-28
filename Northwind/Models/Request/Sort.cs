using Northwind.Utility;

namespace Northwind.Models.Request
{
    public class Sort
    {
        public string Property { get; set; }
        public bool Ascending { get; set; }


        public object GetExpression<T>() => PropertyAccessor<T>.GetPropertyExpression(Property);
    }
}