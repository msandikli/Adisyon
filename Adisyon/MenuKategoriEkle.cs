using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace Adisyon
{
    public partial class MenuKategoriEkle : Form
    {
        public MenuKategoriEkle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kat = textBox1.Text;
            kat = kat.ToUpper();
            int sayac = 0;
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Adisyon.mdb");
            baglanti.Open();
            OleDbCommand komut2;
            komut2 = new OleDbCommand("Select * from Adisyon.Menu", baglanti);
            OleDbDataReader dr2;
            dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                string kat2= Convert.ToString(dr2["Tür"]);
                if (kat == kat2)
                {
                    sayac = 1;
                    MessageBox.Show("Kategori Zaten Ekli!");
                }
            }
            if (sayac == 0)
            {
                OleDbCommand komut3;
                komut3 = new OleDbCommand("Insert into Adisyon.Menu(Tür) values('" + kat + "')", baglanti);
                komut3.ExecuteNonQuery();
                MessageBox.Show("Kategori Eklendi!");
                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string kat = textBox1.Text;
            kat = kat.ToUpper();
            int sayac = 1;
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Adisyon.mdb");
            baglanti.Open();
            OleDbCommand komut2;
            komut2 = new OleDbCommand("Select * from Adisyon.Menu", baglanti);
            OleDbDataReader dr2;
            dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                string kat2 = Convert.ToString(dr2["Tür"]);
                if (kat == kat2)
                {
                    sayac = 0;
                }
            }
            if (sayac == 0)
            {
                DialogResult dialog = new DialogResult();
                dialog = MessageBox.Show("Bu işlem kategoriye ekli ürünlerin de silinmesine yol açacaktır. Kategori Silinsin Mi?", "Kategori Sil", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    OleDbCommand komut3;
                    komut3 = new OleDbCommand("Delete from Adisyon.Menu Where Tür='" + kat + "'", baglanti);
                    komut3.ExecuteNonQuery();
                    MessageBox.Show("Kategori ve Kategoriye Bağlı Ürünler Kaldırıldı!");
                    Close();
                }
                
            }
            if (sayac == 1)
            {
                MessageBox.Show("Kategori Bulunamadı!");
                Close();
            }
        }
    }
}
