using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Northwind.Models.Abstractions
{
    public interface IExpAccess<T>
    {
        Expression<Func<T, int>> GetIntPropertyExp(string propName) { return null; }

        Expression<Func<T, string>> GetStringPropertyExp(string propName) { return null; }

        T MakeEmpty();
    }
}
