using System.Globalization;
using System.Reflection;

namespace wslyvh.Core
{
    public class Singleton<T> where T : class
    {
        class Nested
        {
            static Nested() { }

            internal static readonly T Instance =
                typeof(T).InvokeMember(typeof(T).Name,
                                       BindingFlags.CreateInstance |
                                       BindingFlags.Instance |
                                       BindingFlags.Public |
                                       BindingFlags.NonPublic,
                                       null, null, null, CultureInfo.InvariantCulture) as T;
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static T Instance
        {
            get { return Nested.Instance; }
        }
    }
}
