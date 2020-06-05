using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sklad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Sklad sk = new Sklad();
            sk.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            add_new_pr pr = new add_new_pr();
            pr.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            add_buyer ad = new add_buyer();
            ad.ShowDialog();
        }
    }
}
