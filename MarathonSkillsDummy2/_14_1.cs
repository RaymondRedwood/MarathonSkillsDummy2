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
    public partial class _14_1 : Form
    {
        int rid2 = 0;
        public _14_1(int rid)
        {
            InitializeComponent();
            //MessageBox.Show(Convert.ToString(rid));
            rid2 = rid;
            SqlConnectionStringBuilder ricsb = new SqlConnectionStringBuilder();
            ricsb.DataSource = @"localhost";
            ricsb.InitialCatalog = "MarathonSkills2016";
            ricsb.IntegratedSecurity = true;
            ricsb.ConnectTimeout = 30;
            SqlConnection ric = new SqlConnection(ricsb.ConnectionString);
            SqlCommand ricom = ric.CreateCommand();
            ricom.CommandText = @"SELECT Runner.RunnerId, CONCAT([User].FirstName, ' ' , [User].LastName) As 'RunnerName',
                                    Country.CountryName, DATEDIFF(YEAR, Runner.DateOfBirth, GETDATE()) As 'RunnerAge',
                                    Runner.Photo FROM Registration
                                    inner join (Runner inner join Country on Runner.CountryCode = Country.CountryCode)
                                    inner join [User] on [User].Email = Runner.Email on 
                                    (Runner.RunnerId = Registration.RunnerId) AND (Registration.RegistrationId = " +
                                    rid + @")";
            SqlDataAdapter rida = new SqlDataAdapter();
            rida.SelectCommand = ricom;
            DataTable ridt = new DataTable();
            rida.Fill(ridt);
            //MessageBox.Show(Convert.ToString(ridt.Rows.Count));
            label6.Text = Convert.ToString(ridt.Rows[0]["RunnerAge"]);
            label5.Text = ridt.Rows[0]["CountryName"] as string;
            label12.Text = ridt.Rows[0]["RunnerName"] as string;
            if (!(ridt.Rows[0]["Photo"] == DBNull.Value) & !(ridt.Rows[0]["Photo"] as string == ""))
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\RunnerPhoto\" + (string)ridt.Rows[0]["Photo"]);
            }

            SqlConnectionStringBuilder mcsb = new SqlConnectionStringBuilder();
            mcsb.DataSource = @"localhost";
            mcsb.InitialCatalog = @"MarathonSkills2016";
            mcsb.IntegratedSecurity = true;
            mcsb.ConnectTimeout = 30;

            SqlConnection ruidc = new SqlConnection(mcsb.ConnectionString);
            SqlCommand ruidcom = ruidc.CreateCommand();
            ruidcom.CommandText = "SELECT Registration.RegistrationId From Registration WHERE Registration.RunnerId = " 
                                    +  Convert.ToString(ridt.Rows[0]["RunnerId"]);
            SqlDataAdapter ruidda = new SqlDataAdapter();
            ruidda.SelectCommand = ruidcom;
            DataTable ruiddt = new DataTable();
            ruidda.Fill(ruiddt);

            SqlConnection mc = new SqlConnection(mcsb.ConnectionString);
            SqlCommand mcom = mc.CreateCommand();
            mcom.CommandText = "SELECT * FROM Marathon WHERE Marathon.YearHeld != 2015";
            SqlDataAdapter mda = new SqlDataAdapter();
            mda.SelectCommand = mcom;
            DataTable mdt = new DataTable();
            mda.Fill(mdt);

            DataTable rr = new DataTable();
            DataColumn rr1 = new DataColumn("Место", typeof(int));
            DataColumn rr2 = new DataColumn("Время", typeof(string));
            DataColumn rr3 = new DataColumn("Событие", typeof(string));
            DataColumn rr4 = new DataColumn("Марафон", typeof(string));
            rr.Columns.AddRange(new DataColumn[] { rr1, rr2, rr3, rr4 });

            for (int h = 0; h < ruiddt.Rows.Count; h++)
            {
                for (int i = 0; i < mdt.Rows.Count; i++)
                {
                    SqlConnection ec = new SqlConnection(mcsb.ConnectionString);
                    SqlCommand ecom = ec.CreateCommand();
                    string eid = Convert.ToString(mdt.Rows[i]["MarathonId"]);
                    ecom.CommandText = "SELECT * From Event WHERE Event.MarathonId = " + eid;
                    SqlDataAdapter eda = new SqlDataAdapter();
                    eda.SelectCommand = ecom;
                    DataTable edt = new DataTable();
                    eda.Fill(edt);
                    for (int j = 0; j < edt.Rows.Count; j++)
                    {
                        SqlConnection rc = new SqlConnection(mcsb.ConnectionString);
                        SqlCommand rcom = rc.CreateCommand();
                        rcom.CommandText = @"SELECT Registration.RegistrationId, 
       RegistrationEvent.RaceTime, 
	   Event.EventName, 
	   CONCAT(Marathon.YearHeld, ' ', Marathon.CityName) As 'MarathonName' 
	   FROM
      (RegistrationEvent inner join 
	  (Event inner join Marathon on (Marathon.MarathonId = Event.MarathonId) AND 
	  (Event.EventId = '" + edt.Rows[j]["EventId"] + @"')) on 
	  RegistrationEvent.EventId = Event.EventId) 
      inner join Registration on (Registration.RegistrationId = 
      RegistrationEvent.RegistrationId) AND (RegistrationEvent.RaceTime IS NOT NULL) 
	  AND (RegistrationEvent.RaceTime != 0) ORDER BY RegistrationEvent.RaceTime;";
                        SqlDataAdapter rda = new SqlDataAdapter();
                        rda.SelectCommand = rcom;
                        DataTable rdt = new DataTable();
                        rda.Fill(rdt);
                        int place = 1;
                        //rdt.Rows[0]["Place"] = place;
                        if ((int)rdt.Rows[0]["RegistrationId"] == (int)ruiddt.Rows[h]["RegistrationId"])
                        {
                            DataRow fr = rr.NewRow();
                            fr["Место"] = place;
                            TimeSpan sp = new TimeSpan(0, 0, (int)rdt.Rows[0]["RaceTime"]);
                            fr["Время"] = Convert.ToString(sp.Hours) + "h. " + Convert.ToString(sp.Minutes) + "m. " +
                                            Convert.ToString(sp.Seconds) + "s.";
                            fr["Событие"] = rdt.Rows[0]["EventName"];
                            fr["Марафон"] = rdt.Rows[0]["MarathonName"];
                            rr.Rows.Add(fr);
                        }
                        else
                        {
                            for (int k = 1; k < rdt.Rows.Count; k++)
                            {
                                if ((int)rdt.Rows[k - 1]["RaceTime"] != (int)rdt.Rows[k]["RaceTime"]) place++;
                                //rdt.Rows[0]["Place"] = place;
                                if ((int)rdt.Rows[k]["RegistrationId"] == (int)ruiddt.Rows[h]["RegistrationId"])
                                {
                                    DataRow fr = rr.NewRow();
                                    fr["Место"] = place;
                                    TimeSpan sp = new TimeSpan(0, 0, (int)rdt.Rows[k]["RaceTime"]);
                                    fr["Время"] = Convert.ToString(sp.Hours) + "h. " + Convert.ToString(sp.Minutes) + "m. " +
                                                    Convert.ToString(sp.Seconds) + "s.";
                                    fr["Событие"] = rdt.Rows[k]["EventName"];
                                    fr["Марафон"] = rdt.Rows[k]["MarathonName"];
                                    rr.Rows.Add(fr);
                                }
                            }
                        }
                        rdt.Clear();
                    }
                    edt.Clear();
                }

            }

            dataGridView1.DataSource = rr;
        }

        private void _14_1_FormClosing(object sender, FormClosingEventArgs e)
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
