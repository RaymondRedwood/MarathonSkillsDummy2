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
    public partial class _28 : Form
    {
        public _28()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            SqlConnectionStringBuilder vlcsb = new SqlConnectionStringBuilder();
            vlcsb.DataSource = @"localhost";
            vlcsb.InitialCatalog = "MarathonSkills2016";
            vlcsb.IntegratedSecurity = true;
            vlcsb.ConnectTimeout = 30;
            SqlConnection vlc = new SqlConnection(vlcsb.ConnectionString);
            SqlCommand vlcom = vlc.CreateCommand();
            vlcom.CommandText = @"SELECT Volunteer.LastName As 'Фамилия', Volunteer.FirstName As 'Имя',

        Country.CountryName As 'Страна', Volunteer.Gender As 'Пол'

        FROM Volunteer inner join Country on Volunteer.CountryCode = Country.CountryCode ORDER BY VolunteerId";
            SqlDataAdapter vlda = new SqlDataAdapter();
            vlda.SelectCommand = vlcom;
            DataTable vldt = new DataTable();
            vlda.Fill(vldt);
            dataGridView1.DataSource = vldt;
            label4.Text = Convert.ToString(vldt.Rows.Count);
        }

        private void _28_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms[Application.OpenForms.Count - 2].Visible = true;
            Dispose();
        }

        private void marathonSkills2016BasePanel1_BackButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
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
            dataGridView1.Sort(dataGridView1.Columns[comboBox1.SelectedIndex], ListSortDirection.Ascending);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _29 f29 = new _29();
            f29.Show();
            Dispose();
        }
    }
}
