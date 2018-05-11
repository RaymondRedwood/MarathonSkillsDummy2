using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MarathonSkillsDummy2
{
    public partial class _22_1 : Form
    {
        private List<string> tmp;
        public _22_1(List<string> input)
        {
            InitializeComponent();
            tmp = input;
            hintTextBox2.Lines = input.ToArray();
        }

        private void _22_1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms[Application.OpenForms.Count - 2].Visible = true;
            Dispose();
        }

        private void marathonSkills2016BasePanel1_BackButtonClick(object sender, EventArgs e)
        {
            Close();
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

        private void button1_Click(object sender, EventArgs e)
        {
            hintTextBox2.SelectAll();
            hintTextBox2.Copy();
            hintTextBox2.DeselectAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "TXT|*.txt";
            sfd.Title = "POOP";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(sfd.FileName);
                foreach (string line in hintTextBox2.Lines)
                {
                    writer.WriteLine(line);
                }
                writer.Close();
                MessageBox.Show("Экспорт списка рассылки произошел успешно", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
