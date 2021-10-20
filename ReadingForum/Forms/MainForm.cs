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
    public partial class MainForm : Form
    {
        public ClientController ClientController;
        public MainForm()
        {
            InitializeComponent();
            Msglabel.Text ="欢迎回来，"+ ClientController.User.Account;
        }
    }
}
