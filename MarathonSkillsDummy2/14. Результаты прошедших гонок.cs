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
    public partial class _14 : Form
    {
        DataTable rfldt;
        public DataTable LoadGender()
        {
            SqlConnectionStringBuilder gcsb = new SqlConnectionStringBuilder();
            gcsb.DataSource = @"localhost";
            gcsb.InitialCatalog = "MarathonSkills2016";
            gcsb.IntegratedSecurity = true;
            gcsb.ConnectTimeout = 30;
            SqlConnection gc = new SqlConnection(gcsb.ConnectionString);
            SqlCommand gcom = gc.CreateCommand();
            gcom.CommandText = "SELECT * FROM Gender";
            SqlDataAdapter gda = new SqlDataAdapter();
            gda.SelectCommand = gcom;
            DataTable gdt = new DataTable();
            gda.Fill(gdt);
            return gdt;
        }

        public DataTable LoadMarathons()
        {
            SqlConnectionStringBuilder mtcsb = new SqlConnectionStringBuilder();
            mtcsb.DataSource = @"localhost";
            mtcsb.InitialCatalog = "MarathonSkills2016";
            mtcsb.IntegratedSecurity = true;
            mtcsb.ConnectTimeout = 30;
            SqlConnection mtc = new SqlConnection(mtcsb.ConnectionString);
            SqlCommand mtcon = mtc.CreateCommand();
            mtcon.CommandText = "SELECT Marathon.YearHeld, CONCAT(Marathon.YearHeld, ' - ',  Country.CountryName) As 'MarathonComplex' FROM Marathon inner join Country on (Country.CountryCode = Marathon.CountryCode) AND (Marathon.YearHeld != 2015)";
            SqlDataAdapter mtda = new SqlDataAdapter();
            mtda.SelectCommand = mtcon;
            DataTable mtdt = new DataTable();
            mtda.Fill(mtdt);
            return mtdt;
        }  
        
        public DataTable LoadDistance(int year)
        {
            SqlConnectionStringBuilder dtcsb = new SqlConnectionStringBuilder();
            dtcsb.DataSource = @"localhost";
            dtcsb.InitialCatalog = "MarathonSkills2016";
            dtcsb.IntegratedSecurity = true;
            dtcsb.ConnectTimeout = 30;
            SqlConnection dtc = new SqlConnection(dtcsb.ConnectionString);
            SqlCommand dtcom = dtc.CreateCommand();
            dtcom.CommandText = "SELECT [Event].EventId, [Event].EventName FROM [Event] inner join Marathon on ([Event].MarathonId = [Marathon].MarathonId) AND (Marathon.YearHeld = " + Convert.ToString(year) + ")";
            SqlDataAdapter dtda = new SqlDataAdapter();
            dtda.SelectCommand = dtcom;
            DataTable dtdt = new DataTable();
            dtda.Fill(dtdt);
            return dtdt;
        }     

        public _14()
        {
            InitializeComponent();
            DataTable mt = LoadMarathons();
            comboBox1.DataSource = mt;
            comboBox1.DisplayMember = "MarathonComplex";
            comboBox1.ValueMember = "YearHeld";
            DataTable gt = LoadGender();
            comboBox4.DataSource = gt;
            comboBox4.DisplayMember = "Gender";
            comboBox4.ValueMember = "Gender";
            comboBox3.SelectedIndex = 0;
            button3_Click(this, new EventArgs());
        }

        private void _14_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms[Application.OpenForms.Count - 2].Visible = true;
            Dispose();

        }

        private void marathonSkills2016BasePanel1_BackButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            DataTable dt = LoadDistance(Convert.ToInt32((comboBox1.DataSource as DataTable).Rows[comboBox1.SelectedIndex]["YearHeld"]));
            comboBox2.DataSource = dt;
            comboBox2.ValueMember = "EventId";
            comboBox2.DisplayMember = "EventName";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string EvId = Convert.ToString((comboBox2.DataSource as DataTable).Rows[comboBox2.SelectedIndex]["EventId"]);
            string Gen = Convert.ToString((comboBox4.DataSource as DataTable).Rows[comboBox4.SelectedIndex]["Gender"]);
            string minage = "0", maxage = "2000";
            switch (comboBox3.SelectedIndex)
            {
                case 0:
                     {
                        minage = "9"; maxage = "18";
                     }
                    break;
                case 1:
                    {
                        minage = "17"; maxage = "30"; 
                    }
                    break;
                case 2:
                    {
                        minage = "29"; maxage = "40";
                    }
                    break;
                case 3:
                    {
                        minage = "39"; maxage = "56";
                    }
                    break;
                case 4:
                    {
                        minage = "55"; maxage = "71";
                    }
                    break;
                case 5:
                    {
                        minage = "69"; maxage = "1000";
                    }
                    break;
            }

            SqlConnectionStringBuilder rflcsb = new SqlConnectionStringBuilder();
            rflcsb.DataSource = @"localhost";
            rflcsb.InitialCatalog = "MarathonSkills2016";
            rflcsb.IntegratedSecurity = true;
            rflcsb.ConnectTimeout = 30;
            SqlConnection rflc = new SqlConnection(rflcsb.ConnectionString);
            
            SqlCommand rflcom = rflc.CreateCommand();
            rflcom.CommandText = @"SELECT Registration.RegistrationId, RegistrationEvent.RaceTime, 

        CONCAT([User].FirstName, ' ', [User].LastName) As 'RunnerName', 
		Country.CountryCode FROM RegistrationEvent inner join Registration inner join
        (Runner inner join Country on (Country.CountryCode = Runner.CountryCode)

        AND(DATEDIFF(YEAR, Runner.DateOfBirth, GETDATE()) > " + minage + @")

        AND(DATEDIFF(YEAR, Runner.DateOfBirth, GETDATE()) < " + maxage + @")) 
		inner join[User] on([User].Email = Runner.Email) And ([User].RoleId = 'R') And(Runner.Gender = '" + Gen + @"')

        on(Runner.RunnerId = Registration.RunnerId)  on
       (Registration.RegistrationId = RegistrationEvent.RegistrationId)

        AND(RegistrationEvent.EventId = '" + EvId + @"')

        ORDER BY RegistrationEvent.RaceTime";

            SqlDataAdapter rflda = new SqlDataAdapter();
            rflda.SelectCommand = rflcom;
            rfldt = new DataTable();
            rflda.Fill(rfldt);
            int arsc = rfldt.Rows.Count;
            rfldt.Clear();

            rflcom.CommandText = @"SELECT Registration.RegistrationId, RegistrationEvent.RaceTime, 

        CONCAT([User].FirstName, ' ', [User].LastName) As 'RunnerName', 
		Country.CountryCode FROM RegistrationEvent inner join Registration inner join
        (Runner inner join Country on (Country.CountryCode = Runner.CountryCode)

        AND(DATEDIFF(YEAR, Runner.DateOfBirth, GETDATE()) > " + minage +@")

        AND(DATEDIFF(YEAR, Runner.DateOfBirth, GETDATE()) < " + maxage +@")) 
		inner join[User] on([User].Email = Runner.Email) And(Runner.Gender = '"+Gen+@"')

        on(Runner.RunnerId = Registration.RunnerId)  on
       (Registration.RegistrationId = RegistrationEvent.RegistrationId)

        AND(RegistrationEvent.EventId = '"+EvId+@"') AND
       (RegistrationEvent.RaceTime IS NOT NULL) AND(RegistrationEvent.RaceTime != 0)

        ORDER BY RegistrationEvent.RaceTime";

            rflda.SelectCommand = rflcom;
            rflda.Fill(rfldt);
            if (rfldt.Rows.Count < 1)
            {
                MessageBox.Show("Nothing Was Found!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dataGridView1.Columns.Clear();
                label7.Text = "";
                label10.Text = "";
                label11.Text = "";
                return;
            }
            label7.Text = Convert.ToString(arsc);
            label10.Text = Convert.ToString(rfldt.Rows.Count);
            DataTable rfl = new DataTable();
            DataColumn rpos = new DataColumn("Место", typeof(int));
            DataColumn rtim = new DataColumn("Время", typeof(string));
            DataColumn rnam = new DataColumn("Имя бегуна", typeof(string));
            DataColumn rccd = new DataColumn("Страна", typeof(string));
            rfl.Columns.Add(rpos);
            rfl.Columns.Add(rtim);
            rfl.Columns.Add(rnam);
            rfl.Columns.Add(rccd);
            int runnerpos = 1;
            DataRow rr = rfl.NewRow();
            rr["Место"] = runnerpos;
            TimeSpan span = new TimeSpan(0, 0, (int)rfldt.Rows[0]["RaceTime"]);
            int rtime = (int)rfldt.Rows[0]["RaceTime"];
            string runnertime = Convert.ToString(span.Hours) + "h. " + Convert.ToString(span.Minutes) + "m. " + Convert.ToInt32(span.Seconds) + "s. ";
            rr["Время"] = runnertime;
            rr["Имя бегуна"] = rfldt.Rows[0]["RunnerName"] as string;
            rr["Страна"] = rfldt.Rows[0]["CountryCode"] as string;
            rfl.Rows.Add(rr);
            for (int i = 1; i < rfldt.Rows.Count; i++)
            {
                rr = rfl.NewRow();
                if ((int)rfldt.Rows[i - 1]["RaceTime"] != (int)rfldt.Rows[i]["RaceTime"]) runnerpos++;
                rr["Место"] = runnerpos;
                TimeSpan span2 = new TimeSpan(0, 0, (int)rfldt.Rows[i]["RaceTime"]);
                rtime = rtime += (int)rfldt.Rows[i]["RaceTime"];
                string runnertime2 = Convert.ToString(span2.Hours) + "h. " + Convert.ToString(span2.Minutes) + "m. " + Convert.ToInt32(span2.Seconds) + "s.";
                rr["Время"] = runnertime2;
                rr["Имя бегуна"] = rfldt.Rows[i]["RunnerName"] as string;
                rr["Страна"] = rfldt.Rows[i]["CountryCode"] as string;
                rfl.Rows.Add(rr);
            }
            int rmid = rtime / rfldt.Rows.Count;
            TimeSpan atime = new TimeSpan(0, 0, rmid);
            label11.Text = Convert.ToString(atime.Hours) + "h. " + Convert.ToString(atime.Minutes) + "m. " + Convert.ToString(atime.Seconds) + "s. ";
            dataGridView1.DataSource = rfl;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int crid = (int)rfldt.Rows[dataGridView1.SelectedRows[0].Index]["RegistrationId"];
            _14_1 f141 = new _14_1(crid);
            Visible = false;
            f141.Show();
        }
    }
}
