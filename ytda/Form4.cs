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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;

        public void dd()
        {
            con = new SqlConnection("Server=.; Initial Catalog=db2;Integrated Security=SSPI");
            da = new SqlDataAdapter("Select * From alinan", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "alinan");
            dataGridView1.DataSource = ds.Tables["alinan"];
            con.Close();
            DataGridViewColumn id = dataGridView1.Columns["ID"];
            DataGridViewColumn urkd = dataGridView1.Columns["urkd"];
            DataGridViewColumn kID = dataGridView1.Columns["kID"];
            DataGridViewColumn uradt = dataGridView1.Columns["uradt"];
            DataGridViewColumn urfyt = dataGridView1.Columns["urfyt"];
            id.HeaderText = "ID";
            urkd.HeaderText = "ÜRÜN KODU";
            kID.HeaderText = "KULLANICI ID";
            uradt.HeaderText = "ÜRÜN ADETİ";
            urfyt.HeaderText = "ÜRÜN FİYATI";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlCommand cmd = new SqlCommand(@"insert into alinan(urkd, kID, uradt, urfyt) values(@urkd, @kID, @uradt, @urfyt)", con)) 
            {
                cmd.Parameters.Add("@urkd", SqlDbType.Int).Value = textBox1.Text;
                cmd.Parameters.Add("@kID", SqlDbType.Int).Value = textBox4.Text;
                cmd.Parameters.Add("@uradt", SqlDbType.Int).Value = textBox2.Text;
                decimal.TryParse(textBox3.Text, out decimal urfyt);
                cmd.Parameters.Add("urfyt", SqlDbType.Decimal).Value = urfyt;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            MessageBox.Show("İşlem başarılı.");
            dd();
            /*cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO alinan VALUES('" + textBox1.Text + "', '" + textBox4.Text + "', '" + textBox2.Text + "', '" + double.Parse(textBox3.Text) + "')";
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show(".");
            dd();*/
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    TextBox tbox = (TextBox)item;
                    tbox.Clear();
                }
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            dd();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("UPDATE alinan SET urkd=@urkd, uradt=@uradt, urfyt=@urfyt WHERE ID=@id", con);
            cmd.Parameters.AddWithValue("@id", int.Parse(textBox5.Text));
            cmd.Parameters.AddWithValue("@urkd", textBox1.Text);
            cmd.Parameters.AddWithValue("@uradt", int.Parse(textBox2.Text));
            decimal.TryParse(textBox3.Text, out decimal urfyt);
            cmd.Parameters.AddWithValue("urfyt", SqlDbType.Decimal).Value = urfyt;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            /*cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE  alinan SET urkd='" + textBox1.Text + "', auradt='" + textBox2.Text + "', aurfyt='" + textBox3.Text + "' WHERE ID='" + textBox5.Text + "'";
            cmd.ExecuteNonQuery();
            con.Close();*/
            MessageBox.Show("İşlem başarılı.");
            dd();
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    TextBox tbox = (TextBox)item;
                    tbox.Clear();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "DELETE FROM alinan WHERE ID='" + textBox5.Text + "'";
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("İşlem başarılı");
            dd();
            foreach (Control item in this.Controls)              
            {               
                if (item is TextBox)                    
                {                   
                    TextBox tbox = (TextBox)item;                   
                    tbox.Clear();                   
                }               
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f3=new Form3();
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
    }
}
