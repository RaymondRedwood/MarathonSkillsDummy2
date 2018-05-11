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
    public partial class _32 : Form
    {

        private DataTable umdt;
        private SqlDataAdapter umda;

        public _32()
        {
            InitializeComponent();

            SqlConnectionStringBuilder umcsb = new SqlConnectionStringBuilder();
            umcsb.DataSource = @"localhost";
            umcsb.InitialCatalog = "MarathonSkills2016";
            umcsb.IntegratedSecurity = true;
            umcsb.ConnectTimeout = 30;

            SqlConnection rlc = new SqlConnection(umcsb.ConnectionString);
            SqlCommand rlcom = rlc.CreateCommand();
            rlcom.CommandText = "SELECT * FROM [Role]";
            SqlDataAdapter rlda = new SqlDataAdapter(rlcom);
            DataTable rldt = new DataTable();
            rlda.Fill(rldt);

            comboBox1.DataSource = rldt;
            comboBox1.DisplayMember = "RoleName";
            comboBox1.ValueMember = "RoleId";
            comboBox1.SelectedIndex = 0;

            SqlConnection umc = new SqlConnection(umcsb.ConnectionString);
            SqlCommand umcom = umc.CreateCommand();
            umcom.CommandText = "SELECT * FROM [User]";
            umda = new SqlDataAdapter(umcom);
            SqlCommandBuilder umcomb = new SqlCommandBuilder(umda);
            umda.DeleteCommand = umcomb.GetDeleteCommand();
            umda.InsertCommand = umcomb.GetInsertCommand();
            umda.UpdateCommand = umcomb.GetUpdateCommand();
            umdt = new DataTable();
            umda.Fill(umdt);
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

        private void _32_FormClosing(object sender, FormClosingEventArgs e)
        {
            _30 f30 = new _30();
            f30.Show();
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (hintTextBox1.Text == "")
            {
                MessageBox.Show("Поле ввода адреса электронной почты пусто.", "Marathon Skills 2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Regex reg = new Regex(@"^([A-Za-z0-9_-]+\.)*[A-Za-z0-9_-]+@[A-Za-z0-9_-]+(\.[A-Za-z0-9_-]+)*\.[A-Za-z]{1,6}$");
            if (!reg.IsMatch(hintTextBox1.Text))
            {
                MessageBox.Show("Адрес электронной почты имеет неправильный формат.", "Marathon Skills 2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (hintTextBox2.Text == "")
            {
                MessageBox.Show("Поле ввода пароля пусто.", "Marathon Skills 2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (hintTextBox3.Text == "")
            {
                MessageBox.Show("Поле ввода повтора пароля пусто.", "Marathon Skills 2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Regex reg2 = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^])(?=.*[0-9])[A-Za-z0-9!@#$%^]{6,}$");
            if (!reg2.IsMatch(hintTextBox2.Text))
            {
                MessageBox.Show("Пароль не соотвествует требованиям безопасности.", "Marathon Skills 2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (hintTextBox2.Text != hintTextBox3.Text)
            {
                MessageBox.Show("Пароли не совпадают.", "Marathon Skills 2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (hintTextBox5.Text == "")
            {
                MessageBox.Show("Поле ввода имени пусто.", "Marathon Skills 2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (hintTextBox4.Text == "")
            {
                MessageBox.Show("Поле ввода фамилии пусто.", "Marathon Skills 2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Regex reg3 = new Regex("^(?=.*[A-Za-z-'])[A-Za-z'-]{2,}$");
            if (!reg3.IsMatch(hintTextBox5.Text))
            {
                MessageBox.Show("Неправильный формат имени.", "Marathon Skills 2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!reg3.IsMatch(hintTextBox4.Text))
            {
                MessageBox.Show("Неправильный формат фамилии.", "Marathon Skills 2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataRow row = umdt.NewRow();
            row["Email"] = hintTextBox1.Text;
            row["Password"] = hintTextBox2.Text;
            row["FirstName"] = hintTextBox5.Text;
            row["LastName"] = hintTextBox4.Text;
            row["RoleId"] = comboBox1.SelectedValue;
            umdt.Rows.Add(row);
            umda.Update(umdt);
            MessageBox.Show("User successfully added!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();

        }
    }
}
