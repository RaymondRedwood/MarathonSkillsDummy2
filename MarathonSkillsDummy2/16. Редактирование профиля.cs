﻿using System;
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
using System.IO;

namespace MarathonSkillsDummy2
{
    public partial class _16 : Form
    {

        string oldphoto = "";

        public DataTable LoadGender()
        {
            SqlConnectionStringBuilder gcsb = new SqlConnectionStringBuilder();
            gcsb.DataSource = @"localhost";
            gcsb.InitialCatalog = "MarathonSkills2016";
            gcsb.IntegratedSecurity = true;
            gcsb.ConnectTimeout = 30;
            SqlConnection gc = new SqlConnection(gcsb.ConnectionString);
            SqlCommand gcom = gc.CreateCommand();
            gcom.CommandText = "SELECT * FROM Gender";
            SqlDataAdapter gda = new SqlDataAdapter();
            gda.SelectCommand = gcom;
            DataTable gdt = new DataTable();
            gda.Fill(gdt);
            return gdt;
        }

        public DataTable LoadCountry()
        {
            SqlConnectionStringBuilder ccsb = new SqlConnectionStringBuilder();
            ccsb.DataSource = @"localhost";
            ccsb.InitialCatalog = "MarathonSkills2016";
            ccsb.IntegratedSecurity = true;
            ccsb.ConnectTimeout = 30;
            SqlConnection cc = new SqlConnection(ccsb.ConnectionString);
            SqlCommand ccom = cc.CreateCommand();
            ccom.CommandText = "SELECT * FROM Country";
            SqlDataAdapter cda = new SqlDataAdapter();
            cda.SelectCommand = ccom;
            DataTable cdt = new DataTable();
            cda.Fill(cdt);
            return cdt;
        }

        public void Register()
        {
            SqlConnectionStringBuilder uacsb = new SqlConnectionStringBuilder();
            uacsb.DataSource = @"localhost";
            uacsb.InitialCatalog = "MarathonSkills2016";
            uacsb.IntegratedSecurity = true;
            uacsb.ConnectTimeout = 30;
            SqlConnection uac = new SqlConnection(uacsb.ConnectionString);
            SqlCommand uacom = uac.CreateCommand();
            uacom.CommandText = "SELECT * FROM [MarathonSkills2016].[dbo].[User]";
            SqlDataAdapter uada = new SqlDataAdapter();
            uada.SelectCommand = uacom;
            SqlCommandBuilder uacb = new SqlCommandBuilder(uada);
            uada.DeleteCommand = uacb.GetDeleteCommand();
            uada.InsertCommand = uacb.GetInsertCommand();
            uada.UpdateCommand = uacb.GetUpdateCommand();
            DataTable uadt = new DataTable();
            uada.Fill(uadt);
            foreach (DataRow eaec in uadt.Rows)
            {
                if (eaec["Email"] as string == CurrentUserContext.Email)
                {
                    eaec["Password"] = CurrentUserContext.Password;
                    eaec["LastName"] = CurrentUserContext.LastName;
                    eaec["FirstName"] = CurrentUserContext.FirstName;
                    uada.Update(uadt);
                    MessageBox.Show("User successfully edited", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            SqlConnectionStringBuilder racsb = new SqlConnectionStringBuilder();
            racsb.DataSource = @"localhost";
            racsb.InitialCatalog = "MarathonSkills2016";
            racsb.IntegratedSecurity = true;
            racsb.ConnectTimeout = 30;
            SqlConnection rac = new SqlConnection(racsb.ConnectionString);
            SqlCommand racom = rac.CreateCommand();
            racom.CommandText = "SELECT * FROM Runner";
            SqlDataAdapter rada = new SqlDataAdapter();
            rada.SelectCommand = racom;
            SqlCommandBuilder racb = new SqlCommandBuilder(rada);
            rada.DeleteCommand = racb.GetDeleteCommand();
            rada.InsertCommand = racb.GetInsertCommand();
            rada.UpdateCommand = racb.GetUpdateCommand();
            DataTable radt = new DataTable();
            rada.Fill(radt);
            foreach (DataRow ranr in radt.Rows)
            {
                if ((int)ranr["RunnerId"] == RunnerCurrentContext.RunnerId)
                {
                    ranr["Gender"] = RunnerCurrentContext.Gender;
                    ranr["DateOfBirth"] = RunnerCurrentContext.DOB;
                    ranr["CountryCode"] = RunnerCurrentContext.CountryCode;
                    ranr["Photo"] = RunnerCurrentContext.Photo;
                    rada.Update(radt);
                    MessageBox.Show("Runner successfully edited", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            MessageBox.Show("Данные бегуна успешно изменены.", "MarathonSkills2015", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.OpenForms[Application.OpenForms.Count - 2].Visible = true;
            Dispose();
        }

        public _16()
        {
            InitializeComponent();
            oldphoto = RunnerCurrentContext.Photo;
            DataTable g = LoadGender();
            comboBox1.DataSource = g;
            comboBox1.ValueMember = "Gender";
            comboBox1.DisplayMember = "Gender";
            DataTable c = LoadCountry();
            comboBox2.DataSource = c;
            comboBox2.DisplayMember = "CountryName";
            comboBox2.ValueMember = "CountryCode";
            label12.Text = CurrentUserContext.Email;
            hintTextBox4.Text = CurrentUserContext.LastName;
            hintTextBox5.Text = CurrentUserContext.FirstName;
            comboBox1.SelectedValue = RunnerCurrentContext.Gender;
            comboBox2.SelectedValue = RunnerCurrentContext.CountryCode;
            dateTimePicker1.Value = RunnerCurrentContext.DOB;
            if ((RunnerCurrentContext.Photo == null) | (RunnerCurrentContext.Photo == ""))
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\dsa.png");
            }
            else
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\RunnerPhoto\" + RunnerCurrentContext.Photo);
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

        private void _16_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms[Application.OpenForms.Count - 2].Visible = true;
            Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (RunnerCurrentContext.Photo != oldphoto) RunnerCurrentContext.Photo = oldphoto;
            Close();
        }

        private void marathonSkills2016BasePanel1_BackButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            oldphoto = RunnerCurrentContext.Photo;
            RunnerCurrentContext.Photo = "";
            Image b = Image.FromFile(Application.StartupPath + "\\dsa.png");
            pictureBox1.Image = b;
            hintTextBox6.Text = "";
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
                    oldphoto = RunnerCurrentContext.Photo;
                    RunnerCurrentContext.Photo = ofd.FileName;
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
                CurrentUserContext.Password = hintTextBox2.Text;
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
            CurrentUserContext.LastName = hintTextBox4.Text;
            CurrentUserContext.FirstName = hintTextBox5.Text;
            RunnerCurrentContext.CountryCode = comboBox2.SelectedValue as string;
            RunnerCurrentContext.DOB = dateTimePicker1.Value;
            RunnerCurrentContext.Gender = comboBox1.SelectedValue as string;
            if ((RunnerCurrentContext.Photo != "") & (RunnerCurrentContext.Photo != oldphoto))
            {
                FileInfo fi = new FileInfo(RunnerCurrentContext.Photo);
                RunnerCurrentContext.Photo = fi.Name;
                if (!File.Exists(Application.StartupPath + @"\RunnerPhoto\" + fi.Name))
                    fi.CopyTo(Application.StartupPath + @"\RunnerPhoto\" + fi.Name);
            }
            Register();
        }
    }
}
