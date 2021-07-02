using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Security.Cryptography;
namespace Hafta1_Deneme
{
    class veritabani
    {
        veritabani()
        {

        }
       static SqlConnection con;
       static SqlDataAdapter da;
       static SqlCommand cmd;
       static System.Data.DataSet ds;
        static SqlDataReader dr;

       public static string SqlCon = @"Data Source=FURKAN\SQLEXPRESS;Initial Catalog=ilkDatabase;Integrated Security=True";
           
        public static bool BaglantiDurum()
        {
            //veritabanı baglantısı kontrol
            using (con = new SqlConnection(SqlCon))
            {
                try
                {
                    con.Open();
                    //System.Windows.Forms.MessageBox.Show("baglanti kuruldu");
                    return true;
                }
                catch (SqlException exp)
                {
                    System.Windows.Forms.MessageBox.Show(exp.Message);
                    return false;
                }
            }
        }

        public static DataGridView GridTumunuDoldur(System.Windows.Forms.DataGridView gridim, string sqlSelectSorgu)
        {
            con = new SqlConnection(SqlCon);
            da = new SqlDataAdapter("select * from " + sqlSelectSorgu, con);
            ds = new System.Data.DataSet();
            con.Open();
            da.Fill(ds, sqlSelectSorgu);

            gridim.DataSource = ds.Tables[sqlSelectSorgu];
            con.Close();

            return gridim;
        }
        public static bool LoginKontrol(string kullaniciAdi, string sifre)
        {
            string sorgu = "select * from tbl_login where kullanici = @user and sifre = @pass";

            con = new SqlConnection(SqlCon);
            cmd = new SqlCommand(sorgu, con);
            cmd.Parameters.AddWithValue("@user", kullaniciAdi);
            cmd.Parameters.AddWithValue("@pass", veritabani.MD5Sifrele(sifre));

            con.Open();
            dr = cmd.ExecuteReader();
            //eğer veri geldiyse; veri nasıl gelir: where kullanici = 'XXX' and sifre = 'YYY'
            if (dr.Read())
            {
                con.Close();
                return true;
                
            }
            
            else
            {
                con.Close();
                return false;
                
            }
            
        }
        public static string MD5Sifrele(string sifrelenecekMetin)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] dizi = Encoding.UTF8.GetBytes(sifrelenecekMetin);

            //dizinin hash degeri cıkarılıyor
            dizi = md5.ComputeHash(dizi);

            StringBuilder sb = new StringBuilder();
            foreach (byte item in dizi)
                sb.Append(item.ToString("x2").ToLower());


            return sb.ToString();
        }

        public static string SHA256Sifrele(string sifrelenecekMetin)
        {
            SHA256 sha256Hash = SHA256.Create();
            byte[] dizi = Encoding.UTF8.GetBytes(sifrelenecekMetin);
            dizi = sha256Hash.ComputeHash(dizi);

            StringBuilder sb = new StringBuilder();
            foreach (byte item in dizi)
                sb.Append(item.ToString("x2").ToLower());

            return sb.ToString();
        }
        public static void KomutYolla(string sql)
        {
            con = new SqlConnection(SqlCon);
            //sıralama önemlidir
            
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static void KomutYollaParametreli(string sql, SqlCommand cmd)
        {
            con = new SqlConnection(SqlCon);
            //sıralama önemlidir
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
