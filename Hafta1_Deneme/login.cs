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
    public partial class login : Form
    {
        SqlConnection con;
        SqlDataReader dr;
        SqlCommand cmd;
        DataSet ds;
        public static string SqlCon = @"Data Source=FURKAN\SQLEXPRESS;Initial Catalog=ilkDatabase;Integrated Security=True";

        public int denemeSayisi = 0;

        public static string kullanicimSession = "";
        public login()
        {
            InitializeComponent();
            
            //da = new SqlDataAdapter("select * from tbl_login", con);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public void Login()
        {
            string sorgu = "select * from tbl_login where kullanici = @user and sifre = @pass";

            con = new SqlConnection(SqlCon);
            cmd = new SqlCommand(sorgu, con);
            cmd.Parameters.AddWithValue("@user", textBox1.Text);
            cmd.Parameters.AddWithValue("@pass", veritabani.MD5Sifrele(textBox2.Text));

            con.Open();
            dr = cmd.ExecuteReader();
            //eğer veri geldiyse; veri nasıl gelir: where kullanici = 'XXX' and sifre = 'YYY'
            if (dr.Read())
            {
                MessageBox.Show("tebrikler, giriş yaptınız");
            }
            else
            {
                MessageBox.Show("kullanıcı adı ya da şifre hatalı");
                textBox1.Clear();
                //text silmek
                textBox2.Clear();
                //imleci text'e getirmek
                textBox1.Focus();
            }
            con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(veritabani.LoginKontrol(textBox1.Text, textBox2.Text))
            {
                this.Hide();
                kullanicimSession = textBox1.Text;

                //yöneticiyse
                if(kullanicimSession == "furkan")
                {
                    Islemler_Yonetici a = new Islemler_Yonetici();
                    a.Show();
                }
                //normal kullanıcıysa
                else
                {
                    Islemler_Uye a = new Islemler_Uye();
                    a.Show();
                }

                //Form1 a = new Form1();
                //a.Show();
            }
            else
            {
                denemeSayisi++;
                if(denemeSayisi == 3)
                {
                    MessageBox.Show("3 defa hatali giris yaptiniz");
                    Application.Exit();
                }
            }
            //Login();

            /*
            veritabani.MD5Sifrele(textBox2.Text);
            string sorgu = "select * from tbl_login where kullanici = '"+textBox1.Text+"' and sifre = '"+ veritabani.MD5Sifrele(textBox2.Text)+"'";
            con = new SqlConnection(SqlCon);
            cmd = new SqlCommand(sorgu, con);

            con.Open();
            dr = cmd.ExecuteReader();
            //eğer veri geldiyse; veri nasıl gelir: where kullanici = 'XXX' and sifre = 'YYY'
            if (dr.Read())
            {
                MessageBox.Show("tebrikler, giriş yaptınız");
            }
            else
            {
                MessageBox.Show("kullanıcı adı ya da şifre hatalı");
                textBox1.Clear();
                //text silmek
                textBox2.Clear();
                //imleci text'e getirmek
                textBox1.Focus();
            }
            con.Close();    
            */
        }
       
    }
}
