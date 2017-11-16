using System;
using System.Collections.Generic;

namespace Infrastructure
{
    /// <summary>
    /// Switch по типам
    /// </summary>
    public class TypedSwitch<TOutputType>
    {
        private readonly Dictionary<Type, Func<object, TOutputType>> _matches = new Dictionary<Type, Func<object, TOutputType>>();

        public TypedSwitch<TOutputType> Case<T>(Func<T, TOutputType> action)
        {
            _matches.Add(typeof(T), (x) => action((T)x));

            return this;
        }

        public TOutputType Switch(object x)
        {
            var type = x.GetType();
            if (_matches.ContainsKey(type))
            {
                return _matches[type](x);
            }

            return default(TOutputType);
        }
    }
}