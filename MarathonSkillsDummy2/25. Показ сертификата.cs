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
    public partial class _25 : Form
    {
        private int rid;

        private int koo = 0;
        private bool foundsmth = false;
        private bool zummer = true;

        public _25(int runnerid)
        {
            InitializeComponent();

            rid = runnerid;

            SqlConnectionStringBuilder rmcsb = new SqlConnectionStringBuilder();
            rmcsb.DataSource = @"localhost";
            rmcsb.InitialCatalog = "MarathonSkills2016";
            rmcsb.IntegratedSecurity = true;
            rmcsb.ConnectTimeout = 30;

            SqlConnection ec = new SqlConnection(rmcsb.ConnectionString);
            SqlCommand ecom = ec.CreateCommand();
            ecom.CommandText = "SELECT * FROM [Event] WHERE (([Event].EventId = '14_4FR') OR ([Event].EventId = '14_4FM') OR ([Event].EventId = '14_4HM'))";
            SqlDataAdapter eda = new SqlDataAdapter();
            eda.SelectCommand = ecom;
            DataTable edt = new DataTable();
            eda.Fill(edt);

            comboBox3.DataSource = edt;
            comboBox3.DisplayMember = "EventName";
            comboBox3.ValueMember = "EventId";


            foreach (DataRow row in edt.Rows)
            {
                comboBox3.SelectedValue = row["EventId"];
                zummer = false;
                button1_Click(new object(), new EventArgs());
                if (foundsmth == true) return;
            }

            if (koo > 2)
            {
                MessageBox.Show("Ни одно из мероприятий недоступно!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Close();
            }
        }

        private void _25_FormClosing(object sender, FormClosingEventArgs e)
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
            SqlConnectionStringBuilder rmcsb = new SqlConnectionStringBuilder();
            rmcsb.DataSource = @"localhost";
            rmcsb.InitialCatalog = "MarathonSkills2016";
            rmcsb.IntegratedSecurity = true;
            rmcsb.ConnectTimeout = 30;

            SqlConnection rcic = new SqlConnection(rmcsb.ConnectionString);
            SqlCommand rcicom = rcic.CreateCommand();
            rcicom.CommandText = @"SELECT Runner.RunnerId,
    Registration.RegistrationId,
    CONCAT([User].FirstName, ' ', [User].LastName) As 'UserName',
	[Event].EventName,
	RegistrationEvent.RaceTime,
	Charity.CharityName
    FROM (RegistrationEvent inner join [Event] on [Event].EventId = RegistrationEvent.EventId)
inner join (Registration inner join Charity on Charity.CharityId = Registration.CharityId)
inner join Runner
inner join [User] on Runner.Email = [User].Email on Registration.RunnerId = Runner.RunnerId on
(Registration.RegistrationId = RegistrationEvent.RegistrationId) AND
((RegistrationEvent.EventId = '"+comboBox3.SelectedValue+@"') AND (RegistrationEvent.RaceTime IS NOT NULL) AND (RegistrationEvent.RaceTime != 0))
ORDER BY RegistrationEvent.RaceTime";

            SqlDataAdapter rcida = new SqlDataAdapter(rcicom);
            DataTable rcidt = new DataTable();
            rcida.Fill(rcidt);

            int stpos = 1;
            List<int> pos = new List<int>();
            pos.Add(stpos);
            for (int i = 1; i < rcidt.Rows.Count; i++)
            {
                if (rcidt.Rows[i - 1]["RaceTime"] != rcidt.Rows[i]["RaceTime"]) stpos++;
                pos.Add(stpos);
                if ((int)rcidt.Rows[i]["RunnerId"] == rid)
                {
                    foundsmth = true;
                    //MessageBox.Show("Hey, i'm here!");
                    label7.Text = (string)rcidt.Rows[i]["UserName"];
                    label7.Location = new Point(324 - (label7.ClientSize.Width / 2), label7.Location.Y);
                    label6.Text = (string)rcidt.Rows[i]["EventName"];
                    TimeSpan ts = new TimeSpan(0, 0, (int)rcidt.Rows[i]["RaceTime"]);
                    label13.Text = Convert.ToString(ts.Hours) + "h. " + Convert.ToString(ts.Minutes) + "m. " + Convert.ToString(ts.Seconds) + "s";
                    label14.Text = Convert.ToString(stpos);
                    label15.Text = Convert.ToString((string)rcidt.Rows[i]["CharityName"]);

                    decimal sum = 0;

                    SqlConnection sac = new SqlConnection(rmcsb.ConnectionString);
                    SqlCommand sacom = sac.CreateCommand();
                    sacom.CommandText = "SELECT * FROM Registration";
                    SqlDataAdapter sada = new SqlDataAdapter(sacom);
                    DataTable sadt = new DataTable();
                    sada.Fill(sadt);

                    foreach (DataRow row in sadt.Rows)
                    {
                        if (row["RegistrationId"] == rcidt.Rows[i]["RegistrationId"])
                        {
                            sum += (decimal)row["Amount"]; 
                        }
                    }

                    label16.Text = "$" + Convert.ToString(Math.Round(sum, 2));
                    return;
                }
            }

            if (zummer == true) MessageBox.Show("Мероприятие недоступно!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            zummer = true;
            koo++;
        }
    }
}
