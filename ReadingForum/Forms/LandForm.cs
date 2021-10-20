using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadingForum
{
    public partial class LandForm : Form
    {
        public ClientController ClientController { get; set; }
        public LandForm()
        {
            InitializeComponent();
        }

        private async void LandForm_Load(object sender, EventArgs e)
        {
            bool flag=await ClientController.Init("127.0.0.1",8700);
            if(!flag) MessageBox.Show("服务端未打开");
        }

        private void Reg_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            ClientController.RegForm.Show();
        }

        private async void Login_btn_Click(object sender, EventArgs e)
        {
            bool flag= await ClientController.Login(new string[] { Acc_textBox.Text,Psw_textBox.Text});
            if (flag)
            {
                this.Hide();
                ClientController.MainForm.Show();
            }
        }
    }
}
