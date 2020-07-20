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
    public partial class şifre_unuttum_güncelleme : Form
    {
        public şifre_unuttum_güncelleme()
        {
            InitializeComponent();
        }
        bool durum;
        SqlConnection bağlanti = new SqlConnection(VT_Bağlanti.bağlantı);
        DataSet dataset = new DataSet();
        private void btnsifre_Click(object sender, EventArgs e)
        {
            bağlanti.Open();
            SqlCommand sorgu = new SqlCommand("select *from sifre", bağlanti);
            SqlDataReader oku = sorgu.ExecuteReader();
            while (oku.Read())
            {
                if (textBox1.Text == oku["ensevdiğinvarlık"].ToString()) 
                {
                    durum = true;
                }
                else
                {
                    durum = false;
                }
            }
            bağlanti.Close();
            if (durum == false)
            {
                if (textBox1.Text.Trim() != "")
                {
                   
                    SqlCommand komut = new SqlCommand("update sifre set ensevdiğinvarlık=@ensevdiğinvarlık", bağlanti);
                    komut.Parameters.AddWithValue("@ensevdiğinvarlık", textBox1.Text);
                    MessageBox.Show("Güncelleme Yapıldı", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    bağlanti.Open();
                    komut.ExecuteNonQuery();
                    textBox1.Clear();
                    bağlanti.Close();
                }
                else
                {
                    MessageBox.Show("Formu Doldurduğunuzdan Emin Olunuz", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Zaten Aynı Güvenlik Sorusunun Cevabını Girdiniz", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
            }
        }
    }
}
