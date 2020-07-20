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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection bağlan = new SqlConnection(VT_Bağlanti.bağlantı);
        private void btngirişyap_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "")
            {
                bağlan.Open();
                string sqlcomut = "Select *From sifre where Ad=@adı AND sifre=@sifre";
                SqlParameter parametre = new SqlParameter("adı", textBox1.Text.Trim());
                SqlParameter parametre1 = new SqlParameter("sifre", textBox2.Text.Trim());
                SqlCommand komut = new SqlCommand(sqlcomut, bağlan);
                komut.Parameters.Add(parametre);
                komut.Parameters.Add(parametre1);
                DataTable table = new DataTable();
                SqlDataAdapter adptor = new SqlDataAdapter(komut);
                adptor.Fill(table);
                if (table.Rows.Count > 0)
                {
                    stok_otomasyon otomasyon = new stok_otomasyon();
                    otomasyon.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Hatalı Giriş", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);  
                }
            }
            else
            {
                MessageBox.Show("Formu Doldurduğunuzdan Emin Olunuz", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            bağlan.Close();
            textBox1.Clear();
            textBox2.Clear();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Şifremi_Unuttum şifreunuttum = new Şifremi_Unuttum();
            şifreunuttum.ShowDialog();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar);
        }
    }
}
