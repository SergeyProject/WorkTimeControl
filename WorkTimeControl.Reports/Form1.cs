using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WorkTimeControl.BLL.Camera;
using WorkTimeControl.BLL.Infrastructure;
using WorkTimeControl.BLL.Models;

namespace WorkTimeControl.Reports
{
    public partial class Form1 : Form
    { 
        List<UserTimeReport> listUserTimeRep = new List<UserTimeReport>();
        ServiceDTO serviceDTO = new ServiceDTO();
        public Form1()
        {
            InitializeComponent();
            listView1.SelectedIndexChanged += ListView1_SelectedIndexChanged;           
        }

        private void button1_Click(object sender, EventArgs e)
        {           
            pictureBox1.Image = null;
            pictureBox2.Image = null;          
            listView1.Items.Clear();          
            listUserTimeRep.Clear();

            foreach (UserDTO user in serviceDTO.UserService().GetAllUsers())
            {
                LoadListTimeUserT(user.Id);
            }
            loadViewLstT();
        }     

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {           
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            int index = listView1.FocusedItem.Index;
            if(listUserTimeRep[index].StartPhoto!=null)
                pictureBox1.Image = ImageConvert.ByteToImage(listUserTimeRep[index].StartPhoto);
            if (listUserTimeRep[index].StopPhoto != null)
                pictureBox2.Image = ImageConvert.ByteToImage(listUserTimeRep[index].StopPhoto);
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoadListTimeUserT(int userID)
        {   
            UserTimeReport _timeUser = null;
            string[] value = new string[2];
            byte[] imgStart = null;
            byte[] imgStop = null;
            foreach (UserTimeDTO  userTime in serviceDTO.UserTimeService().GetAllUserTime())
            {
                if (userTime.DateTimes.Date == dateTimePicker1.Value.Date)
                {
                    if (userTime.UserId == userID)
                    {
                        _timeUser = new UserTimeReport();
                        _timeUser.UserID = userID;
                        _timeUser.Date = userTime.DateTimes.Date.ToShortDateString();
                        _timeUser.UserName = serviceDTO.UserService().GetUserById(userTime.UserId).Name;    

                        if (userTime.Descript == "Приход на работу")
                        {
                            value[0] = userTime.DateTimes.ToShortTimeString();
                            imgStart = userTime.Photo;
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
                listUserTimeRep.Add(_timeUser);
            }
        }

        private void loadViewLstT()
        {
            foreach (UserTimeReport item in listUserTimeRep)
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
    }
}
