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
    public partial class _10 : Form
    {
        public _10()
        {
            InitializeComponent();
        }

        private void _10_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms[0].Visible = true;
            Dispose();
        }

        private void marathonSkills2016BasePanel1_BackButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _14 f14 = new _14();
            f14.Show();
            Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _11 f11 = new _11();
            f11.Show();
            Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _15 f15 = new _15();
            f15.Show();
            Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _33 f33 = new MarathonSkillsDummy2._33();
            f33.Show();
            Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _34 f34 = new MarathonSkillsDummy2._34();
            f34.Show();
            Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _13 f13 = new MarathonSkillsDummy2._13();
            f13.Show();
            Visible = false;
        }
    }
}
