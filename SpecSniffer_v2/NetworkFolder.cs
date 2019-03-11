using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace SpecSniffer_v2
{
    internal class NetworkFolder
    {


        private string _sharePath;

        public string User { get; protected set; }
        public string Password { get; protected set; }
        public string NetDrive { get; protected set; }
        public string SharePath
        {
            get => _sharePath.Replace(@"\\",@"\");
            protected set => _sharePath = value;
        }


        public void ConnectToNetworkDrive()
        {
            Process.Start("net.exe", $"use {NetDrive} \\\\{SharePath} /u:{User} {Password} ");
        }

        public static void RemoveNetworkDrive(string driveLetter)
        {
            Process.Start("net.exe", $@"use {driveLetter} /delete");
        }

        public static bool IsNetworkDriveOn(string driveLetter)
        {
            return Directory.Exists(driveLetter);
        }

        public static bool IsNetworkOn()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                    return true;
            }
            catch { return false; }
        }

        public IEnumerable<string> GetListOfFolders()
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

        public IEnumerable<string> ListOfFiles(string folderPath)
        {
            try
            {
                return Directory.GetFiles(folderPath).Select(file => new DirectoryInfo(file).Name);
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

        public void RunFile( object folderName, object fileName)
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
