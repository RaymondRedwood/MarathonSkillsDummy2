using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarathonSkillsDummy2
{
    public partial class _1 : Form
    {
        public _1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _2 f2 = new _2();
            Visible = false;
            f2.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _3 f3 = new _3();
            Visible = false;
            f3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _6 f6 =  new _6();
            Visible = false;
            f6.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _10 f10 = new _10();
            f10.Show();
            Visible = false;
        }
    }
}
