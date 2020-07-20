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
    public partial class yeni_ürün_paneli : Form
    {
        public yeni_ürün_paneli()
        {
            InitializeComponent();
        }
        SqlConnection bağlanti = new SqlConnection(VT_Bağlanti.bağlantı);
        bool urundurum;
        private void urunengelle()
        {
            urundurum = true;
            bağlanti.Open();
            SqlCommand komut = new SqlCommand("select *from uruntablosu", bağlanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                if (txtbarkodno.Text == oku["barkodno"].ToString() || txtbarkodno.Text == "")
                {
                    urundurum = false;
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
                combokategori.Items.Add(oku["kategori"].ToString());
            }
            bağlanti.Close();
        }
        private void yeni_ürün_paneli_Load(object sender, EventArgs e)
        {
            kategorigel();
        }
        private void btnürünkaydet_Click(object sender, EventArgs e)
        {
            int z = Convert.ToInt32(txtmiktar.Text);
            if (z > 0)
            {
                if (txtbarkodno.Text.Trim() != "" && combokategori.Text.Trim() != "" && combomarka.Text.Trim() != "" && txtürün.Text.Trim() != "" && txtmiktar.Text.Trim() != "" && txtalışfiy.Text.Trim() != "" && txtsatışfiy.Text.Trim() != "")
                {
                    urunengelle();
                    if (urundurum == true)
                    {
                        bağlanti.Open();
                        SqlCommand komut = new SqlCommand("insert into uruntablosu(barkodno,kategori,marka,urunadi,miktarı,alışfiyatı,satışfiyatı,tarih) values(@barkodno,@kategori,@marka,@urunadi,@miktarı,@alışfiyatı,@satışfiyatı,@tarih) ", bağlanti);
                        komut.Parameters.AddWithValue("@barkodno", txtbarkodno.Text);
                        komut.Parameters.AddWithValue("@kategori", combokategori.Text);
                        komut.Parameters.AddWithValue("@marka", combomarka.Text);
                        komut.Parameters.AddWithValue("@urunadi", txtürün.Text);
                        komut.Parameters.AddWithValue("@miktarı", int.Parse(txtmiktar.Text));
                        komut.Parameters.AddWithValue("@alışfiyatı", double.Parse(txtalışfiy.Text));
                        komut.Parameters.AddWithValue("@satışfiyatı", double.Parse(txtsatışfiy.Text));
                        komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                        komut.ExecuteNonQuery();
                        bağlanti.Close();
                        MessageBox.Show("Ürün Eklendi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Bu Barkod Numarasına Ait Bir Ürün Bulunmaktadır.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Formu Doldurduğunuzdan Emin Olunuz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Adet Sayısını 0 Veya 0 Dan Küçük Giremezsiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            txtürün.Text = "";
            txtsatışfiy.Text = "";
            txtalışfiy.Text = "";
            txtbarkodno.Text = "";
            txtmiktar.Text = "";
            combokategori.Items.Clear();
            combomarka.Items.Clear();
        }
        private void btngücellemeyap_Click(object sender, EventArgs e)
        {
            if (kategoritxt.Text.Trim() != "" && markatxt.Text.Trim() != "" && ürüntxt.Text.Trim() != "" && alıştxt.Text.Trim() != "" && satıştxt.Text.Trim() != "" && kategoritxt.Text.Trim() != "" && miktartxt.Text.Trim() != "")
            {
                bağlanti.Open();
                SqlCommand komut = new SqlCommand("update uruntablosu set miktarı=miktarı+'" + int.Parse(miktartxt.Text) + "' where barkodno='" + barkodnotxt.Text + "'", bağlanti);
                komut.ExecuteNonQuery();
                bağlanti.Close();
            }
            else if (barkodnotxt.Text != "" && miktartxt.Text != "")
            {
                MessageBox.Show("Güncelleme Yapıldı","BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (miktartxt.Text == "")
            {
                MessageBox.Show("Formu Doldurduğunuzdan Emin Olunuz", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            miktartxt.Text = "";
            barkodnotxt.Text = "";
            kategoritxt.Text = "";
            markatxt.Text = "";
            ürüntxt.Text = "";
            miktartxt.Text = "";
            alıştxt.Text = "";
            satıştxt.Text = "";
        }
        private void barkodnotxt_TextChanged(object sender, EventArgs e)
        {
            bağlanti.Open();
            SqlCommand komut = new SqlCommand("select *from uruntablosu where barkodno = '" + barkodnotxt.Text + "'  ", bağlanti);
            SqlDataReader oku = komut.ExecuteReader();
            if (barkodnotxt.Text != "")
            {
                while (oku.Read())
                {
                    kategoritxt.Text = oku["kategori"].ToString();
                    markatxt.Text = oku["marka"].ToString();
                    ürüntxt.Text = oku["urunadi"].ToString();
                    label18.Text = oku["miktarı"].ToString();
                    alıştxt.Text = oku["alışfiyatı"].ToString();
                    satıştxt.Text = oku["satışfiyatı"].ToString();
                }
            }
            else
            {
                miktartxt.Text = "";
                barkodnotxt.Text = "";
                kategoritxt.Text = "";
                markatxt.Text = "";
                ürüntxt.Text = "";
                label18.Text = "";
                alıştxt.Text = "";
                satıştxt.Text = "";
            }
            bağlanti.Close();
        }
        private void txtbarkodno_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void txtmiktar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void txtalışfiy_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void txtsatışfiy_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void combokategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            combomarka.Items.Clear();
            bağlanti.Open();
            SqlCommand komut = new SqlCommand("select *from markabilgisi where kategori='" + combokategori.SelectedItem + "'", bağlanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                combomarka.Items.Add(oku["marka"].ToString());
            }
            bağlanti.Close();
        }
    }
}
