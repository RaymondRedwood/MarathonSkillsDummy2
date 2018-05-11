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
    public partial class _8 : Form
    {
        public _8()
        {
            InitializeComponent();
        }

        private void _8_FormClosed(object sender, FormClosedEventArgs e)
        {
            _9 f9 = new _9();
            f9.Show();
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void marathonSkills2016BasePanel1_BackColorChanged(object sender, EventArgs e)
        {

        }

        private void marathonSkills2016BasePanel1_BackButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
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
    }
}
