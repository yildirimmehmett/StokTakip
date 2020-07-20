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
    public partial class stok_otomasyon : Form
    {
        public stok_otomasyon()
        {
            InitializeComponent();
        }
        bool durum;
        SqlConnection bağlanti = new SqlConnection(VT_Bağlanti.bağlantı);
        DataSet dataset = new DataSet();
        private void sepetliste()
        {
            bağlanti.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select *from sepeteekle", bağlanti);
            adapter.Fill(dataset, "sepeteekle");
            dataGridView1.DataSource = dataset.Tables["sepeteekle"];
            bağlanti.Close();
        }
        private void geneltoplam()
        {
            try
            {
                bağlanti.Open();
                SqlCommand komut = new SqlCommand("select sum(toplamfiyat) from sepeteekle", bağlanti);
                lblgeneltoplam.Text = komut.ExecuteScalar() + "TL";
                bağlanti.Close();
            }
            catch (Exception)
            {

            }
        }
        private void kontrol()
        {
            durum = true;
            bağlanti.Open();
            SqlCommand komut = new SqlCommand("select *from sepeteekle", bağlanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                if (txtbarkodno.Text == oku["barkodno"].ToString())
                {
                    durum = false;
                }
            }
            bağlanti.Close();
        }
        private void btnekle_Click(object sender, EventArgs e)
        {
            if (txtbarkodno.Text.Trim() != "" && txtürünadı.Text.Trim() != "" && txtsatışfiyatı.Text.Trim() != "" && txtmiktar.Text.Trim() != "" && txttoplamfiyat.Text.Trim() != "")
            {
                kontrol();
                if (durum == true)
                {
                    bağlanti.Open();
                    SqlCommand sorgu = new SqlCommand("insert into sepeteekle(tc,adsoyad,telefon,barkodno,urunadi,miktarı,satışfiyatı,toplamfiyat,tarih) values(@tc,@adsoyad,@telefon,@barkodno,@urunadi,@miktarı,@satışfiyatı,@toplamfiyat,@tarih)", bağlanti);
                    sorgu.Parameters.AddWithValue("@tc", txttc.Text);
                    sorgu.Parameters.AddWithValue("@adsoyad", txtadsoyad.Text);
                    sorgu.Parameters.AddWithValue("@telefon", txttelefon.Text);
                    sorgu.Parameters.AddWithValue("@barkodno", txtbarkodno.Text);
                    sorgu.Parameters.AddWithValue("@urunadi", txtürünadı.Text);
                    sorgu.Parameters.AddWithValue("@miktarı", int.Parse(txtmiktar.Text));
                    sorgu.Parameters.AddWithValue("@satışfiyatı", double.Parse(txtsatışfiyatı.Text));
                    sorgu.Parameters.AddWithValue("@toplamfiyat", double.Parse(txttoplamfiyat.Text));
                    sorgu.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                    sorgu.ExecuteNonQuery();
                    MessageBox.Show("İşleminiz Yapılıyor", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bağlanti.Close();
                    txtbarkodno.Text = "";
                    txtadsoyad.Text = "";
                    txttc.Text = "";
                    txttelefon.Text = "";
                    txtsatışfiyatı.Text = "";
                    txtürünadı.Text = "";
                    txttoplamfiyat.Text = "";

                }
                else
                {
                    bağlanti.Open();
                    SqlCommand komut = new SqlCommand("update sepeteekle set miktarı = miktarı+ '" + int.Parse(txtmiktar.Text) + "' where tc='" + txttc.Text + "'", bağlanti);
                    komut.ExecuteNonQuery();
                    SqlCommand komut1 = new SqlCommand("update sepeteekle set toplamfiyat=miktarı*satışfiyatı where tc='" + txttc.Text + "'", bağlanti);
                    komut1.ExecuteNonQuery();
                    bağlanti.Close();
                }
                dataset.Tables["sepeteekle"].Clear();
                sepetliste();
                geneltoplam();
                if (txtbarkodno.Text == "")
                {
                    txtbarkodno.Text = "";
                    txtürünadı.Text = "";
                    txtsatışfiyatı.Text = "";
                    txttoplamfiyat.Text = "";
                }
                if (txttc.Text == "")
                {
                    txtadsoyad.Text = "";
                    txttelefon.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Formu Doldurduğunuzdan Emin Olunuz", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSatışSil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                bağlanti.Open();
                SqlCommand komut = new SqlCommand("delete from sepeteekle where barkodno='" + dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString() + "'", bağlanti);
                komut.ExecuteNonQuery();
                bağlanti.Close();
                MessageBox.Show("Ürün Sepetten Çıkarıldı", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataset.Tables["sepeteekle"].Clear();
                sepetliste();
                geneltoplam();
            }
            else
            {
                MessageBox.Show("Ürün Yok Veya Ürün Seçilmedi", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            txtbarkodno.Text = "";
            txtadsoyad.Text = "";
            txttc.Text = "";
            txttelefon.Text = "";
            txtsatışfiyatı.Text = "";
            txtürünadı.Text = "";
            txttoplamfiyat.Text = "";
            txtstok.Text = "";
        }

        private void btnsatışyap_Click(object sender, EventArgs e)
        {
            

            if (dataGridView1.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (int.Parse(txtstok.Text) >= int.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString()))
                    {
                        bağlanti.Open();
                        SqlCommand komut = new SqlCommand("insert into satiştablosu(tc,adsoyad,telefon,barkodno,ürünadı,miktarı,satışfiyatı,toplamfiyat,tarih) values(@tc,@adsoyad,@telefon,@barkodno,@ürünadı,@miktarı,@satışfiyatı,@toplamfiyat,@tarih)", bağlanti);
                        komut.Parameters.AddWithValue("@tc", dataGridView1.Rows[i].Cells[0].Value.ToString());
                        komut.Parameters.AddWithValue("@adsoyad", dataGridView1.Rows[i].Cells[1].Value.ToString());
                        komut.Parameters.AddWithValue("@telefon", dataGridView1.Rows[i].Cells[2].Value.ToString());
                        komut.Parameters.AddWithValue("@barkodno", dataGridView1.Rows[i].Cells[3].Value.ToString());
                        komut.Parameters.AddWithValue("@ürünadı", dataGridView1.Rows[i].Cells[4].Value.ToString());
                        komut.Parameters.AddWithValue("@miktarı", int.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString()));
                        komut.Parameters.AddWithValue("@satışfiyatı", double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString()));
                        komut.Parameters.AddWithValue("@toplamfiyat", double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString()));
                        komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                        komut.ExecuteNonQuery();
                        SqlCommand komut2 = new SqlCommand("update uruntablosu set miktarı=miktarı-'" + int.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString()) + "' where barkodno='" + dataGridView1.Rows[i].Cells[3].Value.ToString() + "'", bağlanti);
                        komut2.ExecuteNonQuery();
                        bağlanti.Close();
                        bağlanti.Open();
                        SqlCommand komut3 = new SqlCommand("delete from sepeteekle", bağlanti);
                        komut3.ExecuteNonQuery();
                        bağlanti.Close();
                        MessageBox.Show("Satışınız Gerçekleşti", "TEBRİKLER", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataset.Tables["sepeteekle"].Clear();
                        sepetliste();
                        geneltoplam();
                    }
                    else
                    {
                        MessageBox.Show("Yeterli Ürün Bulunmamaktadır", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                txtbarkodno.Text = "";
                txtadsoyad.Text = "";
                txttc.Text = "";
                txttelefon.Text = "";
                txtsatışfiyatı.Text = "";
                txtürünadı.Text = "";
                txttoplamfiyat.Text = "";
                txtstok.Text = "";
            }
            else
            {
                MessageBox.Show("Ürün Yok Veya Ürün Seçilmedi", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void satış_sayfa_Load(object sender, EventArgs e)
        {
            geneltoplam();
            sepetliste();
            txtsatışfiyatı.Enabled = false;
            txtadsoyad.Enabled = false;
            txttelefon.Enabled = false;
            txtürünadı.Enabled = false;
            txttoplamfiyat.Enabled = false;
            txtsatışfiyatı.Enabled = false;
            txtstok.Enabled = false;
            txtsatışfiyatı.Text = "1";
            dataGridView1.AllowUserToAddRows = false;
        }

        private void btnsatışiptal_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                bağlanti.Open();
                SqlCommand komut = new SqlCommand("delete from sepeteekle", bağlanti);
                komut.ExecuteNonQuery();
                bağlanti.Close();
                MessageBox.Show("Ürünler Sepetten çıkarıldı", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataset.Tables["sepeteekle"].Clear();
                geneltoplam();
                sepetliste();
            }
            else
            {
                MessageBox.Show("Ürün Yok Veya Ürün Seçilmedi", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            txtbarkodno.Text = "";
            txtadsoyad.Text = "";
            txttc.Text = "";
            txttelefon.Text = "";
            txtsatışfiyatı.Text = "";
            txtürünadı.Text = "";
            txttoplamfiyat.Text = "";
            txtstok.Text = "";
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txttc.Text = dataGridView1.CurrentRow.Cells["tc"].Value.ToString();
            txtadsoyad.Text = dataGridView1.CurrentRow.Cells["adsoyad"].Value.ToString();
            txtbarkodno.Text = dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString();
            txtmiktar.Text = dataGridView1.CurrentRow.Cells["miktarı"].Value.ToString();
            txtsatışfiyatı.Text = dataGridView1.CurrentRow.Cells["satışfiyatı"].Value.ToString();
            txttelefon.Text = dataGridView1.CurrentRow.Cells["telefon"].Value.ToString();
        }

        private void satış_sayfa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Multiply)
            {
                txtmiktar.Text = txtbarkodno.Text.Substring(txtbarkodno.Text.Length - 1);
                txtbarkodno.Text = "";
            }
        }

        private void txtbarkodno_TextChanged(object sender, EventArgs e)
        {
            if (txtbarkodno.Text.Trim() == "")
            {
                txtürünadı.Text = "";
                txttoplamfiyat.Text = "";
                txtsatışfiyatı.Text = "";
            }
            bağlanti.Open();
            SqlCommand komut = new SqlCommand("select *from uruntablosu where barkodno = '" + txtbarkodno.Text + "'", bağlanti);
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                txtürünadı.Text = oku["urunadi"].ToString();
                txtsatışfiyatı.Text = oku["satışfiyatı"].ToString();
                txtstok.Text = oku["miktarı"].ToString();
                
            }
            else 
            {
                txtürünadı.Text = "";
                txttoplamfiyat.Text = "";
                txtsatışfiyatı.Text = "";
                txtstok.Text = "";
            }
            bağlanti.Close();
            txtmiktar.Text = "1";
        }

        private void txtsatışfiyatı_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txttoplamfiyat.Text = (double.Parse(txtmiktar.Text) * double.Parse(txtsatışfiyatı.Text)).ToString();
            }
            catch (Exception)
            {

            }
        }

        private void txtmiktar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txttoplamfiyat.Text = (double.Parse(txtmiktar.Text) * double.Parse(txtsatışfiyatı.Text)).ToString();
            }
            catch (Exception)
            {

            }
        }

        private void txttc_TextChanged(object sender, EventArgs e)
        {
            if (txttc.Text.Trim() == "")
            {
                txtadsoyad.Text = "";
                txttelefon.Text = "";
            }
            bağlanti.Open();
            SqlCommand komut = new SqlCommand("select *from müşteri where tc = '" + txttc.Text + "'", bağlanti);
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                txtadsoyad.Text = oku["adsoyad"].ToString();
                txttelefon.Text = oku["telefon"].ToString();
            }
            else
            {
                txtadsoyad.Text = "";
                txttelefon.Text = "";
            }
            bağlanti.Close();
        }

        private void mÜŞTERİEKLEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            müşteri_ekle müşteriekle = new müşteri_ekle();
            müşteriekle.ShowDialog();
        }

        private void mÜŞTERİLİSTELEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            müşteri_listele müşterilistele = new müşteri_listele();
            müşterilistele.ShowDialog();
        }

        private void üRÜNEKLEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yeni_ürün_paneli ürünekle = new yeni_ürün_paneli();
            ürünekle.ShowDialog();
        }

        private void üRÜNLİSTELEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ürün_listeleme ürünlistele = new ürün_listeleme();
            ürünlistele.ShowDialog();
        }

        private void kATEGORİEKLEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kategori_ekle kategoriekle = new kategori_ekle();
            kategoriekle.ShowDialog();
        }

        private void kATEGORİSİLVEGÜNCELLEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kategori_sil_güncelle kategorisilvegüncelle = new kategori_sil_güncelle();
            kategorisilvegüncelle.ShowDialog();
        }

        private void mARKAEKLEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            markaekle markaekle = new markaekle();
            markaekle.ShowDialog();
        }

        private void mARKASİLVEGÜNCELLEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            marka_sil_güncelle markasilgüncelle = new marka_sil_güncelle();
            markasilgüncelle.ShowDialog();
        }

        private void sATIŞLARILİSTELEMEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            satışları_listeleme satışlarılisteleme = new satışları_listeleme();
            satışlarılisteleme.ShowDialog();
        }

        private void çIKIŞYAPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult secim = MessageBox.Show("Çıkış yapmak istediğinize emin misiniz ?", "Çıkış Yapılıyor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes) 
            {
                Application.Exit();
            }
        }

        private void şİFREUNUTTUMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            şifre_unuttum_güncelleme güncelle = new şifre_unuttum_güncelleme();
            güncelle.ShowDialog();
        }

        private void şİFREGÜNCELLEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            şifre_güncelle şifregüncelle = new şifre_güncelle();
            şifregüncelle.ShowDialog();
        }

        private void txtbarkodno_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txttc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void txtadsoyad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar);
        }

        private void txttelefon_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtürünadı_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar);
        }
    }
}