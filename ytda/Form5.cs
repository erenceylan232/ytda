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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;

        void dd()
        {
            con = new SqlConnection("Server=.; Initial Catalog=db2;Integrated Security=SSPI");
            da = new SqlDataAdapter("Select * From satilan", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "satilan");
            dataGridView1.DataSource = ds.Tables["satilan"];
            con.Close();
            DataGridViewColumn id = dataGridView1.Columns["ID"];
            DataGridViewColumn urkd = dataGridView1.Columns["urkd"];
            DataGridViewColumn kID = dataGridView1.Columns["kID"];
            DataGridViewColumn uradt = dataGridView1.Columns["suradt"];
            DataGridViewColumn urfyt = dataGridView1.Columns["surfyt"];
            id.HeaderText = "ID";
            urkd.HeaderText = "ÜRÜN KODU";
            kID.HeaderText = "KULLANICI ID";
            uradt.HeaderText = "ÜRÜN ADETİ";
            urfyt.HeaderText = "ÜRÜN FİYATI";
        }
        
        private void button1_Click(object sender, EventArgs e)//ekle butonu
        {
            cmd = new SqlCommand("INSERT INTO satilan(urkd, kID, suradt, surfyt) VALUES(@urkd, @kID, @suradt, @surfyt)", con);
            cmd.Parameters.Add("@urkd", SqlDbType.Int).Value = textBox1.Text;
            cmd.Parameters.Add("@kID", SqlDbType.Int).Value = textBox4.Text;
            cmd.Parameters.Add("@suradt", SqlDbType.Int).Value = textBox2.Text;
            decimal.TryParse(textBox3.Text, out decimal surfyt);
            cmd.Parameters.Add("surfyt", SqlDbType.Decimal).Value = surfyt;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("İşlem başarılı.");
            /*cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO satilan VALUES('" + textBox1.Text + "', '" + textBox4.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "')";
            cmd.ExecuteNonQuery();
            con.Close();*/
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

        private void Form5_Load(object sender, EventArgs e)
        {
            dd();
        }

        private void button2_Click(object sender, EventArgs e)//güncelle butonu
        {
            cmd = new SqlCommand("UPDATE satilan SET urkd=@urkd, suradt=@uradt, surfyt=@urfyt WHERE ID=@id", con);
            cmd.Parameters.AddWithValue("@id", int.Parse(textBox5.Text));
            cmd.Parameters.AddWithValue("@urkd", textBox1.Text);
            cmd.Parameters.AddWithValue("@uradt", int.Parse(textBox2.Text));
            decimal.TryParse(textBox3.Text, out decimal urfyt);
            cmd.Parameters.AddWithValue("urfyt", SqlDbType.Decimal).Value = urfyt;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("İşlem başarılı.");
            /*cmd = new SqlCommand();
            con.Open();
            cmd.Connection=con;
            cmd.CommandText = "UPDATE satilan SET urkd='" + textBox1.Text + "', suradt='" + textBox2.Text + "', surfyt='" + textBox3.Text + "' WHERE ID='" + textBox5.Text + "'";
            cmd.ExecuteNonQuery();
            con.Close();*/
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
            cmd=new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "DELETE FROM satilan WHERE ID='" + textBox5.Text + "'";
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("İşlem başarılı.");
            dd();
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    TextBox tbx = (TextBox)item;
                    tbx.Clear();
                }
            }        
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3();
            f3.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox5.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            dd();
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
