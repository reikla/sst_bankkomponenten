﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BankingApplication
{
    internal static class AssemblyLocator
    {
        internal static IEnumerable<Assembly> GetServiceAssemblies(ServiceType type)
        {
            var assemblyFiles = Directory.GetFiles(".", "*.dll", SearchOption.TopDirectoryOnly) //alle dlls
                .Select(Path.GetFileName) //. ".\\DllName.dll" -> "DllName.dll"
                .Where(x => x.StartsWith("Components.Service")) // alle die mit Components.Service beginnen
                .Where(x => x.Contains(type == ServiceType.Own ? "Own" : "Foreign")); // entweder eigene oder Fremde
            return assemblyFiles.Select(Assembly.LoadFrom); // AssemblyLaden
        }
    }

    enum ServiceType
    {
        Own,
        Foreign
    }
}
