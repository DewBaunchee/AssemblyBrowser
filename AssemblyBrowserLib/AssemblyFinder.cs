using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AssemblyBrowserLib
{
    public static class AssemblyFinder
    {
        public static List<string> FindAssemblies(string root)
        {
            if (File.Exists(root))
            {
                return IsAssembly(root) ? new List<string> {root} : new List<string>();
            }

            if (Directory.Exists(root))
            {
                var paths = new List<string>();
                try
                {
                    Directory.GetFileSystemEntries(root).ToList()
                        .ForEach(path => paths.AddRange(FindAssemblies(path)));
                }
                catch (Exception)
                {
                    // ignored
                }

                return paths;
            }

            throw new Exception("Cannot find " + root);
        }

        private static bool IsAssembly(string path)
        {
            try
            {
                var a = Assembly.LoadFrom(path);
                return true;
            }
            catch(Exception)
            {
                return false;
            }  
        }
    }
}