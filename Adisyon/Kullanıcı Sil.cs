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
    public partial class Kullanıcı_Sil : Form
    {
        public Kullanıcı_Sil()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ad = textBox1.Text;
            string soyad = textBox2.Text;
            string yetki = "";
            int id = 0;
            if(radioButton1.Checked)
            {
                yetki = "Admin";
            }
            if(radioButton2.Checked)
            {
                yetki = "Garson";
            }
            if(textBox1.Text!=""&&textBox2.Text!=""&&yetki!="")
            {
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Adisyon.mdb");
                baglanti.Open();
                OleDbCommand komut;
                string yetki2 = "";
                komut = new OleDbCommand("Select * from Adisyon.User where  Ad='" + ad + "' AND Soyad='" + soyad + "' AND Yetki='"+yetki+"'", baglanti);
                OleDbDataReader dr;
                dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    id = Convert.ToInt32(dr["Kimlik"]);
                }
                OleDbCommand komut2;
                komut2 = new OleDbCommand("Select * from Adisyon.User where Yetki='" + yetki + "'", baglanti);
                OleDbDataReader dr2;
                dr2 = komut2.ExecuteReader();
                while (dr2.Read())
                {
                    yetki2 += Convert.ToString(dr2["Yetki"]);
                }
                if (yetki2!="Admin")
                {
                    if (id != 0)
                    {
                        OleDbCommand komut3;
                        komut3 = new OleDbCommand("Delete from Adisyon.User Where Kimlik=" + id + "", baglanti);
                        komut3.ExecuteNonQuery();
                        MessageBox.Show("Kullanıcı Silindi!");
                    }
                    else if (id == 0)
                    {
                        MessageBox.Show("Kullanıcı Bulunamadı!");
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("Sistemde ki yönetici sayısı 1 den az olamaz!");
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Gerekli yerleri doldurduğunuzdan emin olun!");
                textBox1.Clear();
                textBox2.Clear();
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                textBox1.Focus();
            }
        }
    }
}
