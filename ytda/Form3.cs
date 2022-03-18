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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        private void Form3_Load(object sender, EventArgs e)
        {
            Button b = Application.OpenForms["Form3"].Controls["d1"] as Button;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f4 = new Form4();
            f4.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form5 f5=new Form5();   
            f5.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form6 f6 = new Form6();
            f6.ShowDialog();
        }

        private void d1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form7 f7 = new Form7();
            f7.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "STOK") 
            {
                con = new SqlConnection("Server=.;Initial Catalog=db2;Integrated Security=SSPI");
                da = new SqlDataAdapter("SELECT * FROM stok", con);
                ds = new DataSet();
                con.Open();
                da.Fill(ds, "stok");
                dataGridView1.DataSource = ds.Tables["stok"];
                con.Close();
                DataGridViewColumn id = dataGridView1.Columns["ID"];
                DataGridViewColumn urkd = dataGridView1.Columns["urkd"];
                DataGridViewColumn kID = dataGridView1.Columns["kID"];
                DataGridViewColumn urad = dataGridView1.Columns["urad"];
                DataGridViewColumn uradt = dataGridView1.Columns["uradt"];
                DataGridViewColumn urfyt = dataGridView1.Columns["urfyt"];
                id.HeaderText = "ID";
                urkd.HeaderText = "ÜRÜN KODU";
                kID.HeaderText = "KULLANICI ID";
                urad.HeaderText = "ÜRÜN ADI";
                uradt.HeaderText = "ÜRÜN ADETİ";
                urfyt.HeaderText = "ÜRÜN FİYATI";
            }
            else if (comboBox1.Text == "ALINAN EŞYA")
            {
                con = new SqlConnection("Server=.;Initial Catalog=db2;Integrated Security=SSPI");
                da = new SqlDataAdapter("SELECT * FROM alinan", con);
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
            else if (comboBox1.Text == "SATILAN EŞYA")
            {
                con = new SqlConnection("Server=.;Initial Catalog=db2;Integrated Security=SSPI");
                da = new SqlDataAdapter("SELECT * FROM satilan", con);
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double s ,l1,l4;
            l1 = double.Parse(label1.Text);
            l4 = double.Parse(label4.Text);
            s = l1 * l4;
            label6.Text = "" + s.ToString() + " TL";
            label6.Visible = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (comboBox1.Text == "STOK")
            {
                label1.Visible = true;
                label4.Visible = true;
                label1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                label4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();

            }
            else if (comboBox1.Text == "ALINAN EŞYA")
            {
                label1.Visible = true;
                label4.Visible = true;
                label1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                label4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            }
            else if (comboBox1.Text == "SATILAN EŞYA")
            {
                label1.Visible = true;
                label4.Visible = true;
                label1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                label4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SaveFileDialog sv = new SaveFileDialog();
            sv.OverwritePrompt = false;
            sv.Title = "Excel Dosyaları";
            sv.DefaultExt = "xlsx";
            sv.Filter = "xlsx Dosyaları (*.xlsx)|*xlsx|Tüm Dosyalar(*.*)|*.*";
            if (sv.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook wbk = app.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel._Worksheet wkt = null;
                app.Visible = true;
                wkt = wbk.Sheets["Sayfa1"];
                wkt = wbk.ActiveSheet;
                wkt.Name = "Excel dışa aktarım";
                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    wkt.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                }
                for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        wkt.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
                wbk.SaveAs(sv.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
        }
    }
}