using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System;
using System.Windows.Forms;

namespace SpecSniffer_v2
{
    public class Camera
    {
        private ImageBox _imageBox;
        private Mat _noCamImage;
        private bool _isCapturing;
        private readonly VideoCapture _camCapture;

        public Camera()
        {
            //long initialization
            _camCapture = new VideoCapture();
            _isCapturing = false;
            _imageBox = null;
        }

        public void StartStopCapture(ImageBox imageBox)
        {
            _imageBox = imageBox;
            if (_isCapturing == false)
            {
                Start();
            }
            else
            {
                StopCapture();
            }
        }

        private void Start()
        {
            try
            {
                if (_camCapture.QuerySmallFrame() != null)
                {
                    Application.Idle += ProcessFrame;
                }
                else
                {
                    _imageBox.Image = SetCamImage("404: Camera not found.");
                }

                _isCapturing = true;
            }
            catch (Exception)
            {
                _imageBox.Image = SetCamImage("Camera capture error ...");
            }
        }

        private void StopCapture()
        {
            _isCapturing = false;
            _camCapture.Stop();
            _imageBox.Image = SetCamImage("Press 2 to capture camera image.");
        }

        private Mat SetCamImage(string text)
        {
            _noCamImage = new Mat(450, 700, DepthType.Cv8U, 3);
            _noCamImage.SetTo(new Bgr(100, 90, 80).MCvScalar);
            CvInvoke.PutText(_noCamImage, text, new System.Drawing.Point(10, 50), FontFace.HersheyDuplex, 0.9,
                new Bgr(0, 10, 255).MCvScalar);

            return _noCamImage;
        }

        private void ProcessFrame(object sender, EventArgs e)
        {
            if (_isCapturing) _imageBox.Image = _camCapture.QuerySmallFrame();
        }
    }
}
  