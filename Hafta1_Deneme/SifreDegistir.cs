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
    public partial class SifreDegistir : Form
    {
        SqlConnection con;
        SqlDataReader dr;
        SqlCommand cmd;
        DataSet ds;
        public static string SqlCon = @"Data Source=FURKAN\SQLEXPRESS;Initial Catalog=ilkDatabase;Integrated Security=True";

        public int sonuc = 0;
        public SifreDegistir()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        public void captchaOlustur()
        {
            Random r = new Random();
            int ilk = r.Next(0, 50);
            int ikinci = r.Next(0, 50);

            sonuc = ilk + ikinci;
            label_Captcha.Text = ilk.ToString() + " + " + ikinci.ToString() + " = ";
            sonuc = ilk + ikinci;

            //label_mesaj.Text = login.kullanicimSession;
            textBox_Captcha.Clear();
            
        }
        private void SifreDegistir_Load(object sender, EventArgs e)
        {
            label_mesaj.Text = "";
            captchaOlustur();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox_Captcha.Text.Equals(sonuc.ToString()))
            {
                label_mesaj.Text = "";
                if(textBox_YeniSifre.Text == textBox_YeniSifre_Onay.Text)
                {
                    eskiSifreKontrol();
                }
                else
                {
                    label_mesaj.Text = "yeni sifre ve tekrari ayni degil";
                    captchaOlustur();
                }
            }
            else
            {
                label_mesaj.Text = "captcha hatali girildi.";
                captchaOlustur();
            }
        }

        public void eskiSifreKontrol()
        {
            string sorgu = "select sifre from tbl_login where kullanici = @user and sifre = @pass";

            con = new SqlConnection(SqlCon);
            cmd = new SqlCommand(sorgu, con);
            cmd.Parameters.AddWithValue("@user", login.kullanicimSession);
            cmd.Parameters.AddWithValue("@pass", veritabani.MD5Sifrele(textBox_EskiSifre.Text));

            con.Open();
            dr = cmd.ExecuteReader();
            //eğer veri geldiyse; veri nasıl gelir: where kullanici = 'XXX' and sifre = 'YYY'
            if (dr.Read())
            {
                //giris islemi dogruysa yapılacak islemler
                string sql = "update tbl_login set sifre = '"+veritabani.MD5Sifrele(textBox_YeniSifre.Text)+"'";
                veritabani.KomutYolla(sql);
                MessageBox.Show("şifre değiştirme işlemi başarılı");
                label_mesaj.Text = "şifreniz başarıyla değiştirildi";
            }
            else
            {
                label_mesaj.Text = "eski sifre hatali";
                captchaOlustur();
            }
            con.Close();
        }
    }
}
