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
using System.Threading;
using KBCsv;

namespace MarathonSkillsDummy2
{
    public partial class _29 : Form
    {
        private bool IsWorking = false;
        private List<VolunteerInfo> vt;
        private int addCount = 0;
        private int changedCount = 0;
        private string fname = "";

        private Thread CSVLThread, DBIThread;

        public _29()
        {
            InitializeComponent();
            vt = new List<VolunteerInfo>();
        }



        private void ReportStatus(string status)
        {
            label4.Text = status;
        }

        private void ReportProgress(int curr, int max)
        {
            label5.Text = Convert.ToString(curr) + "/" + Convert.ToString(max);
        }

        private void ClearProgress()
        {
            label5.Text = "";  
        }

        private void ClearStatus()
        {
            label4.Text = "Готовность";
        }

        private void blockbuttons()
        {
            button1.Enabled = false;
            button2.Enabled = false;
        }

        private void closeform()
        {
            Close();
        }


        private void LoadCSV()
        {
            vt.Clear();
            IsWorking = true;
            Invoke(new Action(() => { blockbuttons(); }));
            Invoke(new Action(() => { ReportStatus("Загружаю файл"); }));
            StreamReader sr = new StreamReader(fname);
            int count = sr.ReadToEnd().Split('\n').Length;
            sr.Close();
            StreamReader rd = new StreamReader(fname);
            int curr = 0;
            CsvReader reader = new CsvReader(rd, true);
            try
            {
                reader.ReadHeaderRecord();
            }
            catch (InvalidOperationException E)
            {
                MessageBox.Show("File read error!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Invoke(new Action(() => { ClearStatus(); ClearProgress(); }));
                IsWorking = false;
                sr.Close();
                Invoke(new Action(() => { closeform(); }));
                return;
            }
            //reader.HeaderRecord
            while (reader.HasMoreRecords)
            {
                Invoke(new Action(() => { ReportProgress(curr, count - 1); }));
                var obj = reader.ReadDataRecord();
                VolunteerInfo vi = new VolunteerInfo();
                try
                {
                    vi.CountryCode = obj["CountryCode"];
                    vi.FirstName = obj["FirstName"];
                    vi.Gender = obj["Gender"];
                    vi.LastName = obj["LastName"];
                }
                catch (InvalidOperationException E)
                {
                    MessageBox.Show("File read error!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Invoke(new Action(() => { ClearStatus(); ClearProgress(); }));
                    IsWorking = false;
                    rd.Close();
                    Invoke(new Action(() => { closeform(); }));
                    return;
                }
                catch (NullReferenceException E)
                {
                    MessageBox.Show("File read error!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Invoke(new Action(() => { ClearStatus(); ClearProgress(); }));
                    IsWorking = false;
                    rd.Close();
                    Invoke(new Action(() => { closeform(); }));
                    return;
                }
                catch (ArgumentException E)
                {
                    MessageBox.Show("File read error!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Invoke(new Action(() => { ClearStatus(); ClearProgress(); }));
                    IsWorking = false;
                    rd.Close();
                    Invoke(new Action(() => { closeform(); }));
                    return;
                }
                try
                {
                    vi.VolunteerId = Convert.ToInt32(obj["VolunteerId"]);
                }
                catch (FormatException E)
                {
                    MessageBox.Show("File read error!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Invoke(new Action(() => { ClearStatus(); ClearProgress(); }));
                    IsWorking = false;
                    rd.Close();
                    Invoke(new Action(() => { closeform(); }));
                    return;
                }
                catch (OverflowException E)
                {
                    MessageBox.Show("File read error!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Invoke(new Action(() => { ClearStatus(); ClearProgress(); }));
                    IsWorking = false;
                    rd.Close();
                    Invoke(new Action(() => { closeform(); }));
                    return;
                }
                catch (InvalidOperationException E)
                {
                        MessageBox.Show("File read error!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Invoke(new Action(() => { ClearStatus(); ClearProgress(); }));
                        IsWorking = false;
                        rd.Close();
                        Invoke(new Action(() => { closeform(); }));
                        return;
                }
                catch (NullReferenceException E)
                {
                    MessageBox.Show("File read error!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Invoke(new Action(() => { ClearStatus(); ClearProgress(); }));
                    IsWorking = false;
                    rd.Close();
                    Invoke(new Action(() => { closeform(); }));
                    return;
                }
                catch (ArgumentException E)
                {
                    MessageBox.Show("File read error!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Invoke(new Action(() => { ClearStatus(); ClearProgress(); }));
                    IsWorking = false;
                    rd.Close();
                    Invoke(new Action(() => { closeform(); }));
                    return;
                }
                vt.Add(vi);
                curr++;
            }
            rd.Close();
            if (vt.Count < 1)
            {
                MessageBox.Show("File is empty!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Invoke(new Action(() => { ClearStatus(); ClearProgress(); }));
                IsWorking = false;
                Invoke(new Action(() => { closeform(); }));
                return;
            }
            Invoke(new Action(() => { ReportStatus("Проверяю данные"); }));
            for (int i = 0; i < vt.Count; i++)
            {
                Invoke(new Action(() => { ReportProgress(i, vt.Count - 1); }));
                Regex reg1 = new Regex("^(?=.*[A-Za-z-'])[A-Za-z'-]{2,}$");
                if (!reg1.IsMatch(vt[i].FirstName) | !reg1.IsMatch(vt[i].LastName))
                {
                    MessageBox.Show("Wrong string format!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Invoke(new Action(() => { ClearStatus(); ClearProgress(); }));
                    IsWorking = false;
                    Invoke(new Action(() => { closeform(); }));
                    return;
                }
                Regex reg2 = new Regex("^(?=.*[A-Z])[A-Z]{3}$");
                if (!reg1.IsMatch(vt[i].CountryCode))
                {
                    MessageBox.Show("Wrong string format!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Invoke(new Action(() => { ClearStatus(); ClearProgress(); }));
                    IsWorking = false;
                    Invoke(new Action(() => { closeform(); }));
                    return;
                } 
                if ((vt[i].Gender != "F") & (vt[i].Gender != "M"))
                {
                    MessageBox.Show("Wrong string format!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Invoke(new Action(() => { ClearStatus(); ClearProgress(); }));
                    IsWorking = false;
                    Invoke(new Action(() => { closeform(); }));
                    return;
                }
                if (vt[i].Gender == "F")
                {
                    VolunteerInfo tmp = vt[i];
                    tmp.Gender = "Female";
                    vt[i] = tmp;
                }
                if (vt[i].Gender == "M")
                {
                    VolunteerInfo tmp = vt[i];
                    tmp.Gender = "Male";
                    vt[i] = tmp;
                }
            }
            Invoke(new Action(() => { ReportStatus("Импортирую данные"); }));
            Invoke(new Action(() => { ClearProgress(); }));
            SqlConnectionStringBuilder lvcsb = new SqlConnectionStringBuilder();
            lvcsb.DataSource = @"localhost";
            lvcsb.InitialCatalog = "MarathonSkills2016";
            lvcsb.IntegratedSecurity = true;
            lvcsb.ConnectTimeout = 30;
            SqlConnection lvc = new SqlConnection(lvcsb.ConnectionString);
            SqlCommand lvcom = lvc.CreateCommand();
            lvcom.CommandText = "SELECT * FROM Volunteer";
            SqlDataAdapter lvda = new SqlDataAdapter();
            lvda.SelectCommand = lvcom;
            SqlCommandBuilder lvcomb = new SqlCommandBuilder(lvda);
            lvda.DeleteCommand = lvcomb.GetDeleteCommand();
            lvda.InsertCommand = lvcomb.GetInsertCommand();
            lvda.UpdateCommand = lvcomb.GetUpdateCommand();
            DataTable updater = new DataTable();
            lvda.Fill(updater);
            for (int i = 0; i < vt.Count; i++)
            {
                Invoke(new Action(() => { ReportProgress(i, vt.Count - 1); }));
                bool isfound = false;
                if (updater.Rows.Count > 0)
                {
                    foreach (DataRow row in updater.Rows)
                    {
                        if ((int)row["VolunteerId"] == vt[i].VolunteerId)
                        {
                            isfound = true;
                            row["FirstName"] = vt[i].FirstName;
                            row["LastName"] = vt[i].LastName;
                            row["CountryCode"] = vt[i].CountryCode;
                            row["Gender"] = vt[i].Gender;
                            changedCount++;
                        }
                    }
                    if (isfound == false)
                    {
                        DataRow ua = updater.NewRow();
                        ua["VolunteerId"] = vt[i].VolunteerId;
                        ua["FirstName"] = vt[i].FirstName;
                        ua["LastName"] = vt[i].LastName;
                        ua["CountryCode"] = vt[i].CountryCode;
                        ua["Gender"] = vt[i].Gender;
                        updater.Rows.Add(ua);
                        addCount++;
                    }
                }
                else
                {
                    DataRow ua = updater.NewRow();
                    ua["VolunteerId"] = vt[i].VolunteerId;
                    ua["FirstName"] = vt[i].FirstName;
                    ua["LastName"] = vt[i].LastName;
                    ua["CountryCode"] = vt[i].CountryCode;
                    ua["Gender"] = vt[i].Gender;
                    updater.Rows.Add(ua);
                    addCount++;
                }
            }
            try
            {
                lvda.Update(updater);
            }
            catch (Exception E)
            {
                MessageBox.Show("Some issue occured with database updation", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Invoke(new Action(() => { ClearStatus(); ClearProgress(); }));
                IsWorking = false;
                Invoke(new Action(() => { closeform(); }));
                return;
            }
            MessageBox.Show("Загружено записей: " + Convert.ToString(addCount + changedCount) +
                            "\nДобавлено записей: " + Convert.ToString(addCount) +
                            "\nИзменено записей: " + Convert.ToString(changedCount),
                            "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Invoke(new Action(() => { ClearStatus(); ClearProgress(); }));
            IsWorking = false;
            Invoke(new Action(() => { closeform(); }));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!IsWorking)
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
            else
            {
                MessageBox.Show("Не могу выходить во время работы!");
            }
        }

        private void _29_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsWorking)
            {
                _28 f28 = new _28();
                f28.Show();
                Dispose();
            }
            else
            {
                MessageBox.Show("Не могу выходить во время работы!");
                e.Cancel = true;
                Refresh();
            }
        }

        private void marathonSkills2016BasePanel1_BackButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV|*.csv";
            ofd.Title = "Poop";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fname = ofd.FileName;
                hintTextBox1.Text = ofd.FileName;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (fname == "")
            {
                MessageBox.Show("File doesn't choosed!", "MS2015", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            ThreadStart thr = new ThreadStart(LoadCSV);
            CSVLThread = new Thread(thr);
            CSVLThread.IsBackground = false;
            CSVLThread.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Загружено записей: " + Convert.ToString(addCount + changedCount) +
                            "\nДобавлено записей: " + Convert.ToString(addCount) +
                            "\nИзменено записей: " + Convert.ToString(changedCount));
        }
    }
}
