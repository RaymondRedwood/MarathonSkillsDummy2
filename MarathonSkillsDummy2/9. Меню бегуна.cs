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
    public partial class _9 : Form
    {
        public _9()
        {
            InitializeComponent();
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

        private void button6_Click(object sender, EventArgs e)
        {
            _9_1 contacts = new _9_1();
            contacts.ShowDialog();
        }

        private void _9_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms[0].Visible = true;
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _5 f5 = new _5();
            if (!f5.IsDisposed)
            {
                f5.Show();
                Dispose();
            }
            
        }

        private void marathonSkills2016BasePanel1_BackButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _16 f16 = new _16();
            f16.Show();
            Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _17 f17 = new _17();

            if (!(f17.IsDisposed))
            {
                f17.Show();
                Visible = false;
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            _18 f18 = new _18();

            if (!(f18.IsDisposed))
            {
                f18.Show();
                Visible = false;
            } 
        }
    }
}
