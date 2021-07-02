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
    public partial class Yonetici_InnerJoin : Form
    {
        public Yonetici_InnerJoin()
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

        void GridDoldurStoredProcedure(string deger)
        {
            con = new SqlConnection(SqlCon);
            //sql command olustur
            //command'ınla hangi prosedürü calıstıracaksan onu yaz
            cmd1 = new SqlCommand("UrunleriBirlestir2", con);
            //sonra bu cmd1'in parametrelerini gir
            cmd1.CommandType = CommandType.StoredProcedure;
            //parametre adını yaz ve hangi tiple calıstıracaksan onu yaz
            cmd1.Parameters.Add("urunAdi", SqlDbType.NVarChar, 50).Value = "%" + deger + "%";
            //cmd1.Parameters.Add("urunID", SqlDbType.Int).Value = 5;


            da = new SqlDataAdapter(cmd1);
            ds = new DataSet();
            con.Open();
            da.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }


        void GridDoldurStoredProcedure2(string deger)
        {
            con = new SqlConnection(SqlCon);
            con.Open();
            cmd = new SqlCommand();
            //olusturulan command'a baglantı nesnesi atamak
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            //komut ismi commandtext
            cmd.CommandText = "UrunleriBirlestir2";
            //parametre adını yaz ve hangi tiple calıstıracaksan onu yaz
            cmd.Parameters.Add("urunAdi", SqlDbType.NVarChar, 50).Value = "%" + deger + "%";
            //cmd1.Parameters.Add("urunID", SqlDbType.Int).Value = 5;


            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            
            da.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
            {
                //ON'dan sonra hangi sartlar altında birleştirmek istedigini soylersin
                //hangi satırın hangi alan icinde oldugunu nokta kullanarak belirtmek daha doğrudur
                //sqlSorgu = "select tbl_urunler.*, tbl_islemler.* from tbl_urunler   INNER JOIN tbl_islemler ON    tbl_urunler.uID = tbl_islemler.uID    where tbl_urunler.uAd LIKE '%" + textBox1.Text + "%'";            
                //GridDoldur(sqlSorgu);
               
                GridDoldurStoredProcedure2(textBox1.Text);
            }
        }

        private void Yonetici_InnerJoin_Load(object sender, EventArgs e)
        {
            sqlSorgu = "select tbl_urunler.*, tbl_islemler.* from tbl_urunler   INNER JOIN tbl_islemler ON    tbl_urunler.uID = tbl_islemler.uID";
            GridDoldur(sqlSorgu);
        }
    }
}
