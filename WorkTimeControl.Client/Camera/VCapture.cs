﻿using Emgu;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.Structure;
using Emgu.Util;
using DirectShowLib;
using System;
using System.Windows.Forms;
using System.Drawing;

namespace WorkTimeControl.Client.Camera
{
    public class VCapture
    {
        VideoCapture capture = null;
        DsDevice[] webCams = null;
        public Bitmap image = null;

        public VCapture()
        {
            webCams = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            ViewCams();
        }

        void ViewCams()
        {
            try
            {
                if (webCams.Length == 0)
                    throw new Exception("Нет доступных камер!");
                else if (capture != null)
                {
                    capture.Start();
                }
                else
                {
                    capture = new VideoCapture(0);
                    capture.ImageGrabbed += Capture_ImageGrabbed;
                    capture.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            try
            {
                Mat m = new Mat();
                capture.Retrieve(m);
                image = m.ToImage<Bgr, byte>().Flip(Emgu.CV.CvEnum.FlipType.Horizontal).Bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Screenshot
        public Image MakeScreenShot(Image<Bgr, byte> _image)
        {
           return _image.Bitmap;
        }

    }
}
