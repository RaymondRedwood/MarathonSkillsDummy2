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

namespace MarathonSkillsDummy2
{
    public partial class _26 : Form
    {
        public _26()
        {
            InitializeComponent();

            SqlConnectionStringBuilder cmcsb = new SqlConnectionStringBuilder();
            cmcsb.DataSource = @"localhost";
            cmcsb.InitialCatalog = "MarathonSkills2016";
            cmcsb.IntegratedSecurity = true;
            cmcsb.ConnectTimeout = 30;
            SqlConnection cmc = new SqlConnection(cmcsb.ConnectionString);
            SqlCommand cmcom = cmc.CreateCommand();
            cmcom.CommandText = "SELECT * FROM Charity";
            SqlDataAdapter cmda = new SqlDataAdapter();
            cmda.SelectCommand = cmcom;
            DataTable cmdt = new DataTable();
            cmda.Fill(cmdt);

            DataGridViewImageColumn dgvi = new DataGridViewImageColumn();
            dgvi.Name = "Image";
            dgvi.HeaderText = "Логотип";
            DataGridViewColumn dgvn = new DataGridViewColumn();
            dgvn.Name = "Name";
            dgvn.HeaderText = "Название";
            dgvn.CellTemplate = new DataGridViewTextBoxCell();
            DataGridViewColumn dgvd = new DataGridViewColumn();
            dgvd.Name = "Desc";
            dgvd.HeaderText = "Краткое описание";
            dgvd.CellTemplate = new DataGridViewTextBoxCell();
            DataGridViewButtonColumn dgvb = new DataGridViewButtonColumn();
            dgvb.FlatStyle = FlatStyle.Flat;
            dgvb.Name = "Button";
            dgvb.HeaderText = "Редактировать";
            dgvb.Text = "Править";

            dataGridView1.Columns.Add(dgvi);
            dataGridView1.Columns.Add(dgvn);
            dataGridView1.Columns.Add(dgvd);
            dataGridView1.Columns.Add(dgvb);

            foreach (DataRow row in cmdt.Rows)
            {
                Image li = Image.FromFile(Application.StartupPath + @"\CharityLogo\" + row["CharityLogo"]);
                Bitmap ti = new Bitmap(li, 50, 50);
                string boo = (row["CharityDescription"] as string).Split('\n')[0];

                dataGridView1.Rows.Add();
                dataGridView1["Image", dataGridView1.Rows.Count - 1].Value = ti;
                dataGridView1["Name", dataGridView1.Rows.Count - 1].Value = row["CharityName"];
                dataGridView1["Desc", dataGridView1.Rows.Count - 1].Value = boo;
                dataGridView1["Button", dataGridView1.Rows.Count - 1].Value = "Править";
            }


            
        }

        private void _26_FormClosing(object sender, FormClosingEventArgs e)
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                _27 f27 = new _27(e.RowIndex + 1);
                f27.Show();
                Dispose();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _27 f27 = new _27();
            f27.Show();
            Dispose();
        }
    }
}
