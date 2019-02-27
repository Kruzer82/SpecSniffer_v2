using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using SpecSniffer_v2.Properties;

namespace SpecSniffer_v2
{

    public partial class Form1 : Form
    {
        //long initialization
     // private readonly Camera capture = new Camera();

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


        private NAudio.Wave.WaveFileReader wave = null;
        private NAudio.Wave.DirectSoundOut output = null;
        private bool audioPlay = true;
        private void AudioPlay()
        {
            if (audioPlay == true)
            {
                SoundTimer.Enabled = true;
                MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
                var devices = enumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);
                wave = new NAudio.Wave.WaveFileReader();
                output = new NAudio.Wave.DirectSoundOut();
                output.Init(new NAudio.Wave.WaveChannel32(wave));
                try
                {
                    output.Play();
                    audioPlay = false;
                }
                catch (Exception) {  }
            }
            else
            {
                AudioStop();
                audioPlay = true;
            }
        }
        MMDevice device = null;

        private void AudioStop()
        {
            output.Stop();
            output.Dispose();
            SoundTimer.Enabled = false;
        }
        private void timerAudio_Tick_1(object sender, EventArgs e)
        {
            device = GetDefaultAudioEndpoint();

            if (device != null)
                progressBar1.Value = (int)(Math.Round(device.AudioMeterInformation.MasterPeakValue * 100));
            else
            {
                output.Stop();
                output.Dispose();
                SoundTimer.Enabled = false;
                progressBar1.Value = 0;
            }
        }
        public MMDevice GetDefaultAudioEndpoint()
        {
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            return enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
        }

    }
}
