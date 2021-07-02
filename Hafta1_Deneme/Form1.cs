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
    public partial class Form1 : Form
    {
        //Data Source=FURKAN\SQLEXPRESS;Initial Catalog=ilkDatabase;Integrated Security=True

        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        DataSet ds;
        public static string SqlCon = @"Data Source=FURKAN\SQLEXPRESS;Initial Catalog=ilkDatabase;Integrated Security=True";
        void GridDoldur()
        {
            con = new SqlConnection(SqlCon);
            da = new SqlDataAdapter("select * from tbl_login", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "tbl_login");

            dataGridView1.DataSource = ds.Tables["tbl_login"];
        }
        public Form1()
        {
            InitializeComponent();
            if (veritabani.BaglantiDurum())
            {
                //MessageBox.Show("vt baglantisi kuruldu");

            }
        }

        


        private void Form1_Load(object sender, EventArgs e)
        {

            //GridDoldur();
            veritabani.GridTumunuDoldur(dataGridView1, "tbl_login");
            //dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[1].HeaderText = "kullanıcı adı";
            dataGridView1.Columns[3].HeaderText = "son giriş tarihi";
            dataGridView1.Columns[0].HeaderCell.Value = "ID";
            dataGridView1.Columns[3].Width = 100;

            //satır ekleme
            //string[] satir = new string[]
            //{
            //   "1453", "deneme", "99999", "10/10/2021"
            //};
            //dataGridView1.Rows.Add(satir);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            dateTimePicker1.Value = DateTime.Now;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
            con = new SqlConnection(SqlCon);
            //sıralama önemlidir
            string sql = "insert into tbl_login(kullanici, sifre, tarih) values('" + textBox2.Text + "', '" + veritabani.MD5Sifrele(textBox3.Text) + "', '" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "') ";
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            con.Close();

            GridDoldur();
            */

            /*
            con = new SqlConnection(SqlCon);
            //sıralama önemlidir
            string sql = "insert into tbl_login(kullanici, sifre, tarih) values(@user, @password, @tarih)";
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@user", textBox2.Text);
            cmd.Parameters.AddWithValue("@password", veritabani.MD5Sifrele(textBox3.Text));
            cmd.Parameters.AddWithValue("@tarih", DateTime.Now);
            con.Open();

            cmd.Connection = con;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            con.Close();
            */

            cmd = new SqlCommand();
            string sql = "insert into tbl_login(kullanici, sifre, tarih) values (@user, @password, @tarih)";
            cmd.Parameters.AddWithValue("@user", textBox2.Text);
            cmd.Parameters.AddWithValue("@password", veritabani.MD5Sifrele(textBox3.Text));
            cmd.Parameters.AddWithValue("@tarih", DateTime.Now);

            veritabani.KomutYollaParametreli(sql, cmd);
            


            GridDoldur();
            
            
        }
        public void EklemeSorgu(string sql)
        {
            con = new SqlConnection(SqlCon);
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            con.Close();

            GridDoldur();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*
            con = new SqlConnection(SqlCon);
            //sıralama önemlidir
            string sql = "update tbl_login set sifre = '"+ veritabani.MD5Sifrele(textBox3.Text) + "' where kullanici = '" + textBox2.Text + "' and kID =" + textBox1.Text + "";
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            con.Close();

            GridDoldur();
            */

            /*
            con = new SqlConnection(SqlCon);
            //sıralama önemlidir
            string sql = "update tbl_login set sifre = @password where kullanici = @user and kID = @idm";
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@user", textBox2.Text);
            cmd.Parameters.AddWithValue("@password", veritabani.MD5Sifrele(textBox3.Text));
            cmd.Parameters.AddWithValue("@idm", Convert.ToInt32(textBox1.Text));
            con.Open();

            cmd.Connection = con;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            con.Close();

            GridDoldur();
            */

            string sql = "update tbl_login set sifre = '" + veritabani.MD5Sifrele(textBox3.Text) + "' where kullanici = '" + textBox2.Text + "' and kID =" + textBox1.Text + "";
            veritabani.KomutYolla(sql);
        }

        private void button4_Click(object sender, EventArgs e)
        {

            string sql = "delete from tbl_login where kullanici = '" + textBox2.Text + "' and sifre = '" + textBox3.Text + "' and kID =" + textBox1.Text + "";
            veritabani.KomutYolla(sql);

            /*

            con = new SqlConnection(SqlCon);
            //sıralama önemlidir
            string sql = "delete from tbl_login where kullanici = '"+textBox2.Text+"' and sifre = '"+textBox3.Text+"' and kID ="+textBox1.Text+"";
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@user", textBox2.Text);
            cmd.Parameters.AddWithValue("@password", veritabani.MD5Sifrele(textBox3.Text));
            cmd.Parameters.AddWithValue("@tarih", DateTime.Now);
            con.Open();

            cmd.Connection = con;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            con.Close();

            GridDoldur();
            */


            /*
            con = new SqlConnection(SqlCon);
            //sıralama önemlidir
            string sql = "delete from tbl_login where kullanici = @user and sifre = @password and kID = @idm";
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@user", textBox2.Text);
            cmd.Parameters.AddWithValue("@password", textBox3.Text);
            cmd.Parameters.AddWithValue("@idm", Convert.ToInt32(textBox1.Text));
            con.Open();

            cmd.Connection = con;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            con.Close();

            GridDoldur();
            */


        }

        

        private void şifreDeğiştirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SifreDegistir a = new SifreDegistir();
            a.ShowDialog();
        }

        private void üyeleriGörüntüleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void hazırModülİleYapılanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Uyeler a = new Uyeler();
            a.ShowDialog();
        }

        private void kodİleYapılanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Uyeler2 a = new Uyeler2();
            a.ShowDialog();
        }

        private void işlemleriGörüntüleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
