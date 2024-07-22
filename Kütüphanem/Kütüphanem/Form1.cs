using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace Kütüphanem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=AHMET\SQLEXPRESS;Initial Catalog=Kütüphanem;Integrated Security=True");
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
               
                SqlCommand komut = new SqlCommand("insert into Kitaplar(KitapAdı,YazarAdı,Durum) values (@p1,@p2,@p3)", baglanti);
                komut.Parameters.Add("@p1", textKitap.Text);
                komut.Parameters.Add("@p2", textYazar.Text);
                if (radioOkundu.Checked)
                {
                    komut.Parameters.Add("@p3", radioOkundu.Text);
                }
                if (radioOkunmadı.Checked)
                {
                    komut.Parameters.Add("@p3", radioOkunmadı.Text);
                }
                komut.ExecuteNonQuery();
               
                MessageBox.Show("Kitap Listenize Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("HATA" + ex.Message);
               
            }
            listele();
            textKitap.Text = "";
            textYazar.Text = "";
            textKitap.Focus();
            baglanti.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();

        }

        void listele()
        {
           
            SqlDataAdapter da = new SqlDataAdapter("Select * from Kitaplar ", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
           
        }
    }
}
