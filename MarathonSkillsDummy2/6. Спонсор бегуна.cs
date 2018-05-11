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
using System.Text.RegularExpressions;

namespace MarathonSkillsDummy2
{
    public partial class _6 : Form
    {

        int poop = 0;
        const int pl = 437;
        const int pt = 193;
        const int ll = 434;
        const int lt = 277;
        uint sum = 0;
        int rid = 0;

        public void LoadRunners()
        {
            SqlConnectionStringBuilder rccsb = new SqlConnectionStringBuilder();
            rccsb.DataSource = @"localhost";
            rccsb.InitialCatalog = "MarathonSkills2016";
            rccsb.IntegratedSecurity = true;
            rccsb.ConnectTimeout = 30;
            SqlConnection rcc = new SqlConnection(rccsb.ConnectionString);
            SqlCommand rccom = rcc.CreateCommand();
            rccom.CommandText = "SELECT DISTINCT Registration.RegistrationId, CONCAT([User].FirstName, ' ', [User].LastName, ' - ', Runner.RunnerId, ' (', Country.CountryName, ')') as 'RegistrationName' FROM RegistrationEvent inner join Registration inner join (Runner inner join Country on Runner.CountryCode = Country.CountryCode) inner join [User] on [User].Email = Runner.Email on Runner.RunnerId = Registration.RunnerId on (Registration.RegistrationId = RegistrationEvent.RegistrationId) AND ((EventId = '15_5FM') OR (EventId = '15_5FR') OR (EventId = '15_5HM')) order by RegistrationName";
            SqlDataAdapter rcda = new SqlDataAdapter();
            rcda.SelectCommand = rccom;
            DataTable rcdt = new DataTable();
            rcda.Fill(rcdt);
            comboBox1.DataSource = rcdt;
            comboBox1.DisplayMember = "RegistrationName";
            comboBox1.ValueMember = "RegistrationId";
        }

        public int gChar(int RegInt)
        {
            SqlConnectionStringBuilder clcsb = new SqlConnectionStringBuilder();
            clcsb.DataSource = @"localhost";
            clcsb.InitialCatalog = "MarathonSkills2016";
            clcsb.IntegratedSecurity = true;
            clcsb.ConnectTimeout = 30;
            SqlConnection clc = new SqlConnection(clcsb.ConnectionString);
            SqlCommand clcom = clc.CreateCommand();
            clcom.CommandText = "SELECT * FROM Registration";
            SqlDataAdapter clda = new SqlDataAdapter();
            clda.SelectCommand = clcom;
            DataTable cldt = new DataTable();
            clda.Fill(cldt);
            foreach (DataRow cldr in cldt.Rows)
            {
                if ((int)cldr["RegistrationId"] == RegInt)
                {
                    return (int)cldr["CharityId"];
                }
            }
            return -1;
        }

        public _6()
        {
            InitializeComponent();
            LoadRunners();
            hintTextBox2.Text = Convert.ToString(500);
            sum = 500;
         
        }

        private void _6_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms[0].Visible = true;
            Dispose();
        }

