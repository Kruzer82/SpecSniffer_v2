using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;

namespace SpecSniffer_v2
{

    public partial class Form1 : Form
    {
        //long initialization
      private readonly Camera capture = new Camera();

        public Form1()
        {
            InitializeComponent();
            Stopwatch timer = new Stopwatch();
            Pc pc = new Pc();
            DiskDrive hdd=new DiskDrive();
            pc.Manufacturer = GetSpec.Manufacturer();
            pc.Model = GetSpec.Model();
            pc.Serial = GetSpec.Serial();
            pc.Cpu = GetSpec.Cpu();
            pc.Ram = GetSpec.Ram();
            pc.GpuList = GetSpec.Gpu();
            pc.OpticalDrive = GetSpec.OpticalDrive();
            pc.Diagonal= GetSpec.Diagonal();
            pc.ResolutionName = GetSpec.ResName();
            pc.OsName = GetSpec.OsName();
            pc.OsBuild = GetSpec.OsBuild();
            pc.OsLanguage = GetSpec.OsLanguages();
            pc.OsLicenceKey= GetSpec.OsLicence();
            pc.BatteryCharge = GetSpec.BatteryCharge();
            pc.BatteryHealth = GetSpec.BatterHealth();
            pc.BatteryChargeRate = GetSpec.ChargeRate();
            pc.Wwan = GetSpec.WwanPresence();
            pc.BlueTooth = GetSpec.BluetoothPresence();
            pc.FingerPrint = GetSpec.FprPresence();
            pc.DriverStatus = GetSpec.DriverStatus();


            ModelTextBox.Text = pc.Model;
            SerialTextBox.Text = pc.Serial;
            CpuTextBox.Text = pc.Cpu;
            RamTextBox.Text = pc.Ram;
            GpuMultiTextBox.Lines = pc.GpuList.ToArray();
            OpticalTextBox.Text = pc.OpticalDrive;
            DiagonalTextBox.Text = pc.Diagonal;
            ResNameTextBox.Text = pc.ResolutionName;
            OsBuildTextBox.Text = pc.OsBuild;
            OsLangTextBox.Text = pc.OsLanguage;
            BatteryHealthTextBox.Text = pc.BatteryHealth + "%";
            PowerLeftTextBox.Text = pc.BatteryCharge + "%";
            ChargeRateTextBox.Text = pc.BatteryChargeRate.ToString();
            WlanCheckBox.Checked = pc.Wlan;
            WwanCheckBox.Checked = pc.Wwan;
            BluetoothCheckBox.Checked = pc.BlueTooth;
            CamCheckBox.Checked = pc.Camera;
            FingerprintCheckBox.Checked = pc.FingerPrint;
            DriversStatusTextBox.Text = pc.DriverStatus;


            //timer.Start();
            hdd.GetDisks();
            HddNameMultiTextBox.Lines = hdd.HDDs.Select(d => d.Value.Model).ToArray();
            HddSizeMultiTextBox.Lines = hdd.HDDs.Select(d => d.Value.Size).ToArray();
            HddStatusMultiTextBox.Lines = hdd.HDDs.Select(d => d.Value.Status).ToArray();
           // timer.Stop();
            //MessageBox.Show("Hdd load time:"+timer.ElapsedMilliseconds);
            OsBuildTextBox.Text = pc.OsBuild;
            OsNameTextBox.Text = pc.OsName;
            OsLangTextBox.Text = pc.OsLanguage;
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            capture.StartCapture(CamBox);


        }

        private void button3_Click(object sender, EventArgs e)
        {
        }
    }
}
