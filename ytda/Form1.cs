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

namespace ytda
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        private void gitf3()
        {
            this.Hide();
            Form3 frm3 = new Form3();
            frm3.ShowDialog();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dg = new DialogResult();
            dg = MessageBox.Show("Çıkmak istediğinizden emin misiniz?","UYARI",MessageBoxButtons.YesNo);

            if (dg == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            label1.Text = " --ÜRÜN TAKİP PROGRAMI-- ";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = label1.Text.Substring(1) + label1.Text.Substring(0,1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kadi = textBox1.Text;
            string sifre = textBox2.Text;
            con = new SqlConnection("Server=.;Initial Catalog=db2;Integrated Security=SSPI");
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM klnc WHERE kadi='" + kadi + "'AND sifre='" + sifre + "'";
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Hoşgeldiniz!");
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                gitf3();
            }
            else if (textBox1.Text == "admin" && textBox2.Text == "1354")
            {
                this.Hide();
                Form3 f3=new Form3();
                f3.d1.Visible = true;
                f3.ShowDialog();
                gitf3();
            }
            else if (textBox1.Text == "25" && textBox2.Text == "E06")
            {
                button3.Visible = true;
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else if (textBox1.Text != "admin" || textBox2.Text != "1354") 
            {
                MessageBox.Show("Yetkiniz bulunmamaktadır.", "UYARI");
                this.BackColor = Color.White;
                label1.Text = " --ÜRÜN TAKİP PROGRAMI-- ";
                label2.Text = "Kullanıcı Adı: ";
                label2.ForeColor = Color.Black;
                label3.ForeColor = Color.Black;
                textBox1.Text = "";
                textBox2.Text = "";
                label1.Visible = true;
                linkLabel1.Visible = true;
                button3.Visible = false;
            }
            else
            {
                MessageBox.Show("Hatalı bilgi girdiniz lütfen kontrol ediniz");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
                Form2 frm2 = new Form2();
                frm2.ShowDialog();
        }

        public void button3_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Black;
            label2.Text = "Admin Girişi: ";
            label2.ForeColor = Color.Red;
            label3.ForeColor = Color.Red;
            label1.Visible = false;
            linkLabel1.Visible = false;
        }
    }
}
