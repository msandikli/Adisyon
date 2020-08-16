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
    public partial class katdüzenle : Form
    {
        public katdüzenle()
        {
            InitializeComponent();
        }

        private void katdüzenle_Load(object sender, EventArgs e)
        {
            int sayac = 0;
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Adisyon.mdb");
            baglanti.Open();
            OleDbCommand komut;
            komut = new OleDbCommand("Select * from Adisyon.Menu", baglanti);
            OleDbDataReader dr;
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                int count = comboBox1.Items.Count;
                string kat = Convert.ToString(dr["Tür"]);
                int i = 0;
                while (i < count)
                {
                    comboBox1.SelectedIndex = i;
                    string kat2 = comboBox1.SelectedItem.ToString();
                    if (kat == kat2)
                    {
                        sayac = 1;
                    }
                    i++;
                }
                if (sayac == 0)
                {
                    comboBox1.Items.Add(kat);
                }
            }
            groupBox1.Visible = true;
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = comboBox1.SelectedItem.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int say = 0;
            string kat1 = textBox1.Text;
            kat1 = kat1.ToUpper();
            if(kat1==comboBox1.SelectedItem.ToString()&&kat1!="")
            {
                MessageBox.Show("Değişiklik yapılmadı...");
            }
            else if(kat1!="")
            {
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Adisyon.mdb");
                baglanti.Open();
                OleDbCommand komut;
                komut = new OleDbCommand("Select * from Adisyon.Menu where  Tür='" + comboBox1.SelectedItem.ToString() + "'", baglanti);
                OleDbDataReader dr;
                dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    string kat2= Convert.ToString(dr["Tür"]);
                    if(kat2==comboBox1.SelectedItem.ToString())
                    {
                        say = 1;
                    }
                }
                if(say==1)
                {
                    DialogResult dialog = new DialogResult();
                    dialog = MessageBox.Show("Bu işlem kategoriye ekli ürünlerin kategorilerinin değişmesine yol açacaktır. Kategori Güncellensin Mi?", "Kategori Güncelle", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        OleDbCommand komut2;
                        komut2 = new OleDbCommand();
                        komut2.Connection = baglanti;
                        komut2.CommandText = "update Adisyon.Menu set Tür='" + kat1 + "' where Tür='" + comboBox1.SelectedItem.ToString() + "'";
                        komut2.ExecuteNonQuery();
                        MessageBox.Show("Kategori Başarıyla Güncellendi!");
                        Close();
                    }
                    
                }
            }
        }
    }
}
