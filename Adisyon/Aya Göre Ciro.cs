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
    public partial class Aya_Göre_Ciro : Form
    {
        public Aya_Göre_Ciro()
        {
            InitializeComponent();
        }

        private void Aya_Göre_Ciro_Load(object sender, EventArgs e)
        {
            string tarih = DateTime.Today.ToString();
            
            
            string yıl = "";
            yıl += tarih[6];
            yıl += tarih[7];
            yıl += tarih[8];
            yıl += tarih[9];
            int yılcount = Convert.ToInt32(yıl);
            int yılsayac = Convert.ToInt32(yıl) - 1;
            while(yılsayac<=yılcount)
            {
                int aysayac = 1;
                while (aysayac <= 12)
                {
                    int ay = Convert.ToInt32(aysayac);
                    string ay2 = "";
                    if (ay < 10)
                    {
                         ay2+= "0" + ay.ToString();
                    }
                    else
                    {
                        ay2 += ay.ToString();
                    }
                    OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Adisyon.mdb");
                    baglanti.Open();
                    OleDbCommand komut;
                    komut = new OleDbCommand("Select * from Adisyon.Cirolar where Ay='" +ay2+ "' AND Yıl='"+yılsayac+"'", baglanti);
                    OleDbDataReader dr;
                    dr = komut.ExecuteReader();
                    int sayac = 0;
                    while(dr.Read())
                    {
                        string ay3= Convert.ToString(dr["Ay"]);
                        string tarih3 = "";
                        tarih3 += ay3;
                        tarih3 += "/" + yılsayac;
                        int count = comboBox1.Items.Count;
                        int i =0;
                        while(i<count)
                        {
                            comboBox1.SelectedIndex = i;
                            string tarih4 = comboBox1.SelectedItem.ToString();
                            if(tarih3==tarih4)
                            {
                                sayac = 1;
                            }
                            i++;
                        }
                        if (sayac == 0)
                        {
                            comboBox1.Items.Add(tarih3);
                        }
                        sayac = 0;
                    }
                    baglanti.Close();
                    aysayac++;
                }
                    
                
                yılsayac += 1;
            }
            int count2 = comboBox1.Items.Count;
            if (count2 > 0)
                comboBox1.SelectedIndex = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tarih = comboBox1.SelectedItem.ToString();
            string ay = tarih[0].ToString();
            ay += tarih[1].ToString();
            string yıl = tarih[3].ToString();
            yıl+= tarih[4].ToString();
            yıl += tarih[5].ToString();
            yıl += tarih[6].ToString();
            double ciro = 0;
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Adisyon.mdb");
            baglanti.Open();
            OleDbCommand komut;
            komut = new OleDbCommand("Select * from Adisyon.Cirolar where Ay='" + ay + "' AND Yıl='" + yıl + "'", baglanti);
            OleDbDataReader dr;
            dr = komut.ExecuteReader();
            while(dr.Read())
            {
                ciro+= Convert.ToDouble(dr["Ciro"]);
            }
            label1.Text = tarih+" Cirosu: "+ciro.ToString() + " TL";
        }
    }
}
