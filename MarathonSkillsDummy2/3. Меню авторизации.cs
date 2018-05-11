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
    public partial class _3 : Form
    {
        public _3()
        {
            InitializeComponent();
        }

        private void marathonSkills2016BasePanel1_BackButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void _3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms[0].Visible = true;
            Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (hintTextBox1.Text == "")
            {
                MessageBox.Show("Вы ввели пустой логин!", "Marathon Skills 2018", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (hintTextBox2.Text == "")
            {
                MessageBox.Show("Вы ввели пустой пароль!", "Marathon Skills 2018", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            SqlConnectionStringBuilder sqlcsb = new SqlConnectionStringBuilder();
            sqlcsb.DataSource = @"localhost";
            sqlcsb.InitialCatalog = "MarathonSkills2016";
            sqlcsb.IntegratedSecurity = true;
            sqlcsb.ConnectTimeout = 30;
            SqlConnection sqlcon = new SqlConnection(sqlcsb.ConnectionString);
            SqlDataAdapter sqlda = new SqlDataAdapter();
            SqlCommand uloader = sqlcon.CreateCommand();
            uloader.CommandText = "SELECT * FROM [MarathonSkills2016].[dbo].[User]";
            sqlda.SelectCommand = uloader;
            DataTable auth = new DataTable();
            sqlda.Fill(auth);
            bool flog = false;
            for (int i = 0; i < auth.Rows.Count; i++)
            {
                if (auth.Rows[i]["Email"] as string == hintTextBox1.Text)
                {
                    flog = true;
                    CurrentUserContext.Email = auth.Rows[i]["Email"] as string;
                    CurrentUserContext.Password = auth.Rows[i]["Password"] as string;
                    CurrentUserContext.FirstName = auth.Rows[i]["FirstName"] as string;
                    CurrentUserContext.LastName = auth.Rows[i]["LastName"] as string;
                    CurrentUserContext.RoleId = auth.Rows[i]["RoleId"] as string;
                }
            }
            if (flog)
            {
                if (CurrentUserContext.Password == hintTextBox2.Text)
                {
                    CurrentUserContext.IsUsingNow = true;
                    switch (CurrentUserContext.RoleId[0])
                    {
                        case 'A':
                            {
                                MessageBox.Show("Вы администратор!", "Marathon Skills 2018", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                _20 f20 = new _20();
                                f20.Show();
                                Dispose();
                            }
                            break;
                        case 'C':
                            {
                                MessageBox.Show("Вы координатор!", "Marathon Skills 2018", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                _19 f19 = new _19();
                                f19.Show();
                                Dispose();
                            }
                            break;
                        case 'R':
                            {
                                MessageBox.Show("Вы бегун!", "Marathon Skills 2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                SqlConnectionStringBuilder racsb = new SqlConnectionStringBuilder();
                                racsb.DataSource = @"localhost";
                                racsb.InitialCatalog = "MarathonSkills2016";
                                racsb.IntegratedSecurity = true;
                                racsb.ConnectTimeout = 30;
                                SqlConnection rac = new SqlConnection(racsb.ConnectionString);
                                SqlCommand raccom = rac.CreateCommand();
                                raccom.CommandText = "SELECT * FROM Runner WHERE Runner.Email = '" + CurrentUserContext.Email + "'";
                                SqlDataAdapter racda = new SqlDataAdapter();
                                racda.SelectCommand = raccom;
                                DataTable racdt = new DataTable();
                                racda.Fill(racdt);
                                if (racdt.Rows.Count == 0)
                                {
                                    MessageBox.Show("Профиль пользователя поврежден. Обратитесь к координаторам MarathonSkills!", "Marathon Skills 2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                                else
                                {
                                    RunnerCurrentContext.CountryCode = racdt.Rows[0]["CountryCode"] as string;
                                    RunnerCurrentContext.DOB = (DateTime)racdt.Rows[0]["DateOfBirth"];
                                    RunnerCurrentContext.Email = racdt.Rows[0]["Email"] as string;
                                    RunnerCurrentContext.Gender = racdt.Rows[0]["Gender"] as string;
                                    RunnerCurrentContext.Photo = racdt.Rows[0]["Photo"] as string;
                                    RunnerCurrentContext.RunnerId = (int)racdt.Rows[0]["RunnerId"];
                                    _9 f9 = new _9();
                                    f9.Show();
                                    Dispose();
                                }
                            }
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Вы ввели неправильный пароль!", "Marathon Skills 2018", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    CurrentUserContext.Email = "";
                    CurrentUserContext.Password = "";
                    CurrentUserContext.FirstName = "";
                    CurrentUserContext.LastName = "";
                    CurrentUserContext.RoleId = "";
                    return;
                }
            }
            else
            {
                MessageBox.Show("Такого пользователя нет в БД!", "Marathon Skills 2018", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
    }
}
