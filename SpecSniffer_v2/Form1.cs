﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Management;

namespace SpecSniffer_v2
{
    public partial class Form1 : Form
    {
        private readonly Spec _spec = new Spec();
        private DriversTab _driversTab;
        private readonly DiskDrive _hdd=new DiskDrive();

        //long initialization
        private  Camera _capture;
        //  private readonly Audio _audio = new Audio();
        //  private readonly Microphone _mic = new Microphone();
        //  Stopwatch stopwatch = new Stopwatch();

        private void CallCamera()
        {
            _capture =  new Camera();
        }

        public Form1()
        {
            InitializeComponent();
           // CallCamera();

            //ThreadStart childref = new ThreadStart(CallCamera);
            //Thread childThread = new Thread(childref);
            //childThread.Start();
            #region #### Set Spec ####
            //_spec.GetManufacturer();
            //_spec.GetModel();
            //_spec.GetSerial();
            //_spec.GetCpu();
            //_spec.GetRam();
            //_spec.GetDiagonal();
            //_spec.GetResName();
            //_spec.GetGpu();
            //_spec.GetOpticalDrive();
            //_spec.GetOsBuild();
            //_spec.GetOsLanguages();
            //_spec.GetOsLicence();
            //_spec.GetOsName();
            //_spec.GetBatterHealth();
            //_spec.GetBatteryCharge();
            //_spec.GetNetAdapters();
            //_spec.GetFprPresence();
            //_spec.GetDriverStatus();
            //_spec.GetCameraPresence();
            //hdd.GetDisks();

            #endregion

            #region #### Display Spec ####

            //ModelTextBox.Text = _spec.Model;
            //SerialTextBox.Text = _spec.Serial;
            //CpuTextBox.Text = _spec.Cpu;
            //RamTextBox.Text = _spec.Ram;
            //GpuMultiTextBox.Lines = _spec.GpuList.ToArray();
            //OpticalTextBox.Text = _spec.OpticalDrive;
            //DiagonalTextBox.Text = _spec.Diagonal;
            //ResNameTextBox.Text = _spec.ResolutionName;
            //OsBuildTextBox.Text = _spec.OsBuild;
            //OsLangTextBox.Text = _spec.OsLanguage;
            //BatteryHealthTextBox.Text = _spec.BatteryHealth + @"%";
            //PowerLeftTextBox.Text = _spec.BatteryCharge + @"%";

            //WlanCheckBox.Checked = _spec.Wlan;
            //WwanCheckBox.Checked = _spec.Wwan;
            //BluetoothCheckBox.Checked = _spec.BlueTooth;
            //CamCheckBox.Checked = _spec.Camera;
            //FingerprintCheckBox.Checked = _spec.FingerPrint;
            //DriversStatusTextBox.Text = _spec.DriverStatus;
            //HddNameMultiTextBox.Lines = hdd.HDDs.Select(d => d.Value.Model).ToArray();
            //HddSizeMultiTextBox.Lines = hdd.HDDs.Select(d => d.Value.Size).ToArray();
            //HddStatusMultiTextBox.Lines = hdd.HDDs.Select(d => d.Value.Status).ToArray();
            //OsBuildTextBox.Text = _spec.OsBuild;
            //OsNameTextBox.Text = _spec.OsName;
            //OsLangTextBox.Text = _spec.OsLanguage;

            #endregion
            SpecBackgroundWorker.RunWorkerAsync();
        }

        #region #### Timers ####

        private void ChargeTimer_Tick(object sender, EventArgs e)
        {
            _spec.GetBatteryCharge();
            _spec.GetChargeRate();
            BatteryChargeTextBox.Text = _spec.BatteryCharge + @"%";
            ChargeRateTextBox.Text = _spec.BatteryChargeRate.ToString();

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
            if (NetworkFolder.IsNetworkDriveOn("x:"))
            {
                if (DriversFolderStatusLabel.Text != "Connected")
                {
                    DriversFolderStatusLabel.Text = "Connected";
                    DriversFolderStatusLabel.ForeColor = Color.LimeGreen;
                }
            }
            else
            {
                if (DriversFolderStatusLabel.Text != "Disconnected")
                {
                    DriversFolderStatusLabel.Text = "Disconnected";
                    DriversFolderStatusLabel.ForeColor = Color.Maroon;
                    DriversModelsListBox.Items.Clear();
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
            _capture?.StartStopCapture(CamBox);
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
            _driversTab.RunInstallBat(DriversModelsListBox.SelectedItem.ToString());
        }

        private void RunFileButton_Click(object sender, EventArgs e)
        {
            _driversTab.RunFile(DriversModelsListBox.SelectedItem, DriversFilesListBox.SelectedItem);
        }


        private void ModelsListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            _driversTab.FillListBoxWithFilesFromFolder(DriversFilesListBox, DriversModelsListBox.SelectedItem);
        }

        #endregion

        #region Save Tab Events

        #endregion

        #region Global Tab Events

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            NetworkFolder.RemoveNetworkDrive("x:");
        }

        #endregion

        #region Background Workers

