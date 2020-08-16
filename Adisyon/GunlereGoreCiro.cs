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
    public partial class GunlereGoreCiro : Form
    {
        public GunlereGoreCiro()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tarih = dateTimePicker1.Text;
            string gun = "";
            string ay = "";
            int say = 0;
            if(tarih[1].ToString()==" ")
            {
                gun += "0";
                gun += tarih[0].ToString();
                say = 2;
            }
            else
            {
                gun += tarih[0].ToString() + tarih[1].ToString();
                say = 3;
            }
            for(int i=say;i<50;i++)
            {
                ay += tarih[i].ToString();
                if(ay=="Ocak")
                {
                    say = i;
                    break;
                }
                else if (ay == "Şubat")
                {
                    say = i;
                    break;
                }
                else if (ay == "Mart")
                {
                    say = i;
                    break;
                }
                else if (ay == "Nisan")
                {
                    say = i;
                    break;
                }
                else if (ay == "Mayıs")
                {
                    say = i;
                    break;
                }
                else if (ay == "Haziran")
                {
                    say = i;
                    break;
                }
                else if (ay == "Temmuz")
                {
                    say = i;
                    break;
                }
                else if (ay == "Ağustos")
                {
                    say = i;
                    break;
                }
                else if (ay == "Eylül")
                {
                    say = i;
                    break;
                }
                else if (ay == "Ekim")
                {
                    say = i;
                    break;
                }
                else if (ay == "Kasım")
                {
                    say = i;
                    break;
                }
                else if (ay == "Aralık")
                {
                    say = i;
                    break;
                }
            }
            say += 2;
            string yıl = tarih[say].ToString();
            say += 1;
            yıl+= tarih[say].ToString();
            say += 1;
            yıl += tarih[say].ToString();
            say += 1;
            yıl += tarih[say].ToString();
            string ay2 = "";
            if (ay == "Ocak")
            {
                ay2 = "01";
            }
            else if (ay == "Şubat")
            {
                ay2 = "02";
            }
            else if (ay == "Mart")
            {
                ay2 = "03";
            }
            else if (ay == "Nisan")
            {
                ay2 = "04";
            }
            else if (ay == "Mayıs")
            {
                ay2 = "05";
            }
            else if (ay == "Haziran")
            {
                ay2 = "06";
            }
            else if (ay == "Temmuz")
            {
                ay2 = "07";
            }
            else if (ay == "Ağustos")
            {
                ay2 = "08";
            }
            else if (ay == "Eylül")
            {
                ay2 = "09";
            }
            else if (ay == "Ekim")
            {
                ay2 = "10";
            }
            else if (ay == "Kasım")
            {
                ay2 = "11";
            }
            else if (ay == "Aralık")
            {
                ay2 = "12";
            }

            double ciro = 0;
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Adisyon.mdb");
            baglanti.Open();
            OleDbCommand komut;
            komut = new OleDbCommand("Select * from Adisyon.Cirolar where Ay='" + ay2 + "' AND Yıl='" + yıl + "' AND Gün='"+gun+"'", baglanti);
            OleDbDataReader dr;
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                string cr = Convert.ToString(dr["Ciro"]);
                if (cr != "")
                {
                    ciro += Convert.ToDouble(cr);
                }
            }
            baglanti.Close();
            if (ciro == 0)
            {
                label1.Text = tarih + " tarihinde hiç ciro yapmadınız!";
            }
            else
            {
                label1.Text = tarih + " tarihinde yapılan ciro=" + ciro + " TL";
            }
        }
    }
}
