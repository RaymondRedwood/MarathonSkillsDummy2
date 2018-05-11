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
    public partial class _33 : Form
    {
        public _33()
        {
            InitializeComponent();
        }

        private void _33_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms[Application.OpenForms.Count - 2].Visible = true;
            Dispose();
        }

        private void marathonSkills2016BasePanel1_BackButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Green;
            panel2.BackColor = Color.Gray;
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Gray;
            panel2.BackColor = Color.Green;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panel1_Click(sender, e);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            panel2_Click(sender, e);
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
            double weight = Convert.ToDouble(hintTextBox1.Text);
            double growth = Convert.ToDouble(hintTextBox2.Text) / 100;
            double bmi = Math.Round(weight / (growth * growth), 1);
            label12.Text = Convert.ToString(bmi);
            if (bmi >= 50) progressBar1.Value = 200;
            else
                progressBar1.Value = Convert.ToInt32(bmi) * 2;
            if (bmi > 18.5)
            {
                if (bmi > 24.9)
                {
                    if (bmi > 29.9)
                    {
                        pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\BMI\4.png");
                        label11.Text = "Ожирение";
                    }
                    else
                    {
                        pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\BMI\3.png");
                        label11.Text = "Избыточный вес";
                    }
                }
                else
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\BMI\2.png");
                    label11.Text = "Норма";
                }
            }
            else
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\BMI\1.png");
                label11.Text = "Нехватка веса";
            }
        }
    }
}
