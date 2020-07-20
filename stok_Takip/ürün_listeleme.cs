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
    public partial class ürün_listeleme : Form
    {
        public ürün_listeleme()
        {
            InitializeComponent();
        }
        SqlConnection bağlanti = new SqlConnection(VT_Bağlanti.bağlantı);
        DataSet dataset = new DataSet();
        private void ürün_listeleme_Load(object sender, EventArgs e)
        {
            ürünliste();
            kategorigel();
            barkodnotxt.Enabled = false;
            dataGridView1.AllowUserToAddRows = false;
        }
        private void ürünliste()
        {
            bağlanti.Open();
            SqlDataAdapter adaptor = new SqlDataAdapter("select *from uruntablosu", bağlanti);
            adaptor.Fill(dataset, "uruntablosu");
            dataGridView1.DataSource = dataset.Tables["uruntablosu"];
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

        private void btnkategorimarkagüncelle_Click(object sender, EventArgs e)
        {
            if (combokategori.Text.Trim() != "" && combomarka.Text.Trim() != "")
            {
                bağlanti.Open();
                SqlCommand komut = new SqlCommand("update uruntablosu set kategori=@kategori,marka=@marka where barkodno=@barkodno", bağlanti);
                komut.Parameters.AddWithValue("@barkodno", barkodnotxt.Text);
                komut.Parameters.AddWithValue("@marka", combomarka.Text);
                komut.Parameters.AddWithValue("@kategori", combokategori.Text);
                komut.ExecuteNonQuery();
                bağlanti.Close();
                MessageBox.Show("Güncelleme Yapıldı", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataset.Tables["uruntablosu"].Clear();
                ürünliste();
                combokategori.Text = "";
                combomarka.Text = "";
            }
            if (combokategori.Text != "" || combomarka.Text != "")
            {
                MessageBox.Show("Barkodno Girilmemiş", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString() != "")
            {
                bağlanti.Open();
                SqlCommand komut = new SqlCommand("delete from uruntablosu where barkodno='" + dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString() + "'", bağlanti);
                komut.ExecuteNonQuery();
                bağlanti.Close();
                MessageBox.Show("Silme İşlemi Tamamlanıyor Lütfen Bekleyiniz", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataset.Tables["uruntablosu"].Clear();
                ürünliste();
                barkodnotxt.Text = "";
                kategoritxt.Text = "";
                markatxt.Text = "";
                ürüntxt.Text = "";
                miktartxt.Text = "";
                alıştxt.Text = "";
                satıştxt.Text = "";
                combokategori.Text = "";
                combomarka.Text = "";
            }
            else
            {
                MessageBox.Show("Sütün Zaten Boş Neyi Silecen", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btngüncelle_Click(object sender, EventArgs e)
        {
            if (alıştxt.Text.Trim() != "" && satıştxt.Text.Trim() != "" && markatxt.Text.Trim() != "" && kategoritxt.Text.Trim() != "" && barkodnotxt.Text.Trim() != "" && miktartxt.Text.Trim() != "")
            {
                bağlanti.Open();
                SqlCommand komut = new SqlCommand("update uruntablosu set urunadi=@urunadi,miktarı=@miktarı,alışfiyatı=@alışfiyatı,satışfiyatı=@satışfiyatı where barkodno=@barkodno", bağlanti);
                komut.Parameters.AddWithValue("@barkodno", barkodnotxt.Text);
                komut.Parameters.AddWithValue("@miktarı", int.Parse(miktartxt.Text));
                komut.Parameters.AddWithValue("@alışfiyatı", double.Parse(alıştxt.Text));
                komut.Parameters.AddWithValue("@satışfiyatı", double.Parse(satıştxt.Text));
                komut.Parameters.AddWithValue("@urunadi", ürüntxt.Text);
                komut.ExecuteNonQuery();
                bağlanti.Close();
                MessageBox.Show("Güncelleme Yapıldı", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataset.Tables["uruntablosu"].Clear();
                ürünliste();
            }
            else
            {
                MessageBox.Show("Formu Doldurduğunuzdan Emin Olunuz", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            barkodnotxt.Text = "";
            miktartxt.Text = "";
            alıştxt.Text = "";
            satıştxt.Text = "";
            ürüntxt.Text = "";
            kategoritxt.Text = "";
            markatxt.Text = "";
            ürüntxt.Text = "";
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            barkodnotxt.Text = dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString();
            kategoritxt.Text = dataGridView1.CurrentRow.Cells["kategori"].Value.ToString();
            markatxt.Text = dataGridView1.CurrentRow.Cells["marka"].Value.ToString();
            ürüntxt.Text = dataGridView1.CurrentRow.Cells["urunadi"].Value.ToString();
            miktartxt.Text = dataGridView1.CurrentRow.Cells["miktarı"].Value.ToString();
            alıştxt.Text = dataGridView1.CurrentRow.Cells["alışfiyatı"].Value.ToString();
            satıştxt.Text = dataGridView1.CurrentRow.Cells["satışfiyatı"].Value.ToString();
        }

        private void combokategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            combomarka.Items.Clear();
            combomarka.Text = "";
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
