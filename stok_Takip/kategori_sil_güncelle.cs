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
    public partial class kategori_sil_güncelle : Form
    {
        public kategori_sil_güncelle()
        {
            InitializeComponent();
        }
        SqlConnection bağlanti = new SqlConnection(VT_Bağlanti.bağlantı);
        DataSet dataset = new DataSet();
        private void btnsil_Click(object sender, EventArgs e)
        {

            if (dataGridView1.CurrentRow.Cells["kategori"].Value.ToString() != "")
            {
                bağlanti.Open();
                SqlCommand komut = new SqlCommand("delete from kategoribilgisi", bağlanti);
                komut.ExecuteNonQuery();
                bağlanti.Close();
                dataset.Tables["kategoribilgisi"].Clear();
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
            if (textBox1.Text.Trim() != "")
            {
                bağlanti.Open();
                SqlCommand komut = new SqlCommand("update kategoribilgisi set kategori=@kategori", bağlanti);
                komut.Parameters.AddWithValue("@kategori", textBox1.Text);
                komut.ExecuteNonQuery();
                bağlanti.Close();
                MessageBox.Show("Güncelleme Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataset.Tables["kategoribilgisi"].Clear();
                ürünliste();
            }
            else
            {
                MessageBox.Show("Güncelleme Yapılamadı", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ürünliste()
        {
            bağlanti.Open();
            SqlDataAdapter adaptor = new SqlDataAdapter("select kategori from kategoribilgisi", bağlanti);
            adaptor.Fill(dataset, "kategoribilgisi");
            dataGridView1.DataSource = dataset.Tables["kategoribilgisi"];
            bağlanti.Close();
        }

        private void kategori_sil_güncelle_Load(object sender, EventArgs e)
        {
            ürünliste();
            dataGridView1.AllowUserToAddRows = false;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["kategori"].Value.ToString();
        }
    }
}
