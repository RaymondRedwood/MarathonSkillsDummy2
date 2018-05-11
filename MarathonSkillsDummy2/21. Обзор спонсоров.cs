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
    public partial class _21 : Form
    {

        DataTable CharityTable;

        public _21()
        {
            InitializeComponent();

            SqlConnectionStringBuilder ctcsb = new SqlConnectionStringBuilder();
            ctcsb.DataSource = @"localhost";
            ctcsb.InitialCatalog = "MarathonSkills2016";
            ctcsb.IntegratedSecurity = true;
            ctcsb.ConnectTimeout = 30;
            SqlConnection ctc = new SqlConnection(ctcsb.ConnectionString);
            SqlCommand ctcom = ctc.CreateCommand();
            ctcom.CommandText = "SELECT * FROM Charity";
            SqlDataAdapter ctda = new SqlDataAdapter();
            ctda.SelectCommand = ctcom;
            CharityTable = new DataTable();
            ctda.Fill(CharityTable);
            label3.Text = Convert.ToString(CharityTable.Rows.Count);

            List<float> ssum = new List<float>();

            foreach (DataRow row in CharityTable.Rows)
            {
                SqlConnection stc = new SqlConnection(ctcsb.ConnectionString);
                SqlCommand stcom = stc.CreateCommand();
                stcom.CommandText = @"SELECT Sponsorship.Amount FROM Sponsorship inner join (Registration inner join Charity on
	(Registration.CharityId = Charity.CharityId) AND (Charity.CharityName = '"+row["CharityName"]+@"'))
	inner join RegistrationEvent on (Registration.RegistrationId = RegistrationEvent.RegistrationId) and
	((RegistrationEvent.EventId = '15_5FR') OR (RegistrationEvent.EventId = '15_5FM') OR (RegistrationEvent.EventId = '15_5HM'))
	on SponsorShip.RegistrationId = Registration.RegistrationId";
                SqlDataAdapter stda = new SqlDataAdapter();
                stda.SelectCommand = stcom;
                DataTable stdt = new DataTable();
                stda.Fill(stdt);
                float sum = 0;
                foreach (DataRow row2 in stdt.Rows)
                {
                    sum += Convert.ToSingle(row2["Amount"]);
                }
                ssum.Add(sum);
            }

            DataGridViewImageColumn dgvi = new DataGridViewImageColumn();
            dgvi.Name = "Image";
            dgvi.HeaderText = "Логотип";
            DataGridViewColumn dgvn = new DataGridViewColumn();
            dgvn.Name = "Name";
            dgvn.HeaderText = "Название";
            dgvn.CellTemplate = new DataGridViewTextBoxCell();
            DataGridViewColumn dgvd = new DataGridViewColumn();
            dgvd.Name = "Summary";
            dgvd.HeaderText = "Сумма";
            dgvd.CellTemplate = new DataGridViewTextBoxCell();

            dataGridView1.Columns.Add(dgvi);
            dataGridView1.Columns.Add(dgvn);
            dataGridView1.Columns.Add(dgvd);

            for (int i = 0; i < CharityTable.Rows.Count; i++)
            {
                Image li = Image.FromFile(Application.StartupPath + @"\CharityLogo\" + CharityTable.Rows[i]["CharityLogo"]);
                Bitmap ti = new Bitmap(li, 50, 50);

                dataGridView1.Rows.Add();
                dataGridView1["Image", dataGridView1.Rows.Count - 1].Value = ti;
                dataGridView1["Name", dataGridView1.Rows.Count - 1].Value = CharityTable.Rows[i]["CharityName"];
                dataGridView1["Summary", dataGridView1.Rows.Count - 1].Value = ssum[i];
            }

            float isum = 0;

            foreach (float am in ssum)
            {
                isum += am;
            }

            label4.Text = "$" + Convert.ToString(isum);
            comboBox1.SelectedIndex = 0;
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

        private void _21_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms[Application.OpenForms.Count - 2].Visible = true;
            Dispose();
        }

        private void marathonSkills2016BasePanel1_BackButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns[comboBox1.SelectedIndex+1], ListSortDirection.Ascending);
        }
    }
}
