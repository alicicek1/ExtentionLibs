using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace UsefulExtensions
{
    public static class AssemblyExtension
    {
        public static string GetDirectoryPath(this Assembly assembly)
        {
            var filePath = new Uri(assembly.CodeBase).LocalPath;

            if (filePath.EndsWith(@"\") == false)
            {
                filePath += @"\";
            }
            return Path.GetDirectoryName(filePath);
        }
    }
}
