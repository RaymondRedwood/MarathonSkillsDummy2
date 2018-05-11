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
    public partial class _5 : Form
    {
        UInt16 Last = 0;
        private int mcost = 0;
        private int fcost = 0;

        public DataTable LoadCharity()
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
            return cdt;
        }

        public bool HaveRegistration(int rid)
        {
            SqlConnectionStringBuilder rccsb = new SqlConnectionStringBuilder();
            rccsb.DataSource = @"localhost";
            rccsb.InitialCatalog = "MarathonSkills2016";
            rccsb.IntegratedSecurity = true;
            rccsb.ConnectTimeout = 30;
            SqlConnection rcc = new SqlConnection(rccsb.ConnectionString);
            SqlCommand rccom = rcc.CreateCommand();
            rccom.CommandText = "SELECT * FROM Registration inner join RegistrationEvent on (EventId = '15_5FM' OR EventId = '15_5HM' OR EventId = '15_5FR') AND (Registration.RegistrationId = RegistrationEvent.RegistrationId)";
            SqlDataAdapter rcda = new SqlDataAdapter();
            rcda.SelectCommand = rccom;
            DataTable rcdt = new DataTable();
            rcda.Fill(rcdt);
            foreach (DataRow dr in rcdt.Rows)
            {
                if ((int)dr["RunnerId"] == RunnerCurrentContext.RunnerId)
                {
                    return true;
                }
            }
            return false;
        }

        public DataTable LoadInventory()
        {
            SqlConnectionStringBuilder itcsb = new SqlConnectionStringBuilder();
            itcsb.DataSource = @"localhost";
            itcsb.InitialCatalog = @"MarathonSkills2016";
            itcsb.IntegratedSecurity = true;
            itcsb.ConnectTimeout = 30;
            SqlConnection itc = new SqlConnection(itcsb.ConnectionString);
            SqlDataAdapter itda = new SqlDataAdapter();
            SqlCommand itcom = itc.CreateCommand();
            itcom.CommandText = @"SELECT * FROM Inventory";
            itda.SelectCommand = itcom;
            DataTable rettab = new DataTable();
            itda.Fill(rettab);
            return rettab;
        }

        public void RegisterToMarathon()
        {
            //RegisterTable
            int rid = RunnerCurrentContext.RunnerId;
            DateTime rdd = DateTime.Now;
            string rkoi = "";
            int regs = 1;
            decimal cost = mcost;
            int cid = (int)comboBox1.SelectedValue;
            decimal st = fcost;

            int ivtc = 0;
            if (checkBox1.Checked) ivtc++;
            if (checkBox2.Checked) ivtc++;
            if (checkBox3.Checked) ivtc++;

            SqlConnectionStringBuilder imcsb = new SqlConnectionStringBuilder();
            imcsb.DataSource = @"localhost";
            imcsb.InitialCatalog = "MarathonSkills2016";
            imcsb.IntegratedSecurity = true;
            imcsb.ConnectTimeout = 30;
            SqlConnection imc = new SqlConnection(imcsb.ConnectionString);
            SqlCommand imcom = imc.CreateCommand();
            imcom.CommandText = "SELECT * FROM Inventory";
            SqlDataAdapter imda = new SqlDataAdapter();
            imda.SelectCommand = imcom;
            SqlCommandBuilder imcomb = new SqlCommandBuilder(imda);
            imda.DeleteCommand = imcomb.GetDeleteCommand();
            imda.InsertCommand = imcomb.GetInsertCommand();
            imda.UpdateCommand = imcomb.GetUpdateCommand();
            DataTable imdt = new DataTable();
            imda.Fill(imdt);
            if (radioButton1.Checked == true)
            {
                if (((int)imdt.Rows[0]["InventoryFullCount"] <= 0) | (((int)imdt.Rows[3]["InventoryFullCount"] <= 0)))
                {
                    MessageBox.Show("Требуемые позиции в инвентаре отсутствуют!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    rkoi = "A";
                    imdt.Rows[0]["InventoryFullCount"] = (int)imdt.Rows[0]["InventoryFullCount"] - ivtc;
                    imdt.Rows[3]["InventoryFullCount"] = (int)imdt.Rows[3]["InventoryFullCount"] - ivtc;
                    imdt.Rows[0]["InventoryBusyCount"] = (int)imdt.Rows[0]["InventoryBusyCount"] + ivtc;
                    imdt.Rows[3]["InventoryBusyCount"] = (int)imdt.Rows[3]["InventoryBusyCount"] + ivtc;
                    imda.Update(imdt);
                }
            }
            if (radioButton2.Checked == true)
            {
                if (((int)imdt.Rows[0]["InventoryFullCount"] <= 0) | (((int)imdt.Rows[3]["InventoryFullCount"] <= 0)) | (((int)imdt.Rows[1]["InventoryFullCount"] <= 0)) | (((int)imdt.Rows[2]["InventoryFullCount"] <= 0)))
                {
                    MessageBox.Show("Требуемые позиции в инвентаре отсутствуют!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    rkoi = "B";
                    imdt.Rows[0]["InventoryFullCount"] = (int)imdt.Rows[0]["InventoryFullCount"] - ivtc;
                    imdt.Rows[3]["InventoryFullCount"] = (int)imdt.Rows[3]["InventoryFullCount"] - ivtc;
                    imdt.Rows[1]["InventoryFullCount"] = (int)imdt.Rows[1]["InventoryFullCount"] - ivtc;
                    imdt.Rows[2]["InventoryFullCount"] = (int)imdt.Rows[2]["InventoryFullCount"] - ivtc;
                    imdt.Rows[0]["InventoryBusyCount"] = (int)imdt.Rows[0]["InventoryBusyCount"] + ivtc;
                    imdt.Rows[3]["InventoryBusyCount"] = (int)imdt.Rows[3]["InventoryBusyCount"] + ivtc;
                    imdt.Rows[1]["InventoryBusyCount"] = (int)imdt.Rows[1]["InventoryBusyCount"] + ivtc;
                    imdt.Rows[2]["InventoryBusyCount"] = (int)imdt.Rows[2]["InventoryBusyCount"] + ivtc;
                    imda.Update(imdt);
                }
            }
            if (radioButton3.Checked == true)
            {
                if (((int)imdt.Rows[0]["InventoryFullCount"] <= 0) | (((int)imdt.Rows[3]["InventoryFullCount"] <= 0)) | (((int)imdt.Rows[1]["InventoryFullCount"] <= 0)) | (((int)imdt.Rows[2]["InventoryFullCount"] <= 0)) | (((int)imdt.Rows[5]["InventoryFullCount"] <= 0)) | (((int)imdt.Rows[4]["InventoryFullCount"] <= 0)))
                {
                    MessageBox.Show("Требуемые позиции в инвентаре отсутствуют!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    rkoi = "C";
                    imdt.Rows[0]["InventoryFullCount"] = (int)imdt.Rows[0]["InventoryFullCount"] - ivtc;
                    imdt.Rows[3]["InventoryFullCount"] = (int)imdt.Rows[3]["InventoryFullCount"] - ivtc;
                    imdt.Rows[1]["InventoryFullCount"] = (int)imdt.Rows[1]["InventoryFullCount"] - ivtc;
                    imdt.Rows[2]["InventoryFullCount"] = (int)imdt.Rows[2]["InventoryFullCount"] - ivtc;
                    imdt.Rows[5]["InventoryFullCount"] = (int)imdt.Rows[5]["InventoryFullCount"] - ivtc;
                    imdt.Rows[4]["InventoryFullCount"] = (int)imdt.Rows[4]["InventoryFullCount"] - ivtc;
                    imdt.Rows[0]["InventoryBusyCount"] = (int)imdt.Rows[0]["InventoryBusyCount"] + ivtc;
                    imdt.Rows[3]["InventoryBusyCount"] = (int)imdt.Rows[3]["InventoryBusyCount"] + ivtc;
                    imdt.Rows[1]["InventoryBusyCount"] = (int)imdt.Rows[1]["InventoryBusyCount"] + ivtc;
                    imdt.Rows[2]["InventoryBusyCount"] = (int)imdt.Rows[2]["InventoryBusyCount"] + ivtc;
                    imdt.Rows[5]["InventoryBusyCount"] = (int)imdt.Rows[5]["InventoryBusyCount"] + ivtc;
                    imdt.Rows[4]["InventoryBusyCount"] = (int)imdt.Rows[4]["InventoryBusyCount"] + ivtc;
                    imda.Update(imdt);
                }
            }
            SqlConnectionStringBuilder rrcsb = new SqlConnectionStringBuilder();
            rrcsb.DataSource = @"localhost";
            rrcsb.InitialCatalog = "MarathonSkills2016";
            rrcsb.IntegratedSecurity = true;
            rrcsb.ConnectTimeout = 30;
            SqlConnection rrc = new SqlConnection(rrcsb.ConnectionString);
            SqlCommand rrcom = rrc.CreateCommand();
            rrcom.CommandText = "SELECT * FROM Registration";
            SqlDataAdapter rrda = new SqlDataAdapter();
            rrda.SelectCommand = rrcom;
            SqlCommandBuilder rrcomb = new SqlCommandBuilder(rrda);
            rrda.DeleteCommand = rrcomb.GetDeleteCommand();
            rrda.InsertCommand = rrcomb.GetInsertCommand();
            rrda.UpdateCommand = rrcomb.GetUpdateCommand();
            DataTable rrdt = new DataTable();
            rrda.Fill(rrdt);
            DataRow rrr = rrdt.NewRow();
            rrr["RunnerId"] = rid;
            rrr["RegistrationDateTime"] = rdd;
            rrr["RaceKitOptionId"] = rkoi;
            rrr["RegistrationStatusId"] = regs;
            rrr["Cost"] = cost;
            rrr["CharityId"] = cid;
            rrr["SponsorshipTarget"] = st;
            rrdt.Rows.Add(rrr);
            rrda.Update(rrdt);
            rrdt.Clear();
            rrda.Fill(rrdt);

            //RegiesterEventTable
            int reid = (int)rrdt.Rows[rrdt.Rows.Count - 1]["RegistrationId"];
            string eid1 = "15_5FM";
            string eid2 = "15_5HM";
            string eid3 = "15_5FR";

            SqlConnectionStringBuilder remcsb = new SqlConnectionStringBuilder();
            remcsb.DataSource = @"localhost";
            remcsb.InitialCatalog = "MarathonSkills2016";
            remcsb.IntegratedSecurity = true;
            remcsb.ConnectTimeout = 30;
            SqlConnection remc = new SqlConnection(remcsb.ConnectionString);
            SqlCommand remcom = remc.CreateCommand();
            remcom.CommandText = "SELECT * FROM RegistrationEvent";
            SqlDataAdapter remda = new SqlDataAdapter();
            remda.SelectCommand = remcom;
            SqlCommandBuilder remcomb = new SqlCommandBuilder(remda);
            remda.DeleteCommand = remcomb.GetDeleteCommand();
            remda.InsertCommand = remcomb.GetInsertCommand();
            remda.UpdateCommand = remcomb.GetUpdateCommand();
            DataTable remdt = new DataTable();
            remda.Fill(remdt);

            if (checkBox1.Checked)
            {
                DataRow remnr = remdt.NewRow();
                remnr["RegistrationId"] = reid;
                remnr["EventId"] = eid1;
                remnr["BibNumber"] = DBNull.Value;
                remnr["RaceTime"] = DBNull.Value;
                remdt.Rows.Add(remnr);
                remda.Update(remdt);
                remdt.Clear();
                remda.Fill(remdt);
            }

            if (checkBox2.Checked)
            {
                DataRow remnr = remdt.NewRow();
                remnr["RegistrationId"] = reid;
                remnr["EventId"] = eid2;
                remnr["BibNumber"] = DBNull.Value;
                remnr["RaceTime"] = DBNull.Value;
                remdt.Rows.Add(remnr);
                remda.Update(remdt);
                remdt.Clear();
                remda.Fill(remdt);
            }

            if (checkBox3.Checked)
            {
                DataRow remnr = remdt.NewRow();
                remnr["RegistrationId"] = reid;
                remnr["EventId"] = eid3;
                remnr["BibNumber"] = DBNull.Value;
                remnr["RaceTime"] = DBNull.Value;
                remdt.Rows.Add(remnr);
                remda.Update(remdt);
            }
            _8 f8 = new _8();
            f8.Show();
            Dispose();
        }

        public _5()
        {
            InitializeComponent();
            DataTable dt = LoadCharity();
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "CharityName";
            comboBox1.ValueMember = "CharityId";
            if (HaveRegistration(RunnerCurrentContext.RunnerId))
            {
                MessageBox.Show("Вы уже регистрировались на марафон", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Dispose();
            }
        }

        private void button3_Click(object sender, EventArgs e)
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

        private void _5_FormClosing(object sender, FormClosingEventArgs e)
        {
            _9 f9 = new _9();
            f9.Show();
            Dispose();
        }

        private void marathonSkills2016BasePanel1_BackButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            _5_1 f51 = new _5_1((int)comboBox1.SelectedValue);
            f51.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                fcost = Convert.ToUInt16(hintTextBox2.Text);
            }
            catch (FormatException)
            {
                fcost = 0;
            }
            catch (OverflowException)
            {
                fcost = 0;
            }
            label15.Text = "$" + Convert.ToString(mcost);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) mcost += 145; else mcost -= 145; 
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true) mcost += 75; else mcost -= 75;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true) mcost += 20; else mcost -= 20;
        }

        private void hintTextBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if ((radioButton1.Checked == true) & (radioButton2.Checked == false) & (radioButton3.Checked == false)) mcost += 0; else mcost -= 0;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if ((radioButton1.Checked == false) & (radioButton2.Checked == true) & (radioButton3.Checked == false)) mcost += 20; else mcost -= 20;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if ((radioButton1.Checked == false) & (radioButton2.Checked == false) & (radioButton3.Checked == true)) mcost += 45; else mcost -= 45;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ushort t = Convert.ToUInt16(hintTextBox2.Text);
            }
            catch (OverflowException)
            {
                MessageBox.Show("Не указана сумма взноса для благотворительной организации!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            catch (FormatException)
            {
                MessageBox.Show("Не указана сумма взноса для благотворительной организации!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!checkBox1.Checked & !checkBox2.Checked & !checkBox3.Checked)
            {
                MessageBox.Show("Не выбран вариант марафона!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!radioButton1.Checked & !radioButton2.Checked & !radioButton3.Checked)
            {
                MessageBox.Show("Не выбран вариант комплекта!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            RegisterToMarathon();
        }
    }
}
