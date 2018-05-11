using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace MarathonSkillsDummy2
{
    public partial class _34 : Form
    {
        bool Man = true;
        public _34()
        {
            InitializeComponent();
        }

        private void _34_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms[Application.OpenForms.Count - 2].Visible = true;
            Dispose();
        }

        private void marathonSkills2016BasePanel1_BackButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            _34_1 f341 = new _34_1();
            f341.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Regex checkg = new Regex(@"^(?=.*[0-9])(1[0-9][0-9]|2([0-2][0-9]|30))$");
            if (!checkg.IsMatch(hintTextBox2.Text))
            {
                MessageBox.Show("Growth validation error", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Regex checkw = new Regex(@"^(?=.*[0-9])([4-9][0-9]|1[0-9][0-9]|200)$");
            if (!checkw.IsMatch(hintTextBox1.Text))
            {
                MessageBox.Show("Weight validation error", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Regex checka = new Regex(@"^(?=.*[0-9])([1-9][0-9]|1[0-4][0-9]|150)$");
            if (!checka.IsMatch(hintTextBox3.Text))
            {
                MessageBox.Show("Age validation error", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            double BMR = 0;
            int w = Convert.ToInt32(hintTextBox1.Text);
            int g = Convert.ToInt32(hintTextBox2.Text);
            int a = Convert.ToInt32(hintTextBox3.Text);
            if (Man)
            {
                BMR = 66 + (13.7 * w) + (5 * g) - (6.8 * a);
            }
            else
            {
                BMR = 655 + (9.6 * w) + (1.8 * g) - (4.7 * a);
            }
            label12.Text = Convert.ToString(Math.Round(BMR, 0));
            label14.Text = Convert.ToString(Math.Round(BMR, 0) * 1.2);
            label16.Text = Convert.ToString(Math.Round(BMR, 0) * 1.375);
            label18.Text = Convert.ToString(Math.Round(BMR, 0) * 1.55);
            label20.Text = Convert.ToString(Math.Round(BMR, 0) * 1.725);
            label22.Text = Convert.ToString(Math.Round(BMR, 0) * 1.9);
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Green;
            panel2.BackColor = Color.Gray;
            Man = true;
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Gray;
            panel2.BackColor = Color.Green;
            Man = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panel1_Click(sender, e);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            panel2_Click(sender, e);
        }
    }
}
