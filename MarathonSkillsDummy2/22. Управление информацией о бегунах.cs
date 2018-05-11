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
    public partial class _22 : Form
    {

        private DataTable rldt;

        public _22()
        {
            InitializeComponent();

            SqlConnectionStringBuilder rmcsb = new SqlConnectionStringBuilder();
            rmcsb.DataSource = @"localhost";
            rmcsb.InitialCatalog = "MarathonSkills2016";
            rmcsb.IntegratedSecurity = true;
            rmcsb.ConnectTimeout = 30;

            SqlConnection slc = new SqlConnection(rmcsb.ConnectionString); ;
            SqlCommand slcom = slc.CreateCommand();
            slcom.CommandText = "SELECT * FROM RegistrationStatus";
            SqlDataAdapter slda = new SqlDataAdapter();
            slda.SelectCommand = slcom;
            DataTable sldt = new DataTable();
            slda.Fill(sldt);
            comboBox1.DataSource = sldt;
            comboBox1.DisplayMember = "RegistrationStatus";
            comboBox1.ValueMember = "RegistrationStatusId";

            SqlConnection dlc = new SqlConnection(rmcsb.ConnectionString);
            SqlCommand dlcom = dlc.CreateCommand();
            dlcom.CommandText = "SELECT * FROM Event WHERE (EventId = '15_5FR') OR (EventId = '15_5FM') OR (EventId = '15_5HM')";
            SqlDataAdapter dlda = new SqlDataAdapter();
            dlda.SelectCommand = dlcom;
            DataTable dldt = new DataTable();
            dlda.Fill(dldt);
            comboBox2.DataSource = dldt;
            comboBox2.DisplayMember = "EventName";
            comboBox2.ValueMember = "EventId";

            comboBox3.SelectedIndex = 0;

            button2_Click(button2, new EventArgs());
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

        private void _22_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms[Application.OpenForms.Count - 2].Visible = true;
            Dispose();
        }

        private void marathonSkills2016BasePanel1_BackButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder rmcsb = new SqlConnectionStringBuilder();
            rmcsb.DataSource = @"localhost";
            rmcsb.InitialCatalog = "MarathonSkills2016";
            rmcsb.IntegratedSecurity = true;
            rmcsb.ConnectTimeout = 30;

            SqlConnection rlc = new SqlConnection(rmcsb.ConnectionString);
            SqlCommand rlcom = rlc.CreateCommand();
            rlcom.CommandText = @"SELECT Runner.RunnerId, [User].FirstName, [User].LastName, 
		[User].Email, RegistrationStatus.RegistrationStatus FROM
		RegistrationEvent inner join (Registration inner join RegistrationStatus on Registration.RegistrationStatusId = RegistrationStatus.RegistrationStatusId) inner join Runner inner join [user]
		on ([User].Email = Runner.Email) AND ([User].RoleId = 'R') on Runner.RunnerId = Registration.RunnerId on
		(Registration.RegistrationId = RegistrationEvent.RegistrationId) AND
		(RegistrationEvent.EventId = '"+comboBox2.SelectedValue+@"') AND (Registration.RegistrationStatusId = "+comboBox1.SelectedValue+@")";
            SqlDataAdapter rlda = new SqlDataAdapter();
            rlda.SelectCommand = rlcom;
            rldt = new DataTable();
            rlda.Fill(rldt);

            dataGridView1.Columns.Clear();

            DataGridViewColumn dgvn = new DataGridViewColumn();
            dgvn.Name = "Name";
            dgvn.HeaderText = "Имя";
            dgvn.CellTemplate = new DataGridViewTextBoxCell();
            DataGridViewColumn dgvd = new DataGridViewColumn();
            dgvd.Name = "Desc";
            dgvd.HeaderText = "Фамилия";
            dgvd.CellTemplate = new DataGridViewTextBoxCell();
            DataGridViewColumn dgvd2 = new DataGridViewColumn();
            dgvd2.Name = "Email";
            dgvd2.HeaderText = "Email";
            dgvd2.CellTemplate = new DataGridViewTextBoxCell();
            DataGridViewColumn dgvd3 = new DataGridViewColumn();
            dgvd3.Name = "Status";
            dgvd3.HeaderText = "Статус";
            dgvd3.CellTemplate = new DataGridViewTextBoxCell();
            DataGridViewButtonColumn dgvb = new DataGridViewButtonColumn();
            dgvb.FlatStyle = FlatStyle.Flat;
            dgvb.Name = "Button";
            dgvb.HeaderText = "";
            dgvb.Text = "Править";

            dataGridView1.Columns.Add(dgvn);
            dataGridView1.Columns.Add(dgvd);
            dataGridView1.Columns.Add(dgvd2);
            dataGridView1.Columns.Add(dgvd3);
            dataGridView1.Columns.Add(dgvb);

            label6.Text = Convert.ToString(rldt.Rows.Count);

            foreach (DataRow row in rldt.Rows)
            {
                dataGridView1.Rows.Add();
                dataGridView1["Name", dataGridView1.Rows.Count - 1].Value = row["FirstName"];
                dataGridView1["Desc", dataGridView1.Rows.Count - 1].Value = row["LastName"];
                dataGridView1["Email", dataGridView1.Rows.Count - 1].Value = row["Email"];
                dataGridView1["Status", dataGridView1.Rows.Count - 1].Value = row["RegistrationStatus"];
                dataGridView1["Button", dataGridView1.Rows.Count - 1].Value = "Edit";
            }

            dataGridView1.Sort(dataGridView1.Columns[comboBox3.SelectedIndex], ListSortDirection.Ascending);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                int runnerid = 0;
                MessageBox.Show(Convert.ToString(dataGridView1["Email", e.RowIndex]));
                foreach (DataRow row in rldt.Rows)
                {
                    if (row["Email"] == dataGridView1["Email", e.RowIndex].Value)
                    {
                        runnerid = Convert.ToInt32(row["RunnerId"]);
                    }
                    
                }
                MessageBox.Show(Convert.ToString(runnerid));
                _23 f23 = new _23(runnerid);
                f23.Show();
                Dispose();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (rldt.Rows.Count > 0)
            {
                List<string> csvcontent = new List<string>();
                csvcontent.Add("FirstName,LastName,Email,Gender,CountryName,DateOfBirth,Age,RegistrationStatusId,EventName,RaceKitOption");
                foreach (DataRow row in rldt.Rows)
                {
                    SqlConnectionStringBuilder rmcsb = new SqlConnectionStringBuilder();
                    rmcsb.DataSource = @"localhost";
                    rmcsb.InitialCatalog = "MarathonSkills2016";
                    rmcsb.IntegratedSecurity = true;
                    rmcsb.ConnectTimeout = 30;

                    SqlConnection ecc = new SqlConnection(rmcsb.ConnectionString);
                    SqlCommand eccom = ecc.CreateCommand();
                    eccom.CommandText = @"SELECT DISTINCT Runner.RunnerId, 
       [user].FirstName,

       [user].LastName,
	   [user].Email,
	   Runner.Gender,
	   Country.CountryName,
	   Runner.DateOfBirth,

       DATEDIFF(YEAR, Runner.DateOfBirth, GETDATE()) As 'Age',
	   RegistrationStatus.RegistrationStatus,
	   [Event].EventName,
	   RaceKitOption.RaceKitOption
       FROM
       (RegistrationEvent inner join[Event] on  (RegistrationEvent.EventId = [Event].EventId) AND(([Event].EventId = '15_5FM') OR([Event].EventId = '15_5FR') OR([Event].EventId = '15_5HM')))
	   inner join((Registration inner join RegistrationStatus on (RegistrationStatus.RegistrationStatusId = Registration.RegistrationStatusId)) inner join RaceKitOption on Registration.RaceKitOptionId = RaceKitOption.RaceKitOptionId)
	   inner join(Runner inner join Country on Runner.CountryCode = Country.CountryCode)

       inner join[User] on ([User].RoleId = 'R') AND ([User].Email = Runner.Email) on(Runner.RunnerId = Registration.RunnerId) AND(Runner.RunnerId = "+Convert.ToString(row["RunnerId"])+@") on RegistrationEvent.RegistrationId = Registration.RegistrationId";
                    SqlDataAdapter ecda = new SqlDataAdapter();
                    ecda.SelectCommand = eccom;
                    DataTable ecdt = new DataTable();
                    ecda.Fill(ecdt);
                    string Mars = "";
                    for (int i = 0; i < ecdt.Rows.Count; i++)
                    {
                        if (i != ecdt.Rows.Count - 1)
                        {
                            Mars += ecdt.Rows[i]["EventName"] + ",";
                        }
                        else
                        {
                            Mars += ecdt.Rows[i]["EventName"];
                        }
                    }
                    string res = ecdt.Rows[0]["FirstName"] + "," + ecdt.Rows[0]["LastName"] + "," + ecdt.Rows[0]["Email"] + "," + ecdt.Rows[0]["Gender"] +
                        "," + ecdt.Rows[0]["CountryName"] + "," + ecdt.Rows[0]["DateOfBirth"] + "," + ecdt.Rows[0]["Age"] + "," +
                        ecdt.Rows[0]["RegistrationStatus"] + ",\"" + Mars + "\"," + ecdt.Rows[0]["RaceKitOption"];
                    csvcontent.Add(res);
                }

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Poop";
                sfd.Filter = "CSV|*.csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter writer = new StreamWriter(sfd.FileName);
                    foreach(string data in csvcontent)
                    {
                        writer.WriteLine(data);
                    }
                    writer.Close();
                    MessageBox.Show("Экспорт данных успешно произведен", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("В текущей выборке отсуствуют записи!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> poop = new List<string>();
            foreach (DataRow row in rldt.Rows)
            {
                string tmp = "\""+row["FirstName"] + " " + row["LastName"] + "\" " + row["Email"];
                poop.Add(tmp);
            }
            _22_1 f221 = new _22_1(poop);
            f221.Show();
            Visible = false;
        }
    }
}
