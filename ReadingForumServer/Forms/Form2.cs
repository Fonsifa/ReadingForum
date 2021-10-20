using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReadingForumServer.Controllers;

namespace ReadingForumServer
{
    public partial class Form2 : Form
    {
        public ServerService ServerService { get; set; }
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ServerService.Start("50", "127.0.0.1:8700");
           // ServerService.Start("50", "192.168.146.1:8700");

        }
    }
}
