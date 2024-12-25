using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.IO;

namespace WebThuongMaiDienTu.Admin
{
    public partial class DanhMuc : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["breadCumbTittle"] = "Quản lý Danh Mục";
            Session["breadCumbPage"] = "DanhMuc";
            lblMsg.Visible = false;
            getDanhmuc();
        }

        void getDanhmuc()
        {
            con = new SqlConnection(TienIch.getConnection());
            cmd = new SqlCommand("DanhMuc_Crud", con);
            cmd.Parameters.AddWithValue("@Action", "GETALL");
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rDanhMuc.DataSource = dt;
            rDanhMuc.DataBind();
        }

        protected void btnThemHoacSua_Click(object sender, EventArgs e)
        {
            string actionName = string.Empty, imagePath =  string.Empty, fileExtension = string.Empty;
            bool isValidToExecute = false;
            int MaDanhMuc = Convert.ToInt32(hfMaDanhMuc.Value);
            con = new SqlConnection(TienIch.getConnection());
            cmd = new SqlCommand("DanhMuc_Crud", con);
            cmd.Parameters.AddWithValue("@Action", MaDanhMuc == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@MaDanhMuc", MaDanhMuc);
            cmd.Parameters.AddWithValue("@TenDanhMuc", txtTenDanhMuc.Text.Trim());
            cmd.Parameters.AddWithValue("@HoatDong", cbIsActive.Checked);
            if (AnhDanhMuc.HasFile)
            {
                if (TienIch.isValidExtension(AnhDanhMuc.FileName))
                {
                    string newImageName = TienIch.getUniqueID();
                    fileExtension = Path.GetExtension(AnhDanhMuc.FileName);
                    imagePath = "Images/DanhMuc/" + newImageName.ToString() + fileExtension;
                    AnhDanhMuc.PostedFile.SaveAs(Server.MapPath("~/Images/DanhMuc/") + newImageName.ToString() + fileExtension);
                    cmd.Parameters.AddWithValue("@HinhAnhUrl", imagePath);
                    isValidToExecute = true;
                }
                else
                {
                    lblMsg.Visible = false;
                    lblMsg.Text = "Làm ơn chọn đuôi .jpg , .png ";
                    lblMsg.CssClass = "alert alert-danger";
                    isValidToExecute =  false;
                }
            }
            else 
            {
                isValidToExecute = true;
            }
            if (isValidToExecute)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    actionName = MaDanhMuc == 0 ? "inserted" : "updated";
                    lblMsg.Visible = true;
                    lblMsg.Text = "DanhMuc" + actionName + "succsessfully!";
                    lblMsg.CssClass = "alert alert-successs";
                    getDanhmuc();
                    clear();
                }
                catch(Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Error-" + ex.Message;
                    lblMsg.CssClass = "alert alert-danger";
                }
                finally
                {
                    con.Close();
                }
            }
        }

        protected void btnXoa_Click(object sender, EventArgs e)
        {
            clear();
        }

        void clear() 
        {
            txtTenDanhMuc.Text = string.Empty;
            cbIsActive.Checked = false;
            hfMaDanhMuc.Value = "0";
            btnThemHoacSua.Text = "Add";
            XemTruocHinhAnh.ImageUrl = string.Empty;
        }

        protected void rDanhMuc_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblMsg.Visible=false;
            if(e.CommandName=="edit")
            {
                con = new SqlConnection(TienIch.getConnection());
                cmd = new SqlCommand("DanhMuc_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "GETBYID");
                cmd.Parameters.AddWithValue("@MaDanhMuc", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                txtTenDanhMuc.Text = dt.Rows[0]["TenDanhMuc"].ToString();
                cbIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["HoatDong"]);
                XemTruocHinhAnh.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["HinhAnhUrl"].ToString()) ? "../Images/No_Image.png" : "../" + dt.Rows[0]["HinhAnhUrl"].ToString();
                XemTruocHinhAnh.Height = 200;
                XemTruocHinhAnh.Width = 200;
                hfMaDanhMuc.Value = dt.Rows[0]["MaDanhMuc"].ToString();
                btnThemHoacSua.Text = "Update";
            }
            else if (e.CommandName == "delete")
            {
                con = new SqlConnection(TienIch.getConnection());
                cmd = new SqlCommand("DanhMuc_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@MaDanhMuc", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMsg.Visible = true;
                    lblMsg.Text = "DanhMuc deleted succsessfully!";
                    lblMsg.CssClass = "alert alert-successs";
                    getDanhmuc();
                }
                catch (Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Error-" + ex.Message;
                    lblMsg.CssClass = "alert alert-danger";
                }
                finally
                {
                    con.Close();
                }
            }    
        }
    }
}