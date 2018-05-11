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
    public partial class _13 : Form
    {
        public _13()
        {
            InitializeComponent();
            SqlConnectionStringBuilder clcsb = new SqlConnectionStringBuilder();
            clcsb.DataSource = @"localhost";
            clcsb.InitialCatalog = "MarathonSkills2016";
            clcsb.IntegratedSecurity = true;
            clcsb.ConnectTimeout = 30;
            SqlConnection clc = new SqlConnection(clcsb.ConnectionString);
            SqlCommand clcom = clc.CreateCommand();
            clcom.CommandText = "SELECT Charity.CharityLogo, CONCAT(Charity.CharityName, CHAR(10), CHAR(10), Charity.CharityDescription) AS 'CharityPOOP' FROM Charity";
            SqlDataAdapter clda = new SqlDataAdapter();
            clda.SelectCommand = clcom;
            DataTable cldt = new DataTable();
            clda.Fill(cldt);
            DataTable cldt2 = new DataTable();
            DataColumn cldc2 = new DataColumn("Desc", typeof(string));
            cldt2.Columns.Add(cldc2);
            foreach (DataRow row in cldt.Rows)
            {
                DataRow crow = cldt2.NewRow();
                crow["Desc"] = row["CharityPOOP"];
                cldt2.Rows.Add(crow);
            }
            DataGridViewImageColumn dgvi = new DataGridViewImageColumn();
            DataGridViewColumn dgvt = new DataGridViewColumn();
            dgvi.Name = "img";
            dgvt.Name = "txt";
            dgvt.CellTemplate = new DataGridViewTextBoxCell();
            dataGridView1.Columns.Add(dgvi);
            dataGridView1.Columns.Add(dgvt);
            for (int i = 0; i < cldt2.Rows.Count; i++)
            {
                Image p = Image.FromFile(Application.StartupPath + @"\CharityLogo\" + cldt.Rows[i]["CharityLogo"]);
                Bitmap im = new Bitmap(p, 100, 100);
                dataGridView1.Rows.Add();
                //dataGridView1.Rows[dataGridView1.Rows.Count - 1].Height = 400;
                dataGridView1["img", dataGridView1.Rows.Count - 1].Value = im;
                dataGridView1["txt", dataGridView1.Rows.Count - 1].Value = cldt2.Rows[i]["Desc"];
                //(dataGridView1[0, i] as DataGridViewImageCell).Value = img;
            }
            dataGridView1.AutoResizeRows();
            
        }

        private void _13_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms[Application.OpenForms.Count - 2].Visible = true;
            Dispose();
        }

        private void marathonSkills2016BasePanel1_BackButtonClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
