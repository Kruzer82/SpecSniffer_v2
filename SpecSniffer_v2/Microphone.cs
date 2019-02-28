using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using NAudio.Wave;

namespace SpecSniffer_v2
{
    public class Microphone
    {
        private Chart _chart;
        private Timer _micTimer;
        private const int XAxis = 4000; //number of x-axis pints
        private WaveIn _waveIn;
        private Queue<double> _myQueue;
        private bool _isRecording;


        public Microphone()
        {
            _chart = null;
            _micTimer = null;
            _waveIn = null;
            _myQueue = null;
            _isRecording = false;
        }

        public void StartStopRecord(Chart chart,Timer timer)
        {
            _chart = chart;
            _micTimer = timer;

            if (_isRecording)
            {
                _micTimer.Enabled = false;
                
                StopRecord();
            }
            else
            {
                _micTimer.Enabled = true;
                
                StartRecord();
            }
        }

        private void StartRecord()
        {
                _myQueue = new Queue<double>(Enumerable.Repeat(0.0, XAxis).ToList()); // fill myQueue w/ zeros
                _chart.ChartAreas[0].AxisY.Minimum = -10000;
                _chart.ChartAreas[0].AxisY.Maximum = 10000;
                _waveIn = new WaveIn();

                try
                {
                    _waveIn.StartRecording();
                    _waveIn.WaveFormat = new WaveFormat(4, 16, 1); // (44100, 16, 1);
                    _waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(waveIn_DataAvailable);
                    _isRecording = true;
                }
                catch (Exception)
                {
                    // ignored
                }
        }

        private void StopRecord()
        {
            _waveIn.StopRecording();
            _isRecording = false;
        }

        private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            for (int i = 0; i < e.BytesRecorded; i += 2)
            {
                _myQueue.Enqueue(BitConverter.ToInt16(e.Buffer, i));
                _myQueue.Dequeue();
            }
        }

        public void ChartTick()
        {
            try { _chart.Series["Series1"].Points.DataBindY(_myQueue); }
            catch { MessageBox.Show("No bytes recorded"); }
        }

    }
}
