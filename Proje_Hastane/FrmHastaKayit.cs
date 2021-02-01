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
    public partial class FrmHastaKayit : MetroForm
    {
        public FrmHastaKayit()
        {
            InitializeComponent();
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;


            // Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Green500, Primary.Lime700,
                Primary.Red500, Accent.LightBlue700,
                TextShade.WHITE);
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void BtnHastaKayıt_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Hastalar (hastaAd,hastasoyad,hastatc,hastatelefon,hastasifre,hastacinsiyet) values(@p1,@p2,@p3,@p4,@p5,@p6)",bgl.baglanti());
            
            if (TxtAd.Text!="" && TxtSoyad.Text != "" && MskTC.Text != ""&& MskTel.Text != "(   )    -" && TxtSifre.Text != "" && CmbCinsiyet.Text != "")
            {
                komut.Parameters.AddWithValue("@p1",TxtAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                komut.Parameters.AddWithValue("@p3", MskTC.Text);
                komut.Parameters.AddWithValue("@p4", MskTel.Text);
                komut.Parameters.AddWithValue("@p5", TxtSifre.Text);
                komut.Parameters.AddWithValue("@p6", CmbCinsiyet.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                DialogResult result = MessageBox.Show("Kayıt Oluşturuldu Şifreniz= " + TxtSifre.Text, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    this.Close();
                }
            }
            else
            {
               DialogResult re = MessageBox.Show("Boş Alanları Doldurunuz!" , "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
            }

        }
    }
}
