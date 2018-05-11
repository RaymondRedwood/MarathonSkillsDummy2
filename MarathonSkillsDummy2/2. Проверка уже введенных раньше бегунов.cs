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
    public partial class _2 : Form
    {
        public _2()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _3 f3 = new _3();
            f3.Show();
            Dispose();
        }

        private void _2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms[0].Visible = true;
            Dispose();
        }

        private void marathonSkills2016BasePanel1_BackButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _3 f3 = new _3();
            f3.Show();
            Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _4 f4 = new _4();
            f4.Show();
            Dispose();
        }
    }
}
