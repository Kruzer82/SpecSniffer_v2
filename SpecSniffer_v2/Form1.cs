using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace SpecSniffer_v2
{

    public partial class Form1 : Form
    {
        //long initialization
        private readonly Camera _capture = new Camera();
        private readonly Audio _audio = new Audio();
        Microphone _mic=new Microphone();
        public Form1()
        {
            InitializeComponent();

            var pc = new Pc();
            var hdd = new DiskDrive();

            #region set spec

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

            

        }

        #region Microphone
        //int n = 4000; //number of x-axis pints
        //WaveIn wi;
        //Queue<double> myQ;
        //private bool micStart = true;
        //private void MicStart()
        //{
        //    if (micStart == true)
        //    {
        //        MicTimer.Enabled = true;
        //        myQ = new Queue<double>(Enumerable.Repeat(0.0, n).ToList()); // fill myQ w/ zeros
        //        MicChart.ChartAreas[0].AxisY.Minimum = -10000;
        //        MicChart.ChartAreas[0].AxisY.Maximum = 10000;
        //        wi = new WaveIn();
        //        try
        //        {
        //            wi.StartRecording();
        //            wi.WaveFormat = new WaveFormat(44100, 16, 1); // (44100, 16, 1);
        //            wi.DataAvailable += new EventHandler<WaveInEventArgs>(wi_DataAvailable);

        //        }
        //        catch (NAudio.MmException) { MicChart.Visible = false; }
        //        catch (Exception) { }

        //        void wi_DataAvailable(object sender, WaveInEventArgs e)
        //        {
        //            for (int i = 0; i < e.BytesRecorded; i += 2)
        //            {
        //                myQ.Enqueue(BitConverter.ToInt16(e.Buffer, i));
        //                myQ.Dequeue();
        //            }
        //        }
        //        micStart = false;
        //    }
        //    else
        //    {
        //        MicTimer.Enabled = false;
        //        MicStop();
        //        micStart = true;
        //    }
        //}

        ////private void MicrophoneTimer_Tick(object sender, EventArgs e)
        ////{
        ////    try { MicChart.Series["Series1"].Points.DataBindY(myQ); }
        ////    catch { MessageBox.Show("No bytes recorded"); }
        ////}

        //private void MicStop()
        //{
        //    wi.StopRecording();
        //    wi.Dispose();
        //    wi = null;
        //}

        //private void btnMic_Click(object sender, EventArgs e)
        //{
        //    MicStart();

        //}
        #endregion



        //audio visualization
        private void SoundTimer_Tick(object sender, EventArgs e)
        {
            _audio.ProgressBarTick(progressBar1);
        }
        private void MicrophoneTimer_Tick(object sender, EventArgs e)
        {
            _mic.ChartTick();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            _capture.StartStopCapture(CamBox);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _audio.StartStopPlay(Resources.FilePath("data", "testsound.wav"));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _mic.StartStopRecord(MicChart,MicTimer);
        }
    }
}
