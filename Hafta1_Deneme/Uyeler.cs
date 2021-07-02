using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hafta1_Deneme
{
    public partial class Uyeler : Form
    {
        public Uyeler()
        {
            InitializeComponent();
        }

        private void Uyeler_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'ilkDatabaseDataSet.tbl_login' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.tbl_loginTableAdapter.Fill(this.ilkDatabaseDataSet.tbl_login);

        }
    }
}
