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
            MessageBox.Show("hoşgeldiniiiizzzz");
            timer1.Enabled = true;
            label1.Text = " --ÜRÜN TAKİP PROGRAMI-- ";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = label1.Text.Substring(1) + label1.Text.Substring(0,1);
        }

        private void button1_Click(object sender, EventArgs e)//ekle butonu
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
            else if (textBox1.Text == "admin" && textBox2.Text == "1354")//admin girişi
            {
                this.Hide();
                Form3 f3=new Form3();
                f3.d1.Visible = true;
                f3.ShowDialog();
                gitf3();
            }
            else if (textBox1.Text == "25" && textBox2.Text == "E06")//admin yalnış girer ise bu kodla tekrar giriş yapabilmek için geri döndürür
            {
                button3.Visible = true;
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else if (textBox1.Text != "admin" || textBox2.Text != "1354")//admin girişi yanlış girilir ise form ekranı ve label, textbox'lara yaptığımız değişiklikleri eski haline getirir
            {
                MessageBox.Show("Yetkiniz bulunmamaktadır.", "UYARI");
                this.BackColor = Color.White;//from ekranının arka plan rengini değiştirir
                label1.Text = " --ÜRÜN TAKİP PROGRAMI-- ";//kayan yazı oluşdurduğumuz label'ın texti
                label2.Text = "Kullanıcı Adı: ";//Admin girişi olarak ayarladığımız label'ın text'ini Kullanıcı adı olarak değiştiriyoruz
                label2.ForeColor = Color.Black;//label2 itemının yazı rengini değiştiriyoruz
                label3.ForeColor = Color.Black;
                textBox1.Text = "";
                textBox2.Text = "";
                label1.Visible = true;//label1 görünüşünü aktif yapıyoruz
                linkLabel1.Visible = true;//linklabel itemının görüntüsünü aktif yapıyoruz
                button3.Visible = false;//buton3 ıtemının görünüşünü pasifleştiriyoruz
            }
            else
            {
                MessageBox.Show("Hatalı bilgi girdiniz lütfen kontrol ediniz");//hata mesajı
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)//linklabel'a tıklandığında formu kapatıp bizi form2'ye atıyor
        {
            this.Hide();
            Form2 frm2 = new Form2();
            frm2.ShowDialog();
        }

        public void button3_Click(object sender, EventArgs e)//admin butonu tılandığın da tasarımlar da ve özellikler de değişiklikler oluyor
        {
            this.BackColor = Color.Black;//form arka plan rengi siyah yapıyor
            label2.Text = "Admin Girişi: ";//label2 itemın text'ini Admin Girişi olarak değiştiriyoruz
            label2.ForeColor = Color.Red;//label2 ıteminin yazı rengini kırımızı yapıyoruz
            label3.ForeColor = Color.Red;
            label1.Visible = false;//label1 itemının görünürlüğünü pasifleştiriyoruz
            linkLabel1.Visible = false;//linklabel itemının görünürlüğünü pasifleştiriyoruz
        }
    }
}