        private void marathonSkills2016BasePanel1_BackButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            _5_1 f5 = new _5_1(poop);
            f5.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            rid = Convert.ToInt32((comboBox1.DataSource as DataTable).Rows[comboBox1.SelectedIndex]["RegistrationId"]);
            int charid = gChar(Convert.ToInt32((comboBox1.DataSource as DataTable).Rows[comboBox1.SelectedIndex]["RegistrationId"]));
            if (charid != -1)
            {
                SqlConnectionStringBuilder ccsb = new SqlConnectionStringBuilder();
                ccsb.DataSource = @"localhost";
                ccsb.InitialCatalog = "MarathonSkills2016";
                ccsb.IntegratedSecurity = true;
                ccsb.ConnectTimeout = 30;
                SqlConnection cc = new SqlConnection(ccsb.ConnectionString);
                SqlCommand ccom = cc.CreateCommand();
                ccom.CommandText = "SELECT * FROM Charity";
                SqlDataAdapter cda = new SqlDataAdapter();
                cda.SelectCommand = ccom;
                DataTable cdt = new DataTable();
                cda.Fill(cdt);
                foreach (DataRow row in cdt.Rows)
                {
                    if ((int)row["CharityId"] == charid)
                    {
                        label5.Text = row["CharityName"] as string;
                        label5.Location = new Point(pl - (label5.ClientSize.Width / 2), pt);
                        poop = charid;
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label6.Text = "$"+Convert.ToString(sum);
            label6.Location = new Point(ll - (label6.ClientSize.Width / 2), lt);
        }

        private void hintTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            uint old = sum;
            if (hintTextBox2.Text == "")
            {
                sum = 0;
                //hintTextBox2.Text = "0";
                //hintTextBox2.SelectionStart = hintTextBox2.Text.Length;
                return;
            }
            try
            {
                sum = Convert.ToUInt16(hintTextBox2.Text);
            }
            catch (FormatException)
            {
                //MessageBox.Show("Wrong number format!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Information);
                hintTextBox2.Text = Convert.ToString(old);
                hintTextBox2.SelectionStart = hintTextBox2.Text.Length;
                sum = old;
                return;
            }
            catch (OverflowException)
            {
                //MessageBox.Show("Buffer overflowed!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Information);
                sum = old;
                hintTextBox2.Text = Convert.ToString(old);
                hintTextBox2.SelectionStart = hintTextBox2.Text.Length;
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sum < uint.MaxValue)
            {
                sum++;
                hintTextBox2.Text = Convert.ToString(sum);
                hintTextBox2.SelectionStart = hintTextBox2.Text.Length;
            }
            else
            {
                MessageBox.Show("Buffer overflowed!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (sum > uint.MinValue)
            {
                sum--;
                hintTextBox2.Text = Convert.ToString(sum);
                hintTextBox2.SelectionStart = hintTextBox2.Text.Length;
            }
            else
            {
                MessageBox.Show("Buffer overflowed!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Regex reg1 = new Regex("^(?=.*[A-Za-z-'])[A-Za-z'-]{2,} [A-Za-z'-]{2,}$");
            if (!reg1.IsMatch(hintTextBox1.Text))
            {
                MessageBox.Show("Wrong Name format!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Regex reg2 = new Regex("^(?=.*[A-Z-'])[A-Z'-]{2,} [A-Z'-]{2,}$");
            if (!reg2.IsMatch(hintTextBox3.Text))
            {
                MessageBox.Show("Wrong Name format!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Regex reg3 = new Regex("^^(?=.*[0-9])[0-9]{4} [0-9]{4} [0-9]{4} [0-9]{4}$$");
            if (!reg3.IsMatch(hintTextBox4.Text))
            {
                MessageBox.Show("Wrong Card number format!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Regex reg4 = new Regex("^(?=.*[0-9])(1[0-2]|0[1-9])$");
            if (!reg4.IsMatch(hintTextBox5.Text))
            {
                MessageBox.Show("Wrong Month format!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Regex reg5 = new Regex("^(?=.*[0-9])(20[2-4][0-9]|2050)$");
            if (!reg5.IsMatch(hintTextBox7.Text))
            {
                MessageBox.Show("Wrong year format!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Regex reg6 = new Regex("^(?=.*[0-9])[0-9]{3}$");
            if (!reg6.IsMatch(hintTextBox6.Text))
            {
                MessageBox.Show("Wrong cvs format!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            decimal fcost = Convert.ToDecimal(sum);
            if (fcost < 1)
            {
                MessageBox.Show("Too small payment!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SqlConnectionStringBuilder stcsb = new SqlConnectionStringBuilder();
            stcsb.DataSource = @"localhost";
            stcsb.InitialCatalog = "MarathonSkills2016";
            stcsb.IntegratedSecurity = true;
            stcsb.ConnectTimeout = 30;
            SqlConnection stc = new SqlConnection(stcsb.ConnectionString);
            SqlCommand stcom = stc.CreateCommand();
            stcom.CommandText = "SELECT * FROM Sponsorship";
            SqlDataAdapter stda = new SqlDataAdapter();
            stda.SelectCommand = stcom;
            SqlCommandBuilder stcomb = new SqlCommandBuilder(stda);
            stda.DeleteCommand = stcomb.GetDeleteCommand();
            stda.InsertCommand = stcomb.GetInsertCommand();
            stda.UpdateCommand = stcomb.GetUpdateCommand();
            DataTable stdt = new DataTable();
            stda.Fill(stdt);
            DataRow stdr = stdt.NewRow();
            stdr["SponsorName"] = hintTextBox1.Text;
            stdr["RegistrationId"] = rid;
            stdr["Amount"] = fcost;
            stdt.Rows.Add(stdr);
            stda.Update(stdt);
            _7 f7 = new _7(rid);
            f7.Show();
            Dispose();  
        }
    }
}
