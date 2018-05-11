using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarathonSkillsDummy2
{
    public partial class _15 : Form
    {

        int lx = 171;
        int ly = 107;
        int lx1 = 171;
        int ly1 = 333;

        public _15()
        {
            InitializeComponent();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            pictureBox11.Image = pictureBox2.Image;
            label2.Text = "F1 Машина";
            label3.Text = "Имеет скорость 345 км/ч\n\nЕй нужно " + Convert.ToString(Math.Round(42.0/345, 2)) + " часов для завершения маршрута";
            label2.Location = new Point(Convert.ToInt32(lx - (label2.ClientSize.Width / 2) + (label2.Font.SizeInPoints)), ly);
            label3.Location = new Point(Convert.ToInt32(lx - (label3.ClientSize.Width / 2) + (label3.Font.SizeInPoints)), ly1);
        }

        private void _15_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms[Application.OpenForms.Count - 2].Visible = true;
            Dispose();
        }

        private void marathonSkills2016BasePanel1_BackButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            pictureBox11.Image = pictureBox1.Image;
            label2.Text = "Ягуар";
            label3.Text = "Имеет скорость 80 км/ч\n\nЕму нужно " + Convert.ToString(Math.Round(42.0 / 80, 2)) + " часов для завершения маршрута";
            label2.Location = new Point(Convert.ToInt32(lx - (label2.ClientSize.Width / 2) + (label2.Font.SizeInPoints)), ly);
            label3.Location = new Point(Convert.ToInt32(lx - (label3.ClientSize.Width / 2) + (label3.Font.SizeInPoints)), ly1);
        }

        private void label11_Click(object sender, EventArgs e)
        {
            pictureBox11.Image = pictureBox3.Image;
            label2.Text = "Бобр";
            label3.Text = "Имеет скорость 35 км/ч\n\nЕму нужно " + Convert.ToString(Math.Round(42.0 / 35, 2)) + " часов для завершения маршрута";
            label2.Location = new Point(Convert.ToInt32(lx - (label2.ClientSize.Width / 2) + (label2.Font.SizeInPoints)), ly);
            label3.Location = new Point(Convert.ToInt32(lx - (label3.ClientSize.Width / 2) + (label3.Font.SizeInPoints)), ly1);
        }

        private void label12_Click(object sender, EventArgs e)
        {
            pictureBox11.Image = pictureBox4.Image;
            label2.Text = "Ленивец";
            label3.Text = "Имеет скорость 0.12 км/ч\n\nЕму нужно " + Convert.ToString(Math.Round(42.0 / 0.12, 2)) + " часов для завершения маршрута";
            label2.Location = new Point(Convert.ToInt32(lx - (label2.ClientSize.Width / 2) + (label2.Font.SizeInPoints)), ly);
            label3.Location = new Point(Convert.ToInt32(lx - (label3.ClientSize.Width / 2) + (label3.Font.SizeInPoints)), ly1);
        }

        private void label13_Click(object sender, EventArgs e)
        {
            pictureBox11.Image = pictureBox5.Image;
            label2.Text = "Дождевой червь";
            label3.Text = "Имеет скорость 0.03 км/ч\n\nЕму нужно " + Convert.ToString(Math.Round(42.0 / 0.03, 2)) + " часов для завершения маршрута";
            label2.Location = new Point(Convert.ToInt32(lx - (label2.ClientSize.Width / 2) + (label2.Font.SizeInPoints)), ly);
            label3.Location = new Point(Convert.ToInt32(lx - (label3.ClientSize.Width / 2) + (label3.Font.SizeInPoints)), ly1);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            pictureBox11.Image = pictureBox10.Image;
            label2.Text = "Футбольное поле";
            label3.Text = "Имеет длину 105 метров\n\nНужно " + Convert.ToString(Math.Round(42.0 * 1000 / 105, 2)) + " полей для покрытия маршрута";
            label2.Location = new Point(Convert.ToInt32(lx - (label2.ClientSize.Width / 2) + (label2.Font.SizeInPoints)), ly);
            label3.Location = new Point(Convert.ToInt32(lx - (label3.ClientSize.Width / 2) + (label3.Font.SizeInPoints)), ly1);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            pictureBox11.Image = pictureBox9.Image;
            label2.Text = "Airbus A380";
            label3.Text = "Имеет длину 73 метра\n\nНужно " + Convert.ToString(Math.Round(42.0 * 1000 / 73, 2)) + " самолетов для покрытия маршрута";
            label2.Location = new Point(Convert.ToInt32(lx - (label2.ClientSize.Width / 2) + (label2.Font.SizeInPoints)), ly);
            label3.Location = new Point(Convert.ToInt32(lx - (label3.ClientSize.Width / 2) + (label3.Font.SizeInPoints)), ly1);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            pictureBox11.Image = pictureBox8.Image;
            label2.Text = "Школьный автобус";
            label3.Text = "Имеет длину 10 метров\n\nНужно " + Convert.ToString(Math.Round(42.0 * 1000 / 10, 2)) + " автобусов для покрытия маршрута";
            label2.Location = new Point(Convert.ToInt32(lx - (label2.ClientSize.Width / 2) + (label2.Font.SizeInPoints)), ly);
            label3.Location = new Point(Convert.ToInt32(lx - (label3.ClientSize.Width / 2) + (label3.Font.SizeInPoints)), ly1);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            pictureBox11.Image = pictureBox7.Image;
            label2.Text = "Рональдино";
            label3.Text = "Имеет рост 1.81 метров\n\nНужно " + Convert.ToString(Math.Round(42.0 * 1000 / 1.81, 2)) + " людей для покрытия маршрута";
            label2.Location = new Point(Convert.ToInt32(lx - (label2.ClientSize.Width / 2) + (label2.Font.SizeInPoints)), ly);
            label3.Location = new Point(Convert.ToInt32(lx - (label3.ClientSize.Width / 2) + (label3.Font.SizeInPoints)), ly1);
        }

        private void label8_Click(object sender, EventArgs e)
        {
            pictureBox11.Image = pictureBox6.Image;
            label2.Text = "Гавайские шлепанцы";
            label3.Text = "Имеет длину 0.245 метров\n\nНужно " + Convert.ToString(Math.Round(42.0 * 1000 / 0.245, 2)) + " тапок для покрытия маршрута";
            label2.Location = new Point(Convert.ToInt32(lx - (label2.ClientSize.Width / 2) + (label2.Font.SizeInPoints)), ly);
            label3.Location = new Point(Convert.ToInt32(lx - (label3.ClientSize.Width / 2) + (label3.Font.SizeInPoints)), ly1);
        }
    }
}
