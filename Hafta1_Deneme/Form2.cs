using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Hafta1_Deneme
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
            {
                richTextBox1.Text = MD5Sifrele(textBox1.Text);
                label4.Text = richTextBox1.Text.Length.ToString();

                richTextBox2.Text =SHA256Sifrele(textBox1.Text);
                label5.Text = richTextBox2.Text.Length.ToString();
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
    }
}
