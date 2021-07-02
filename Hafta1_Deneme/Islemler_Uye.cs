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
    public partial class Islemler_Uye : Form
    {
        public Islemler_Uye()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd, cmd1;
        DataSet ds;
        public static string SqlCon = @"Data Source=FURKAN\SQLEXPRESS;Initial Catalog=ilkDatabase;Integrated Security=True";

        public int urunID;

        void GridDoldur()
        {
            con = new SqlConnection(SqlCon);
            da = new SqlDataAdapter("select uID, uAD, uFiyat from tbl_urunler where uStok > 0", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "tbl_urunler");

            dataGridView1.DataSource = ds.Tables["tbl_urunler"];
        }

        private void Islemler_Uye_Load(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 0;

            GridDoldur();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            //dataGridView1.Columns[3].Visible = false;
            //dataGridView1.Columns[4].Visible = false;
            //dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Ürünler";

            comboBox2.SelectedIndex = 0;
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            urunID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            label_Urun.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            label_fiyat.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            //dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
                        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand();
            string sql = "insert into tbl_islemler(islemTutar, islemTarih, islemAciklama, islemBirim, username, uID) values (@tutar, @tarih, @aciklama, @birim, @username, @uID)";
            cmd.Parameters.AddWithValue("@tutar", Convert.ToDouble(label_tutar.Text));
            cmd.Parameters.AddWithValue("@aciklama", richTextBox1.Text);
            cmd.Parameters.AddWithValue("@tarih", DateTime.Now);
            cmd.Parameters.AddWithValue("@birim", Convert.ToDouble(comboBox2.Text));
            cmd.Parameters.AddWithValue("@username", login.kullanicimSession);
            cmd.Parameters.AddWithValue("@uID", urunID);
            //@tutar, @tarih, @aciklama, @birim, @user, @uID
            veritabani.KomutYollaParametreli(sql, cmd);


            cmd1 = new SqlCommand();
            sql = "update tbl_urunler set uStok += @birim where uID = @uID";
            cmd1.Parameters.AddWithValue("@birim", Convert.ToDouble(comboBox2.Text));
            cmd1.Parameters.AddWithValue("@uID", urunID);

            veritabani.KomutYollaParametreli(sql, cmd1);
            GridDoldur();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.Text != "Seçiniz...")
            {
                label_tutar.Text = (Convert.ToDouble(comboBox2.Text) * Convert.ToDouble(label_fiyat.Text)).ToString();
            }
            
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
