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
    public partial class Garson_Parolası_Değiştir : Form
    {
        public Garson_Parolası_Değiştir()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mevcut = textBox3.Text;
            string mevcut2 = "";
            string mevcut3 = "";
            string yeni1 = textBox4.Text;
            string yeni2 = textBox5.Text;
            string ad = textBox1.Text;
            string soyad = textBox2.Text;
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Adisyon.mdb");
            baglanti.Open();
            OleDbCommand komut;
            komut = new OleDbCommand("Select * from Adisyon.User where  Ad='" + ad + "' AND Soyad='"+soyad+"'", baglanti);
            OleDbDataReader dr;
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                mevcut2 = Convert.ToString(dr["Parola"]);
            }
            OleDbCommand komut3;
            komut3 = new OleDbCommand("Select * from Adisyon.User where  Parola='" + yeni1+ "'", baglanti);
            OleDbDataReader dr2;
            dr2 = komut3.ExecuteReader();
            while (dr2.Read())
            {
                mevcut3 = Convert.ToString(dr2["Parola"]);
            }
            if (ad==""||soyad==""||mevcut == "" || yeni1 == "" || yeni2 == "")
            {
                MessageBox.Show("Gerekli yerleri doldurduğunuzdan emin olun!");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox1.Focus();
            }
            else if(yeni1==mevcut3)
            {
                MessageBox.Show("Sistemde bu parolayı kullanan başka kullanıcı var.\nİşlem başarısız!");
                Close();
            }
            else if (mevcut != mevcut2)
            {
                MessageBox.Show("Mevcut Parolanız YANLIŞ!");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox1.Focus();

            }
            else if (yeni1 != yeni2)
            {
                MessageBox.Show("Girilen Yeni Parolalar Uyuşmuyor!");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox1.Focus();
            }
            else if (mevcut2 == yeni1)
            {
                MessageBox.Show("Yeni Parolanız Eski Parolanız İle Aynı Olamaz!");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox1.Focus();
            }
            else if (mevcut == mevcut2 && yeni1 == yeni2 && yeni1 != mevcut2)
            {
                OleDbCommand komut2;
                komut2 = new OleDbCommand();
                komut2.Connection = baglanti;
                komut2.CommandText = "update Adisyon.User set Parola='" + yeni1 + "' where Yetki='Garson' AND Ad='" + ad + "' AND Soyad='"+soyad+"'";
                komut2.ExecuteNonQuery();
                MessageBox.Show("Parola Değiştirme Başarılı");
                Close();
            }
            baglanti.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
