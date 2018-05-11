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
    public partial class _23 : Form
    {
        int rid;
        public _23(int RunnerId)
        {
            InitializeComponent();

            rid = RunnerId;

            SqlConnectionStringBuilder rmcsb = new SqlConnectionStringBuilder();
            rmcsb.DataSource = @"localhost";
            rmcsb.InitialCatalog = "MarathonSkills2016";
            rmcsb.IntegratedSecurity = true;
            rmcsb.ConnectTimeout = 30;

            SqlConnection ric = new SqlConnection(rmcsb.ConnectionString);
            SqlCommand ricom = ric.CreateCommand();
            ricom.CommandText = @"SELECT [User].Email,
		[User].FirstName,
		[User].LastName,
		Runner.Gender,
		Runner.DateOfBirth,
		Country.CountryName,
        Country.CountryFlag,
		Charity.CharityName,
		Registration.SponsorshipTarget,
		RaceKitOption.RaceKitOption,
		[Event].EventName,
		Runner.Photo,
		Registration.RegistrationStatusId FROM
		(RegistrationEvent inner join [Event] on RegistrationEvent.EventId = [Event].EventId) 
inner join ((Registration inner join Charity on Registration.CharityId = Charity.CharityId) inner join RaceKitOption on Registration.RaceKitOptionId = RaceKitOption.RaceKitOptionId)
inner join (Runner inner join Country on Runner.CountryCode = Country.CountryCode)
inner join [User]
		on [User].Email = Runner.Email
		on (Runner.RunnerId = Registration.RunnerId) AND (Runner.RunnerId = "+RunnerId+@")
		on (Registration.RegistrationId = RegistrationEvent.RegistrationId)
			AND ((RegistrationEvent.EventId = '15_5FR') OR (RegistrationEvent.EventId = '15_5FM') OR (RegistrationEvent.EventId = '15_5HM'))";
            SqlDataAdapter rida = new SqlDataAdapter();
            rida.SelectCommand = ricom;
            DataTable ridt = new DataTable();
            rida.Fill(ridt);

            label12.Text = Convert.ToString(ridt.Rows[0]["Email"]);
            label13.Text = Convert.ToString(ridt.Rows[0]["FirstName"]);
            RunnerBib.UserName = Convert.ToString(ridt.Rows[0]["FirstName"]) + " " + Convert.ToString(ridt.Rows[0]["LastName"]);
            label14.Text = Convert.ToString(ridt.Rows[0]["LastName"]);
            label15.Text = Convert.ToString(ridt.Rows[0]["Gender"]);
            label16.Text = Convert.ToString(ridt.Rows[0]["DateOfBirth"]);
            label17.Text = Convert.ToString(ridt.Rows[0]["CountryName"]);
            RunnerBib.CountryName = Convert.ToString(ridt.Rows[0]["CountryName"]);
            RunnerBib.CountryFlag = Convert.ToString(ridt.Rows[0]["CountryFlag"]);
            RunnerBib.CharityName = Convert.ToString(ridt.Rows[0]["CharityName"]);
            RunnerBib.RunnerPhoto = Convert.ToString(ridt.Rows[0]["Photo"]);
            label18.Text = Convert.ToString(ridt.Rows[0]["CharityName"]);
            label19.Text = Convert.ToString(ridt.Rows[0]["SponsorshipTarget"]);
            label20.Text = Convert.ToString(ridt.Rows[0]["RaceKitOption"]);

            label21.Text = "";

            foreach (DataRow row in ridt.Rows)
            {
                label21.Text += row["EventName"] + "\n";
            }

            RunnerBib.EventsName = label21.Text;

            if (!(ridt.Rows[0]["Photo"] == DBNull.Value) & !(ridt.Rows[0]["Photo"] as string == ""))
            {
                pictureBox6.Image = Image.FromFile(Application.StartupPath + @"\RunnerPhoto\" + (string)ridt.Rows[0]["Photo"]);
            }

            if (Convert.ToInt32(ridt.Rows[0]["RegistrationStatusId"]) > 0)
            {
                pictureBox2.Image = Image.FromFile(Application.StartupPath + @"\rrstatus\tick-icon.png");
            }
            if (Convert.ToInt32(ridt.Rows[0]["RegistrationStatusId"]) > 1)
            {
                pictureBox3.Image = Image.FromFile(Application.StartupPath + @"\rrstatus\tick-icon.png");
            }
            if (Convert.ToInt32(ridt.Rows[0]["RegistrationStatusId"]) > 2)
            {
                pictureBox4.Image = Image.FromFile(Application.StartupPath + @"\rrstatus\tick-icon.png");
            }
            if (Convert.ToInt32(ridt.Rows[0]["RegistrationStatusId"]) > 3)
            {
                pictureBox5.Image = Image.FromFile(Application.StartupPath + @"\rrstatus\tick-icon.png");
            }
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

        private void _23_FormClosing(object sender, FormClosingEventArgs e)
        {
            _22 f22 = new _22();
            f22.Show();
            Dispose();
        }

        private void marathonSkills2016BasePanel1_BackButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _24 f24 = new MarathonSkillsDummy2._24(rid);
            f24.Show();
            Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _25 f25 = new MarathonSkillsDummy2._25(rid);
            if (!f25.IsDisposed)
            { 
                f25.Show();
                Visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _23_1 f231 = new _23_1();
            f231.Show();
            Visible = false;
        }
    }
}
