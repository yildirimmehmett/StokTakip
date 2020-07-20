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
    public partial class kategori_ekle : Form
    {
        public kategori_ekle()
        {
            InitializeComponent();
        }
        SqlConnection bağlanti = new SqlConnection(VT_Bağlanti.bağlantı);
        bool kategoridurum;

        private void btnekle_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "")
            {
                engelle();
                if (kategoridurum == true)
                {
                    bağlanti.Open();
                    SqlCommand komut = new SqlCommand("insert into kategoribilgisi(kategori) values (@kategori)", bağlanti);
                    komut.Parameters.AddWithValue("@kategori", textBox1.Text);
                    komut.ExecuteNonQuery();
                    bağlanti.Close();

                    MessageBox.Show("Kategori Eklendi, ", "TEBRİKLER", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Text = "";
                }
                else
                {
                    MessageBox.Show("Zaten Böyle Bir Kategori Var", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Formu Doldurduğunuzdan Emin Olunuz", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "";
            }
        }
        private void engelle()
        {
            kategoridurum = true;
            bağlanti.Open();
            SqlCommand komut = new SqlCommand("select *from kategoribilgisi", bağlanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                if (textBox1.Text == oku["kategori"].ToString() || textBox1.Text == "")
                {
                    kategoridurum = false;
                }
            }
            bağlanti.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar);
        }

        private void kategori_ekle_Load(object sender, EventArgs e)
        {

        }
    }
}
