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
    public partial class Uyeler2 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        DataSet ds;
        public static string SqlCon = @"Data Source=FURKAN\SQLEXPRESS;Initial Catalog=ilkDatabase;Integrated Security=True";
        public Uyeler2()
        {
            InitializeComponent();
        }

        private void Uyeler2_Load(object sender, EventArgs e)
        {
            VeriNavigasyon();
        }
        public void VeriNavigasyon()
        {
            con = new SqlConnection(SqlCon);
            da = new SqlDataAdapter("select * from tbl_login", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds);
            con.Close(); 


            bindingSource1.DataSource = ds.Tables[0];
            bindingNavigator1.BindingSource = bindingSource1;

            label1.DataBindings.Add(new Binding("Text", bindingSource1, "kID"));
            textBox1.DataBindings.Add(new Binding("Text", bindingSource1, "kullanici"));
            textBox2.DataBindings.Add(new Binding("Text", bindingSource1, "sifre"));
            dateTimePicker1.DataBindings.Add(new Binding("Text", bindingSource1, "tarih"));
        }
    }
}
