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
using System.IO;
using System.Text.RegularExpressions;

namespace MarathonSkillsDummy2
{
    public partial class _24 : Form
    {
        private int registrationid;
        private int runnerid;
        private string email;
        private string oldphoto = "";
        private string norphoto = "";

        private DataTable regtable;
        private DataTable runtable;
        private DataTable usrtable;

        private SqlDataAdapter regda;
        private SqlDataAdapter runda;
        private SqlDataAdapter usrda;

        public _24(int RunnerId)
        {
            InitializeComponent();

            runnerid = RunnerId;

            SqlConnectionStringBuilder rmcsb = new SqlConnectionStringBuilder();
            rmcsb.DataSource = @"localhost";
            rmcsb.InitialCatalog = "MarathonSkills2016";
            rmcsb.IntegratedSecurity = true;
            rmcsb.ConnectTimeout = 30;

            SqlConnection glc = new SqlConnection(rmcsb.ConnectionString);
            SqlCommand glcom = glc.CreateCommand();
            glcom.CommandText = "SELECT * FROM Gender";
            SqlDataAdapter glda = new SqlDataAdapter();
            glda.SelectCommand = glcom;
            DataTable gldt = new DataTable();
            glda.Fill(gldt);
            comboBox1.DataSource = gldt;
            comboBox1.DisplayMember = "Gender";
            comboBox1.ValueMember = "Gender";

            SqlConnection clc = new SqlConnection(rmcsb.ConnectionString);
            SqlCommand clcom = clc.CreateCommand();
            clcom.CommandText = "SELECT * FROM Country";
            SqlDataAdapter clda = new SqlDataAdapter();
            clda.SelectCommand = clcom;
            DataTable cldt = new DataTable();
            clda.Fill(cldt);
            comboBox2.DataSource = cldt;
            comboBox2.DisplayMember = "CountryName";
            comboBox2.ValueMember = "CountryCode";

            SqlConnection rslc = new SqlConnection(rmcsb.ConnectionString);
            SqlCommand rslcom = rslc.CreateCommand();
            rslcom.CommandText = "SELECT * FROM RegistrationStatus";
            SqlDataAdapter rslda = new SqlDataAdapter();
            rslda.SelectCommand = rslcom;
            DataTable rsldt = new DataTable();
            rslda.Fill(rsldt);
            comboBox3.DataSource = rsldt;
            comboBox3.DisplayMember = "RegistrationStatus";
            comboBox3.ValueMember = "RegistrationStatusId";

            SqlConnection regc = new SqlConnection(rmcsb.ConnectionString);
            SqlCommand regcom = regc.CreateCommand();
            regcom.CommandText = "SELECT * FROM Registration";
            regda = new SqlDataAdapter();
            regda.SelectCommand = regcom;
            SqlCommandBuilder regcomb = new SqlCommandBuilder(regda);
            regda.DeleteCommand = regcomb.GetDeleteCommand();
            regda.InsertCommand = regcomb.GetInsertCommand();
            regda.UpdateCommand = regcomb.GetUpdateCommand();
            regtable = new DataTable();
            regda.Fill(regtable);

            SqlConnection runc = new SqlConnection(rmcsb.ConnectionString);
            SqlCommand runcom = runc.CreateCommand();
            runcom.CommandText = "SELECT * FROM Runner";
            runda = new SqlDataAdapter();
            runda.SelectCommand = runcom;
            SqlCommandBuilder runcomb = new SqlCommandBuilder(runda);
            runda.DeleteCommand = runcomb.GetDeleteCommand();
            runda.InsertCommand = runcomb.GetInsertCommand();
            runda.UpdateCommand = runcomb.GetUpdateCommand();
            runtable = new DataTable();
            runda.Fill(runtable);

            SqlConnection usrc = new SqlConnection(rmcsb.ConnectionString);
            SqlCommand usrcom = usrc.CreateCommand();
            usrcom.CommandText = "SELECT * FROM [User]";
            usrda = new SqlDataAdapter();
            usrda.SelectCommand = usrcom;
            SqlCommandBuilder usrcomb = new SqlCommandBuilder(usrda);
            usrda.DeleteCommand = usrcomb.GetDeleteCommand();
            usrda.InsertCommand = usrcomb.GetInsertCommand();
            usrda.UpdateCommand = usrcomb.GetUpdateCommand();
            usrtable = new DataTable();
            usrda.Fill(usrtable);

            foreach (DataRow row in runtable.Rows)
            {
                if ((int)row["RunnerId"] == runnerid)
                {
                    email = (string)row["Email"];
                    dateTimePicker1.Value = (DateTime)row["DateOfBirth"];
                    comboBox1.SelectedValue = row["Gender"];
                    comboBox2.SelectedValue = row["CountryCode"];
                    if ((!(row["Photo"] == DBNull.Value)) & (!(row["Photo"] as string == "")))
                    {
                        pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\RunnerPhoto\" + (string)row["Photo"]);
                        oldphoto = (string)row["Photo"];
                        norphoto = (string)row["Photo"];
                    }
                }
            }

            foreach (DataRow row in usrtable.Rows)
            {
                if ((string)row["Email"] == email)
                {
                    label12.Text = (string)row["Email"];
                    hintTextBox5.Text = (string)row["FirstName"];
                    hintTextBox4.Text = (string)row["LastName"];
                }
            }

            SqlConnection rflc = new SqlConnection(rmcsb.ConnectionString);
            SqlCommand rflcom = rflc.CreateCommand();
            rflcom.CommandText = @"SELECT DISTINCT Registration.RegistrationId, Registration.RunnerId, Registration.RegistrationStatusId FROM RegistrationEvent inner join Registration on 
    (RegistrationEvent.RegistrationId = Registration.RegistrationId) AND
    ((RegistrationEvent.EventId = '15_5FR') OR(RegistrationEvent.EventId = '15_5FM') OR(RegistrationEvent.EventId = '15_5HM'))";
            SqlDataAdapter rflda = new SqlDataAdapter();
            rflda.SelectCommand = rflcom;
            DataTable rfldt = new DataTable();
            rflda.Fill(rfldt);

            foreach (DataRow row in rfldt.Rows)
            {
                if ((int)row["RunnerId"] == runnerid)
                {
                    registrationid = (int)row["RegistrationId"];
                    comboBox3.SelectedValue = row["RegistrationStatusId"];
                }
            }
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

        private void button4_Click(object sender, EventArgs e)
        {
            oldphoto = norphoto;
            norphoto = "";
            pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\dsa.png");
            hintTextBox6.Text = "";
        }

        private void _24_FormClosing(object sender, FormClosingEventArgs e)
        {
            _23 f23 = new _23(runnerid);
            f23.Show();
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

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Please, select image for runner";
            ofd.Filter = "Image Files|*.*";
            if (DialogResult.OK == ofd.ShowDialog())
            {
                try
                {
                    Image b = Image.FromFile(ofd.FileName);
                    pictureBox1.Image = b;
                    oldphoto = norphoto;
                    norphoto = ofd.FileName;
                    hintTextBox6.Text = ofd.FileName;

                }
                catch (OutOfMemoryException E)
                {
                    MessageBox.Show("Файл поврежден или не поддерживается!", "Marathon Skills 2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool ntp = false;
            if (hintTextBox2.Text != "")
            {
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
                ntp = true;
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
            if (comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Пол не выбран!", "Marathon Skills 2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (comboBox2.SelectedIndex < 0)
            {
                MessageBox.Show("Страна не выбрана!", "Marathon Skills 2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (DateTime.Now.Year - dateTimePicker1.Value.Year < 10)
            {
                MessageBox.Show("Ты еще слишком мал для участия в марафоне!", "Marathon Skills 2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if ((norphoto != "") & (norphoto != oldphoto))
            {
                FileInfo fi = new FileInfo(norphoto);
                norphoto = fi.Name;
                if (!File.Exists(Application.StartupPath + @"\RunnerPhoto\" + fi.Name))
                    fi.CopyTo(Application.StartupPath + @"\RunnerPhoto\" + fi.Name);
            }
            foreach (DataRow row in usrtable.Rows)
            {
                if ((string)row["Email"] ==  email)
                {
                    if (ntp == true)
                    {
                        row["Password"] = hintTextBox2.Text;
                    }
                    row["FirstName"] = hintTextBox5.Text;
                    row["LastName"] = hintTextBox4.Text;
                }
            }
            usrda.Update(usrtable);
            MessageBox.Show("User successfully edited", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Information);
            foreach (DataRow row in runtable.Rows)
            {
                if ((int)row["RunnerId"] == runnerid)
                {
                    row["Gender"] = comboBox1.SelectedValue;
                    row["CountryCode"] = comboBox2.SelectedValue;
                    row["DateOfBirth"] = dateTimePicker1.Value;
                    row["Photo"] = norphoto;
                }
            }
            runda.Update(runtable);
            MessageBox.Show("Runner successfully edited", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Information);
            foreach (DataRow row in regtable.Rows)
            {
                if ((int)row["RegistrationId"] == registrationid)
                {
                    row["RegistrationStatusId"] = comboBox3.SelectedValue;
                }
            }
            regda.Update(regtable);
            MessageBox.Show("Runner REG successfully edited", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MessageBox.Show("Данные бегуна успешно изменены.", "MarathonSkills2015", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
    }
}
