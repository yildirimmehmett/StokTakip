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
    public partial class markaekle : Form
    {
        public markaekle()
        {
            InitializeComponent();
        }
        
        SqlConnection bağlanti = new SqlConnection(VT_Bağlanti.bağlantı);
        private void btnekle_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "" && comboBox1.Text.Trim() != "")
            {
                engelle();
                if (markadurum == true)
                {

                    SqlCommand komut = new SqlCommand("insert into markabilgisi(kategori,marka) values(@kategori,@marka)", bağlanti);
                    komut.Parameters.AddWithValue("@kategori", comboBox1.Text);
                    komut.Parameters.AddWithValue("@marka", textBox1.Text);
                    bağlanti.Open();
                    komut.ExecuteNonQuery();
                    bağlanti.Close();
                    MessageBox.Show("Marka Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Text = "";
                    comboBox1.Text = "";
                }
                else
                {
                    MessageBox.Show("Kategori Ve Marka Var Aynısını Oluşturamazsınız", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "";
                    comboBox1.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Formu Doldurduğunuzdan Emin Olunuz", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "";
                comboBox1.Text = "";
            }
        }

        private void markaekle_Load(object sender, EventArgs e)
        {
            kategorigel();
        }
       
        bool markadurum;
        private void engelle()
        {
            markadurum = true;
            bağlanti.Open();
            SqlCommand komut = new SqlCommand("select *from markabilgisi", bağlanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                if (textBox1.Text == oku["marka"].ToString() || textBox1.Text == "" || comboBox1.Text == "" && comboBox1.Text == oku["kategori"].ToString())
                {
                    markadurum = false;
                }
            }
            bağlanti.Close();
        }
        private void kategorigel()
        {
            bağlanti.Open();
            SqlCommand komut = new SqlCommand("select *from kategoribilgisi ", bağlanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox1.Items.Add(oku["kategori"].ToString());
            }
            bağlanti.Close();
        }
    }
}
