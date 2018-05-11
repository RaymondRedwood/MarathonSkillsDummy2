using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace MarathonSkillsDummy2
{
    public partial class _35_1 : Form
    {

        _35 inv;

        public _35_1()
        {
            InitializeComponent();
            inv = (_35)Application.OpenForms[Application.OpenForms.Count - 1];
            DataTable report = new DataTable();
            DataColumn rf1 = new DataColumn("Наименование", typeof(string));
            DataColumn rf2 = new DataColumn("Занято", typeof(int));
            DataColumn rf3 = new DataColumn("Свободно", typeof(int));
            DataColumn rf4 = new DataColumn("Разность", typeof(int));
            report.Columns.Add(rf1);
            report.Columns.Add(rf2);
            report.Columns.Add(rf3);
            report.Columns.Add(rf4);
            DataRow ri1 = report.NewRow();
            ri1[rf1] = "Номер бегуна";
            ri1[rf2] = (int)inv.tinventory.Rows[0]["Необходимо"];
            ri1[rf3] = (int)inv.tinventory.Rows[0]["Остаток"];
            ri1[rf4] = Math.Abs((int)inv.tinventory.Rows[0]["Необходимо"] - (int)inv.tinventory.Rows[0]["Остаток"]);
            report.Rows.Add(ri1);
            DataRow ri7 = report.NewRow();
            ri7[rf1] = "RFID браслет";
            ri7[rf2] = (int)inv.tinventory.Rows[1]["Необходимо"];
            ri7[rf3] = (int)inv.tinventory.Rows[1]["Остаток"];
            ri7[rf4] = Math.Abs((int)inv.tinventory.Rows[1]["Необходимо"] - (int)inv.tinventory.Rows[1]["Остаток"]);
            report.Rows.Add(ri7);
            DataRow ri2 = report.NewRow();
            ri2[rf1] = "Бейсболка";
            ri2[rf2] = (int)inv.tinventory.Rows[2]["Необходимо"];
            ri2[rf3] = (int)inv.tinventory.Rows[2]["Остаток"];
            ri2[rf4] = Math.Abs((int)inv.tinventory.Rows[2]["Необходимо"] - (int)inv.tinventory.Rows[2]["Остаток"]);
            report.Rows.Add(ri2);
            DataRow ri3 = report.NewRow();
            ri3[rf1] = "Бутылка воды";
            ri3[rf2] = (int)inv.tinventory.Rows[3]["Необходимо"];
            ri3[rf3] = (int)inv.tinventory.Rows[3]["Остаток"];
            ri3[rf4] = Math.Abs((int)inv.tinventory.Rows[3]["Необходимо"] - (int)inv.tinventory.Rows[3]["Остаток"]);
            report.Rows.Add(ri3);
            DataRow ri4 = report.NewRow();
            ri4[rf1] = "Футболка";
            ri4[rf2] = (int)inv.tinventory.Rows[4]["Необходимо"];
            ri4[rf3] = (int)inv.tinventory.Rows[4]["Остаток"];
            ri4[rf4] = Math.Abs((int)inv.tinventory.Rows[4]["Необходимо"] - (int)inv.tinventory.Rows[4]["Остаток"]);
            report.Rows.Add(ri4);
            DataRow ri5 = report.NewRow();
            ri5[rf1] = "Сувенирный буклет";
            ri5[rf2] = (int)inv.tinventory.Rows[5]["Необходимо"];
            ri5[rf3] = (int)inv.tinventory.Rows[5]["Остаток"];
            ri5[rf4] = Math.Abs((int)inv.tinventory.Rows[5]["Необходимо"] - (int)inv.tinventory.Rows[5]["Остаток"]);
            report.Rows.Add(ri5);
            dataGridView1.DataSource = report;
        }

        private void _35_1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dispose();
        }

        private void pr(object sender, PrintPageEventArgs e)
        {
            Bitmap b = new Bitmap(dataGridView1.Width + 10, dataGridView1.Height + 10);
            dataGridView1.DrawToBitmap(b, dataGridView1.ClientRectangle);
            e.Graphics.DrawImage(b, 100, 100);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == DialogResult.OK)
            {
                PrintDocument pdoc = new PrintDocument();
                pdoc.PrinterSettings = pd.PrinterSettings;
                pdoc.PrintPage += new PrintPageEventHandler(pr);
                pdoc.Print();
            }
        }
    }
}
