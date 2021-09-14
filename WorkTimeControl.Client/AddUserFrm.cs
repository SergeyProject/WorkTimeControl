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

namespace WorkTimeControl.Client
{
    public partial class AddUserFrm : Form
    {
        public AddUserFrm()
        {
            InitializeComponent();
            textBox1.KeyDown += TextBox1_KeyDown;
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                AddUser();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddUser();
        }

        void AddUser()
        {
            UserRepository repos = new UserRepository();
            User user = new User();
            user.Name = textBox1.Text;
            int idx = repos.Create(user);
            //MessageBox.Show(idx.ToString());
            Close();
        }


    }
}
