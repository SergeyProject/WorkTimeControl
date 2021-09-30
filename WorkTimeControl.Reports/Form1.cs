using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WorkTimeControl.Client.Camera;
using WorkTimeControl.DATA.Models;
using WorkTimeControl.DATA.Repositories;

namespace WorkTimeControl.Reports
{
    public partial class Form1 : Form
    {       
        List<byte[]> listImgStart = new List<byte[]>();
        List<byte[]> listImgStop = new List<byte[]>();
        List<TimeUser> listTimeUser = new List<TimeUser>(); 
        public Form1()
        {
            InitializeComponent();
            listView1.SelectedIndexChanged += ListView1_SelectedIndexChanged;           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            listImgStart.Clear();
            listImgStop.Clear();

            UserRepository db = new UserRepository();
            listView1.Items.Clear();          
            listTimeUser.Clear();


            foreach (User user in db.GetAllUsers())
            {               
                LoadListTimeUser(user.Id);
            }
            loadViewLst();
        }     

        private void LoadListTimeUser(int userID)
        {
            UserTimeRepository db = new UserTimeRepository();
            UserRepository userDb = new UserRepository();
            TimeUser _timeUser = null;
            string[] value = new string[2];          
            byte[] imgStart = null;
            byte[] imgStop = null;
            foreach (UserTime userTime in db.GetAllUserTime())
            {
                if (userTime.DateTimes.Date == dateTimePicker1.Value.Date)
                {
                    if (userTime.UserId == userID)
                    {
                        _timeUser = new TimeUser();
                        _timeUser.UserID = userID;
                        _timeUser.Date = userTime.DateTimes.Date.ToShortDateString();
                        _timeUser.UserName = userDb.GetUserById(userTime.UserId).Name;

                        if (userTime.Descript == "Приход на работу")
                        {
                            value[0] = userTime.DateTimes.ToShortTimeString();                         
                            imgStart= userTime.Photo;
                        }
                        if (userTime.Descript == "Уход с работы")
                        {
                            value[1] = userTime.DateTimes.ToShortTimeString();                           
                            imgStop = userTime.Photo;
                        }
                    }
                }
            }
            if (_timeUser != null)
            {
                _timeUser.StartPhoto = imgStart;
                _timeUser.StopPhoto = imgStop;
                _timeUser.StartTime = value[0];
                _timeUser.StopTime = value[1];
                listTimeUser.Add(_timeUser);
            }
        }

        private void loadViewLst()
        {
            foreach (TimeUser item in listTimeUser)
            {
                if (item != null)
                {
                    ListViewItem viewItem = new ListViewItem(item.Date);
                    viewItem.SubItems.Add(item.UserName);
                    viewItem.SubItems.Add(item.StartTime);
                    viewItem.SubItems.Add(item.StopTime);
                    listView1.Items.Add(viewItem);
                }
            }
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {           
            pictureBox1.Image = null;
            pictureBox2.Image = null;

            int index = listView1.FocusedItem.Index;
          
            if (listTimeUser[index].StartPhoto != null)
                pictureBox1.Image = ImageConvert.ByteToImage(listTimeUser[index].StartPhoto);
            if (listTimeUser[index].StopPhoto != null)
                pictureBox2.Image = ImageConvert.ByteToImage(listTimeUser[index].StopPhoto);
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
