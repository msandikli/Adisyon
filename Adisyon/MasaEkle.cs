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
    public partial class MasaEkle : Form
    {
        public MasaEkle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int masano = Convert.ToInt32(numericUpDown1.Value);
            int sayac = 0;
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Adisyon.mdb");
            baglanti.Open();
            OleDbCommand komut2;
            komut2 = new OleDbCommand("Select * from Adisyon.MasaNo", baglanti);
            OleDbDataReader dr2;
            dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                int masano2 = Convert.ToInt32(dr2["Masa"]);
                if(masano==masano2)
                {
                    sayac = 1;
                    MessageBox.Show("Masa Zaten Ekli!");
                }
            }
            if(sayac==0)
            {
                OleDbCommand komut3;
                komut3 = new OleDbCommand("Insert into Adisyon.MasaNo(Masa) values("+masano+")", baglanti);
                komut3.ExecuteNonQuery();
                MessageBox.Show("Masa Eklendi!");
            }
        }

        private void MasaEkle_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int masano = Convert.ToInt32(numericUpDown1.Value);
            int sayac = 1;
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Adisyon.mdb");
            baglanti.Open();
            OleDbCommand komut2;
            komut2 = new OleDbCommand("Select * from Adisyon.MasaNo", baglanti);
            OleDbDataReader dr2;
            dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                int masano2 = Convert.ToInt32(dr2["Masa"]);
                if (masano == masano2)
                {
                    sayac = 0;
                }
            }
            if (sayac == 0)
            {
                OleDbCommand komut3;
                komut3 = new OleDbCommand("Delete from Adisyon.MasaNo Where Masa="+masano+"", baglanti);
                komut3.ExecuteNonQuery();
                MessageBox.Show("Masa Kaldırıldı!");
            }
            if(sayac==1)
            {
                MessageBox.Show("Masa Bulunamadı!");
            }
        }
    }
}
