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
    public partial class Admin : Form
    {
        public Admin()
        {
            
            InitializeComponent();
            
        }

        private void yeniKullanıcıEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Yeni_Kullanıcı frm1 = new Yeni_Kullanıcı();
            frm1.Show();
        }

        private void yöneticiParolasıDeğiştirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParolaDegistir frm2 = new ParolaDegistir();
            frm2.label5.Text = label2.Text;
            frm2.Show();
        }
        public string gun = "";
        public string ay = "";
        public string yıl = "";
        private void Admin_Load(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            string tarih= DateTime.Today.ToString();
            
            gun += tarih[0];
            gun += tarih[1];
            ay += tarih[3];
            ay += tarih[4];
            yıl += tarih[6];
            yıl += tarih[7];
            yıl += tarih[8];
            yıl += tarih[9];
            timer1.Start();

            
        }

        private void kullanıcıSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kullanıcı_Sil frm3 = new Kullanıcı_Sil();
            frm3.Show();
        }

        private void Admin_SizeChanged(object sender, EventArgs e)
        {
            if(System.Windows.Forms.Form.ActiveForm !=null)
            {
                int boyut = Admin.ActiveForm.Height;
                int boyut2 = label2.Width;
                int boyut3 = boyut2 + 10;
                boyut -= 60;
                label1.Location = new Point(11, boyut);
                label2.Location = new Point(55, boyut);
            }
        }

        private void Admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void garsonParolasıDeğiştirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Garson_Parolası_Değiştir grs = new Garson_Parolası_Değiştir();
            grs.Show();
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buAyınKârınıGörüntüleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double ciro = 0;
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Adisyon.mdb");
                baglanti.Open();
                OleDbCommand komut;
                komut = new OleDbCommand("Select * from Adisyon.Cirolar where Ay='"+ay+"' AND Yıl='"+yıl+"'", baglanti);
                OleDbDataReader dr;
                dr = komut.ExecuteReader();
                while(dr.Read())
                {
                    string cr= Convert.ToString(dr["Ciro"]);
                    if(cr!="")
                    {
                        ciro += Convert.ToDouble(cr);
                    }
                }
                baglanti.Close();
            
            if(ciro==0)
            {
                MessageBox.Show("Bu Ay Henüz Ciro Yapmadınız...");
            }
            else
            {
                MessageBox.Show("Bu Ayın Cirosu: "+ciro+" TL");
            }


        }

        private void çalışanGarsonSayısıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Adisyon.mdb");
            baglanti.Open();
            int garsonsayi = 0;
            OleDbCommand komut;
            komut = new OleDbCommand("Select * from Adisyon.User where Yetki='Garson'", baglanti);
            OleDbDataReader dr;
            dr = komut.ExecuteReader();
            while(dr.Read())
            {
                garsonsayi += 1;
            }
            baglanti.Close();
            MessageBox.Show("Çalışan garson sayısı: " + garsonsayi);
        }

        private void masaEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MasaEkle ms = new MasaEkle();
            ms.Show();
        }
        public int sayac = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            if(sayac%10==0||sayac==1)
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox3.Items.Clear();
                listBox4.Items.Clear();
                string sipariş = "";
                string kimlik = "";
                int sayac = 0;
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Adisyon.mdb");
                baglanti.Open();
                OleDbCommand komut;
                komut = new OleDbCommand("Select * from Adisyon.Siparişler where Ödendi='Hayır'", baglanti);
                OleDbDataReader dr;
                dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    int count1 = listBox2.Items.Count;
                    sipariş = "Garson: " + Convert.ToString(dr["Garson"]) + " | Masa NO: " + Convert.ToString(dr["MasaNo"]) + " | Sipariş: " + Convert.ToString(dr["Sipariş"]) + " | Adet: " + Convert.ToString(dr["Adet"]) + " | Toplam: " + Convert.ToString(dr["Toplam"])+" TL";
                    kimlik = Convert.ToString(dr["Kimlik"]);
                    int i = 0;
                    while (i < count1)
                    {
                        listBox2.SelectedIndex = i;
                        if (listBox2.SelectedItem.ToString() == sipariş)
                        {
                            sayac = 1;
                        }
                        i++;
                    }
                    if (sayac == 0)
                    {
                        listBox2.Items.Add(sipariş);
                        listBox3.Items.Add(kimlik);
                    }
                    sayac = 0;
                }
                string istenen = "";
                string garson = "";
                string kimlik2 = "";
                int sayac2 = 0;
                
                OleDbCommand komut2;
                komut2 = new OleDbCommand("Select * from Adisyon.Siparişler where Ödendi='Hayır' AND Hesap='Evet'", baglanti);
                OleDbDataReader dr2;
                dr2 = komut2.ExecuteReader();
                while(dr2.Read())
                {
                    int count2 = listBox1.Items.Count;
                    int j = 0;
                    string aa = "";
                    istenen = Convert.ToString(dr2["MasaNo"]);
                    garson = Convert.ToString(dr2["Garson"]);
                    int istenen2 = Convert.ToInt32(istenen);
                    if(istenen2<10)
                    {
                        aa = "Masa No: 0" + istenen + " | Garson: " + garson;
                    }
                    else
                    {
                        aa = "Masa No: " + istenen + " | Garson: " + garson;
                    }
                    
                    kimlik2= Convert.ToString(dr2["Kimlik"]);
                    
                    while(j<count2)
                    {
                        listBox1.SelectedIndex = j;
                        if(listBox1.SelectedItem.ToString()==aa)
                        {
                            sayac2 = 1;
                        }
                       j++;
                    }
                    if(sayac2==0)
                    {
                        listBox1.Items.Add(aa);
                        listBox4.Items.Add(kimlik2);
                    }
                    sayac2 = 0;
                }
                baglanti.Close();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem!=null)
            {
                string hesap = listBox1.SelectedItem.ToString();
                string hesap2 = hesap[9].ToString();
                hesap2 += hesap[10].ToString();
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Adisyon.mdb");
                baglanti.Open();
                OleDbCommand komut;
                komut = new OleDbCommand("Select * from Adisyon.Siparişler where Ödendi='Hayır' AND Hesap='Evet' AND MasaNo=" + hesap2, baglanti);
                OleDbDataReader dr;
                dr = komut.ExecuteReader();
                double ciro2 = 0;
                while (dr.Read())
                {
                    ciro2 = Convert.ToDouble(dr["Toplam"]);
                    string aranan = Convert.ToString(dr["Kimlik"]);
                    string tarih = "";
                    string tarih2 = gun + "." + ay + "." + yıl;
                    string tarih3 = "";
                    double ciro = 0;
                    string garsonad = Convert.ToString(dr["Garson"]);
                    double ciro3 = 0;
                    int sayac = 0;
                    OleDbCommand komut3;
                    komut3 = new OleDbCommand("Select * from Adisyon.Cirolar", baglanti);
                    OleDbDataReader dr2;
                    dr2 = komut3.ExecuteReader();
                    while (dr2.Read())
                    {
                        tarih = Convert.ToString(dr2["Gün"]) + ".";
                        tarih+= Convert.ToString(dr2["Ay"]) + "." + Convert.ToString(dr2["Yıl"]);
                        if (tarih2 == tarih)
                        {
                            ciro += Convert.ToDouble(dr2["Ciro"]);
                            sayac = 1;
                        }

                    }

                    if (sayac == 0)
                    {
                        OleDbCommand komut5;
                        komut5 = new OleDbCommand();
                        komut5.Connection = baglanti;
                        komut5.CommandText = "Insert into Adisyon.Cirolar(Gün,Ay,Yıl,Ciro) values('" + gun + "','" + ay + "','"+yıl+"',"+ciro2+")";
                        komut5.ExecuteNonQuery();
                    }
                    else if (sayac == 1)
                    {
                        ciro += ciro2;
                        OleDbCommand komut6;
                        komut6 = new OleDbCommand();
                        komut6.Connection = baglanti;
                        komut6.CommandText = "update Adisyon.Cirolar set Ciro=" + ciro + " where Gün='" + gun + "' AND Ay='"+ay+"' AND Yıl='"+yıl+"'";
                        komut6.ExecuteNonQuery();
                    }
                    sayac = 0;
                    OleDbCommand komut7;
                    komut7 = new OleDbCommand("Select * from Adisyon.Garsonlar", baglanti);
                    OleDbDataReader dr3;
                    dr3 = komut7.ExecuteReader();
                    while (dr3.Read())
                    {
                        string garsonad2= Convert.ToString(dr3["GarsonAd"]);
                        tarih3 = Convert.ToString(dr3["Gün"]);
                        tarih3 += ".";
                        tarih3 += Convert.ToString(dr3["Ay"]) + "." + Convert.ToString(dr3["Yıl"]);
                        if (tarih3 == tarih2&&garsonad==garsonad2)
                        {
                            ciro3 += Convert.ToDouble(dr3["Ciro"]);
                            sayac = 1;
                        }
                    }

                    if (sayac == 0)
                    {
                        OleDbCommand komut8;
                        komut8 = new OleDbCommand();
                        komut8.Connection = baglanti;
                        komut8.CommandText = "Insert into Adisyon.Garsonlar(Gün,Ay,Yıl,GarsonAd,Ciro) values(@Gun,@Ay,@Yıl,@Garson,@Ciro)";
                        komut8.Parameters.Add("@Gun", OleDbType.VarChar).Value = gun;
                        komut8.Parameters.Add("@Ay", OleDbType.VarChar).Value = ay;
                        komut8.Parameters.Add("@Yıl", OleDbType.VarChar).Value = yıl;
                        komut8.Parameters.Add("@Garson", OleDbType.VarChar).Value = garsonad;
                        komut8.Parameters.Add("@Ciro", OleDbType.Numeric).Value = ciro2;
                        komut8.ExecuteNonQuery();
                    }
                    else if (sayac == 1)
                    {
                        ciro3 += ciro2;
                        OleDbCommand komut9;
                        komut9 = new OleDbCommand();
                        komut9.Connection = baglanti;
                        komut9.CommandText = "update Adisyon.Garsonlar set Ciro="+ciro3+" where Gun=@Gun AND Ay=@Ay AND Yıl=@Yıl AND GarsonAd=@Garson";
                        komut9.Parameters.Add("@Gun", OleDbType.VarChar).Value =gun;
                        komut9.Parameters.Add("@Ay", OleDbType.VarChar).Value = ay;
                        komut9.Parameters.Add("@Yıl", OleDbType.VarChar).Value = yıl;
                        komut9.Parameters.Add("@Garson", OleDbType.VarChar).Value = garsonad;
                        komut9.ExecuteNonQuery();
                    }
                    sayac =0;
                    //********************************************************************************
                    OleDbCommand komut2;
                    komut2 = new OleDbCommand();
                    komut2.Connection = baglanti;
                    komut2.CommandText = "update Adisyon.Siparişler set Ödendi='Evet' where Kimlik=" + aranan;
                    komut2.ExecuteNonQuery();
                }


                baglanti.Close();
            }
            MessageBox.Show("Hesap Dökümü Basıldı!");
            sayac = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string iptal = listBox3.SelectedItem.ToString();
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Adisyon.mdb");
            baglanti.Open();
            OleDbCommand komut2;
            komut2 = new OleDbCommand("Delete from Adisyon.Siparişler Where Kimlik=" + iptal + "", baglanti);
            komut2.ExecuteNonQuery();
            MessageBox.Show("Sipariş İptal Edildi!");
            sayac = 0;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox3.SelectedIndex = listBox2.SelectedIndex;
        }

        private void geçmişAylaraAitKârlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Aya_Göre_Ciro frm = new Aya_Göre_Ciro();
            frm.Show();
        }

        private void garsonlarınCirosuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GarsonlarınCirosu grs = new GarsonlarınCirosu();
            grs.Show();
        }

        private void günlereGöreCiroGörüntüleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GunlereGoreCiro grc = new GunlereGoreCiro();
            grc.Show();
        }

        private void menüKategorisiEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuKategoriEkle KT = new MenuKategoriEkle();
            KT.Show();
        }

        private void ürünEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            urunekle ur = new urunekle();
            ur.Show();
        }

        private void menüKategorisiDüzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            katdüzenle kt = new katdüzenle();
            kt.Show();
        }

        private void ürünGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            urguncelle urg = new urguncelle();
            urg.Show();
        }

        private void sistemiSıfırlaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sıfırla sfr = new sıfırla();
            sfr.Show();
        }
    }
}
