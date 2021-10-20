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
    public partial class RegForm : Form
    {
        public ClientController ClientController { get; set; }
        public RegForm()
        {
            InitializeComponent();
        }

        private async void ConfirmBtn_Click(object sender, EventArgs e)
        {
            if(textBox3.Text.Length<6||textBox3.Text.Length>18)
            {
                MessageBox.Show("密码过长或者过短");
            }
            else if(textBox3.Text!=textBox4.Text)
            {
                MessageBox.Show("两次密码不一致");
            }
            else
            {
                bool flag=await ClientController.Regist(new string[] { textBox1.Text, textBox2.Text, textBox3.Text});
		if(flag){
		MessageBox.Show("注册成功");
		this.Hide();
		ClientController.LandForm.Show();
}
            }
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            ClientController.LandForm.Show();
        }
    }
}
