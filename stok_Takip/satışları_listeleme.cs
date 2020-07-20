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
using System.Data.OleDb;

namespace stok_Takip
{
    public partial class satışları_listeleme : Form
    {
        public satışları_listeleme()
        {
            InitializeComponent();
        }
        SqlConnection bağlanti = new SqlConnection(VT_Bağlanti.bağlantı);
        DataSet dataset = new DataSet();
        private void satışliste()
        {
            bağlanti.Open();
            SqlCommand sorgu = new SqlCommand("select *from satiştablosu", bağlanti);
            SqlDataReader oku = sorgu.ExecuteReader();
            while (oku.Read())
            {
                //listviewdeki kayıt (satır) sayısı kadar döngüyü döndür
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    //döngü her döndüğünde tc ve barkod kısmını kontrol et
                    //eğer şu an "oku" isimli sqldatareader da bulunan veriler listviewdeki alanlara eklenmiş ise miktarı ve toplam fiyatı arttır, metodu return et
                    if (listView1.Items[i].SubItems[1].Text == oku["tc"].ToString() && listView1.Items[i].SubItems[4].Text == oku["barkodno"].ToString())
                    {
                        //o an listviewde bulunan miktar ile şu an veritabanından gelen miktarı topla, listviewdeki miktar kısmına yaz
                        int miktar = Convert.ToInt16(listView1.Items[i].SubItems[6].Text) + Convert.ToInt16(oku["miktarı"]);
                        listView1.Items[i].SubItems[6].Text = miktar.ToString();

                        double fiyat = Convert.ToDouble(listView1.Items[i].SubItems[8].Text) + Convert.ToDouble(oku["toplamfiyat"]);
                        listView1.Items[i].SubItems[8].Text = fiyat.ToString();

                        return;
                    }
                }

                //döngü içinde veriler girilip return edilirse metodun ordan sonraki kısmında bulunan kodlar çalışmaz, bu kısma hiç gelmez
                //eğer döngüden hiç veri girilmeden çıkılırsa bu kısımdaki kodlar çalışır ve veriler girilir
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["id"].ToString();
                ekle.SubItems.Add(oku["tc"].ToString());
                ekle.SubItems.Add(oku["adsoyad"].ToString());
                ekle.SubItems.Add(oku["telefon"].ToString());
                ekle.SubItems.Add(oku["barkodno"].ToString());
                ekle.SubItems.Add(oku["ürünadı"].ToString());
                ekle.SubItems.Add(oku["miktarı"].ToString());
                ekle.SubItems.Add(oku["satışfiyatı"].ToString());
                ekle.SubItems.Add(oku["toplamfiyat"].ToString());
                ekle.SubItems.Add(oku["tarih"].ToString());
                listView1.Items.Add(ekle);
            }
            bağlanti.Close();
        }

        private void btnaktar_Click(object sender, EventArgs e)
        {
           
        }

        private void şatışları_listeleme_Load(object sender, EventArgs e)
        {
            satışliste();
            listView1.Items[0].Selected = true;
            
        }
    }
}
