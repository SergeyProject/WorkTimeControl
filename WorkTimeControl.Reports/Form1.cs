using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkTimeControl.Client.Camera;
using WorkTimeControl.DATA.Models;
using WorkTimeControl.DATA.Repositories;

namespace WorkTimeControl.Reports
{
    public partial class Form1 : Form
    {
        List<UserTime> listUserTime = new List<UserTime>();
        List<DateTime> listTime = new List<DateTime>();
        //ListViewItem viewItem = null;
        List<byte[]> imgStart = new List<byte[]>();
        List<byte[]> imgStop = new List<byte[]>();
        public Form1()
        {
            InitializeComponent();
            listView1.SelectedIndexChanged += ListView1_SelectedIndexChanged;
            dateTimePicker1.ValueChanged += DateTimePicker1_ValueChanged;
        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            imgStart.Clear();
            imgStop.Clear();

            UserRepository db = new UserRepository();
            listView1.Items.Clear();
            listUserTime.Clear();
          
           
            foreach (User user in db.GetAllUsers())
            {
                LoadListView(user.Id);
            }
        }

        private void LoadListView(int userID)
        {
            try
            {
                ListViewItem viewItem = null;
                UserTimeRepository db = new UserTimeRepository();
                string[] value = new string[2];

                foreach (UserTime userTime in db.GetAllUserTime())
                {
                    if (userTime.DateTimes.Date == dateTimePicker1.Value.Date)
                    {
                        if (userTime.UserId == userID)
                        {
                            viewItem = new ListViewItem(userTime.DateTimes.Date.ToShortDateString());
                            UserRepository userDb = new UserRepository();
                            viewItem.SubItems.Add(userDb.GetUserById(userTime.UserId).Name);
                            if (userTime.Descript == "Приход на работу")
                            {
                                value[0] = userTime.DateTimes.ToLongTimeString();
                                imgStart.Add(userTime.Photo);
                            }
                            if (userTime.Descript == "Уход с работы")
                            {
                                value[1] = userTime.DateTimes.ToLongTimeString();
                                imgStop.Add(userTime.Photo);
                            }
                        }
                    }
                }
                if (viewItem != null)
                {
                    viewItem.SubItems.Add(value[0]);
                    viewItem.SubItems.Add(value[1]);
                    listView1.Items.Add(viewItem);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Получить ID
        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // label2.Text = listView1.FocusedItem.SubItems[1].Text;
            int index = listView1.FocusedItem.Index;
            pictureBox1.Image = ImageConvert.ByteToImage(imgStart[index]);
            pictureBox2.Image = ImageConvert.ByteToImage(imgStop[index]);
        }

        //  Загрузить в pictureBox снимки
        void LoadImage()
        {

        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
