using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace MarathonSkillsDummy2
{
    public partial class _27 : Form
    {

        private DataTable cldt;
        private SqlDataAdapter clda;
        private string oldphoto = "";

        public _27()
        {
            InitializeComponent();
            CharityInfo.CharityId = -1;
            SqlConnectionStringBuilder cmcsb = new SqlConnectionStringBuilder();
            cmcsb.DataSource = @"localhost";
            cmcsb.InitialCatalog = "MarathonSkills2016";
            cmcsb.IntegratedSecurity = true;
            cmcsb.ConnectTimeout = 30;
            SqlConnection cmc = new SqlConnection(cmcsb.ConnectionString);
            SqlCommand cmcom = cmc.CreateCommand();
            cmcom.CommandText = "SELECT * FROM Charity";
            clda = new SqlDataAdapter();
            clda.SelectCommand = cmcom;
            SqlCommandBuilder cmcb = new SqlCommandBuilder(clda);
            clda.DeleteCommand = cmcb.GetDeleteCommand();
            clda.InsertCommand = cmcb.GetInsertCommand();
            clda.UpdateCommand = cmcb.GetUpdateCommand();
            cldt = new DataTable();
            clda.Fill(cldt);
        }

        public _27(int cid)
        {
            InitializeComponent();
            CharityInfo.CharityId = cid;
            SqlConnectionStringBuilder cmcsb = new SqlConnectionStringBuilder();
            cmcsb.DataSource = @"localhost";
            cmcsb.InitialCatalog = "MarathonSkills2016";
            cmcsb.IntegratedSecurity = true;
            cmcsb.ConnectTimeout = 30;
            SqlConnection cmc = new SqlConnection(cmcsb.ConnectionString);
            SqlCommand cmcom = cmc.CreateCommand();
            cmcom.CommandText = "SELECT * FROM Charity";
            clda = new SqlDataAdapter();
            clda.SelectCommand = cmcom;
            SqlCommandBuilder cmcb = new SqlCommandBuilder(clda);
            clda.DeleteCommand = cmcb.GetDeleteCommand();
            clda.InsertCommand = cmcb.GetInsertCommand();
            clda.UpdateCommand = cmcb.GetUpdateCommand();
            cldt = new DataTable();
            clda.Fill(cldt);

            foreach (DataRow row in cldt.Rows)
            {
                if ((int)row["CharityId"] == CharityInfo.CharityId)
                {
                    hintTextBox1.Text = row["CharityName"] as string;
                    hintTextBox2.Text = row["CharityDescription"] as string;
                    if (((row["CharityDescription"] as string) != "") & (row["CharityDescription"] != DBNull.Value))
                    {
                        pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\CharityLogo\" + (string)row["CharityLogo"]);
                        CharityInfo.CharityLogo = row["CharityLogo"] as string;
                    }
                }
            }

            oldphoto = CharityInfo.CharityLogo;
        }

        private void _27_FormClosing(object sender, FormClosingEventArgs e)
        {
            _26 f26 = new _26();
            f26.Show();
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

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CharityInfo.CharityLogo = "";
            pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\dsa.png");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog opd = new OpenFileDialog();
            opd.Title = "Pls, select image file on your pc";
            opd.Filter = "Image Files|*.*";
            if (opd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Image testthis = Image.FromFile(opd.FileName);
                    pictureBox1.Image = testthis;
                    oldphoto = CharityInfo.CharityLogo;
                    CharityInfo.CharityLogo = opd.FileName;
                    hintTextBox6.Text = opd.FileName;
                }
                catch (OutOfMemoryException E)
                {
                    MessageBox.Show("Файл поврежден или не поддерживается!", "Marathon Skills 2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (hintTextBox1.Text == "")
            {
                MessageBox.Show("Название благотворительной организации должно обязтаельно быть заполнено!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (CharityInfo.CharityLogo != oldphoto)
            {
                FileInfo fi = new FileInfo(CharityInfo.CharityLogo);
                if (!File.Exists(Application.StartupPath + @"\CharityLogo\" + fi.Name))
                {
                    fi.CopyTo(Application.StartupPath + @"\CharityLogo\" + fi.Name);
                    CharityInfo.CharityLogo = fi.Name;
                }
            }
            DataRow nrow = cldt.NewRow();
            nrow["CharityName"] = hintTextBox1.Text;
            nrow["CharityDescription"] = hintTextBox2.Text;
            nrow["CharityLogo"] = CharityInfo.CharityLogo;
            if (CharityInfo.CharityId == -1)
            {
                cldt.Rows.Add(nrow);
            }
            else
            {
                for (int i = 0; i < cldt.Rows.Count; i++)
                {
                    if ((int)cldt.Rows[i]["CharityId"] == CharityInfo.CharityId)
                    {
                        cldt.Rows[i]["CharityName"] = hintTextBox1.Text;
                        cldt.Rows[i]["CharityDescription"] = hintTextBox2.Text;
                        cldt.Rows[i]["CharityLogo"] = CharityInfo.CharityLogo;
                    }
                }
            }
            clda.Update(cldt);
            Close();
        }
    }
}
