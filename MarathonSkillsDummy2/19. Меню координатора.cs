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
    public partial class _19 : Form
    {
        public _19()
        {
            InitializeComponent();
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

        private void _19_FormClosing(object sender, FormClosingEventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            _21 f21 = new MarathonSkillsDummy2._21();
            f21.Show();
            Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _22 f22 = new _22();
            f22.Show();
            Visible = false;
        }
    }
}
