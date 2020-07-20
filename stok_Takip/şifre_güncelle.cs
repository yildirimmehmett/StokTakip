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

namespace stok_Takip
{
    public partial class şifre_güncelle : Form
    {
        public şifre_güncelle()
        {
            InitializeComponent();
        }
        SqlConnection bağlanti = new SqlConnection(VT_Bağlanti.bağlantı);
        bool durum;
        private void btnsifre_Click(object sender, EventArgs e)
        {

            SqlCommand sorgu = new SqlCommand("select *from sifre", bağlanti);
            SqlDataReader oku = sorgu.ExecuteReader();
            while (oku.Read())
            {
                if (textBox1.Text == oku["sifre"].ToString() )
                {
                    durum = true;
                }
                else
                {
                    durum = false;
                }
            }
            if (textBox1.Text.Trim() != "")
            {
                if (durum == false)
                {
                    SqlCommand komut = new SqlCommand("update sifre set sifre = @sifre", bağlanti);
                    komut.Parameters.AddWithValue("@sifre", textBox1.Text);
                    bağlanti.Open();
                    komut.ExecuteNonQuery();
                    bağlanti.Close();
                    MessageBox.Show("Şifre Değistirildi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Text = "";
                }
                else
                {
                    MessageBox.Show("Eski Şifrenizle Uyuşuyor Farklı Bir Şifre Seçiniz", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Formu Doldurduğunuzdan Emin Olunuz", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
