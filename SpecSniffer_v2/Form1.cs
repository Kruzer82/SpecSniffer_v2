using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SpecSniffer_v2
{
    public partial class Form1 : Form
    {
        private Pc pc = new Pc();

        //long initialization
        //   private readonly Camera _capture = new Camera();
        //  private readonly Audio _audio = new Audio();
        //  private readonly Microphone _mic = new Microphone();
        //  Stopwatch stopwatch = new Stopwatch();


       private readonly DriversTab _driversTab = new DriversTab("X:", @"192.168.8.101\new","wb","test");

        public Form1()
        {
            InitializeComponent();

            var hdd = new DiskDrive();

            #region #### Set Spec ####
            //pc.GetManufacturer();
            //pc.GetModel();
            //pc.GetSerial();
            //pc.GetCpu();
            //pc.GetRam();
            //pc.GetDiagonal();
            //pc.GetResName();
            //pc.GetGpu();
            //pc.GetOpticalDrive();
            //pc.GetOsBuild();
            //pc.GetOsLanguages();
            //pc.GetOsLicence();
            //pc.GetOsName();
            //pc.GetBatterHealth();
            //pc.GetBatteryCharge();
            //pc.GetNetAdapters();
            //pc.GetFprPresence();
            //pc.GetDriverStatus();
            //pc.GetCameraPresence();
            //hdd.GetDisks();

            #endregion

            #region #### Display Spec ####

            //ModelTextBox.Text = pc.Model;
            //SerialTextBox.Text = pc.Serial;
            //CpuTextBox.Text = pc.Cpu;
            //RamTextBox.Text = pc.Ram;
            //GpuMultiTextBox.Lines = pc.GpuList.ToArray();
            //OpticalTextBox.Text = pc.OpticalDrive;
            //DiagonalTextBox.Text = pc.Diagonal;
            //ResNameTextBox.Text = pc.ResolutionName;
            //OsBuildTextBox.Text = pc.OsBuild;
            //OsLangTextBox.Text = pc.OsLanguage;
            //BatteryHealthTextBox.Text = pc.BatteryHealth + @"%";
            //PowerLeftTextBox.Text = pc.BatteryCharge + @"%";

            //WlanCheckBox.Checked = pc.Wlan;
            //WwanCheckBox.Checked = pc.Wwan;
            //BluetoothCheckBox.Checked = pc.BlueTooth;
            //CamCheckBox.Checked = pc.Camera;
            //FingerprintCheckBox.Checked = pc.FingerPrint;
            //DriversStatusTextBox.Text = pc.DriverStatus;
            //HddNameMultiTextBox.Lines = hdd.HDDs.Select(d => d.Value.Model).ToArray();
            //HddSizeMultiTextBox.Lines = hdd.HDDs.Select(d => d.Value.Size).ToArray();
            //HddStatusMultiTextBox.Lines = hdd.HDDs.Select(d => d.Value.Status).ToArray();
            //OsBuildTextBox.Text = pc.OsBuild;
            //OsNameTextBox.Text = pc.OsName;
            //OsLangTextBox.Text = pc.OsLanguage;

            #endregion

            _driversTab.ConnectToNetworkDrive();
            Thread.Sleep(100);
           _driversTab.FillListBoxWithFolders(ModelsListBox);
        }

        #region #### Timers ####

        private void ChargeTimer_Tick(object sender, EventArgs e)
        {
           // ChargeRateTextBox.Text = GetSpec.ChargeRate().ToString();
        }
        //audio visualization
        private void SoundTimer_Tick(object sender, EventArgs e)
        {
            //_audio.ProgressBarTick(progressBar1);
        }

        private void MicrophoneTimer_Tick(object sender, EventArgs e)
        {
        //    _mic.ChartTick();
        }

        private void StatusTimer_Tick(object sender, EventArgs e)
        {
            if (_driversTab.IsConnected())
            {
                if (DriversFolderStatusLabel.Text != "Connected")
                {
                    DriversFolderStatusLabel.Text = "Connected";
                    DriversFolderStatusLabel.ForeColor = Color.LimeGreen;
                    _driversTab.FillListBoxWithFolders(ModelsListBox);
                }
            }
            else
            {
                if (DriversFolderStatusLabel.Text != "Disconnected")
                {
                    DriversFolderStatusLabel.Text = "Disconnected";
                    DriversFolderStatusLabel.ForeColor = Color.Maroon;
                    ModelsListBox.Items.Clear();
                }
            }
        }
        #endregion

        #region Diagnostics Tab Events

        private void LcdTest_Click(object sender, EventArgs e)
        {
        }

        private void Camera_Click(object sender, EventArgs e)
        {
            //_capture.StartStopCapture(CamBox);
        }

        private void Audio_Click(object sender, EventArgs e)
        {
            //_audio.StartStopPlay(Resources.FilePath("data", "testsound.wav"),SoundTimer);
        }

        private void Microphone_Click(object sender, EventArgs e)
        {
            //_mic.StartStopRecord(MicChart, MicTimer);
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

        #region Drivers Tab Events

        private void InstallDriversButton_Click(object sender, EventArgs e)
        {
            //  _driversTab.RunFile("Run.bat", ModelsListBox.SelectedItem);
            _driversTab.RunInstallBat(ModelsListBox.SelectedItem.ToString());
        }

        private void RunFileButton_Click(object sender, EventArgs e)
        {
            _driversTab.RunFile(ModelsListBox.SelectedItem, FilesListBox.SelectedItem);
        }


        private void ModelsListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            _driversTab.FillListBoxWithFilesFromFolder(FilesListBox, ModelsListBox.SelectedItem);
        }

        #endregion

        #region Save Tab Events

        #endregion

        #region Global Tab Events

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _driversTab.RemoveNetworkDrive();
        }

        #endregion

        







    }
}
