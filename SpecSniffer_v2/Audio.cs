﻿using NAudio.CoreAudioApi;
using System;
using System.Windows.Forms;

namespace SpecSniffer_v2
{
    class Audio
    {
        private NAudio.Wave.WaveFileReader _wave;
        private NAudio.Wave.DirectSoundOut _output;
        private bool _isPlaying;
        private MMDevice _device;
        private string _soundFilePath;




        public Audio(string soundFilePath)
        {
            _soundFilePath = soundFilePath;

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
