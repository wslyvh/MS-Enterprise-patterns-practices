using System;
using System.Reflection;

namespace wslyvh.Core
{
    internal static class Core
    {
        private static AssemblyName _assemblyName = new AssemblyName(Assembly.GetExecutingAssembly().FullName);

        public static string Name = _assemblyName.Name;
        public static string FullName = _assemblyName.FullName;
        public static Version Version = _assemblyName.Version;
    }
}
