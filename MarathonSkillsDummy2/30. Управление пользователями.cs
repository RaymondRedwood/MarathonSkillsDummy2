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
    public partial class _30 : Form
    {
        public _30()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            button2_Click(new object(), new EventArgs());
            
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

        private void _30_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms[Application.OpenForms.Count - 2].Visible = true;
            Dispose();
        }

        private void marathonSkills2016BasePanel1_BackButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();

            string orderrule = "";
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    {
                        orderrule = "FirstName";
                    }break;
                case 1:
                    {
                        orderrule = "LastName";
                    }
                    break;
                case 2:
                    {
                        orderrule = "Email";
                    }
                    break;
            }

            string rolerule = "";
            switch (comboBox1.SelectedIndex)
            {
                case 1:
                    {
                        rolerule = "Administrator";
                    }
                    break;
                case 2:
                    {
                        rolerule = "Coordinator";
                    }
                    break;
                case 3:
                    {
                        rolerule = "Runner";
                    }
                    break;
            }


            SqlConnectionStringBuilder umcsb = new SqlConnectionStringBuilder();
            umcsb.DataSource = @"localhost";
            umcsb.InitialCatalog = "MarathonSkills2016";
            umcsb.IntegratedSecurity = true;
            umcsb.ConnectTimeout = 30;

            SqlConnection umc = new SqlConnection(umcsb.ConnectionString);
            SqlCommand umcom = umc.CreateCommand();
            umcom.CommandText = @"SELECT [User].FirstName, [user].LastName, [User].Email, [Role].RoleName From
    [User] inner join[Role] on ([User].RoleId = [Role].RoleId) AND
  (([User].FirstName LIKE '"+hintTextBox2.Text+ @"%') OR ([user].LastName LIKE '" + hintTextBox2.Text + @"%') OR ([user].Email LIKE '" + hintTextBox2.Text + @"%'))
	ORDER BY [User]." + orderrule;
            SqlDataAdapter umda = new SqlDataAdapter(umcom);
            DataTable umdt = new DataTable();
            umda.Fill(umdt);


            if (rolerule != "")
            {
                umdt.DefaultView.RowFilter = ("RoleName = '" + rolerule + "'");
            }

            
            label6.Text = Convert.ToString(umdt.DefaultView.ToTable().Rows.Count);

            DataGridViewColumn dgvc1 = new DataGridViewColumn();
            dgvc1.Name = "FirstName";
            dgvc1.HeaderText = "Имя";
            dgvc1.CellTemplate = new DataGridViewTextBoxCell();
            DataGridViewColumn dgvc2 = new DataGridViewColumn();
            dgvc2.Name = "LastName";
            dgvc2.HeaderText = "Фамилия";
            dgvc2.CellTemplate = new DataGridViewTextBoxCell();
            DataGridViewColumn dgvc3 = new DataGridViewColumn();
            dgvc3.Name = "Email";
            dgvc3.HeaderText = "Email";
            dgvc3.CellTemplate = new DataGridViewTextBoxCell();
            DataGridViewColumn dgvc4 = new DataGridViewColumn();
            dgvc4.Name = "RoleName";
            dgvc4.HeaderText = "Роль";
            dgvc4.CellTemplate = new DataGridViewTextBoxCell();
            DataGridViewButtonColumn dgvc5 = new DataGridViewButtonColumn();
            dgvc5.FlatStyle = FlatStyle.Flat;
            dgvc5.Text = "Edit";
            dgvc5.Name = "Edit";
            dgvc5.HeaderText = "";

            dataGridView1.Columns.Add(dgvc1);
            dataGridView1.Columns.Add(dgvc2);
            dataGridView1.Columns.Add(dgvc3);
            dataGridView1.Columns.Add(dgvc4);
            dataGridView1.Columns.Add(dgvc5);

            foreach (DataRow row in umdt.DefaultView.ToTable().Rows)
            {
                dataGridView1.Rows.Add();
                dataGridView1["FirstName", dataGridView1.Rows.Count - 1].Value = row["FirstName"];
                dataGridView1["LastName", dataGridView1.Rows.Count - 1].Value = row["LastName"];
                dataGridView1["Email", dataGridView1.Rows.Count - 1].Value = row["Email"];
                dataGridView1["RoleName", dataGridView1.Rows.Count - 1].Value = row["RoleName"];
                dataGridView1["Edit", dataGridView1.Rows.Count - 1].Value = "Edit";
            }

            
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                //MessageBox.Show(dataGridView1[2, e.RowIndex].Value as string);
                _31 f31 = new _31(dataGridView1[2, e.RowIndex].Value as string);
                f31.Show();
                Dispose();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _32 f32 = new _32();
            f32.Show();
            Dispose();
        }
    }
}
