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
    public partial class _17 : Form
    {
        public _17()
        {
            InitializeComponent();
            label7.Text = RunnerCurrentContext.Gender;
            int age = DateTime.Now.Year - RunnerCurrentContext.DOB.Year;
            int minage = 0;
            int maxage = 2000;
         
            if (Enumerable.Range(10, 17).Contains(age))
            {
                minage = 10;
                maxage = 17;
                label4.Text = Convert.ToString(maxage) + "-";
            }
            if (Enumerable.Range(18, 29).Contains(age))
            {
                minage = 18;
                maxage = 29;
                label4.Text = Convert.ToString(minage) + " - " + Convert.ToString(maxage);
            }
            if (Enumerable.Range(30, 39).Contains(age))
            {
                minage = 30;
                maxage = 39;
                label4.Text = Convert.ToString(minage) + " - " + Convert.ToString(maxage);
            }
            if (Enumerable.Range(40, 55).Contains(age))
            {
                minage = 40;
                maxage = 55;
                label4.Text = Convert.ToString(minage) + " - " + Convert.ToString(maxage);
            }
            if (Enumerable.Range(56, 70).Contains(age))
            {
                minage = 56;
                maxage = 70;
                label4.Text = Convert.ToString(minage) + " - " + Convert.ToString(maxage);
            }
            if (Enumerable.Range(71, 2000).Contains(age))
            {
                minage = 71;
                maxage = 2000;
                label4.Text = Convert.ToString(minage) + "+"; 
            }

            SqlConnectionStringBuilder mcsb = new SqlConnectionStringBuilder();
            mcsb.DataSource = @"localhost";
            mcsb.InitialCatalog = @"MarathonSkills2016";
            mcsb.IntegratedSecurity = true;
            mcsb.ConnectTimeout = 30;

            SqlConnection ruidc = new SqlConnection(mcsb.ConnectionString);
            SqlCommand ruidcom = ruidc.CreateCommand();
            ruidcom.CommandText = "SELECT Registration.RegistrationId From Registration WHERE Registration.RunnerId = "
                                    + Convert.ToString(RunnerCurrentContext.RunnerId);
            SqlDataAdapter ruidda = new SqlDataAdapter();
            ruidda.SelectCommand = ruidcom;
            DataTable ruiddt = new DataTable();
            ruidda.Fill(ruiddt);

            if (ruiddt.Rows.Count < 1)
            {
                MessageBox.Show("Вы никогда не соревновались прежде!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            SqlConnection mc = new SqlConnection(mcsb.ConnectionString);
            SqlCommand mcom = mc.CreateCommand();
            mcom.CommandText = "SELECT * FROM Marathon WHERE Marathon.YearHeld != 2015";
            SqlDataAdapter mda = new SqlDataAdapter();
            mda.SelectCommand = mcom;
            DataTable mdt = new DataTable();
            mda.Fill(mdt);

            DataTable rr = new DataTable();
            DataColumn rr1 = new DataColumn("Общее место", typeof(int));
            DataColumn rr2 = new DataColumn("Время", typeof(string));
            DataColumn rr3 = new DataColumn("Дистанция", typeof(string));
            DataColumn rr4 = new DataColumn("Марафон", typeof(string));
            DataColumn rr5 = new DataColumn("Место по категории", typeof(string));
            rr.Columns.AddRange(new DataColumn[] { rr4, rr3, rr2, rr1, rr5});

            bool t = false;
            List<string> Catp = new List<string>();

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
       RegistrationEvent.RaceTime, Runner.DateOfBirth,
	   Event.EventName, 
	   CONCAT(Marathon.YearHeld, ' ', Marathon.CityName) As 'MarathonName' 
	   FROM
      (RegistrationEvent inner join 
	  (Event inner join Marathon on (Marathon.MarathonId = Event.MarathonId) AND 
	  (Event.EventId = '" + edt.Rows[j]["EventId"] + @"')) on
	  RegistrationEvent.EventId = Event.EventId) 
      inner join Registration inner join Runner on Runner.RunnerId = Registration.RunnerId 
      on (Registration.RegistrationId = 
      RegistrationEvent.RegistrationId) AND (RegistrationEvent.RaceTime IS NOT NULL) 
	  AND (RegistrationEvent.RaceTime != 0)
      ORDER BY RegistrationEvent.RaceTime;";
                        SqlDataAdapter rda = new SqlDataAdapter();
                        rda.SelectCommand = rcom;
                        DataTable rdt = new DataTable();
                        rda.Fill(rdt);

                        int place = 1;
                        int place2 = 1;
                        //rdt.Rows[0]["Place"] = place;
                        DateTime lic = DateTime.Now;
                        DateTime dt = (DateTime)rdt.Rows[0]["DateOfBirth"];
                        if ((int)rdt.Rows[0]["RegistrationId"] == (int)ruiddt.Rows[h]["RegistrationId"])
                        {
                            if (Enumerable.Range(minage, maxage).Contains(DateTime.Now.Year - dt.Year))
                            {
                                lic = dt;
                            }
                            DataRow fr = rr.NewRow();
                            //fr["Место по категории"] = place2;
                            fr["Общее Место"] = place;
                            TimeSpan sp = new TimeSpan(0, 0, (int)rdt.Rows[0]["RaceTime"]);
                            fr["Время"] = Convert.ToString(sp.Hours) + "h. " + Convert.ToString(sp.Minutes) + "m. " +
                                            Convert.ToString(sp.Seconds) + "s.";
                            fr["Дистанция"] = rdt.Rows[0]["EventName"];
                            fr["Марафон"] = rdt.Rows[0]["MarathonName"];
                            rr.Rows.Add(fr);
                            t = true;
                        }
                        else
                        {
                            for (int k = 1; k < rdt.Rows.Count; k++)
                            {
                                dt = (DateTime)rdt.Rows[k]["DateOfBirth"];
                                if ((int)rdt.Rows[k - 1]["RaceTime"] != (int)rdt.Rows[k]["RaceTime"]) place++;
                                //rdt.Rows[0]["Place"] = place;
                                if ((int)rdt.Rows[k]["RegistrationId"] == (int)ruiddt.Rows[h]["RegistrationId"])
                                {
                                    DataRow fr = rr.NewRow();
                                    //fr["Место по категории"] = place2;
                                    fr["Общее Место"] = place;
                                    TimeSpan sp = new TimeSpan(0, 0, (int)rdt.Rows[k]["RaceTime"]);
                                    fr["Время"] = Convert.ToString(sp.Hours) + "h. " + Convert.ToString(sp.Minutes) + "m. " +
                                                    Convert.ToString(sp.Seconds) + "s.";
                                    fr["Дистанция"] = rdt.Rows[k]["EventName"];
                                    fr["Марафон"] = rdt.Rows[k]["MarathonName"];
                                    rr.Rows.Add(fr);
                                    t = true;
                                }
                            }
                        }
                        rdt.Clear();
                    }

                    edt.Clear();
                }
                }
                t = false;
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
       RegistrationEvent.RaceTime, Runner.DateOfBirth,
	   Event.EventName, 
	   CONCAT(Marathon.YearHeld, ' ', Marathon.CityName) As 'MarathonName' 
	   FROM
      (RegistrationEvent inner join 
	  (Event inner join Marathon on (Marathon.MarathonId = Event.MarathonId) AND 
	  (Event.EventId = '" + edt.Rows[j]["EventId"] + @"')) on
	  RegistrationEvent.EventId = Event.EventId) 
      inner join Registration inner join Runner on (Runner.RunnerId = Registration.RunnerId)
      AND (Runner.Gender = '"+RunnerCurrentContext.Gender+@"')
      AND (DATEDIFF(YEAR, Runner.DateOfBirth, GETDATE()) >= "+Convert.ToString(minage)+
      @") AND (DATEDIFF(YEAR, Runner.DateOfBirth, GETDATE()) <= "+Convert.ToString(maxage)+@") 
      on (Registration.RegistrationId = 
      RegistrationEvent.RegistrationId) AND (RegistrationEvent.RaceTime IS NOT NULL) 
	  AND (RegistrationEvent.RaceTime != 0)
      ORDER BY RegistrationEvent.RaceTime;";
                            SqlDataAdapter rda = new SqlDataAdapter();
                            rda.SelectCommand = rcom;
                            DataTable rdt = new DataTable();
                            rda.Fill(rdt);

                            int place = 1;
                            int place2 = 1;
                            //rdt.Rows[0]["Place"] = place;
                            DateTime lic = DateTime.Now;
                            DateTime dt = (DateTime)rdt.Rows[0]["DateOfBirth"];
                            if ((int)rdt.Rows[0]["RegistrationId"] == (int)ruiddt.Rows[h]["RegistrationId"])
                            {
                                Catp.Add(Convert.ToString(place2));
                                t = true;
                            }
                            else
                            {
                                for (int k = 1; k < rdt.Rows.Count; k++)
                                {
                                    dt = (DateTime)rdt.Rows[k]["DateOfBirth"];
                                    if ((int)rdt.Rows[k - 1]["RaceTime"] != (int)rdt.Rows[k]["RaceTime"]) place2++;
                                    //rdt.Rows[0]["Place"] = place;
                                    if ((int)rdt.Rows[k]["RegistrationId"] == (int)ruiddt.Rows[h]["RegistrationId"])
                                    {
                                        Catp.Add(Convert.ToString(place2));
                                        t = true;
                                    }
                                }
                            }
                            rdt.Clear();
                        }

                        edt.Clear();
                    }

                }

            

            if (!t)
            {
                MessageBox.Show("Вы никогда не соревновались прежде!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Close();
            }

            for (int m = 0; m < rr.Rows.Count; m++)
            {
                rr.Rows[m]["Место по категории"] = Catp[m];
            }

            dataGridView1.DataSource = rr;
        }

        private void _17_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms[Application.OpenForms.Count - 2].Visible = true;
            Dispose();
        }

        private void marathonSkills2016BasePanel1_BackButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
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
            _14 f14 = new _14();
            f14.Show();
            Visible = false;
        }
    }
}
