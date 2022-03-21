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
            Button b = Application.OpenForms["Form3"].Controls["d1"] as Button;//uygulama açıldığında form3'de ki d1 butonunu kontrol ediyor ve buton b değişkeni olarak eşitliyoruz 
        }

        private void button2_Click(object sender, EventArgs e)//alınan eşya formuna götüren buton
        {
            this.Hide();
            Form4 f4 = new Form4();
            f4.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)//satılan eşya formuna götüren buton
        {
            this.Hide();
            Form5 f5=new Form5();   
            f5.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)//stok formuna götüren buton
        {
            this.Hide();
            Form6 f6 = new Form6();
            f6.ShowDialog();
        }

        private void d1_Click(object sender, EventArgs e)//kullanıcı formuna götüren buton
        {
            this.Hide();
            Form7 f7 = new Form7();
            f7.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)//datagridde göster butonu
        {
            if (comboBox1.Text == "STOK") //combobox1 text'i stok'a eşit ise datagride stok tablomuzu çekiyor
            {
                con = new SqlConnection("Server=.;Initial Catalog=db2;Integrated Security=SSPI");
                da = new SqlDataAdapter("SELECT * FROM stok", con);
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
            else if (comboBox1.Text == "ALINAN EŞYA")//combobox1 text'i alınan eşya'ya eşit ise datagride alinan tablomuzu çekiyor
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
            else if (comboBox1.Text == "SATILAN EŞYA")//combobox1 text'i satılan eşya'ya eşit ise datagride satilan tablomuzu çekiyor
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

        private void button1_Click(object sender, EventArgs e)//hesapla butonu//burada datagrid cell content ile hücreden labele çektiğimiz verilere işlem yapmaktayız
        {
            double s ,l1,l4;
            l1 = double.Parse(label1.Text);
            l4 = double.Parse(label4.Text);
            s = l1 * l4;
            label6.Text = "" + s.ToString() + " TL";
            label6.Visible = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)//datagride gelen verinin hücrelerini labellere aktarıyouz ve bunları koşul ile yapıyoruz sebebi ise her tablonun hücre sayısının farklı olmasıdır
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

        private void button6_Click(object sender, EventArgs e)//excele aktar butonu
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