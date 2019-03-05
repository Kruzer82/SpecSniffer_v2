using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecSniffer_v2
{
    class NetworkFolder
    {

        private string _driveLetter;
        private string _sharePath;
        private string _userName;
        private string _userPassword;

        public NetworkFolder(string driveLetter, string sharePath, string userName, string userPassword)
        {
            _driveLetter = driveLetter+":";
            _sharePath = sharePath;
            _userName = userName;
            _userPassword = userPassword;
        }


        public void ConnectToNetworkDrive()
        {
            System.Diagnostics.Process.Start("net.exe",
                $@"use {_driveLetter} \\{_sharePath} /u:{_userName} {_userPassword}");
        }

        public void RemoveNetworkDrive()
        {
            System.Diagnostics.Process.Start("net.exe", $@"use {_driveLetter} /delete");
        }

        public bool IsConnected()
        {
            return Directory.Exists(_driveLetter) ? true : false;
        }

        public void FoldersList()
        {

        }

        public void FilesList()
        {

        }

        public void RunFile()
        {

        }
    }
}
