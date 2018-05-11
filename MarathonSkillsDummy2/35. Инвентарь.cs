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
    public partial class _35 : Form
    {
        
        public int gcA()
        {
            SqlConnectionStringBuilder taccsb = new SqlConnectionStringBuilder();
            taccsb.DataSource = @"localhost";
            taccsb.InitialCatalog = @"MarathonSkills2016";
            taccsb.IntegratedSecurity = true;
            taccsb.ConnectTimeout = 30;
            SqlConnection tacc = new SqlConnection(taccsb.ConnectionString);
            SqlDataAdapter tacda = new SqlDataAdapter();
            SqlCommand taccom = tacc.CreateCommand();
            taccom.CommandText = @"SELECT * FROM Registration inner join RegistrationEvent on (EventId = '15_5FM' OR EventId = '15_5FR' OR EventId = '15_5HM') AND ([RegistrationEvent].RegistrationId = [Registration].RegistrationId)";
            tacda.SelectCommand = taccom;
            DataTable tacdt = new DataTable();
            tacda.Fill(tacdt);
            int retval = 0;
            foreach (DataRow dr in tacdt.Rows)
            {
                if (dr["RaceKitOptionId"] as string == "A")
                {
                    retval++;
                } 
            }
            return retval;
        }

        public int gcB()
        {
            SqlConnectionStringBuilder taccsb = new SqlConnectionStringBuilder();
            taccsb.DataSource = @"localhost";
            taccsb.InitialCatalog = @"MarathonSkills2016";
            taccsb.IntegratedSecurity = true;
            taccsb.ConnectTimeout = 30;
            SqlConnection tacc = new SqlConnection(taccsb.ConnectionString);
            SqlDataAdapter tacda = new SqlDataAdapter();
            SqlCommand taccom = tacc.CreateCommand();
            taccom.CommandText = @"SELECT * FROM Registration inner join RegistrationEvent on (EventId = '15_5FM' OR EventId = '15_5FR' OR EventId = '15_5HM') AND ([RegistrationEvent].RegistrationId = [Registration].RegistrationId)";
            tacda.SelectCommand = taccom;
            DataTable tacdt = new DataTable();
            tacda.Fill(tacdt);
            int retval = 0;
            foreach (DataRow dr in tacdt.Rows)
            {
                if (dr["RaceKitOptionId"] as string == "B")
                {
                    retval++;
                }
            }
            return retval;
        }

        public int gcC()
        {
            SqlConnectionStringBuilder taccsb = new SqlConnectionStringBuilder();
            taccsb.DataSource = @"localhost";
            taccsb.InitialCatalog = @"MarathonSkills2016";
            taccsb.IntegratedSecurity = true;
            taccsb.ConnectTimeout = 30;
            SqlConnection tacc = new SqlConnection(taccsb.ConnectionString);
            SqlDataAdapter tacda = new SqlDataAdapter();
            SqlCommand taccom = tacc.CreateCommand();
            taccom.CommandText = @"SELECT * FROM Registration inner join RegistrationEvent on (EventId = '15_5FM' OR EventId = '15_5FR' OR EventId = '15_5HM') AND ([RegistrationEvent].RegistrationId = [Registration].RegistrationId)";
            tacda.SelectCommand = taccom;
            DataTable tacdt = new DataTable();
            tacda.Fill(tacdt);
            int retval = 0;
            foreach (DataRow dr in tacdt.Rows)
            {
                if (dr["RaceKitOptionId"] as string == "C")
                {
                    retval++;
                }
            }
            return retval;
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

        public int fuic()
        {
            DataTable tdt = LoadInventory();
            int retval = 0;
            foreach (DataRow fcrows in tdt.Rows)
            {
                retval += (int)fcrows["InventoryFullCount"];
            }
            return retval;
        }

        public int byic()
        {
            DataTable tdt = LoadInventory();
            int retval = 0;
            foreach (DataRow bcrows in tdt.Rows)
            {
                retval += (int)bcrows["InventoryBusyCount"];
            }
            return retval;
        }

        //thats only for fun
        internal void FIXIKI()
        {
            MessageBox.Show("А кто такие Фкски боальшый бальший сикрет))0))");
        }
        //***********************************

        //there is all datatable and dataadapters for this module
        public DataTable Inventory;
        public DataTable test;
        public DataTable tinventory;        
        //***********************************


        public _35()
        {
            InitializeComponent();
            Inventory = LoadInventory();
            SqlConnectionStringBuilder rccsb = new SqlConnectionStringBuilder();
            rccsb.DataSource = @"localhost";
            rccsb.InitialCatalog = @"MarathonSkills2016";
            rccsb.IntegratedSecurity = true;
            rccsb.ConnectTimeout = 30;
            SqlConnection rcc = new SqlConnection(rccsb.ConnectionString);
            SqlDataAdapter rcda = new SqlDataAdapter();
            SqlCommand rccom = rcc.CreateCommand();
            rccom.CommandText = @"SELECT * FROM Registration inner join RegistrationEvent ON (EventId = '15_5FM' OR EventId = '15_5FR' OR EventId = '15_5HM') AND ([RegistrationEvent].RegistrationId = [Registration].RegistrationId)";
            rcda.SelectCommand = rccom;
            DataTable rcdt = new DataTable();
            rcda.Fill(rcdt);
            label2.Text = Convert.ToString(rcdt.Rows.Count);
            test = new DataTable();
            DataColumn dc = new DataColumn("Комплект", typeof(string));
            DataColumn dc1 = new DataColumn("Тип A", typeof(int));
            DataColumn dc2 = new DataColumn("Тип B", typeof(int));
            DataColumn dc3 = new DataColumn("Тип C", typeof(int));
            DataColumn dc4 = new DataColumn("Необходимо", typeof(int));
            DataColumn dc5 = new DataColumn("Остаток", typeof(int));
            test.Columns.Add(dc);
            test.Columns.Add(dc1);
            test.Columns.Add(dc2);
            test.Columns.Add(dc3);
            test.Columns.Add(dc4);
            test.Columns.Add(dc5);
            DataRow dr = test.NewRow();
            dr["Комплект"] = "Выбрало данный комплект";
            dr["Тип A"] = gcA();
            dr["Тип B"] = gcB();
            dr["Тип C"] = gcC();
            dr["Необходимо"] = byic();
            dr["Остаток"] = fuic();
            test.Rows.Add(dr);
            dataGridView1.DataSource = test;
            tinventory = new DataTable();
            DataColumn dc9 = new DataColumn("Комплект", typeof(string));
            DataColumn dc6 = new DataColumn("Тип A", typeof(object));
            DataColumn dc7 = new DataColumn("Тип B", typeof(object));
            DataColumn dc8 = new DataColumn("Тип С", typeof(object));
            DataColumn dc10 = new DataColumn("Необходимо", typeof(int));
            DataColumn dc11 = new DataColumn("Остаток", typeof(int));
            tinventory.Columns.Add(dc9);
            tinventory.Columns.Add(dc6);
            tinventory.Columns.Add(dc7);
            tinventory.Columns.Add(dc8);
            tinventory.Columns.Add(dc10);
            tinventory.Columns.Add(dc11);
            DataRow dr21 = tinventory.NewRow();
            dr21["Комплект"] = "Номер";
            dr21["Тип A"] = gcA();
            dr21["Тип B"] = gcB();
            dr21["Тип С"] = gcC();
            dr21["Необходимо"] = Inventory.Rows[3]["InventoryBusyCount"];
            dr21["Остаток"] = Inventory.Rows[3]["InventoryFullCount"];
            tinventory.Rows.Add(dr21);
            DataRow dr22 = tinventory.NewRow();
            dr22["Комплект"] = "RFID б-т";
            dr22["Тип A"] = gcA();
            dr22["Тип B"] = gcB();
            dr22["Тип С"] = gcC();
            dr22["Необходимо"] = Inventory.Rows[0]["InventoryBusyCount"];
            dr22["Остаток"] = Inventory.Rows[0]["InventoryFullCount"];
            tinventory.Rows.Add(dr22);
            DataRow dr23 = tinventory.NewRow();
            dr23["Комплект"] = "Бейсболка";
            dr23["Тип A"] = "-";
            dr23["Тип B"] = gcB();
            dr23["Тип С"] = gcC();
            dr23["Необходимо"] = Inventory.Rows[1]["InventoryBusyCount"]; ;
            dr23["Остаток"] = Inventory.Rows[1]["InventoryFullCount"];
            tinventory.Rows.Add(dr23);
            DataRow dr24 = tinventory.NewRow();
            dr24["Комплект"] = "Бут. воды";
            dr24["Тип A"] = "-";
            dr24["Тип B"] = gcB();
            dr24["Тип С"] = gcC();
            dr24["Необходимо"] = Inventory.Rows[2]["InventoryBusyCount"]; ;
            dr24["Остаток"] = Inventory.Rows[2]["InventoryFullCount"];
            tinventory.Rows.Add(dr24);
            DataRow dr25 = tinventory.NewRow();
            dr25["Комплект"] = "Футболка";
            dr25["Тип A"] = "-";
            dr25["Тип B"] = "-";
            dr25["Тип С"] = gcC();
            dr25["Необходимо"] = Inventory.Rows[5]["InventoryBusyCount"]; ;
            dr25["Остаток"] = Inventory.Rows[5]["InventoryFullCount"];
            tinventory.Rows.Add(dr25);
            DataRow dr26 = tinventory.NewRow();
            dr26["Комплект"] = "Сув. букл.";
            dr26["Тип A"] = "-";
            dr26["Тип B"] = "-";
            dr26["Тип С"] = gcC();
            dr26["Необходимо"] = Inventory.Rows[4]["InventoryBusyCount"]; ;
            dr26["Остаток"] = Inventory.Rows[4]["InventoryFullCount"];
            tinventory.Rows.Add(dr26);
            dataGridView2.DataSource = tinventory;
        }

        private void _35_FormClosing(object sender, FormClosingEventArgs e)
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
            _36 f36 = new _36();
            Visible = false;
            f36.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _35_1 rp = new _35_1();
            rp.ShowDialog();
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
    }
}
