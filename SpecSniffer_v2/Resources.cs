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

        public static readonly List<double> DiagonalList = new List<double>
        {
            10.1,
            11.6,
            12,
            12.5,
            14,
            15.6,
            17.3,
            18,
            19,
            20,
            20.1,
            21,
            21.3,
            22,
            22.2,
            23,
            24,
            26,
            27
        };

        public static readonly Dictionary<string,string> ResolutionNames = new Dictionary<string, string>()
        {
            {"1280x1024", "SXGA"},
            {"1360x768", "HD"},
            {"1366x768", "HD"},
            {"1600x900", "HD+"},
            {"1920x1080", "FHD"},
            {"1280x800", "WXGA"},
            {"1280x768", "WXGA"},
            {"1280x720", "WXGA"},
            {"1440x900", "WXGA"},
            {"1680x1050", "WSXGA"},
            {"1920x1200", "WUXGA"},
            {"1152x864", "XGA+"},
            {"1024x768", "XGA"},
            {"1024x600", "WSVGA"},
            {"800x600", "SVGA"},
            {"2560x1440", "WQHD"},
            {"3840x2160", "UHD"},
            {"4096x2160", "UHD"},
            {"2560×1600", "WQXGA"}
        };



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
