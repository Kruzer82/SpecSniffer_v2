using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpecSniffer_v2
{
    internal class DriversTab : NetworkFolder
    {
        public DriversTab(string netDrive,string sharePath, string user,string password)
        {
            NetDrive = netDrive;
            SharePath = sharePath;
            User = user;
            Password = password;
        }


        public void FillListBoxWithFolders(ListBox listBox)
        {
            listBox.Items.Clear();
            foreach (var model in GetListOfFolders())
                listBox.Items.Add(model);
        }

        public void FillListBoxWithFilesFromFolder(ListBox listBox, object folderName)
        {
            listBox.Items.Clear();
                foreach (var file in ListOfFiles(NetDrive + "\\" + folderName))
                if (file!="Run.bat")
                    listBox.Items.Add(file);
        }

        public void RunInstallBat(string folder)
        {
            if (!File.Exists(NetDrive +"\\"+ folder+ "\\InstallDrivers.bat"))
            {
                CopyInstallBat(NetDrive + "\\" + folder + "\\InstallDrivers.bat");
            }

            try
            {
                Process.Start(NetDrive + "\\" + folder + "\\InstallDrivers.bat");

            }
            catch (System.ComponentModel.Win32Exception)
            {
                //ignored
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private  void CopyInstallBat(string path)
        {
            if (!File.Exists("Data\\InstallDrivers.bat"))
            {
                MessageBox.Show("Missing crucial files in drivers folder.\nPlug in USB and click OK to copy it now.");
                Thread.Sleep(500);
            }

            try
            {
                File.Copy("Data\\InstallDrivers.bat", path);
                MessageBox.Show("Crucial file has been copied.\nClick ok to install drivers.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while trying copy crucial file.\n"+ex.Message);
            }
           
        }

    }


}
