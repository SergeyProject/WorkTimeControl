using Autofac;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WorkTimeControl.BLL.Infrastructure;
using WorkTimeControl.BLL.Infrastructure.Interfaces;
using WorkTimeControl.BLL.Interfaces;
using WorkTimeControl.BLL.Models;
using WorkTimeControl.Client.Camera;
using WorkTimeControl.DATA.Models;
using WorkTimeControl.DATA.Repositories;
using WorkTimeControl.DATA.Repositories.Abstract;

namespace WorkTimeControl.Reports
{
    public partial class Form1 : Form
    {       
        //List<byte[]> listImgStart = new List<byte[]>();
        //List<byte[]> listImgStop = new List<byte[]>();
        //List<TimeUser> listTimeUser = new List<TimeUser>();
        List<UserTimeReport> listUserTimeRep = new List<UserTimeReport>();
        public Form1()
        {
            InitializeComponent();
            listView1.SelectedIndexChanged += ListView1_SelectedIndexChanged;           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //pictureBox1.Image = null;
            //pictureBox2.Image = null;
            //listImgStart.Clear();
            //listImgStop.Clear();

            //UserRepository db = new UserRepository();
            //listView1.Items.Clear();          
            //listTimeUser.Clear();


            //foreach (User user in db.GetAllUsers())
            //{               
            //    LoadListTimeUser(user.Id);
            //}
            //loadViewLst();

            pictureBox1.Image = null;
            pictureBox2.Image = null;
            //listImgStart.Clear();
            //listImgStop.Clear();
            listView1.Items.Clear();
            //listTimeUser.Clear();
            listUserTimeRep.Clear();

            var builder = new ContainerBuilder();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            var container = builder.Build();

            foreach (UserDTO user in container.Resolve<IUserService>().GetAllUsers())
            {
                LoadListTimeUserT(user.Id);
            }
            loadViewLstT();
        }     

        //private void LoadListTimeUser(int userID)
        //{
        //    UserTimeRepository db = new UserTimeRepository();
        //    UserRepository userDb = new UserRepository();
        //    TimeUser _timeUser = null;
        //    string[] value = new string[2];          
        //    byte[] imgStart = null;
        //    byte[] imgStop = null;
        //    foreach (UserTime userTime in db.GetAllUserTime())
        //    {
        //        if (userTime.DateTimes.Date == dateTimePicker1.Value.Date)
        //        {
        //            if (userTime.UserId == userID)
        //            {
        //                _timeUser = new TimeUser();
        //                _timeUser.UserID = userID;
        //                _timeUser.Date = userTime.DateTimes.Date.ToShortDateString();
        //                _timeUser.UserName = userDb.GetUserById(userTime.UserId).Name;

        //                if (userTime.Descript == "Приход на работу")
        //                {
        //                    value[0] = userTime.DateTimes.ToShortTimeString();                         
        //                    imgStart= userTime.Photo;
        //                }
        //                if (userTime.Descript == "Уход с работы")
        //                {
        //                    value[1] = userTime.DateTimes.ToShortTimeString();                           
        //                    imgStop = userTime.Photo;
        //                }
        //            }
        //        }
        //    }
        //    if (_timeUser != null)
        //    {
        //        _timeUser.StartPhoto = imgStart;
        //        _timeUser.StopPhoto = imgStop;
        //        _timeUser.StartTime = value[0];
        //        _timeUser.StopTime = value[1];
        //        listTimeUser.Add(_timeUser);
        //    }
        //}

        //private void loadViewLst()
        //{
        //    foreach (TimeUser item in listTimeUser)
        //    {
        //        if (item != null)
        //        {
        //            ListViewItem viewItem = new ListViewItem(item.Date);
        //            viewItem.SubItems.Add(item.UserName);
        //            viewItem.SubItems.Add(item.StartTime);
        //            viewItem.SubItems.Add(item.StopTime);
        //            listView1.Items.Add(viewItem);
        //        }
        //    }
        //}

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {           
            pictureBox1.Image = null;
            pictureBox2.Image = null;

            int index = listView1.FocusedItem.Index;
          
            //if (listTimeUser[index].StartPhoto != null)
            //    pictureBox1.Image = ImageConvert.ByteToImage(listTimeUser[index].StartPhoto);
            //if (listTimeUser[index].StopPhoto != null)
            //    pictureBox2.Image = ImageConvert.ByteToImage(listTimeUser[index].StopPhoto);
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
            var userTimeBuilder = new ContainerBuilder();
            userTimeBuilder.RegisterType<UserTimeService>().As<IUserTimeService>();
            userTimeBuilder.RegisterType<UserTimeRepository>().As<IUserTimeRepository>();
            var db = userTimeBuilder.Build();

            var userBuilder = new ContainerBuilder();
            userBuilder.RegisterType<UserService>().As<IUserService>();
            userBuilder.RegisterType<UserRepository>().As<IUserRepository>();
            var userDb = userBuilder.Build();
            


            UserTimeReport _timeUser = null;
            string[] value = new string[2];
            byte[] imgStart = null;
            byte[] imgStop = null;
            foreach (UserTimeDTO  userTime in db.Resolve<IUserTimeService>().GetAllUserTime())
            {
                if (userTime.DateTimes.Date == dateTimePicker1.Value.Date)
                {
                    if (userTime.UserId == userID)
                    {
                        _timeUser = new UserTimeReport();
                        _timeUser.UserID = userID;
                        _timeUser.Date = userTime.DateTimes.Date.ToShortDateString();
                        _timeUser.UserName = userDb.Resolve<IUserService>().GetUserById(userTime.UserId).Name;   //userDb.GetUserById(userTime.UserId).Name;

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
