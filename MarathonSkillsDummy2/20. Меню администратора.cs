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
    public partial class _20 : Form
    {
        public _20()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void _20_FormClosing(object sender, FormClosingEventArgs e)
        {
            CurrentUserContext.Email = "";
            CurrentUserContext.Password = "";
            CurrentUserContext.FirstName = "";
            CurrentUserContext.LastName = "";
            CurrentUserContext.RoleId = "";
            CurrentUserContext.IsUsingNow = false;
            Application.OpenForms[0].Visible = true;
            Dispose();
        }

        private void marathonSkills2016BasePanel1_BackButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _35 f35 = new _35();
            Visible = false;
            f35.Show();
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

        private void button3_Click(object sender, EventArgs e)
        {
            _26 f26 = new _26();
            f26.Show();
            Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _28 f28 = new _28();
            f28.Show();
            Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _30 f30 = new _30();
            f30.Show();
            Visible = false;
        }
    }
}