        private void SpecBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _spec.GetManufacturer();
            SpecBackgroundWorker.ReportProgress(0);
            _spec.GetModel();
            SpecBackgroundWorker.ReportProgress(1);
            _spec.GetSerial();
            SpecBackgroundWorker.ReportProgress(2);
            _spec.GetCpu();
            SpecBackgroundWorker.ReportProgress(3);
            _spec.GetRam();
            SpecBackgroundWorker.ReportProgress(4);
            _spec.GetDiagonal();
            SpecBackgroundWorker.ReportProgress(5);
            _spec.GetResName();
            SpecBackgroundWorker.ReportProgress(6);
            _spec.GetGpu();
            SpecBackgroundWorker.ReportProgress(7);
            _spec.GetOpticalDrive();
            SpecBackgroundWorker.ReportProgress(8);
            _spec.GetOsBuild();
            SpecBackgroundWorker.ReportProgress(9);
            _spec.GetOsLanguages();
            SpecBackgroundWorker.ReportProgress(10);
            _spec.GetOsLicence();
            SpecBackgroundWorker.ReportProgress(11);
            _spec.GetOsName();
            SpecBackgroundWorker.ReportProgress(12);
            _spec.GetBatterHealth();
            SpecBackgroundWorker.ReportProgress(13);
            _spec.GetCameraPresence();
            SpecBackgroundWorker.ReportProgress(14);
            _spec.GetNetAdapters();
            SpecBackgroundWorker.ReportProgress(15);
            _spec.GetFprPresence();
            SpecBackgroundWorker.ReportProgress(16);
            _spec.GetDriverStatus();
            SpecBackgroundWorker.ReportProgress(17);
            _hdd.GetDisks();
            SpecBackgroundWorker.ReportProgress(18);
        }

        private void SpecBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 0:
                    //manufacturer
                    break;
                case 1:
                    ModelTextBox.Text = _spec.Model;
                    break;
                case 2:
                    SerialTextBox.Text = _spec.Serial;
                    break;
                case 3:
                    CpuTextBox.Text = _spec.Cpu;
                    break;
                case 4:
                    RamTextBox.Text = _spec.Ram;
                    break;
                case 5:
                    DiagonalTextBox.Text = _spec.Diagonal;
                    break;
                case 6:
                    ResNameTextBox.Text = _spec.ResolutionName;
                    break;
                case 7:
                    GpuMultiTextBox.Lines = _spec.GpuList.ToArray();
                    break;
                case 8:
                    OpticalTextBox.Text = _spec.OpticalDrive;
                    break;
                case 9:
                    OsBuildTextBox.Text = _spec.OsBuild;
                    break;
                case 10:
                    OsLangTextBox.Text = _spec.OsLanguage;
                    break;
                case 11:
                    //os licence
                    break;
                case 12:
                    OsNameTextBox.Text = _spec.OsName;
                    break;
                case 13:
                    BatteryHealthTextBox.Text = _spec.BatteryHealth + @"%";
                    break;
                case 14:
                    CamCheckBox.Checked = _spec.Camera;
                    break;
                case 15:
                    WlanCheckBox.Checked = _spec.Wlan;
                    WwanCheckBox.Checked = _spec.Wwan;
                    BluetoothCheckBox.Checked = _spec.BlueTooth;
                    break;
                case 16:
                    FingerprintCheckBox.Checked = _spec.FingerPrint;
                    break;
                case 17:
                    DriversStatusTextBox.Text = _spec.DriverStatus;
                    break;
                case 18:
                    HddNameMultiTextBox.Lines = _hdd.HDDs.Select(d => d.Value.Model).ToArray();
                    HddSizeMultiTextBox.Lines = _hdd.HDDs.Select(d => d.Value.Size).ToArray();
                    HddStatusMultiTextBox.Lines = _hdd.HDDs.Select(d => d.Value.Status).ToArray();
                    break;

                default:
                    break;
            }
        }

        private void SpecBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ModelTextBox.Enabled = true;
            SerialTextBox.Enabled = true;
            CpuTextBox.Enabled = true;
            RamTextBox.Enabled = true;
            DiagonalTextBox.Enabled = true;
            ResNameTextBox.Enabled = true;
            GpuMultiTextBox.Enabled = true;
            OpticalTextBox.Enabled = true;
            OsBuildTextBox.Enabled = true;
            OsLangTextBox.Enabled = true;
            OsNameTextBox.Enabled = true;
            BatteryHealthTextBox.Enabled = true;
            CamCheckBox.Enabled = true;
            WlanCheckBox.Enabled = true;
            WwanCheckBox.Enabled = true;
            BluetoothCheckBox.Enabled = true;
            FingerprintCheckBox.Enabled = true;
            DriversStatusTextBox.Enabled = true;
            HddNameMultiTextBox.Enabled = true;
            HddSizeMultiTextBox.Enabled = true;
            HddStatusMultiTextBox.Enabled = true;
        }

        private void DriversBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _driversTab = new DriversTab("X:", @"192.168.8.101\new", "wb", "test");
            _driversTab.ConnectToNetworkDrive();
            //giving NetUse time to create connection
            Thread.Sleep(100);
        }

        private void DriversBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!NetworkFolder.IsNetworkDriveOn("X:")) return;

            DriversInstallButton.Enabled = true;
            DriversRunFileButton.Enabled = true;
            DriversModelsListBox.Enabled = true;
            DriversFilesListBox.Enabled = true;

            _driversTab.FillListBoxWithFolders(DriversModelsListBox);
        }




        #endregion

       
    }
}
