<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="DanhMuc.aspx.cs" Inherits="WebThuongMaiDienTu.Admin.DanhMuc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <script>
        /*For dissappearing alert message*/
        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMsg.ClientID%>").style.display = "none";
            }, seconds * 1000);
        }
    </script>
    <script>
        function XemTruocHinhAnh(input)
        {
            if (input.files && input.files[0])
            {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=XemTruocHinhAnh.ClientID%>').prop('src', e.target.result)
                        .width(200)
                        .height(200);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mb-4">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </div>

    <div class="row">
        <div class="col-sm-12 col-md-4">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Danh mục</h4>
                    <hr />

                    <div class="form-body"></div>

                        <label>Tên Danh mục</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:TextBox ID="txtTenDanhMuc" runat="server" CssClass="form-control" placeholder="Nhập tên Danh mục"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvTenDanhMuc" runat="server" ForeColor="Red" Font-Size="Small"
                                    Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtTenDanhMuc"
                                    ErrorMessage="Bắt buộc nhập tên Danh mục"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                    <label>Ảnh Danh mục</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                   <asp:FileUpload ID="AnhDanhMuc" runat="server" CssClass="form-control"
                                       onchange ="XemTruocHinhAnh(this);"/>
                                    <asp:HiddenField ID="hfMaDanhMuc" runat="server" Value="0"/>
                                </div>
                            </div>
                        </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:CheckBox ID="cbIsActive" runat="server" Text="&nbsp;Còn hàng" />
                            </div>
                        </div>
                    </div>

                </div>

                <div class="form-actiona pb-5">
                    <div class="text-left">
                        <asp:Button ID="btnThemHoacSua" runat="server" CssClass="btn btn-info" Text="Add" OnClick="btnThemHoacSua_Click" />
                        <asp:Button ID="btnXoa" runat="server" CssClass="btn btn-dark" Text="Reset" OnClick="btnXoa_Click" />
                    </div>
                </div>

                <div>
                    <asp:Image ID="XemTruocHinhAnh" runat="server" CssClass="img-thumbnail" AlternateText="" />
                </div>

            </div>
        </div>

        <div class="col-sm-12 col-md-8">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Danh mục</h4>
                    <hr />
                    <div class="table-responsive">
                        <asp:Repeater ID="rDanhMuc" runat="server" OnItemCommand="rDanhMuc_ItemCommand">
                            <HeaderTemplate>
                                <table class="table data-table-export table-hover nowrap">
                                    <thead>
                                        <tr>
                                            <th class="table-plus">Tên</th>
                                            <th>Hình ảnh</th>
                                            <th>Còn hàng</th>
                                            <th>Ngày tạo</th>
                                            <th class="dataTable-nosort">Hoạt động</th>
                                        </tr>
                                    </thead>
                                    <tbody>                       
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="table-plus"><%# Eval("TenDanhMuc") %></td>
                                    <td>
                                        <img width="40" src="<%# WebThuongMaiDienTu.TienIch.getImageUrl(Eval("HinhAnhUrl")) %>" alt="image"/>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblHoatDong" runat="server"
                                            Text='<%# (bool)Eval("HoatDong") == true ? "Hoạt động" : "Hết hàng" %>'
                                            CssClass='<%# (bool)Eval("HoatDong") == true ?" badge badge-success"
                                                : "badge bade-danger"   %>'>

                                        </asp:Label>
                                    </td>
                                    <td><%#Eval("NgayTao")%></td>
                                    <td>
                                        <asp:LinkButton ID="lblChinhSua" Text="Edit" runat="server" CssClass="badge badge-primary"
                                        CommandArgument='<%# Eval("MaDanhMuc") %>' CommandName="edit" CausesValidation="false" >
                                        <i class="fas fa-edit"></i>
                                         </asp:LinkButton>
                                        <asp:LinkButton ID="lblXoa" Text="Delete" runat="server" CssClass="badge badge-danger"
                                            CommandArgument='<%# Eval("MaDanhMuc") %>' CommandName="delete" CausesValidation="false">
                                            <i class="fas fa-trash-alt"></i>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div> 

    </div>

</asp:Content>
