using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkTimeControl.DATA.Models;
using WorkTimeControl.DATA.Repositories;

using Emgu;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.Structure;
using Emgu.Util;
using DirectShowLib;
using WorkTimeControl.Client.Camera;

namespace WorkTimeControl.Client
{
    public partial class Form1 : Form
    {
        VideoCapture capture = null;
        DsDevice[] webCams = null;
        //int selectedCameraId = 0;


        List<int> userId = new List<int>();
        int ID;
        public Form1()
        {
            InitializeComponent();
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            AddUserFrm frm = new AddUserFrm();
            frm.ShowDialog(this);
            LoadList();
        }

        void LoadList()
        {
            listBox1.Items.Clear();
            userId.Clear();
            UserRepository user = new UserRepository();
            foreach(var item in user.GetAllUsers())
            {
                listBox1.Items.Add(item.Name);
                userId.Add(item.Id);
            }
            if (listBox1.Items.Count > 0)
                listBox1.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadList();
            InitialDevice();
        }

        void InitialDevice()
        {
            int ln = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice).Length;
            if (ln > 1)
            {
                webCams = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
                ViewCams();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ID = userId[listBox1.SelectedIndex];
            //label1.Text = ID.ToString();
            label3.Text = listBox1.Items[listBox1.SelectedIndex].ToString();

            //Получить последние данные пользователя
            //listBox2.Items.Clear();
            List<Image> images = new List<Image>();
            List<DateTime> dates = new List<DateTime>();
            images.Clear();
            dates.Clear();
            ClearControl();
            UserTimeRepository userTimeRepository = new UserTimeRepository();
            foreach (UserTime item in userTimeRepository.GetUserTimes(ID))
            {
                if (item.DateTime.Date == DateTime.Now.Date)
                {
                    //listBox2.Items.Add($"{item.Descript} {item.DateTime}");
                    images.Add(ImageConvert.ByteToImage(item.Image));
                    dates.Add(item.DateTime);
                    pictureBox2.Image = images[0];
                    if (images.Count > 1)
                        pictureBox3.Image = images[1];

                    label4.Text = $"{dates[0].Hour:00}:{dates[0].Minute:00}";
                    if (dates.Count > 1)
                        label5.Text = $"{dates[1].Hour:00}:{dates[1].Minute:00}";
                }
            }
            if (dates.Count > 0)
            {
                button3.Enabled = false;
                button3.Text = "Приход на работу зафиксирован";
            }
            else
            {
                button3.Enabled = true;
                button3.Text = "Приход на работу";
            }
            if (dates.Count > 1)
            {
                button4.Enabled = false;
                button4.Text = "Уход с работы зафиксирован";
            }
            else
            {
                button4.Enabled = true;
                button4.Text = "Уход с работы";
            }
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteUser();
        }

        void DeleteUser()
        {
            if(MessageBox.Show("Запись будет удалена.\r\nПродолжить?", "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                UserTimeRepository userTime = new UserTimeRepository();
                userTime.Delete(ID);
                UserRepository user = new UserRepository();
                user.Delete(ID);
                ClearControl();
                LoadList();
            }
        }

        void ClearControl()
        {
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            label4.Text = "Не зафиксирован";
            label5.Text = "Не зафиксирован";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (label3.Text != "**")
            {

                Mat m = new Mat();
                capture.Retrieve(m);
                var image= MakeScreenShot(m.ToImage<Bgr, byte>().Flip(Emgu.CV.CvEnum.FlipType.Horizontal));
                pictureBox2.Image = image;
                

                UserTime userTime = new UserTime();
                userTime.UserId = ID;
                userTime.DateTime = DateTime.Now;
                label4.Text = $"{DateTime.Now.Hour:00}:{DateTime.Now.Minute:00}";
                userTime.Descript = "Приход на работу";
                userTime.Image = ImageConvert.ConvertToByte((Bitmap)image);
                UserTimeRepository user = new UserTimeRepository();
                user.StartTimeCreate(userTime);
                button3.Enabled = false;
               
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (label3.Text != "**")
            {
                Mat m = new Mat();
                capture.Retrieve(m);
                var image = MakeScreenShot(m.ToImage<Bgr, byte>().Flip(Emgu.CV.CvEnum.FlipType.Horizontal));
                pictureBox3.Image = image;

                UserTime userTime = new UserTime();
                userTime.UserId = ID;
                userTime.DateTime = DateTime.Now;
                label5.Text = $"{DateTime.Now.Hour:00}:{DateTime.Now.Minute:00}";
                userTime.Descript = "Уход с работы";
                userTime.Image = ImageConvert.ConvertToByte((Bitmap)image);
                UserTimeRepository user = new UserTimeRepository();
                user.StopTimeCreate(userTime);
                button4.Enabled = false;
            }          
        }

        // View camera

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

        private void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            try
            {
                Mat m = new Mat();
                capture.Retrieve(m);
                pictureBox1.Image = m.ToImage<Bgr, byte>().Flip(Emgu.CV.CvEnum.FlipType.Horizontal).Bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ScreenShot
        Image MakeScreenShot(Image<Bgr, byte> _image)
        {
           return _image.Bitmap;
        }

        bool IsEnableButtonStart(DateTime dt)
        {
            if (dt.Date.Day == DateTime.Now.Day)
                return false;
            else
                return true;
        }
        bool IsEnableButtonStop(DateTime dt)
        {
            if (dt.Date.Day == DateTime.Now.Day)
                return false;
            else
                return true;
        }
    }
}
