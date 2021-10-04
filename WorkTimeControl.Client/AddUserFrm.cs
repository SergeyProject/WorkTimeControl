using Autofac;
using System;
using System.Windows.Forms;
using WorkTimeControl.BLL.Infrastructure;
using WorkTimeControl.BLL.Infrastructure.Interfaces;
using WorkTimeControl.BLL.Models;
using WorkTimeControl.DATA.Models;
using WorkTimeControl.DATA.Repositories;
using WorkTimeControl.DATA.Repositories.Abstract;

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

        //void AddUser()
        //{
        //    UserRepository repos = new UserRepository();
        //    User user = new User();
        //    user.Name = textBox1.Text;
        //    int idx = repos.Create(user);
        //    //MessageBox.Show(idx.ToString());
        //    Close();
        //}

        void AddUser()
        {
            var userBuilder = new ContainerBuilder();
            userBuilder.RegisterType<UserService>().As<IUserService>();
            userBuilder.RegisterType<UserRepository>().As<IUserRepository>();
            var userDb = userBuilder.Build();
            UserDTO user = new UserDTO();
            user.Name = textBox1.Text;
            int idx = userDb.Resolve<IUserService>().Create(user);
            Close();
        }


    }
}
