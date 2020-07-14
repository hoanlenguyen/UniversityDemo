using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace UniversityDemo.Repositories.BaseRepositories
{
    public struct QueryDefinition<TEntity>
    {
        private List<string> _selectFields;

        public QueryDefinition(params string[] selectFields)
        {
            _selectFields = new List<string>(selectFields);
        }

        public static QueryDefinition<TEntity> Select => new QueryDefinition<TEntity>();

        public QueryDefinition<TEntity> Fields(params string[] properties)
        {
            foreach (var propertyName in properties)
                if (!string.IsNullOrEmpty(propertyName))
                    if (_selectFields == null)
                        _selectFields = new List<string> { propertyName };
                    else
                        _selectFields.Add(propertyName);

            return this;
        }

        public QueryDefinition<TEntity> Fields(params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            foreach (var propertyExpression in propertyExpressions)
            {
                var propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
                if (!string.IsNullOrEmpty(propertyName))
                    if (_selectFields == null)
                        _selectFields = new List<string> { propertyName };
                    else
                        _selectFields.Add(propertyName);
            }

            return this;
        }

        public QueryDefinition<TEntity> Field(string name)
        {
            if (!string.IsNullOrEmpty(name))
                if (_selectFields == null)
                    _selectFields = new List<string> { name };
                else
                    _selectFields.Add(name);

            return this;
        }

        public QueryDefinition<TEntity> Field(Expression<Func<TEntity, object>> propertyExpression)
        {
            var propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
            if (!string.IsNullOrEmpty(propertyName))
                if (_selectFields == null)
                    _selectFields = new List<string> { propertyName };
                else
                    _selectFields.Add(propertyName);

            return this;
        }

        public string Build(string from = "c")
        {
            var select = "*";
            if (_selectFields?.Count > 0)
                select = _selectFields.Select(x => from + "." + x).Aggregate((x, y) => x + "," + y);
            return select;
        }

        private static class PropertySupport
        {
            public static string ExtractPropertyName<T>(Expression<Func<T, object>> propertyExpression)
            {
                if (propertyExpression == null)
                    throw new ArgumentNullException(nameof(propertyExpression));

                return ExtractPropertyNameFromLambda(propertyExpression);
            }

            private static string ExtractPropertyNameFromLambda(LambdaExpression expression)
            {
                if (expression == null)
                    throw new ArgumentNullException(nameof(expression));

                var exp = expression.Body;

                if (expression.Body is UnaryExpression unaryExpression)
                    exp = unaryExpression.Operand;

                if (!(exp is MemberExpression memberExpression))
                    throw new ArgumentException(nameof(expression));

                var property = memberExpression.Member as PropertyInfo;
                if (property == null)
                    throw new ArgumentException(nameof(expression));

                var getMethod = property.GetMethod;
                if (getMethod.IsStatic)
                    throw new ArgumentException(nameof(expression));

                var jsonProperty = property.GetCustomAttribute<JsonPropertyAttribute>();

                return jsonProperty?.PropertyName ?? memberExpression.Member.Name;
            }
        }
    }
}