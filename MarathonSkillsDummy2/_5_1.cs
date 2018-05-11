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
    public partial class _5_1 : Form
    {
        const int pbl = 146;
        const int pbt = 54;

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

        public _5_1(int charityid)
        {
            InitializeComponent();
            DataTable dt = LoadCharity();
            foreach (DataRow dr in dt.Rows)
            {
                if ((int)dr["CharityId"] == charityid)
                {
                    label1.Text = dr["CharityName"] as string;
                    hintTextBox2.Text = dr["CharityDescription"] as string;
                    if (dr["CharityLogo"] != null)
                    {
                        pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\CharityLogo\" + (dr["CharityLogo"] as string));
                        //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox1.Size = new Size(pictureBox1.Image.Width, pictureBox1.Width);
                        pictureBox1.Location = new Point(pbl - (pictureBox1.Size.Width / 2) + 30, pbt);
                    }
                    else
                    {
                        pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\dsa.png");
                    }
                }
            }
            label1.Location = new Point(Convert.ToInt32(Math.Round((ClientSize.Width / 2d) - (label1.Width / 2d))), label1.Location.Y);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
