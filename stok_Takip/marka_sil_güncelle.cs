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
    public partial class marka_sil_güncelle : Form
    {
        public marka_sil_güncelle()
        {
            InitializeComponent();
        }
        SqlConnection bağlanti = new SqlConnection(VT_Bağlanti.bağlantı);
        DataSet dataset = new DataSet();
        private void ürünliste()
        {
            bağlanti.Open();
            SqlDataAdapter adaptor = new SqlDataAdapter("select kategori,marka from markabilgisi", bağlanti);
            adaptor.Fill(dataset, "markabilgisi");
            dataGridView1.DataSource = dataset.Tables["markabilgisi"];
            bağlanti.Close();
        }
        private void btnsil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells["id"].Value.ToString() != "")
            {
                SqlCommand komut = new SqlCommand("delete from markabilgisi", bağlanti);
                bağlanti.Open();
                komut.ExecuteNonQuery();
                bağlanti.Close();
                dataset.Tables["markabilgisi"].Clear();
                ürünliste();
                MessageBox.Show("Silme İşlemi Tamamlanıyor Lütfen Bekleyiniz", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = "";
            }
            else
            {
                MessageBox.Show("Sütün Zaten Boş Neyi Silecen", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        private void btngüncelle_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "")
            {
                SqlCommand komut = new SqlCommand("update markabilgisi set kategori=@kategori,marka=@marka", bağlanti);
                komut.Parameters.AddWithValue("@kategori", textBox1.Text);
                komut.Parameters.AddWithValue("@marka", textBox2.Text);
                bağlanti.Open();
                komut.ExecuteNonQuery();
                bağlanti.Close();
                MessageBox.Show("Güncelleme Yapıldı", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataset.Tables["markabilgisi"].Clear();
                ürünliste();
            }
            else
            {
                MessageBox.Show("Güncelleme Yapılamadı", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void marka_ekle_sil_Load(object sender, EventArgs e)
        {
            ürünliste();
            dataGridView1.AllowUserToAddRows = false;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["kategori"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["marka"].Value.ToString();
        }
    }
}
