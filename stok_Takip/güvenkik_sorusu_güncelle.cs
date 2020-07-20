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
    public partial class güvenkik_sorusu_güncelle : Form
    {
        public güvenkik_sorusu_güncelle()
        {
            InitializeComponent();
        }
        SqlConnection bağlanti = new SqlConnection(@"Data Source=DESKTOP-SV0PRS9\SQLEXPRESS01;Initial Catalog=Stok_Takip;Integrated Security=True");
        DataSet dataset = new DataSet();
        private void btngüncelle_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                bağlanti.Open();
                SqlCommand komut = new SqlCommand("update sifre set kategori=@kategori where id='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'", bağlanti);
                komut.Parameters.AddWithValue("@kategori", textBox1.Text);
                komut.ExecuteNonQuery();
                bağlanti.Close();
                MessageBox.Show("Güncelleme Yapıldı", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            SqlDataAdapter adaptor = new SqlDataAdapter("select *from kategoribilgisi", bağlanti);
            adaptor.Fill(dataset, "kategoribilgisi");
            dataGridView1.DataSource = dataset.Tables["kategoribilgisi"];
            bağlanti.Close();
        }
    }
}
