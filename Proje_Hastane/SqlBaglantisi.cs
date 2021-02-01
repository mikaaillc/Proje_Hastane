using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    class SqlBaglantisi//sınıfımız
    {
        public SqlConnection baglanti()//metotumuz
        {
            SqlConnection baglan = new SqlConnection("Data Source=MIKAILPC\\SQLEXPRESS;Initial Catalog=HastaneProje;Integrated Security=True");
            baglan.Open();//baglan nesnenin adı
            return baglan;
        }

    }
}
