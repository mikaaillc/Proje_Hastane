using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using MaterialSkin.Controls;
using MaterialSkin;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    public partial class FrmBilgiDüzenle : MetroForm
    {
        public FrmBilgiDüzenle()
        {
            InitializeComponent();
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;


            // Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Red500, Primary.Lime700,
                Primary.Red500, Accent.LightBlue700,
                TextShade.WHITE);
                
            

        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        public string Tcno;
        private void FrmBilgiDüzenle_Load(object sender, EventArgs e)
        {
            MskTC.Text = Tcno;
            SqlCommand komut = new SqlCommand("Select * from tbl_hastalar where Hastatc='" + MskTC.Text + "'", bgl.baglanti());
            SqlDataReader dr3 = komut.ExecuteReader();
            while (dr3.Read())
            {
                TxtAd.Text = dr3[1].ToString();
                TxtSoyad.Text = dr3[2].ToString();
                MskTel.Text = dr3[4].ToString();
                TxtSifre.Text = dr3[5].ToString();
                CmbCinsiyet.Text = dr3[6].ToString();

            }
            bgl.baglanti().Close();
        }
        public string gunceltc;
        private void BtnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutguncelle = new SqlCommand("Update Tbl_Hastalar Set Hastaad=@a1,hastasoyad=@a2,hastatelefon=@a3,hastasifre=@a4,hastacinsiyet=@a5 where Hastatc='" + Tcno + "'", bgl.baglanti());
            komutguncelle.Parameters.AddWithValue("@a1", TxtAd.Text);
            komutguncelle.Parameters.AddWithValue("@a2", TxtSoyad.Text);
            komutguncelle.Parameters.AddWithValue("@a3", MskTel.Text);
            komutguncelle.Parameters.AddWithValue("@a4", TxtSifre.Text);
            komutguncelle.Parameters.AddWithValue("@a5", CmbCinsiyet.Text);
            komutguncelle.ExecuteNonQuery();
            
            DialogResult result = MessageBox.Show("Bilgiler Güncellendi ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
             
                Tcno = MskTC.Text;
                SqlCommand komut = new SqlCommand("Select * from tbl_hastalar where Hastatc='" + MskTC.Text + "'", bgl.baglanti());
                SqlDataReader dr4 = komut.ExecuteReader();
                while (dr4.Read())
                {
                    TxtAd.Text = dr4[1].ToString();
                    TxtSoyad.Text = dr4[2].ToString();
                    MskTel.Text = dr4[4].ToString();
                    TxtSifre.Text = dr4[5].ToString();
                    CmbCinsiyet.Text = dr4[6].ToString();
                   
                }
                //FrmHastaDetay fr1 = new FrmHastaDetay();
                //fr1.Show();
                bgl.baglanti().Close();

            }

        }
   
    }
}
