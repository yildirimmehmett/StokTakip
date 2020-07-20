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
    public partial class müşteri_listele : Form
    {
        public müşteri_listele()
        {
            InitializeComponent();
        }
        SqlConnection bağlanti = new SqlConnection(VT_Bağlanti.bağlantı);
        DataSet dataset = new DataSet();
        private void btnsil_Click(object sender, EventArgs e)
        {
            string sorgu = "delete from müşteri where tc=@tc";
            SqlCommand komut = new SqlCommand(sorgu, bağlanti);
            komut.Parameters.AddWithValue("@tc", txttc.Text);
            bağlanti.Open();
            komut.ExecuteNonQuery();
            MessageBox.Show("İşleminiz Yapılıyor Lütfen Bekleyiniz", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bağlanti.Close();
            listele();
            txtadres.Text = "";
            txtadsoyad.Text = "";
            txtemail.Text = "";
            txttc.Text = "";
            txttelefon.Text = "";
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            if (txtadres.Text.Trim() != "" && txtadsoyad.Text.Trim() != "" && txtemail.Text.Trim() != "" && txttc.Text.Trim() != "" && txttelefon.Text.Trim() != "")
            {
                bağlanti.Open();
                SqlCommand komut = new SqlCommand("update müşteri set adsoyad=@adsoyad,telefon=@telefon,adres=@adres,email=@email where tc=@tc", bağlanti);
                komut.Parameters.AddWithValue("@tc", txttc.Text);
                komut.Parameters.AddWithValue("@adsoyad", txtadsoyad.Text);
                komut.Parameters.AddWithValue("@telefon", txttelefon.Text);
                komut.Parameters.AddWithValue("@adres", txtadres.Text);
                komut.Parameters.AddWithValue("@email", txtemail.Text);
                komut.ExecuteNonQuery();
                bağlanti.Close();
                listele();
                MessageBox.Show("Müşteri kaydınız Güncellendi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtadres.Text = "";
                txtadsoyad.Text = "";
                txtemail.Text = "";
                txttc.Text = "";
                txttelefon.Text = "";
            }
            else
            {
                MessageBox.Show("Formu Doldurduğunuzdan Emin Olunuz", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void listele()
        {
            bağlanti.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select *from müşteri", bağlanti);
            DataTable tablo = new DataTable();
            adapter.Fill(tablo);
            dataGridView1.DataSource = tablo;
            bağlanti.Close();
        }
        private void müşteri_listele_Load(object sender, EventArgs e)
        {
            listele();
            txttc.Enabled = false;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txttc.Text = dataGridView1.CurrentRow.Cells["tc"].Value.ToString();
            txttelefon.Text = dataGridView1.CurrentRow.Cells["telefon"].Value.ToString();
            txtemail.Text = dataGridView1.CurrentRow.Cells["email"].Value.ToString();
            txtadsoyad.Text = dataGridView1.CurrentRow.Cells["adsoyad"].Value.ToString();
            txtadres.Text = dataGridView1.CurrentRow.Cells["adres"].Value.ToString();
        }
    }
}
