using System;
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

        private void button6_Click(object sender, EventArgs e)
        {
            add_to_sklad add = new add_to_sklad();
            add.ShowDialog();
        }
    }
}
