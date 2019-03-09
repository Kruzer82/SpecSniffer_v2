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
        private string _sharePath;
        public string NetDrive { get; protected set; }

        public string SharePath
        {
            get => _sharePath.Replace(@"\\",@"\");
            protected set => _sharePath = value;
        }

        public void ConnectToNetworkDrive(string userName, string userPassword)
        {
            Process.Start("net.exe", $"use {NetDrive} \\\\{SharePath} /u:{userName} {userPassword} ");
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
            try
            {
                return Directory.GetDirectories(NetDrive).Select(folder => new DirectoryInfo(folder).Name);
            }
            catch (DirectoryNotFoundException)
            {
                return NoDataToReturn();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return NoDataToReturn();
            }
        }

        public IEnumerable<string> ListOfFiles(string folderDirectory)
        {
            try
            {
                return Directory.GetFiles(folderDirectory).Select(file => new DirectoryInfo(file).Name);
            }
            catch (DirectoryNotFoundException)
            {
                return NoDataToReturn();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return NoDataToReturn();
            }
        }

        public void RunFile(string fileName)
        {
            try
            {
                Process.Start(NetDrive + "\\" + fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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


        private static IEnumerable<string> NoDataToReturn()
        {
            List<string> noDir = new List<string> { "Directory Not Found" };
            return noDir;
        }
    }
}
