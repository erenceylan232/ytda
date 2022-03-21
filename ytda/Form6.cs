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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        SqlConnection con;//veritabanı ile bağlantı kurmak için atadığımız değişken
        SqlCommand cmd;//bağlantı kurulan veritabanınımıza komut vermek için atadığımız değişken
        SqlDataAdapter da;//veritabanı ve formumuz arası köprü görevi görmesi için atadığımız değişken
        DataSet ds;//veritabanında ki verileri/bilgileri datagridview de göstermek için atadığımız değişken
        void dd()//daatagridview de tablomuzu göstermek ve yenilemek için kullandığımız kod satırı
        {
            con = new SqlConnection("Server=.; Initial Catalog=db2;Integrated Security=SSPI");
            da = new SqlDataAdapter("Select * From stok", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "stok");
            dataGridView1.DataSource = ds.Tables["stok"];
            con.Close();
            DataGridViewColumn id = dataGridView1.Columns["ID"];//datagride çekilen tablonun sütun adlarını değiştirmek amaçlı değişken atyıp bunu çekilen tablonun sütun adı ile eşitliyoruz
            DataGridViewColumn urkd = dataGridView1.Columns["urkd"];
            DataGridViewColumn kID = dataGridView1.Columns["kID"];
            DataGridViewColumn urad = dataGridView1.Columns["urad"];
            DataGridViewColumn uradt = dataGridView1.Columns["uradt"];
            DataGridViewColumn urfyt = dataGridView1.Columns["urfyt"];
            id.HeaderText = "ID";//burada da datagride çekilen tablonun sütun adlarını istediğimiz stüun adına değiştiriyoruz
            urkd.HeaderText = "ÜRÜN KODU";
            kID.HeaderText = "KULLANICI ID";
            urad.HeaderText = "ÜRÜN ADI";
            uradt.HeaderText = "ÜRÜN ADETİ";
            urfyt.HeaderText = "ÜRÜN FİYATI";
        }
        private void button1_Click(object sender, EventArgs e)//ekle butonu
        {
            using (SqlCommand cmd = new SqlCommand(@"insert into stok(urkd, kID, urad, uradt, urfyt) values(@urkd, @kID, @urad, @uradt, @urfyt)", con))
            //parametre kullanacağımız için sqlcommand komut kod satırımıza tekrar değişken atıyoruz ve veritabanında ki tablomuzun column yani sütunlarına değişken olarak parametreye ekliyoruz
            {
                cmd.Parameters.Add("@urkd", SqlDbType.Int).Value = textBox1.Text;//parametre ekle komutu ile değişken atadığımız sütun textbox1'in text'inden veriyi alacak ve kaydedecek
                cmd.Parameters.Add("@kID", SqlDbType.Int).Value = textBox5.Text;
                cmd.Parameters.Add("@urad", SqlDbType.NChar).Value = textBox2.Text;
                cmd.Parameters.Add("@uradt", SqlDbType.Int).Value = textBox3.Text;
                decimal.TryParse(textBox4.Text, out decimal urfyt);//textbox4'ün text'ini decimal türüne dönştürüp urfyt değişkeni olarak atadık
                cmd.Parameters.Add("urfyt", SqlDbType.Decimal).Value = urfyt;//değişken atadığımız sütun parametresi decimal veri tipi ile veritabanına ekleniyor veriyi urfyt adlı değişkenden alıyor

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            MessageBox.Show("İşlem başarılı.");

            /*cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO stok VALUES('" + textBox1.Text + "', '" + textBox5.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + double.Parse(textBox4.Text) + "')";
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show(".");*/
            foreach (Control item in this.Controls)//forech döngüsü ile textboxların textlerini kontrol edip temizleyen kod satırı
            {
                if (item is TextBox)
                {
                    TextBox tbox = (TextBox)item;
                    tbox.Clear();
                }
            }
            dd();
        }

        private void button2_Click(object sender, EventArgs e)//güncelle butonu
        {
            cmd = new SqlCommand("UPDATE stok SET urkd=@urkd, urad=@urad, uradt=@uradt, urfyt=@urfyt WHERE ID=@id", con);//sütunları parametre olarak değişkene atadık ve id'den çekip veriyi güncelleyecek kod satırı oluşturduk
            cmd.Parameters.AddWithValue("@id", int.Parse(textBox6.Text));
            cmd.Parameters.AddWithValue("@urkd", textBox1.Text);
            cmd.Parameters.AddWithValue("@urad", textBox2.Text);
            cmd.Parameters.AddWithValue("@uradt", int.Parse(textBox3.Text));
            decimal.TryParse(textBox4.Text, out decimal urfyt);
            cmd.Parameters.AddWithValue("urfyt", SqlDbType.Decimal).Value = urfyt;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            /*cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE stok SET urkd='" + textBox1.Text + "', urad='" + textBox2.Text + "', uradt='" + textBox3.Text + "', urfyt='" + textBox4.Text + "' WHERE ID='" + textBox6.Text + "'";
            cmd.ExecuteNonQuery();
            con.Close();*/
            MessageBox.Show("İşlem başarılı.");
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    TextBox tbox = (TextBox)item;
                    tbox.Clear();
                }
            }
            dd();
        }

        private void button3_Click(object sender, EventArgs e)//sil butonu
        {
            cmd = new SqlCommand("DELETE FROM stok WHERE ID=@id", con);
            cmd.Parameters.AddWithValue("@id", textBox6.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            /*cmd=new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "DELETE FROM stok WHERE ID='" + textBox6.Text + "'";
            cmd.ExecuteNonQuery();
            con.Close();*/
            MessageBox.Show("İşlem başarılı.s");
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    TextBox tbox = (TextBox)item;
                    tbox.Clear();
                }
            }
            dd();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3();
            f3.ShowDialog();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            dd();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox6.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            dd();
        }
    }
}
