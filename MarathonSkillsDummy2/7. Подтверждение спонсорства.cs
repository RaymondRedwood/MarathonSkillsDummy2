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
    public partial class _7 : Form
    {

        int l3l = 290;
        int l3t = 156;
        int l5l = 290;
        int l5t = 196;
        int l6l = 284;
        int l6t = 247;
        int l7l = 284;
        int l7t = 301; 

        public string g1(int r)
        {
            SqlConnectionStringBuilder g1csb = new SqlConnectionStringBuilder();
            g1csb.DataSource = @"localhost";
            g1csb.InitialCatalog = "MarathonSkills2016";
            g1csb.IntegratedSecurity = true;
            g1csb.ConnectTimeout = 30;
            SqlConnection g1c = new SqlConnection(g1csb.ConnectionString);
            SqlCommand g1com = g1c.CreateCommand();
            g1com.CommandText = "SELECT Registration.RegistrationId, CONCAT([User].FirstName, ' ', [user].LastName, '(', Runner.RunnerId, ')') AS 'RunnerDesc' FROM Registration inner join Runner inner join [User] on [User].Email = Runner.Email on Runner.RunnerId = Registration.RunnerId order by RunnerDesc";
            SqlDataAdapter g1da = new SqlDataAdapter();
            g1da.SelectCommand = g1com;
            DataTable g1dt = new DataTable();
            g1da.Fill(g1dt);
            foreach (DataRow g1dr in g1dt.Rows)
            {
                if ((int)g1dr["RegistrationId"] == r)
                {
                    return g1dr["RunnerDesc"] as string;
                }
            }
            return "fail";
        }

        public string g2(int r)
        {
            SqlConnectionStringBuilder g1csb = new SqlConnectionStringBuilder();
            g1csb.DataSource = @"localhost";
            g1csb.InitialCatalog = "MarathonSkills2016";
            g1csb.IntegratedSecurity = true;
            g1csb.ConnectTimeout = 30;
            SqlConnection g1c = new SqlConnection(g1csb.ConnectionString);
            SqlCommand g1com = g1c.CreateCommand();
            g1com.CommandText = "SELECT Registration.RegistrationId, Country.CountryName FROM Registration inner join (Runner inner join Country on Country.CountryCode = Runner.CountryCode) on Runner.RunnerId = Registration.RunnerId";
            SqlDataAdapter g1da = new SqlDataAdapter();
            g1da.SelectCommand = g1com;
            DataTable g1dt = new DataTable();
            g1da.Fill(g1dt);
            foreach (DataRow g1dr in g1dt.Rows)
            {
                if ((int)g1dr["RegistrationId"] == r)
                {
                    return g1dr["CountryName"] as string;
                }
            }
            return "fail";
        }

        public string g3(int r)
        {
            SqlConnectionStringBuilder g1csb = new SqlConnectionStringBuilder();
            g1csb.DataSource = @"localhost";
            g1csb.InitialCatalog = "MarathonSkills2016";
            g1csb.IntegratedSecurity = true;
            g1csb.ConnectTimeout = 30;
            SqlConnection g1c = new SqlConnection(g1csb.ConnectionString);
            SqlCommand g1com = g1c.CreateCommand();
            g1com.CommandText = "SELECT Registration.RegistrationId, Charity.CharityName From Registration inner join Charity on Charity.CharityId = Registration.CharityId";
            SqlDataAdapter g1da = new SqlDataAdapter();
            g1da.SelectCommand = g1com;
            DataTable g1dt = new DataTable();
            g1da.Fill(g1dt);
            foreach (DataRow g1dr in g1dt.Rows)
            {
                if ((int)g1dr["RegistrationId"] == r)
                {
                    return g1dr["CharityName"] as string;
                }
            }
            return "fail";
        }

        public string g4(int r)
        {
            SqlConnectionStringBuilder g1csb = new SqlConnectionStringBuilder();
            g1csb.DataSource = @"localhost";
            g1csb.InitialCatalog = "MarathonSkills2016";
            g1csb.IntegratedSecurity = true;
            g1csb.ConnectTimeout = 30;
            SqlConnection g1c = new SqlConnection(g1csb.ConnectionString);
            SqlCommand g1com = g1c.CreateCommand();
            g1com.CommandText = "SELECT Registration.RegistrationId, Sponsorship.Amount FROM Sponsorship inner join Registration on Sponsorship.RegistrationId = Registration.RegistrationId Order by Registration.RegistrationId";
            SqlDataAdapter g1da = new SqlDataAdapter();
            g1da.SelectCommand = g1com;
            DataTable g1dt = new DataTable();
            g1da.Fill(g1dt);
            /*foreach (DataRow g1dr in g1dt.Rows)
            {
                if ((int)g1dr["RegistrationId"] == r)
                {
                    return Convert.ToString((decimal)g1dr["Amount"]);
                }
            }*/
            for (int i = g1dt.Rows.Count - 1; i >= 0; i--)
            {
                if ((int)g1dt.Rows[i]["RegistrationId"] == r)
                {
                    return Convert.ToString((decimal)g1dt.Rows[i]["Amount"]);
                }
            }
            return "fail";
        }

        public _7(int rgid)
        {
            InitializeComponent();
            label3.Text = g1(rgid);
            label3.Location = new Point(l3l - (label3.ClientSize.Width / 2), l3t);
            label5.Text = g2(rgid);
            label5.Location = new Point(l5l - (label6.ClientSize.Width / 2), l5t);
            label6.Text = g3(rgid);
            label6.Location = new Point(l6l - (label6.ClientSize.Width / 2), l6t);
            label7.Text = g4(rgid);
            label7.Location = new Point(l7l - (label7.ClientSize.Width / 2), l7t);
        }

        private void _7_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms[0].Visible = true;
            Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void marathonSkills2016BasePanel1_BackButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void marathonSkills2016BasePanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
