using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace wslyvh.Core
{
    public static class Guard
    {
        public static void ArgumentIsNotNull<T>(T argumentValue, string argumentName)
        {
            if (argumentValue == null) throw new ArgumentNullException(argumentName);
        }

        public static void ArgumentIsNotNullOrEmpty(string argumentValue, string argumentName)
        {
            Guard.ArgumentIsNotNull(argumentValue, argumentName);
            if (argumentValue.Length == 0) throw new ArgumentException(argumentName);
        }

        public static void ArgumentItemsAreNotNull<T>(IEnumerable<T> argumentValue, string argumentName)
        {
            var values = argumentValue as T[] ?? argumentValue.ToArray();
            Guard.ArgumentIsNotNull(values, argumentName);

            foreach (var value in values)
                Guard.ArgumentIsNotNull(value, argumentName);
        }

        public static void ArgumentIsNotNegative(int argumentValue, string argumentName)
        {
            if (argumentValue < 0) throw new ArgumentOutOfRangeException(argumentName);
        }

        public static void ArgumentIsPositive(int argumentValue, string argumentName)
        {
            if (argumentValue <= 0) throw new ArgumentOutOfRangeException(argumentName);
        }
    }
}
