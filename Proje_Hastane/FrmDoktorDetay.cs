using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Data.SqlClient;


namespace Proje_Hastane
{
    public partial class FrmDoktorDetay : MaterialForm
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;

            // Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Red500, Primary.Red700,
                Primary.Red500, Accent.LightBlue700,
                TextShade.WHITE
            );
        }
        public string tcdoktor;


        SqlBaglantisi bgl = new SqlBaglantisi();

        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            LblDoktorTc.Text = tcdoktor;
            SqlCommand komut = new SqlCommand("Select Doktorad ,doktorsoyad From Tbl_doktorlar where doktorTc=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", LblDoktorTc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdsoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();
            //randevular
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_randevular where randevudoktor='"+LblAdsoyad.Text+"'",bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular drp = new FrmDuyurular();
            drp.Show();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            FrmDoktorBilgiDüzenle drp = new FrmDoktorBilgiDüzenle();
            drp.TCNO = LblDoktorTc.Text;
            drp.Show();
        }

        private void BtnÇıkış_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            rchsikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
        }
    }
}
