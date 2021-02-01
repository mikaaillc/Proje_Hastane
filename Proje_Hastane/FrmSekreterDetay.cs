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
using System.Data.SqlClient;

namespace Proje_Hastane
{
    public partial class FrmSekreterDetay :MetroForm
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
            
          
        }
        public string TCsekreter;
    
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            LblTc.Text = TCsekreter;
       
            //ad Soyad
            SqlCommand komut1 = new SqlCommand("Select sekreteradsoyad From Tbl_sekreter where sekreterTc='" + TCsekreter + "'", bgl.baglanti());

            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                LblAdSoyad.Text = dr1[0].ToString();
            }
            bgl.baglanti().Close();
            ///branslar
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_brans ", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            //doktorlaarı listele
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select (doktorad+' '+doktorsoyad) as 'Doktorlar', doktorbrans From Tbl_doktorlar ", bgl.baglanti());
            da1.Fill(dt1);
            dataGridView2.DataSource = dt1;
            //bransgetir
            SqlCommand komut2 = new SqlCommand("Select Bransad from Tbl_brans", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
    }

      

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutkaydet = new SqlCommand("insert into Tbl_randevular (randevutarih,randevusaat,randevubrans,randevudoktor) values(@r1,@r2,@r3,@r4)",bgl.baglanti());
            if (MskTarih.Text != " .  ." && mskSaat.Text!= "  :"&& CmbBrans.Text!=""&& CmbDoktor.Text!="")
            {
                komutkaydet.Parameters.AddWithValue("@r1", MskTarih.Text);
                komutkaydet.Parameters.AddWithValue("@r2", mskSaat.Text);
                komutkaydet.Parameters.AddWithValue("@r3", CmbBrans.Text);
                komutkaydet.Parameters.AddWithValue("@r4", CmbDoktor.Text);
                komutkaydet.ExecuteNonQuery();
                bgl.baglanti();
                MessageBox.Show("Randevu Oluşturuldu");
            }
            else
            {
                MessageBox.Show("Boş Alanları doldurunuz");
            }
            

        }
        
        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();
            SqlCommand komut = new SqlCommand("Select doktorad,doktorsoyad From Tbl_doktorlar where doktorbrans=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", CmbBrans.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbDoktor.Items.Add(dr[0]+" "+dr[1]);
            }
            bgl.baglanti().Close();

        }

        private void BtnDuyuruolustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_duyurular(duyuru) values (@p1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", RchDuyuru.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru OLuşturuldu");
        }

        private void BtnDoktorPanel_Click(object sender, EventArgs e)
        {
            DoktorPaneli drp = new DoktorPaneli();
            drp.Show();
        }

        private void BtnBransPanel_Click(object sender, EventArgs e)
        {
            FrmBrans drp = new FrmBrans();
            drp.Show();
        }

        private void BtnRandevuliste_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi drp = new FrmRandevuListesi();
            drp.Show();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            FrmDuyurular drp = new FrmDuyurular();
            drp.Show();
        }
    }
}
