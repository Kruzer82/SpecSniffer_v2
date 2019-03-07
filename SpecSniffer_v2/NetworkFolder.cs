using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecSniffer_v2
{
    class NetworkFolder
    {
        private readonly string _driveLetter;


        public NetworkFolder(string driveLetter)
        {
            _driveLetter = driveLetter+":";
        }


        public void ConnectToNetworkDrive(string sharePath, string userName, string userPassword)
        {
            Process.Start("net.exe",
                $@"use {_driveLetter} \\{sharePath} /u:{userName} {userPassword}");
        }

        public void RemoveNetworkDrive()
        {
            Process.Start("net.exe", $@"use {_driveLetter} /delete");
        }

        public bool IsConnected()
        {
            return Directory.Exists(_driveLetter) ? true : false;
        }

        public IEnumerable<string> ListOfFolders(string folderDirectory)
        {
            return Directory.GetDirectories(folderDirectory)
                .Select(d => new DirectoryInfo(d).Name);
        }

        public IEnumerable<string> ListOfFiles(string folderDirectory)
        {
            return Directory.GetFiles(folderDirectory);
            //var filenames4 = Directory
            //    .EnumerateFiles(folderDirectory, "*", SearchOption.AllDirectories)
            //    .Select(Path.GetFileName); // <-- note you can shorten the lambda
            //return filenames4;
        }

        public void RunFile(string filePath)
        {
            Process.Start(filePath);
        }


        private IEnumerable<string> RemoveDriveLetter(IEnumerable<string> arr)
        {
            return arr.Select(s => s.Remove(0, 2));
        }
    }
}
