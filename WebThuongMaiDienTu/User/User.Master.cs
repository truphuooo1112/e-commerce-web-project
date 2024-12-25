using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebThuongMaiDienTu.User
{
    public partial class User : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Url.AbsoluteUri.ToString().Contains("TrangChu.aspx"))
            {
                //load the control
                Control UserControl1 = (Control)Page.LoadControl("UserControl1.ascx");
                pnlSliderUC.Controls.Add(UserControl1);
            }
        }
    }
}