using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpecSniffer_v2
{
    internal class DriversTab : NetworkFolder
    {
        public DriversTab(string netDrive,string sharePath)
        {
            NetDrive = netDrive;
            SharePath = sharePath;
        }


        public void FillListBoxWithFolders(ListBox listBox)
        {
            foreach (var model in ListOfFolders())
                listBox.Items.Add(model);
        }

        public void FillListBoxWithFilesFromFolder(ListBox listBox, object folderName)
        {
            listBox.Items.Clear();

            foreach (var file in ListOfFiles(NetDrive + "\\" + folderName))
            {
                if (file!="Run.bat")
                {
                    listBox.Items.Add(file);
                }
            }
        }
    }


}
