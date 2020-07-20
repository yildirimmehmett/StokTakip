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
    public partial class Şifremi_Unuttum : Form
    {
        public Şifremi_Unuttum()
        {
            InitializeComponent();
        }
        SqlConnection bağlan = new SqlConnection(VT_Bağlanti.bağlantı);
        private void btnsifre_Click(object sender, EventArgs e)
        {
            bağlan.Open();
            SqlCommand sorgu = new SqlCommand("select *from sifre", bağlan);
            SqlDataReader oku = sorgu.ExecuteReader();
            while (oku.Read())
            {
                if (textBox1.Text.Trim() == oku["ensevdğinvarlık"].ToString()) 
                {
                    şifre_güncelle güncelle = new şifre_güncelle();
                    güncelle.Show();
                    this.Hide();
                }
                else
                {
                    textBox1.Clear();
                }
                bağlan.Close();
            }
        }
    }
}
