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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == textBox3.Text) 
            {
                Form7 f7 = new Form7();
                this.Hide();
                con = new SqlConnection("Server=.;Initial Catalog=db2;Integrated Security=SSPI");
                cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "INSERT INTO klnc VALUES('"+ textBox1.Text +"','"+ textBox2.Text +"')";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Yeni kullanıcı eklendi.");
                cmd = new SqlCommand("SELECT MAX(ID) FROM klnc", con);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    rd.Read();
                    MessageBox.Show("Kullanıcı ID=" + rd.GetInt32(0).ToString() + " lütfen ID'nizi saklıyınız.");
                }
                con.Close();
                Form1 frm1 = new Form1();
                frm1.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bilgileriniz aynı değildir!","Uyarı!");
            }
        }
    }
}
