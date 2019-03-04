using NAudio.CoreAudioApi;
using System;
using System.Windows.Forms;
using NAudio.Wave;

namespace SpecSniffer_v2
{
    public class Audio
    {
        private WaveFileReader _wave;
        private DirectSoundOut _output;
        private bool _isPlaying;
        private MMDevice _device;

        public Audio()
        {
            _wave = null;
            _output = null;
            _isPlaying = false;
            _device = null;
        }

        public void StartStopPlay(string soundFilePath, Timer soundTimer)
        {
            if (_isPlaying == false)
            {
                soundTimer.Enabled = true;
                Start(soundFilePath);
            }
            else
            {
                soundTimer.Enabled = false;
                Stop();
            }
        }

        private void Start(string soundFilePath)
        {
            try
            {
                _wave = new WaveFileReader(soundFilePath);
                _output = new DirectSoundOut();
                _output.Init(new WaveChannel32(_wave));
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
            _isPlaying = false;
            _output.Stop();
        }

        public void ProgressBarTick(ProgressBar progressBarName)
        {
            _device = GetDefaultAudioEndpoint();

            progressBarName.Value = (int) (Math.Round(_device.AudioMeterInformation.MasterPeakValue * 100));
        }

        private static MMDevice GetDefaultAudioEndpoint()
        {
            var enumerator = new MMDeviceEnumerator();
            return enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
        }
    }
}
