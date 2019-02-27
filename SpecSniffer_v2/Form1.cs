using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using NAudio.CoreAudioApi;

namespace SpecSniffer_v2
{

    public partial class Form1 : Form
    {
        //long initialization
        // private readonly Camera capture = new Camera();
        Audio audioTest = new Audio(Resources.FilePath("Data", "testsound.wav"));
        public Form1()
        {
            InitializeComponent();

            Pc pc = new Pc();
            DiskDrive hdd=new DiskDrive();

            #region set spec

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
            hdd.GetDisks();

            #endregion

            
            #region show spec

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
            HddNameMultiTextBox.Lines = hdd.HDDs.Select(d => d.Value.Model).ToArray();
            HddSizeMultiTextBox.Lines = hdd.HDDs.Select(d => d.Value.Size).ToArray();
            HddStatusMultiTextBox.Lines = hdd.HDDs.Select(d => d.Value.Status).ToArray();
            OsBuildTextBox.Text = pc.OsBuild;
            OsNameTextBox.Text = pc.OsName;
            OsLangTextBox.Text = pc.OsLanguage;


            #endregion




            audioTest.PlayStop();
        }


        private void SoundTimer_Tick(object sender, EventArgs e)
        {
            audioTest.ProgressBarValue_Tick(progressBar1);
        }
    }
}
