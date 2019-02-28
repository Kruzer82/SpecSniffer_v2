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
    class Microphone
    {
        private Chart _chart;
        private Timer _micTimer;
        private int xAxis = 4000; //number of x-axis pints
        private WaveIn waveIn;
        private Queue<double> myQueue;
        private bool isRecording;


        public Microphone()
        {
            _chart = null;
            _micTimer = null;
            waveIn = null;
            myQueue = null;
            isRecording = false;
        }

        public void StartStopRecord(Chart chart,Timer timer)
        {
            _chart = chart;
            _micTimer = timer;

            if (isRecording)
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
                myQueue = new Queue<double>(Enumerable.Repeat(0.0, xAxis).ToList()); // fill myQueue w/ zeros
                _chart.ChartAreas[0].AxisY.Minimum = -10000;
                _chart.ChartAreas[0].AxisY.Maximum = 10000;
                waveIn = new WaveIn();

                try
                {
                    waveIn.StartRecording();
                    waveIn.WaveFormat = new WaveFormat(4, 16, 1); // (44100, 16, 1);
                    waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(waveIn_DataAvailable);

                }
                catch (Exception)
                {
                    // ignored
                }
        }

        private void StopRecord()
        {
            waveIn.StopRecording();
        }

        private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            for (int i = 0; i < e.BytesRecorded; i += 2)
            {
                myQueue.Enqueue(BitConverter.ToInt16(e.Buffer, i));
                myQueue.Dequeue();
            }
        }

        public void ChartTick()
        {
            try { _chart.Series["Series1"].Points.DataBindY(myQueue); }
            catch { MessageBox.Show("No bytes recorded"); }
        }

    }
}
