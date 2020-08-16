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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Adisyon.mdb");
            baglanti.Open();
            OleDbCommand komut;
            komut= new OleDbCommand("Select * from Adisyon.User where Parola=@Parola", baglanti);
            komut.Parameters.AddWithValue("@Parola", textBox1.Text);
            OleDbDataReader dr;
            dr = komut.ExecuteReader();
            string yetki = "";
            string ad = "";
            string soyad = "";
            string parola = "";
            while(dr.Read())
            {
                yetki = Convert.ToString(dr["Yetki"]);
                parola = Convert.ToString(dr["Parola"]);
                ad = Convert.ToString(dr["Ad"]);
                soyad = Convert.ToString(dr["Soyad"]);
            }
            if (yetki == "Admin")
            {
                string masano = "";
                string tur = "";
                OleDbCommand komut2;
                komut2 = new OleDbCommand("Select * from Adisyon.MasaNo", baglanti);
                OleDbDataReader dr2;
                dr2 = komut2.ExecuteReader();
                while(dr2.Read())
                {
                    masano= Convert.ToString(dr2["Masa"]);
                }
                OleDbCommand komut3;
                komut3 = new OleDbCommand("Select * from Adisyon.Menu", baglanti);
                OleDbDataReader dr3;
                dr3 = komut3.ExecuteReader();
                while (dr3.Read())
                {
                    tur = Convert.ToString(dr3["Tür"]);
                }
                baglanti.Close();
                if (masano==""&&tur=="")
                {
                    Kurulum krl = new Kurulum();
                    krl.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Hoşgeldin Patron");
                    Admin frm = new Admin();
                    frm.label2.Text = soyad;
                    frm.Show();
                    this.Hide();
                }
            }
            if (yetki == "Garson")
            {
                MessageBox.Show("Hoşgeldin "+ad+" :)\n\nBugün Nasılsın?");
                Garson frm2 = new Garson();
                frm2.label3.Text = soyad;
                frm2.label2.Text = ad;
                frm2.Show();
                this.Hide();
            }
            else if(yetki=="")
            {
                MessageBox.Show("Sistemde Bu Parola İle Kayıtlı Bir Kullanıcı Bulunmamaktadır.");
                textBox1.Clear();
                textBox1.Focus();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Maximized;
            textBox1.Focus();
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
