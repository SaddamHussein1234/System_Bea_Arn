<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelManageSite/MPCPanel.master" AutoEventWireup="true" CodeFile="PageAlbumImgAdd.aspx.cs" Inherits="Cpanel_CPanelManageSite_PageAlbumImgAdd" %>
<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../GridView.css?v=2.2" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnAdd.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>
    <script type="text/javascript">
<!--
    function Check_Click(objRef) {
        var row = objRef.parentNode.parentNode;
        var GridView = row.parentNode;
        var inputList = GridView.getElementsByTagName("input");
        for (var i = 0; i < inputList.length; i++) {
            var headerCheckBox = inputList[0];
            var checked = true;
            if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                if (!inputList[i].checked) {
                    checked = false;
                    break;
                }
            }
        }
        headerCheckBox.checked = checked;
    }
    function checkAll(objRef) {
        var GridView = objRef.parentNode.parentNode.parentNode;
        var inputList = GridView.getElementsByTagName("input");
        for (var i = 0; i < inputList.length; i++) {
            var row = inputList[i].parentNode.parentNode;
            if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                if (objRef.checked) {
                    inputList[i].checked = true;
                }
                else {
                    inputList[i].checked = false;
                }
            }
        }
    }
    //-->
    </script>

    <script type="text/javascript">
        function ConfirmDelete() {
            var count = document.getElementById("<%=hfCount.ClientID %>").value;
            var gv = document.getElementById("<%=GVImgAlbum.ClientID%>");
            var chk = gv.getElementsByTagName("input");
            for (var i = 0; i < chk.length; i++) {
                if (chk[i].checked && chk[i].id.indexOf("chkAll") == -1) {
                    count++;
                }
            }
            if (count == 0) {
                alert("لم تقم بالتحديد على أي سجل");
                return false;
            }
            else {
                return confirm(" هل أنت متأكد من الحذف ؟");
            }
        }
    </script>
    <style>
        @media screen and (min-width: 768px) {
            .WidthText1 {
                float: right;
                Width: 20%;
                padding-left: 10px;
            }

            .WidthText {
                float: right;
                Width: 33%;
                padding-left: 10px;
            }

            .WidthText2 {
                float: right;
                Width: 50%;
            }

            .WidthText3 {
                float: right;
                Width: 50%;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthText {
                Width: 95%;
            }

            .WidthText1 {
                Width: 95%;
            }

            .WidthText2 {
                Width: 95%;
            }

            .WidthText3 {
                Width: 95%;
            }
        }
    </style>
    <link href="../css/chosen.css" rel="stylesheet" />
    <link href="../test/LoginAr.css" rel="stylesheet" />
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث" OnClick="btnRefrish_Click">
                    <li class="fa fa-refresh"></li></asp:LinkButton>
                    <asp:Button ID="btnDelete" runat="server" Text="حذف السجلات المحددة" title="حذف السجلات المحددة" data-toggle="tooltip" CssClass="btn btn-danger" OnClientClick="return ConfirmDelete();" OnClick="btnDelete_Click" />
                </div>
                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="Default.aspx">الرئيسية</a></li>
                        <li><a href="PageMenu.aspx">إضافة البوم الصور </a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-pencil"></i>
                            <asp:Label ID="Label1" runat="server" Text="بيانات الالبوم : "></asp:Label>
                        </h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-sm-12">
                            <div id="IDMessageWarning" runat="server" visible="false" class="alert  alert-warning alert-dismissible" role="alert">
                                <span class="badge badge-pill badge-warning">تحذير</span>
                                <asp:Label ID="lblWarning" runat="server"></asp:Label>
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div id="IDMessageSuccess" runat="server" visible="false" class="alert  alert-success alert-dismissible" role="alert">
                                <span class="badge badge-pill badge-success">عملية ناجحة</span>
                                <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                        </div>
                        <div class="container-fluid">
                            <div class="WidthText2">
                                <h5>عنوان الالبوم : </h5>
                                <asp:Label ID="lblTitleAR" runat="server" Font-Size="Small"></asp:Label>
                                <asp:Panel ID="pnlOtherLangueg" runat="server" Visible="false">
                                    <span class="fa fa-minus"></span>
                                    <asp:Label ID="lblTitleTR" runat="server" Font-Size="Small"></asp:Label>
                                    <span class="fa fa-minus"></span>
                                    <asp:Label ID="lblTitleEn" runat="server" Font-Size="Small"></asp:Label>
                                </asp:Panel>
                            </div>
                            <div class="WidthText2">
                                <h5>حالة ظهور الإلبوم : </h5>
                                <asp:Label ID="lblAr" runat="server"></asp:Label>
                                <asp:Panel ID="pnlOtherLangueg2" runat="server" Visible="false">
                                    <asp:CheckBox ID="CBViewAR" runat="server" Font-Size="14px" CssClass="checkbox-inline"
                                        ValidationGroup="g2" Enabled="false" Text=" عربي " />

                                    <asp:CheckBox ID="CBViewTR" runat="server" Font-Size="14px" CssClass="checkbox-inline"
                                        ValidationGroup="g2" Enabled="false" Text=" تركي " />
                                    <asp:CheckBox ID="CBViewEN" runat="server" Font-Size="14px" CssClass="checkbox-inline"
                                        ValidationGroup="g2" Enabled="false" Text=" إنجليزي " />
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-pencil"></i>
                            <asp:Label ID="lbmsg" runat="server" Text="إضافة البوم الصور : "></asp:Label>
                        </h3>
                    </div>
                    <br />
                        <div class="container-fluid">
                            <div class="WidthText">
                                <h5>حدد الصورة : </h5>
                                <span>الملفات المسموح بها "bmp", "gif", "png", "jpg", "jpeg"</span>
                                    <asp:FileUpload ID="FUImgTeacher" runat="server" ToolTip="العرض 960px * الطول 576px" data-toggle="tooltip" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="مطلوب" CssClass="font"
                                        ControlToValidate="FUImgTeacher" ValidationGroup="g2" Font-Size="10px" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="WidthText1">
                                <h5>حالة ظهور الصورة : </h5>
                                <div class="keepmeLogged">
                                    <label class="switch">
                                        <input name="RememberMe" type="checkbox" id="CBActive" runat="server" checked="checked" />
                                        <span class="slider round"></span>
                                    </label>
                                </div>
                            </div>
                            <div class="WidthText1">
                                <h5>ترتيب : </h5>
                                <asp:TextBox ID="txtOrder" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="مطلوب" CssClass="font"
                                    ControlToValidate="txtOrder" ValidationGroup="g2" Font-Size="10px" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtOrder"
                                    ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2" Display="Dynamic">
                                </asp:RegularExpressionValidator>
                            </div>
                            <div class="WidthText1">
                                <div style="float: left">
                                    <asp:LinkButton ID="LBBack" runat="server" class="btn btn-danger btn-fill pull-left" Visible="false">رجوع</asp:LinkButton>
                                </div>
                                <div style="width: 50%">
                                    <br />
                                    <asp:Button ID="btnAdd" runat="server" Text="رفع الصورة" class="btn btn-info btn-fill pull-left" ValidationGroup="g2" OnClick="btnAdd_Click" />
                                </div>
                            </div>
                        </div>
                    <hr />
                    <div class="panel-body">
                        <asp:Panel ID="pnlData" runat="server" Visible="False">
                            <asp:Panel ID="pnl2" runat="server" Direction="RightToLeft">
                                <div class="table table-responsive">
                                    <asp:GridView ID="GVImgAlbum" runat="server" AutoGenerateColumns="False" DataKeyNames="IDItem"
                                        Width="100%" CssClass="footable1"
                                        EnableTheming="True" GridLines="Horizontal" UseAccessibleHeader="False">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="10px">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ظهور الصورة" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <%# ClassSaddam.FChangeStyleCheckbox2((Boolean) Eval("IsView")) %>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="IDOrder" HeaderText="ترتيب" SortExpression="IDOrder" HeaderStyle-ForeColor="#CCCCCC" />
                                            <asp:TemplateField HeaderText="الصورة" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <a href="<%# "/" + Eval("PathImg") %>" class="lightbox_trigger" target="_blank" title="تكبير الصورة" data-toggle="tooltip">
                                                        <img src="<%# "/" + Eval("PathImg") %>" width="150" style="border-radius: 8px" />
                                                    </a>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="تاريخ الإضافة" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <span class="fa fa-calendar"></span>
                                                    <%# ClassDataAccess.FChangeF((DateTime) Eval("DataAddImg")) %>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="نشر من قبل" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <%# ClassSetting.FGetNameAdmin((Int32) Eval("IDAdmin")) %>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                        <HeaderStyle CssClass="Colorloading" Font-Bold="True" ForeColor="White" />
                                        <PagerSettings Mode="NextPrevious" Position="TopAndBottom" NextPageText=" -- التالي " PreviousPageText=" السابق - " />
                                        <PagerStyle CssClass="pagination-ys" BackColor="White" ForeColor="Red" HorizontalAlign="Right" />
                                        <RowStyle CssClass="rows"></RowStyle>
                                        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                        <SortedDescendingHeaderStyle BackColor="#242121" />
                                    </asp:GridView>
                                </div>
                                <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                                <span style="font-size: 12px; padding-right: 10px">عدد الصور : </span>
                                <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                <span class="fa fa-picture-o"></span>
                            </asp:Panel>
                        </asp:Panel>
                        <asp:Panel ID="pnlNull" runat="server" Visible="False">
                            <br />
                            <br />
                            <br />
                            <div align="center">
                                <h3 style="font-size: 20px">لا توجد صور للالبوم
                                </h3>
                            </div>
                            <br />
                            <br />
                        </asp:Panel>
                        <asp:LinkButton ID="LBEnd" runat="server" class="btn btn-danger btn-fill pull-left" OnClick="LBEnd_Click">إنهاء</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
</asp:Content>

