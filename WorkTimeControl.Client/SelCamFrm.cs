using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

using Emgu;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.Structure;
using Emgu.Util;
using DirectShowLib;
using WorkTimeControl.Client.Camera;


namespace WorkTimeControl.Client
{
    public partial class SelCamFrm : Form
    {
        private DsDevice[] webCams;
        private int selectCameraId = 0;
        string fileOption = "optioncam.wtc";
        public int cameraID { get; set; }
        public SelCamFrm()
        {
            InitializeComponent();
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
           
             
        }


        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectCameraId = comboBox1.SelectedIndex;
        }

        private void SelCamFrm_Load(object sender, EventArgs e)
        {
            FileOptionIsExist();
            webCams = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            for(int i = 0; i < webCams.Length; i++)
            {
                comboBox1.Items.Add(webCams[i].Name);
            }
        }

        void FileOptionIsExist()
        {
            FileInfo file = new FileInfo(fileOption);
            if (file.Exists)
            {              
                this.Close();
            }           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(fileOption))
            {
                cameraID = selectCameraId;
                sw.WriteLine(selectCameraId.ToString());
            }
            FileOptionIsExist();
        }

        
    }
}
