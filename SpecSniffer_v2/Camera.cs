using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;

namespace SpecSniffer_v2
{
    public class Camera
    {

        private ImageBox _imageBox;
        private Mat _noCamImage;
        private bool _isCapturing = false;
        private VideoCapture CamCapture { get; }

        public Camera()
        {
            //long initialization
            CamCapture = new VideoCapture();
        }



        public void StartCapture(ImageBox imgBox)
        {
            _imageBox = imgBox;
            if (_isCapturing == false)
            {
                try
                {
                    if (CamCapture.QuerySmallFrame() != null)
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
            else
            {
                StopCapture();
            }
        }

        private void StopCapture()
        {
            _isCapturing = false;
            CamCapture.Stop();
            _imageBox.Image = SetCamImage("Press 2 to capture camera image.");
        }

        private Mat SetCamImage(string text)
        {
            _noCamImage = new Mat(450, 700, DepthType.Cv8U, 3);
            _noCamImage.SetTo(new Bgr(100, 90, 80).MCvScalar);
            CvInvoke.PutText(
                _noCamImage,
                text,
                new System.Drawing.Point(10, 50),
                FontFace.HersheyDuplex,
                0.9,
                new Bgr(0, 10, 255).MCvScalar);

            return _noCamImage;
        }

        private void ProcessFrame(object sender, EventArgs e)
        {
            if (_isCapturing != false)
                _imageBox.Image = CamCapture.QuerySmallFrame();
        }
    }
}
  