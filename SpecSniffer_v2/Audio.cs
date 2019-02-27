using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.CoreAudioApi;

namespace SpecSniffer_v2
{
    class Audio
    {
        private NAudio.Wave.WaveFileReader _wave = null;
        private NAudio.Wave.DirectSoundOut _output = null;
        private bool _isPlaying = false;
        private MMDevice _device = null;
        private string _soundFilePath;




        public Audio(string soundFilePath)
        {
            this._soundFilePath = soundFilePath;

        }


        public void PlayStop()
        {
            if (_isPlaying == false)
            {
                Play();
            }
            else
            {
                Stop();
            }
        }

        private void Play()
        {
            try
            {
                _wave = new NAudio.Wave.WaveFileReader(_soundFilePath);
                _output = new NAudio.Wave.DirectSoundOut();
                _output.Init(new NAudio.Wave.WaveChannel32(_wave));
                _output.Play();
                _isPlaying = true;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void Stop()
        {
            _output.Stop();
        }

        public void ProgressBarValue_Tick(ProgressBar progressBarName)
        {
            _device = GetDefaultAudioEndpoint();

            progressBarName.Value = (int)(Math.Round(_device.AudioMeterInformation.MasterPeakValue * 100));


        }
        private MMDevice GetDefaultAudioEndpoint()
        {
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            return enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
        }
    }
}
