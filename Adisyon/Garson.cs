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
using System.Collections;

namespace Adisyon
{
    public partial class Garson : Form
    {
        public Garson()
        {
            InitializeComponent();
            
        }
        private void Garson_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void Garson_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
        }

        private void Garson_SizeChanged(object sender, EventArgs e)
        {
            label2.Location = new Point(50,11);
            int boyut2 = label2.Width;
            boyut2 += 45;
            int boyut3 = boyut2;
            label3.Location = new Point(boyut3,11);
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            label8.Text = "1";
            groupBox3.Visible = false;
            groupBox2.Visible = true;
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Adisyon.mdb");
            baglanti.Open();
            OleDbCommand komut;
            komut = new OleDbCommand("Select * from Adisyon.MasaNo", baglanti);
            OleDbDataReader dr;
            dr = komut.ExecuteReader();
            while(dr.Read())
            {
                string no=Convert.ToString(dr["Masa"]);
                int no2 = Convert.ToInt32(no);
                if (no2 < 10)
                    no = "0" + no;
                listBox1.Items.Add("Masa:" + no);
            }
            OleDbCommand komut2;
            komut2 = new OleDbCommand("Select * from Adisyon.Menu", baglanti);
            OleDbDataReader dr2;
            dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                int i = 0;
                int say = 0;
                int count = listBox2.Items.Count;
                string asd =Convert.ToString(dr2["Tür"]);
                while(i<count)
                {
                    listBox2.SelectedIndex = i;
                    string asd2 = listBox2.SelectedItem.ToString();
                    if(asd==asd2)
                    {
                        say = 1;
                    }
                    i++;
                }
                if(say==0)
                {
                    listBox2.Items.Add(asd);
                }
            }

            baglanti.Close();
            ArrayList list = new ArrayList();
            foreach (object o in listBox1.Items)
            {
                list.Add(o);
            }
            list.Sort();
            listBox1.Items.Clear();
            foreach (object o in list)
            {
                listBox1.Items.Add(o);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = true;
            groupBox2.Visible = false;
            listBox4.Items.Clear();
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Adisyon.mdb");
            baglanti.Open();
            OleDbCommand komut3;
            komut3 = new OleDbCommand("Select * from Adisyon.Siparişler where Ödendi='Hayır'", baglanti);
            OleDbDataReader dr3;
            dr3 = komut3.ExecuteReader();
            int sayac = 0;
            
            while(dr3.Read())
            {
                int maks = listBox4.Items.Count;
                string no= Convert.ToString(dr3["MasaNo"]);
                int no2 = Convert.ToInt32(no);
                int i = 0;
                while(i<maks)
                {
                    listBox4.SelectedIndex = i;
                    if(listBox4.SelectedItem.ToString()=="Masa:"+no|| listBox4.SelectedItem.ToString() == "Masa:0" + no)
                    {
                        sayac = 1;
                    }
                    i++;
                }
                if(sayac==0)
                {
                    if(no2<10)
                    {
                        listBox4.Items.Add("Masa:0" + no);
                    }
                    else
                    {
                        listBox4.Items.Add("Masa:" + no);
                    }
                }
                sayac = 0;
            }

        }
        public string fiyat = "";
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            if(listBox2.SelectedItem!=null)
            {
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Adisyon.mdb");
                baglanti.Open();
                string tür = listBox2.SelectedItem.ToString();
                OleDbCommand komut3;
                komut3 = new OleDbCommand("Select * from Adisyon.Menu where Tür='" + tür + "'", baglanti);
                OleDbDataReader dr3;
                dr3 = komut3.ExecuteReader();
                while (dr3.Read())
                {
                    string ürün = Convert.ToString(dr3["Ürün"]);
                    listBox3.Items.Add(ürün);
                }
                baglanti.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(fiyat!="")
            {
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Adisyon.mdb");
                baglanti.Open();
                string masano = (listBox1.SelectedIndex + 1).ToString();
                string sipariş = listBox3.SelectedItem.ToString();
                string adet = label8.Text;
                this.WindowState = FormWindowState.Maximized;
                string tarih = DateTime.Today.ToString();
                string gun = "";
                string ay = "";
                string yıl = "";
                string garson = label2.Text + " " + label3.Text;
                string toplam = (Convert.ToInt32(adet) * Convert.ToDouble(fiyat)).ToString();
                gun += tarih[0];
                gun += tarih[1];
                ay += tarih[3];
                ay += tarih[4];
                yıl += tarih[6];
                yıl += tarih[7];
                yıl += tarih[8];
                yıl += tarih[9];
                OleDbCommand komut2;
                if(listBox1.SelectedItem==null)
                {
                    MessageBox.Show("Masa No Seçin!");
                }
                else if(listBox3.SelectedItem==null)
                {
                    MessageBox.Show("Ürün Seçin!");
                }
                else
                {
                    komut2 = new OleDbCommand();
                    komut2.Connection = baglanti;
                    komut2.CommandText = "Insert into Adisyon.Siparişler(MasaNo,Sipariş,Fiyat,Gün,Ay,Yıl,Adet,Ödendi,Toplam,Garson) values('" + masano + "','" + sipariş + "','" + fiyat + "','" + gun + "','" + ay + "','" + yıl + "','" + adet + "','Hayır','" + toplam + "','" + garson + "')";
                    komut2.ExecuteNonQuery();
                    MessageBox.Show("İşlem Başarılı");
                    baglanti.Close();
                }
            }
            label8.Text = "1";
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox3.SelectedItem!=null)
            {
                string ürün = listBox3.SelectedItem.ToString();
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Adisyon.mdb");
                baglanti.Open();
                string tür = listBox2.SelectedItem.ToString();
                OleDbCommand komut4;
                komut4 = new OleDbCommand("Select * from Adisyon.Menu where Ürün='" + ürün + "' AND Tür='" + tür + "'", baglanti);
                OleDbDataReader dr4;
                dr4 = komut4.ExecuteReader();
                while (dr4.Read())
                {
                    fiyat = Convert.ToString(dr4["Fiyat"]);
                }
                baglanti.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int sayi = Convert.ToInt32(label8.Text);
            sayi += 1;
            label8.Text = sayi.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int sayi = Convert.ToInt32(label8.Text);
            sayi -= 1;
            label8.Text = sayi.ToString();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Adisyon.mdb");
            baglanti.Open();
            string masano = listBox4.SelectedItem.ToString();
            masano = masano[5].ToString()+ masano[6].ToString();
            OleDbCommand komut2;
            komut2 = new OleDbCommand();
            komut2.Connection = baglanti;
            komut2.CommandText = "update Adisyon.Siparişler set Hesap='Evet' where MasaNo=" + masano + "";
            komut2.ExecuteNonQuery();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
