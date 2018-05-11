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
    public partial class _36 : Form
    {

        _35 inv;

        public _36()
        {
            InitializeComponent();
            inv = (_35)Application.OpenForms[Application.OpenForms.Count - 1];
        }

        private void _36_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms[Application.OpenForms.Count - 2].Visible = true;
            Dispose();
        }

        private void marathonSkills2016BasePanel1_BackButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void marathonSkills2016BasePanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int nr, rd, cp, bw, ts, db;
            try
            {
                nr = Convert.ToInt32(hintTextBox1.Text);
                rd = Convert.ToInt32(hintTextBox2.Text);
                cp = Convert.ToInt32(hintTextBox3.Text);
                bw = Convert.ToInt32(hintTextBox4.Text);
                ts = Convert.ToInt32(hintTextBox5.Text);
                db = Convert.ToInt32(hintTextBox6.Text);
            }
            catch (InvalidCastException exp)
            {
                 MessageBox.Show("Недопутимая value преобразования!", "MS2016", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            catch (FormatException exp2)
            {
                MessageBox.Show("Недопутимая value преобразования!", "MS2016", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            SqlConnectionStringBuilder iacsb = new SqlConnectionStringBuilder();
            iacsb.DataSource = @"localhost";
            iacsb.InitialCatalog = "MarathonSkills2016";
            iacsb.IntegratedSecurity = true;
            iacsb.ConnectTimeout = 30;
            SqlConnection iac = new SqlConnection(iacsb.ConnectionString);
            SqlDataAdapter iada = new SqlDataAdapter();
            SqlCommand iacom = iac.CreateCommand();
            iacom.CommandText = "SELECT * FROM Inventory";
            iada.SelectCommand = iacom;
            SqlCommandBuilder iacb = new SqlCommandBuilder(iada);
            iada.InsertCommand = iacb.GetInsertCommand();
            iada.DeleteCommand = iacb.GetDeleteCommand();
            iada.UpdateCommand = iacb.GetUpdateCommand();
            DataTable iadt = new DataTable();
            iada.Fill(iadt);
            /*if ((int)inv.Inventory.Rows[0]["InventoryBusyCount"] <= (int)iadt.Rows[0]["InventoryFullCount"] + nr)*/
            if (0 <= (int)iadt.Rows[0]["InventoryFullCount"] + nr) iadt.Rows[0]["InventoryFullCount"] = (int)iadt.Rows[0]["InventoryFullCount"] + rd;
            /*if ((int)inv.Inventory.Rows[1]["InventoryBusyCount"] <= (int)iadt.Rows[1]["InventoryFullCount"] + rd)*/
            if (0 <= (int)iadt.Rows[1]["InventoryFullCount"] + nr) iadt.Rows[1]["InventoryFullCount"] = (int)iadt.Rows[1]["InventoryFullCount"] + cp;
            /*if ((int)inv.Inventory.Rows[2]["InventoryBusyCount"] <= (int)iadt.Rows[2]["InventoryFullCount"] + cp)*/
            if (0 <= (int)iadt.Rows[2]["InventoryFullCount"] + nr) iadt.Rows[2]["InventoryFullCount"] = (int)iadt.Rows[2]["InventoryFullCount"] + bw;
            /*if ((int)inv.Inventory.Rows[3]["InventoryBusyCount"] <= (int)iadt.Rows[3]["InventoryFullCount"] + bw)*/
            if (0 <= (int)iadt.Rows[3]["InventoryFullCount"] + nr) iadt.Rows[3]["InventoryFullCount"] = (int)iadt.Rows[3]["InventoryFullCount"] + nr;
            /*if ((int)inv.Inventory.Rows[4]["InventoryBusyCount"] <= (int)iadt.Rows[4]["InventoryFullCount"] + ts)*/
            if (0 <= (int)iadt.Rows[4]["InventoryFullCount"] + nr) iadt.Rows[4]["InventoryFullCount"] = (int)iadt.Rows[4]["InventoryFullCount"] + db;
            /*if ((int)inv.Inventory.Rows[5]["InventoryBusyCount"] <= (int)iadt.Rows[5]["InventoryFullCount"] + db)*/
            if (0 <= (int)iadt.Rows[5]["InventoryFullCount"] + nr) iadt.Rows[5]["InventoryFullCount"] = (int)iadt.Rows[5]["InventoryFullCount"] + ts;
            iada.Update(iadt);
            inv.Inventory = inv.LoadInventory();
            inv.test.Rows[0]["Остаток"] = inv.fuic();
            inv.tinventory.Rows[1]["Остаток"] = inv.Inventory.Rows[0]["InventoryFullCount"];
            inv.tinventory.Rows[2]["Остаток"] = inv.Inventory.Rows[1]["InventoryFullCount"];
            inv.tinventory.Rows[3]["Остаток"] = inv.Inventory.Rows[2]["InventoryFullCount"];
            inv.tinventory.Rows[0]["Остаток"] = inv.Inventory.Rows[3]["InventoryFullCount"];
            inv.tinventory.Rows[5]["Остаток"] = inv.Inventory.Rows[4]["InventoryFullCount"];
            inv.tinventory.Rows[4]["Остаток"] = inv.Inventory.Rows[5]["InventoryFullCount"];
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
