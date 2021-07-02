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


namespace Hafta1_Deneme
{
    public partial class Islemler_Yonetici : Form
    {
        public Islemler_Yonetici()
        {
            InitializeComponent();
        }


        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd, cmd1;
        DataSet ds;
        public static string SqlCon = @"Data Source=FURKAN\SQLEXPRESS;Initial Catalog=ilkDatabase;Integrated Security=True";

        public int urunID;
        public string sqlSorgu;
        void GridDoldur(string sql)
        {
            con = new SqlConnection(SqlCon);
            da = new SqlDataAdapter(sql, con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "tbl_urunler");

            dataGridView1.DataSource = ds.Tables["tbl_urunler"];
            con.Close();
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
            {
                if (radioButton1.Checked)
                {
                    //artan sıralama rb5'teydi; küçükten büyüğe sıralama yapıyoruz tik işaretlenmişse
                    if (radioButton5.Checked)
                    {
                        //isme göre arama
                        sqlSorgu = "select * from tbl_urunler where uAd LIKE '%" + textBox1.Text + "%' ORDER BY uAd ASC";
                        //sqlSorgu = "select * from tbl_urunler where uAd NOT LIKE '%" + textBox1.Text + "%'";
                        //alt tire _ tek karakterin yerini tutar; bir önceki ya da bir sonraki karakter ne olursa olsun demektir ikili kullanımda
                        //sqlSorgu = "select * from tbl_urunler where uAd LIKE '_" + textBox1.Text + "_'";
                        GridDoldur(sqlSorgu);
                    }
                    else if (radioButton6.Checked)
                    {
                        sqlSorgu = "select * from tbl_urunler where uAd LIKE '%" + textBox1.Text + "%' ORDER BY uAd DESC";
                        GridDoldur(sqlSorgu);
                    }
                }
                else if (radioButton2.Checked)
                {
                    //stok miktarına göre arama

                    if (radioButton5.Checked)
                    {
                        
                        sqlSorgu = "select * from tbl_urunler where uStok > " + textBox1.Text;
                        //sqlSorgu = "select * from tbl_urunler where uAd NOT LIKE '%" + textBox1.Text + "%'";
                        //alt tire _ tek karakterin yerini tutar; bir önceki ya da bir sonraki karakter ne olursa olsun demektir ikili kullanımda
                        //sqlSorgu = "select * from tbl_urunler where uAd LIKE '_" + textBox1.Text + "_'";
                        GridDoldur(sqlSorgu);
                    }
                    else if (radioButton6.Checked)
                    {
                        sqlSorgu = "select * from tbl_urunler where uStok < " + textBox1.Text;
                        GridDoldur(sqlSorgu);
                    }
                }
                else if (radioButton3.Checked)
                {
                    //tarihe göre arama
                    
                }
                else if (radioButton4.Checked)
                {
                    
                    //Fiyat
                    if (radioButton5.Checked)
                    {

                        //girilen degerin yuzde 10 buyuk ve yuzde 10 kucuk olanları getir.
                        //sqlSorgu = "select * from tbl_urunler where uFiyat > " + textBox1.Text + "*0.9 and uFiyat < " + textBox1.Text + "*1.1" ;
                        //sqlSorgu = "select * from tbl_urunler where uFiyat BETWEEN  " + textBox1.Text + "*0.9 and  " + textBox1.Text + "*1.1";
                        sqlSorgu = "select * from tbl_urunler where uFiyat BETWEEN  " + textBox1.Text + "*0.9 and  " + textBox1.Text + "*1.1 order by uFiyat ASC";

                        GridDoldur(sqlSorgu);
                    }
                    else if (radioButton6.Checked)
                    {
                        sqlSorgu = "select * from tbl_urunler where uFiyat BETWEEN  " + textBox1.Text + "*0.9 and  " + textBox1.Text + "*1.1 order by uFiyat DESC";
                        GridDoldur(sqlSorgu);
                    }

                    //sqlSorgu = "select * from tbl_urunler where uAd IS NULL";
                    //sqlSorgu = "select * from tbl_urunler where uAd IS NOT NULL";

                    //select distinct; bir kaydı sectin mesela, o kayıtta tekrar eden yapıları inaktif yapar, tek bir tane alır
                    //sqlSorgu = "select distinct uAd from tbl_urunler where....";
                }
            }
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //tarih
                if (radioButton5.Checked)
                {
                    sqlSorgu = "select * from tbl_urunler where uSTT > '" + dateTimePicker1.Value.ToString() + "' and uSTT < '" + dateTimePicker2.Value.ToString() + "'order by uSTT ASC";
                    GridDoldur(sqlSorgu);
                }
                else if (radioButton6.Checked)
                {
                    sqlSorgu = "select * from tbl_urunler where uSTT > '" + dateTimePicker1.Value.ToString() + "' and uSTT < '" + dateTimePicker2.Value.ToString() + "'order by uSTT DESC";
                    GridDoldur(sqlSorgu);
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
