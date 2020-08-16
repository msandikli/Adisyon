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
    public partial class Kurulum : Form
    {
        public Kurulum()
        {
            InitializeComponent();
        }
        public int msayi = 0;
        public int ksayi = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            msayi = Convert.ToInt32(numericUpDown1.Value);
            ksayi = Convert.ToInt32(numericUpDown2.Value);
            if(msayi>0&&ksayi>0)
            {
                groupBox1.Visible = false;
                groupBox2.Visible = true;
                label3.Text = ksayi + " Adet Kategori Girin.";
                label4.Text = "1.";
            }
        }
        public int sayi = 2;
        private void button2_Click(object sender, EventArgs e)
        {
            string kategori = textBox1.Text;
            kategori = kategori.ToUpper();
            listBox1.Items.Add(kategori);
            if (sayi > ksayi)
            {
                groupBox3.Visible = true;
                
            }

            else if (textBox1.Text!="")
            {
                
                label4.Text = sayi + ".";
                
                textBox1.Clear();
                textBox1.Focus();
                sayi += 1;
            }
                
            

          
        }
        public string kategori = "";
        private void button3_Click(object sender, EventArgs e)
        {
            double fiyat = Convert.ToDouble(numericUpDown3.Value);
            string küsür = Convert.ToString("0." + numericUpDown4.Value);
            double fiyat2 = Convert.ToDouble(küsür);
            string ürün = textBox2.Text;
            ürün = ürün.ToUpper();
            fiyat2 = fiyat2 / 100;
            fiyat += fiyat2;
            Kurulum.ActiveForm.Size = new System.Drawing.Size(1050, 380);
            if(kategori!=""&&textBox2.Text!=""&&fiyat>=0)
            {
                int count = listBox3.Items.Count;
                int i = 0;
                int hata = 0;
                while(i<count)
                {
                    listBox3.SelectedIndex = i;
                    if(listBox3.SelectedItem.ToString()==ürün)
                    {
                        hata = 1;
                        break;
                    }
                    i++;
                }
                if(hata==0)
                {
                    listBox2.Items.Add(kategori);
                    listBox3.Items.Add(ürün);
                    listBox4.Items.Add(fiyat);
                }
                else
                {
                    MessageBox.Show("Ürün Zaten Ekli!");
                }
            }
        }
        public int secilen = -1;
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem!=null)
            {
                kategori = listBox1.SelectedItem.ToString();
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox3.SelectedIndex = listBox2.SelectedIndex;
            listBox4.SelectedIndex = listBox2.SelectedIndex;
            secilen = listBox2.SelectedIndex;
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.SelectedIndex = listBox3.SelectedIndex;
            listBox4.SelectedIndex = listBox3.SelectedIndex;
            secilen = listBox3.SelectedIndex;
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.SelectedIndex = listBox4.SelectedIndex;
            listBox3.SelectedIndex = listBox4.SelectedIndex;
            secilen = listBox4.SelectedIndex;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int secim = listBox2.SelectedIndex;
            int secim2 = listBox3.SelectedIndex;
            int secim3 = listBox4.SelectedIndex;
            if(secim!=-1||secim2!=-1||secim3!=-1)
            {
                listBox2.Items.RemoveAt(secim);
                listBox3.Items.RemoveAt(secim2);
                listBox4.Items.RemoveAt(secim3);
            }
            MessageBox.Show("Ürün Silindi!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int hata = 0;
            int sayac2 = listBox2.Items.Count;
            int j = 0;
            int i = 0;
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Adisyon.mdb");
            baglanti.Open();
            while (i < sayac2)
            {
                listBox2.SelectedIndex = i;
                listBox3.SelectedIndex = i;
                listBox4.SelectedIndex = i;
                string kat = listBox2.SelectedItem.ToString();
                string ur = listBox3.SelectedItem.ToString();
                string fyt = listBox4.SelectedItem.ToString();
                kat = kat.ToUpper();
                ur = ur.ToUpper();
                OleDbCommand komut;
                komut = new OleDbCommand("Select * from Adisyon.Menu", baglanti);
                OleDbDataReader dr;
                dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    string kat2 = Convert.ToString(dr["Tür"]);
                    string ur2 = Convert.ToString(dr["Ürün"]);
                    string fiyat2 = Convert.ToString(dr["Fiyat"]);
                    if (kat == Convert.ToString(kat2) && ur == ur2 && fyt == fiyat2)
                    {
                        hata = 1;
                    }
                }
                if (hata == 0)
                {
                    OleDbCommand komut2;
                    komut2 = new OleDbCommand();
                    komut2.Connection = baglanti;
                    komut2.CommandText = "Insert into Adisyon.Menu(Tür,Ürün,Fiyat) values('" + kat + "','" + ur + "','" + fyt + "')";
                    komut2.ExecuteNonQuery();
                }
                hata = 0;
                i++;
            }
            i = 0;
            hata = 0;
            while (i < msayi)
            {
                int sayi = i + 1;
                OleDbCommand komut3;
                komut3 = new OleDbCommand("Select * from Adisyon.MasaNo", baglanti);
                OleDbDataReader dr2;
                dr2 = komut3.ExecuteReader();
                while (dr2.Read())
                {
                    string no = Convert.ToString(dr2["Masa"]);
                    if (no == Convert.ToString(sayi))
                    {
                        hata = 1;
                    }
                }
                if (hata == 0)
                {
                    OleDbCommand komut4;
                    komut4 = new OleDbCommand();
                    komut4.Connection = baglanti;
                    komut4.CommandText = "Insert into Adisyon.MasaNo(Masa) values('" + sayi + "')";
                    komut4.ExecuteNonQuery();
                }
                hata = 0;
                i++;
            }
            baglanti.Close();
            if(sayac2!=0)
            {
                Admin frm = new Admin();
                frm.Show();
                this.Hide();
            }
        }

        private void Kurulum_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string ad = textBox4.Text;
            ad = ad.ToUpper();
            string soyad = textBox3.Text;
            soyad = soyad.ToUpper();
            if (ad==""||ad==" ")
            {
                MessageBox.Show("Ad kısmı boş olamaz!");
                textBox3.Focus();
            }
            else if (soyad == "" || soyad == " ")
            {
                MessageBox.Show("Soyad kısmı boş olamaz!");
                textBox3.Focus();
            }
            else
            {
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Adisyon.mdb");
                baglanti.Open();
                OleDbCommand komut2;
                komut2 = new OleDbCommand();
                komut2.Connection = baglanti;
                komut2.CommandText = "update Adisyon.User set Ad='" + ad + "', Soyad='"+soyad+"' where Parola='1234'";
                komut2.ExecuteNonQuery();
                groupBox1.Visible = true;
            }
        }

        private void Kurulum_Load(object sender, EventArgs e)
        {
            textBox3.Focus();
        }
    }
}
