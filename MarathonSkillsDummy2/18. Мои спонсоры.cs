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
    public partial class _18 : Form
    {
        public _18()
        {
            InitializeComponent();

            SqlConnectionStringBuilder cmscsb = new SqlConnectionStringBuilder();
            cmscsb.DataSource = @"localhost";
            cmscsb.InitialCatalog = "MarathonSkills2016";
            cmscsb.IntegratedSecurity = true;
            cmscsb.ConnectTimeout = 30;
            SqlConnection cmsc = new SqlConnection(cmscsb.ConnectionString);
            SqlCommand cmscom = cmsc.CreateCommand();
            cmscom.CommandText = @"SELECT DISTINCT Registration.RegistrationId, Charity.CharityDescription,
		Charity.CharityLogo, Charity.CharityName FROM
		RegistrationEvent inner join
		(Registration inner join Charity on Registration.CharityId = Charity.CharityId)
		inner join Runner on (Runner.RunnerId = Registration.RunnerId) AND
		(Runner.RunnerId = "+RunnerCurrentContext.RunnerId+@") on (Registration.RegistrationId = RegistrationEvent.RegistrationId) AND
		((RegistrationEvent.EventId = '15_5FM') OR
		(RegistrationEvent.EventId = '15_5FR') OR
		(RegistrationEvent.EventId = '15_5HM'))";
            SqlDataAdapter cmsda = new SqlDataAdapter();
            cmsda.SelectCommand = cmscom;
            DataTable cmsdt = new DataTable();
            cmsda.Fill(cmsdt);

            if (cmsdt.Rows.Count < 1)
            {
                MessageBox.Show("Вы не регистрировались на текущий марафон", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Close();
            }
            else
            {
                label2.Text = (string)cmsdt.Rows[0]["CharityName"];
                label2.Location = new Point(label2.Location.X - (label2.ClientSize.Width / 2), label2.Location.Y);
                if ((cmsdt.Rows[0]["CharityLogo"] != DBNull.Value) & ((string)cmsdt.Rows[0]["CharityLogo"] != ""))
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\CharityLogo\" + (string)cmsdt.Rows[0]["CharityLogo"]);
                }
                pictureBox1.Width = pictureBox1.Image.Width;
                pictureBox1.Location = new Point(pictureBox1.Location.X - (pictureBox1.ClientSize.Width / 2), pictureBox1.Location.Y);
                hintTextBox2.Text = (string)cmsdt.Rows[0]["CharityDescription"];

                int rid = (int)cmsdt.Rows[0]["RegistrationId"];

                SqlConnection rsc = new SqlConnection(cmscsb.ConnectionString);
                SqlCommand rscom = rsc.CreateCommand();
                rscom.CommandText = @"SELECT Sponsorship.SponsorName, Sponsorship.Amount FROM Sponsorship 
                     inner join Registration

                    on(Sponsorship.RegistrationId = Registration.RegistrationId)

                    AND(Registration.RegistrationId = "+Convert.ToString(cmsdt.Rows[0]["RegistrationId"])+")";
                SqlDataAdapter rsda = new SqlDataAdapter();
                rsda.SelectCommand = rscom;
                DataTable rsdt = new DataTable();
                rsda.Fill(rsdt);
                dataGridView1.DataSource = rsdt;
                decimal sum = 0;
                if (rsdt.Rows.Count > 0)
                {
                    foreach (DataRow row in rsdt.Rows)
                    {
                        sum += (decimal)row["Amount"]; 
                    }
                }
                label4.Text = Convert.ToString(sum);
            }
        }

        private void _18_FormClosing(object sender, FormClosingEventArgs e)
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
    }
}
