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
    public partial class müşteri_ekle : Form
    {
        public müşteri_ekle()
        {
            InitializeComponent();
        }
        SqlConnection bağlanti = new SqlConnection(VT_Bağlanti.bağlantı);

        bool kategoridurum;
        private void engelle()
        {
            kategoridurum = true;
            bağlanti.Open();
            SqlCommand komut = new SqlCommand("select *from müşteri", bağlanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                if (txttc.Text == oku["tc"].ToString() || txttc.Text == "")
                {
                    kategoridurum = false;
                }
            }
            bağlanti.Close();
        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            if (txttc.TextLength >= 11 && txttelefon.TextLength >=11 && txtemail.Text.Length >=5)
            {
                engelle();
                if (txttc.Text != "" && txtadsoyad.Text.Trim() != "" && txttelefon.Text.Trim() != "" && txtadres.Text.Trim() != "" && txtemail.Text.Trim() != "" && txtemail.Text.Contains('@'))
                {
                    if (kategoridurum == true)
                    {
                        SqlCommand komut = new SqlCommand("insert into müşteri(tc,adsoyad,telefon,adres,email) values(@tc,@adsoyad,@telefon,@adres,@email)", bağlanti);
                        komut.Parameters.AddWithValue("@tc", txttc.Text);
                        komut.Parameters.AddWithValue("@adsoyad", txtadsoyad.Text);
                        komut.Parameters.AddWithValue("@telefon", txttelefon.Text);
                        komut.Parameters.AddWithValue("@adres", txtadres.Text);
                        komut.Parameters.AddWithValue("@email", txtemail.Text);
                        bağlanti.Open();
                        komut.ExecuteNonQuery();
                        bağlanti.Close();
                        MessageBox.Show("Müşteri kaydınız Gerçekleştirildi", "TEBRİKLER", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Zaten Böyle Bir Müşteri Var", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (txttc.Text.Trim() == "" || txtadsoyad.Text.Trim() == "" || txttelefon.Text.Trim() == "" || txtadres.Text.Trim() == "" || txtemail.Text.Trim() == "")
                {
                    MessageBox.Show("Formu Eksiksiz Doldurduğunuzdan Emin Olunuz", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (!txtemail.Text.Contains('@'))
                {
                    MessageBox.Show("Email'de @ işareti koymayı unuttunuz", "BİLGi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Tc Ve Telefonu 11 haneden küçük Oluşturamazsınız Email'ide En Az 5 Karakter Olmalıdır", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
            txtadres.Text = "";
            txtadsoyad.Text = "";
            txttelefon.Text = "";
            txtemail.Text = "";
            txttc.Text = "";
        }

        private void txttc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txttelefon_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtadsoyad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar);
        }

        private void müşteri_ekle_Load(object sender, EventArgs e)
        {

        }
    }
}
