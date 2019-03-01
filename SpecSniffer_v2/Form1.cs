using System;
using System.Linq;
using System.Windows.Forms;

namespace SpecSniffer_v2
{
    public partial class Form1 : Form
    {
        //long initialization
        private readonly Camera _capture = new Camera();
        private readonly Audio _audio = new Audio();
        private readonly Microphone _mic = new Microphone();

        public Form1()
        {
            InitializeComponent();

            var pc = new Pc();
            var hdd = new DiskDrive();

            #region #### Set Spec ####

            pc.Manufacturer = GetSpec.Manufacturer();
            pc.Model = GetSpec.Model();
            pc.Serial = GetSpec.Serial();
            pc.Cpu = GetSpec.Cpu();
            pc.Ram = GetSpec.Ram();
            pc.GpuList = GetSpec.Gpu();
            pc.OpticalDrive = GetSpec.OpticalDrive();
            pc.Diagonal = GetSpec.Diagonal();
            pc.ResolutionName = GetSpec.ResName();
            pc.OsName = GetSpec.OsName();
            pc.OsBuild = GetSpec.OsBuild();
            pc.OsLanguage = GetSpec.OsLanguages();
            pc.OsLicenceKey = GetSpec.OsLicence();
            pc.BatteryCharge = GetSpec.BatteryCharge();
            pc.BatteryHealth = GetSpec.BatterHealth();
            pc.BatteryChargeRate = GetSpec.ChargeRate();
            pc.Wwan = GetSpec.WwanPresence();
            pc.BlueTooth = GetSpec.BluetoothPresence();
            pc.FingerPrint = GetSpec.FprPresence();
            pc.DriverStatus = GetSpec.DriverStatus();
            hdd.GetDisks();

            #endregion

            #region #### Display Spec ####

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
            BatteryHealthTextBox.Text = pc.BatteryHealth + @"%";
            PowerLeftTextBox.Text = pc.BatteryCharge + @"%";
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
        }

        #region #### Timer Events ####

        //audio visualization
        private void SoundTimer_Tick(object sender, EventArgs e)
        {
            _audio.ProgressBarTick(progressBar1);
        }

        private void MicrophoneTimer_Tick(object sender, EventArgs e)
        {
            _mic.ChartTick();
        }

        #endregion

        #region #### Button Events ####

        private void LcdTest_Click(object sender, EventArgs e)
        {
        
        }

        private void Camera_Click(object sender, EventArgs e)
        {
               _capture.StartStopCapture(CamBox);
            
        }

        private void Audio_Click(object sender, EventArgs e)
        {
            _audio.StartStopPlay(Resources.FilePath("data", "testsound.wav"));
        }

        private void Microphone_Click(object sender, EventArgs e)
        {
            _mic.StartStopRecord(MicChart, MicTimer);
        }

        private void Keyboard_Click(object sender, EventArgs e)
        {
        }

        private void HdTune_Click(object sender, EventArgs e)
        {
        }

        private void ShowKey_Click(object sender, EventArgs e)
        {
        }
        #endregion
    }
}
