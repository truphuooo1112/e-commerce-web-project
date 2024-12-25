using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;

namespace WebThuongMaiDienTu
{

    public class TienIch
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        SqlDataReader sdr;
        DataTable dt;

        public static string getConnection()
        {
            return ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        }

        public static bool isValidExtension(string TenFile) 
        {
            bool isValid = false;
            string[] fileExension = { ".jpg", ".png", "jpeg" };
            foreach (string file in fileExension)
            {
                if (TenFile.Contains(file))
                {
                    isValid = true;
                    break;
                }    
            }
            return isValid;
        }

        public static string getUniqueID()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }

        public static string getImageUrl(Object url)
        {
            string url1 = string.Empty;
            if (string.IsNullOrEmpty(url.ToString()) || url == DBNull.Value)
            {
                url1 = "../Images/No_image.png";
            }
            else
            {
                url1 = string.Format("../{0}", url);
            }
            return url1;
        }
    }
}