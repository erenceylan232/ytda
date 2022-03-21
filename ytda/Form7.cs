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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;

        void dd()//datagrid itemımızı dolduracak kod satırı
        {
            con = new SqlConnection("Server=.; Initial Catalog=db2;Integrated Security=SSPI");
            da = new SqlDataAdapter("Select * From klnc", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "klnc");
            dataGridView1.DataSource = ds.Tables["klnc"];
            con.Close();
            DataGridViewColumn id = dataGridView1.Columns["ID"];
            DataGridViewColumn kadi = dataGridView1.Columns["kadi"];
            DataGridViewColumn sifre = dataGridView1.Columns["sifre"];
            id.HeaderText = "ID";
            kadi.HeaderText = "KULLANICI ADI";
            sifre.HeaderText = "ŞİFRE";
        }
        /*void dd2()
        {
            con = new SqlConnection("Server=.;Initial Catalog=db2;Integrated Security=SSPI");
            con.Open();
            cmd = new SqlCommand("SELECT * FROM klnc", con);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                ListViewItem ls = new ListViewItem();
                ls.Text = rd["ID"].ToString();
                ls.SubItems.Add(rd["kadi"].ToString());
                ls.SubItems.Add(rd["sifre"].ToString());
                listView1.Items.Add(ls);
            }
            con.Close();
        }*/

        private void button1_Click(object sender, EventArgs e)//ekle butonumuz
        {
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO klnc VALUES('" + textBox2.Text + "', '" + textBox3.Text + "')";

            if (cmd.ExecuteNonQuery() != 0)
            {
                con.Close();
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
            /*con.Open();
            cmd = new SqlCommand("INSERT INTO klnc(kadi, sifre) VALUES('" + textBox2.Text.ToString() + "', '" + textBox3.Text.ToString() + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    TextBox tx = (TextBox)item;
                    tx.Clear();
                }
            }
            dd2();*/
        }

        private void button2_Click(object sender, EventArgs e)//güncelle butonumuz
        {
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE klnc SET kadi='" + textBox2.Text + "', sifre='" + textBox3.Text + "' WHERE ID='" + textBox1.Text + "'";
            cmd.ExecuteNonQuery();
            con.Close();
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

        private void button3_Click(object sender, EventArgs e)//sil butonumuz
        {
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "DELETE FROM klnc WHERE ID='" + textBox1.Text + "'";
            cmd.ExecuteNonQuery();
            con.Close();
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

        private void Form7_Load(object sender, EventArgs e)
        {
            dd();//datagrid doldur değişkenimiz
        }

        private void button4_Click(object sender, EventArgs e)//admin girişinde visible false olan butonumuzu tekrar kontrol edip form3 aktaran butonumuz
        {
            this.Hide();
            Form3 f3 = new Form3();
            f3.d1.Visible = true;
            f3.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)//datagrid'de hücre veya satıra tıklandığında bilgileri textboxlara aktaran komut satırımız
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            dd();
        }

        private void button5_Click(object sender, EventArgs e)//datagrid'de gösteren bilgileri excele aktaran ve kaydeden butonumuz
        {
            SaveFileDialog sv = new SaveFileDialog();//kaydetmek için dizin ekranında yardım olması için değişken atadık
            sv.OverwritePrompt = false;//üzerine yazma işlemi için onay mesajı vermesi için bu kod satırını kullandık
            sv.Title = "Excel Dosyaları";//dialog pencerisinin başlığını atadık
            sv.DefaultExt = "xlsx";//dosya uzantımızı xlsx olarak eşitledik
            sv.Filter = "xlsx Dosyaları (*.xlsx)|*xlsx|Tüm Dosyalar(*.*)|*.*";//dialog ekranı açıldığında göreceğimiz dosyaları belirledik
            if (sv.ShowDialog() == DialogResult.OK)//dosya kaydedildiği dialog ekranında 'OK' tuşuna basıldı ise aşağıdaki komut satırını çalıştırıcak
            {
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();//excel uygulaması açıldığı sırada işlem yapacağımız için değişken atadık
                Microsoft.Office.Interop.Excel._Workbook wbk = app.Workbooks.Add(Type.Missing);//excel uygulamasında sayfa açıldığında işlem yazdıracağımız için değişken atadık
                Microsoft.Office.Interop.Excel._Worksheet wkt = null;//açılan ilk sayfamızda çalışma kağıdımızı boş yani null'a eşitliyoruz
                app.Visible = true;//uygulama açıldığında/görünür olduğunda
                wkt = wbk.Sheets["Sayfa1"];//çalışma kağıdında sayfa1 adlı sayfa açıyoruz
                wkt = wbk.ActiveSheet;//sayfayı aktive ediyoruz
                wkt.Name = "Excel dışa aktarım";//çalışma kağımıza isim atıyoruz
                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)//datagridview1'de ki tablomuzun sütunlarını 1 artırarak arama/sayma işlemi yapıyoruz
                {
                    wkt.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;//çalışma kağıdımızın hücre1'inden başlayarak datagridview'deki sütunları excel'e yazdırıyoruz
                }
                for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)//datagridview1'de ki tablomuzun satırlarını arama/sayma işlemi yapıyoruz
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)//datagridview1'de ki sütunları tekrar arama/sayma işlemi yapıyoruz
                    {
                        wkt.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();//çalışma kağıdımızda ki hücremize datagridview1'den çekilen satır ve sütunları string türünde hizalı bir şekilde yazdırıyoruz
                    }
                }
                wbk.SaveAs(sv.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //sayfamızı excel tipi olarak kaydediyoruz
            }
        }
    }
}
