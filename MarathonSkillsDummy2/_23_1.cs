using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace MarathonSkillsDummy2
{
    public partial class _23_1 : Form
    {
        public _23_1()
        {
            InitializeComponent();
            label12.Text = RunnerBib.UserName;
            label13.Text = RunnerBib.CountryName;
            label14.Text = RunnerBib.CharityName;
            label15.Text = RunnerBib.EventsName;
            if ((RunnerBib.RunnerPhoto != "") & (RunnerBib.RunnerPhoto != ""))
            {
                pictureBox6.Image = Image.FromFile(Application.StartupPath + @"\RunnerPhoto\" + RunnerBib.RunnerPhoto);
            }
            if ((RunnerBib.CountryFlag != "") & (RunnerBib.CountryFlag != ""))
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\flags\" + RunnerBib.CountryFlag);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.Count > 1)
            {
                if (Application.OpenForms.Count > 2)
                {
                    for (int i = Application.OpenForms.Count - 2; i > 0; i--)
                    {
                        Application.OpenForms[i].Dispose();
                    }
                }
                Dispose();
                Application.OpenForms[0].Visible = true;
            }
        }

        private void _23_1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms[Application.OpenForms.Count - 2].Visible = true;
            Dispose();
        }

        private void marathonSkills2016BasePanel1_BackButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void pr(object sender, PrintPageEventArgs e)
        {
            Bitmap b = new Bitmap(panel1.Width + 10, panel1.Height + 10);
            panel1.DrawToBitmap(b, panel1.ClientRectangle);
            e.Graphics.DrawImage(b, 100, 100);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == DialogResult.OK)
            {
                PrintDocument pdoc = new PrintDocument();
                pdoc.PrinterSettings = pd.PrinterSettings;
                pdoc.PrintPage += new PrintPageEventHandler(pr);
                pdoc.Print();
            }
        }
    }
}
