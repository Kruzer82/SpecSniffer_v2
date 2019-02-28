using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace SpecSniffer_v2
{
    internal class Resources
    {
        public const string WmiError = "Error";
        public const string WmiNoData = "n/a";
        public const string PcNotLoaded = "Not Loaded";
        public const string NoHdd = "no HDD";

        public static readonly string RunPath = System.IO.Path.GetDirectoryName((new System.Uri(Assembly
                .GetExecutingAssembly()
                .CodeBase)).AbsolutePath)
            ?.Replace("%20",
                " ");



        public static string FilePath(string folder, string fileName)
        {
            return string.Format($"{RunPath}\\{folder}\\{fileName}");
        }
    }
}
