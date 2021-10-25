using Northwind.Models.Abstractions;
using Northwind.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Northwind.Models.Request
{
    public class FilterGroup
    {
        public FilterGroupOperator Operator { get; set; }
        public IEnumerable<Filter> Filters { get; set; }
        public IEnumerable<FilterGroup> Groups { get; set; }

        public Expression<Func<T, bool>> GetExpression<T>() where T : IExpAccess<T>
        {
            Expression<Func<T, bool>> res = null;
            var filterExpressions = new List<Expression<Func<T, bool>>>();
            var properties = typeof(T).GetProperties();

            foreach(var filter in Filters)
            {
                var property = properties.FirstOrDefault(x => x.Name == filter.Property);

                if (property == null) continue;

                var type = property.PropertyType;

                var op = filter.Operator;

                Expression<Func<T, bool>> expr = null;

                T obj = (T)typeof(T).GetMethod("GetInstance").Invoke(null, null);

                switch (type.Name) {
                    case nameof(String):
                        {
                            string value = filter.Value;
                            Expression<Func<string, bool>> stringFilterExp = (value, op) switch
                            {
                                ("" or null, _) => (x => true),
                                (_, FilterOperator.Contains) => x => x.Contains(value),
                                (_, _) => (x => false)
                            };                            
                            expr = Exp.Compose(obj.GetStringPropertyExp(property.Name), stringFilterExp);
                        }
                        break;
                    case nameof(Int32):
                        {
                            int value = int.Parse(filter.Value);
                            Expression<Func<int, bool>> intFilterExp = (value, op) switch
                            {
                                (_, FilterOperator.Equal) => x => x == value,
                                (_, FilterOperator.LesserThen) => x => x < value,
                                (_, FilterOperator.GreaterThen) => x => x > value,
                                (_, _) => x => true
                            };
                            expr = Exp.Compose(obj.GetIntPropertyExp(property.Name), intFilterExp);
                        }
                        break;
                    case nameof(Decimal):
                        {
                            decimal value = decimal.Parse(filter.Value);
                            Expression<Func<decimal, bool>> intFilterExp = (value, op) switch
                            {
                                (_, FilterOperator.Equal) => x => x == value,
                                (_, FilterOperator.LesserThen) => x => x < value,
                                (_, FilterOperator.GreaterThen) => x => x > value,
                                (_, _) => x => true
                            };
                            expr = Exp.Compose((T t) => (decimal)property.GetValue(t), intFilterExp);
                        }
                        break;
                    case nameof(DateTime):
                        {
                            DateTime value = DateTime.Parse(filter.Value);
                            Expression<Func<DateTime, bool>> intFilterExp = (value, op) switch
                            {
                                (_, FilterOperator.Equal) => x => x == value,
                                (_, FilterOperator.LesserThen) => x => x < value,
                                (_, FilterOperator.GreaterThen) => x => x > value,
                                (_, _) => x => true
                            };
                            expr = Exp.Compose((T t) => (DateTime)property.GetValue(t), intFilterExp);

                        }
                        break;
                    case nameof(Boolean):
                        {
                            bool value = bool.Parse(filter.Value);
                            Expression<Func<bool, bool>> intFilterExp = (value, op) switch
                            {
                                (_, FilterOperator.Equal) => x => x == value,
                                (_, _) => x => true
                            };
                            expr = Exp.Compose((T t) => (bool)property.GetValue(t), intFilterExp);
                        }
                        break;
                }

                if(expr != null)
                    filterExpressions.Add(expr);
            }

            switch(Operator)
            {
                case FilterGroupOperator.And:
                    {
                        foreach (var exp in filterExpressions)
                            res = Exp.And(res, exp);

                        foreach (var groupExp in Groups)
                        {
                            var exp = groupExp.GetExpression<T>();
                            res.And(exp);
                        }
                    }
                    break;
                case FilterGroupOperator.Or:
                    {
                        foreach (var exp in filterExpressions)
                            res = Exp.Or(res, exp);

                        foreach (var groupExp in Groups)
                        {
                            var exp = groupExp.GetExpression<T>();
                            res.Or(exp);
                        }
                    }
                    break;
                case FilterGroupOperator.Not:
                    {
                        if (filterExpressions.Count > 0) {
                            var fexp = filterExpressions[0];
                            res = Exp.Not(fexp);
                        }

                        if (Groups.Count() > 0)
                        {
                            var gexp = Groups.First().GetExpression<T>();
                            res = Exp.Not(gexp);
                        }
                    }
                    break;
            }            

            while (res.CanReduce)
                res.Reduce();

            return res;
        }        
    }

    public static class Exp
    {
        public static Expression<Func<TSource, TResult>> Compose<TSource, TIntermediate, TResult>(
            this Expression<Func<TSource, TIntermediate>> first,
            Expression<Func<TIntermediate, TResult>> second
        )
        {
            var param = Expression.Parameter(typeof(TSource));
            var intermediateValue = first.Body.ReplaceParameter(first.Parameters[0], param);
            var body = second.Body.ReplaceParameter(second.Parameters[0], intermediateValue);
            return Expression.Lambda<Func<TSource, TResult>>(body, param);
        }

        public static Expression ReplaceParameter(this Expression expression,
            ParameterExpression toReplace,
            Expression newExpression
        )
        {
            return new ParameterReplaceVisitor(toReplace, newExpression)
                .Visit(expression);
        }

        public class ParameterReplaceVisitor : ExpressionVisitor
        {
            private ParameterExpression from;
            private Expression to;
            public ParameterReplaceVisitor(ParameterExpression from, Expression to)
            {
                this.from = from;
                this.to = to;
            }
            protected override Expression VisitParameter(ParameterExpression node)
            {
                return node == from ? to : node;
            }
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                      Expression<Func<T, bool>> expr2)
        {
            if (expr1 is null || expr2 is null)
            {
                if (expr1 is null && expr2 is null) return null;

                return expr1 ?? expr2;
            }

            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            if(expr1 is null || expr2 is null)
            {
                if (expr1 is null && expr2 is null) return null;

                return expr1 ?? expr2;
            }

            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expr1)
        {
            return Expression.Lambda<Func<T, bool>>
                  (Expression.Not(expr1.Body), expr1.Parameters);
        }
    }
}
