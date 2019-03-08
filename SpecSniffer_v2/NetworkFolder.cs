using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SpecSniffer_v2
{
    internal class NetworkFolder
    {
        public string NetDrive { get; protected set; }
        public string SharePath { get; protected set; }

        public void ConnectToNetworkDrive(string userName, string userPassword)
        {
            Process.Start("net.exe", $@"use {NetDrive} \\{SharePath} /u:{userName} {userPassword}");
        }

        public void RemoveNetworkDrive()
        {
            Process.Start("net.exe", $@"use {NetDrive} /delete");
        }

        public bool IsConnectedToDrive()
        {
            return Directory.Exists(NetDrive) ? true : false;
        }

        public IEnumerable<string> ListOfFolders()
        {
            return Directory.GetDirectories(NetDrive).Select(folder => new DirectoryInfo(folder).Name);
        }

        public IEnumerable<string> ListOfFiles(string folderDirectory)
        {
            return Directory.GetFiles(folderDirectory).Select(file => new DirectoryInfo(file).Name);
        }

        public void RunFile(string fileName)
        {
            Process.Start(NetDrive+"\\"+fileName);
        }

        public void RunFile(object fileName,object folderName)
        {
            try
            {
                Process.Start(NetDrive + "\\" + folderName + "\\" + fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
