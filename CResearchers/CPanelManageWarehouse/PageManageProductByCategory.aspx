<%@ Page Title="" Language="C#" MasterPageFile="~/CResearchers/CPanelManageWarehouse/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageProductByCategory.aspx.cs" Inherits="CResearchers_CPanelManageWarehouse_PageManageProductByCategory" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>

<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../GridView.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnSearch.ClientID %>").disabled = true;
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
    </script>

    <script type="text/javascript">
        function ConfirmDelete() {
            var count = document.getElementById("<%=hfCount.ClientID %>").value;
            var gv = document.getElementById("<%=GVProductByCategory.ClientID%>");
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
        .bl {
            color: #fff;
        }

        .fo {
            font-size: 12px;
        }

        @media screen and (min-width: 768px) {
            .WidthText2 {
                Width: 250px;
                height: 36px;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthText2 {
                Width: 150px;
                height: 36px;
            }
        }

        @media screen and (min-width: 768px) {
            .WidthTex {
                float: right;
                Width: 13%;
                padding-right: 5px;
            }

            .WidthText {
                float: right;
                Width: 13%;
                padding-right: 5px;
            }

            .WidthText3 {
                float: right;
                Width: 19%;
                padding-right: 5px;
            }

            .WidthText1 {
                float: right;
                Width: 31%;
                padding-right: 5px;
            }

            .WidthText4 {
                float: right;
                Width: 50%;
            }

            .WidthText5 {
                float: right;
                Width: 100%;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthTex {
                Width: 95%;
            }

            .WidthText {
                Width: 95%;
            }

            .WidthText1 {
                Width: 95%;
            }

            .WidthText3 {
                Width: 95%;
            }

            .WidthText4 {
                Width: 95%;
            }

            .WidthText5 {
                Width: 95%;
            }
        }
    </style>

    <link href="../css/chosen.css" rel="stylesheet" />
    <script src="../../view/javascript/jquery.min.js"></script>
    <script src="../../view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <a href="PageManageCategory.aspx" data-toggle="tooltip" title="إضافة صنف جديد" class="btn btn-primary" runat="server" id="IDAdd"><i class="fa fa-plus"></i></a>
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث" OnClick="btnRefrish_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
                    <asp:LinkButton ID="btnDelete1" runat="server" class="btn btn-danger" OnClick="btnDelete1_Click"
                        OnClientClick="return ConfirmDelete();" title="حذف" data-toggle="tooltip"><span class="tip-bottom">
                    <i class="fa fa-trash-o"></i></span></asp:LinkButton>
                    <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                        title="طباعة" OnClientClick="return insertConfirmation();">
                    <i class="fa fa-print"></i></asp:LinkButton>
                </div>
                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="Default.aspx">الرئيسية</a></li>
                        <li><a href="PageManageProductByCategory.aspx">قائمة المنتجات حسب الصنف</a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>قائمة المنتجات حسب الصنف
                        </h3>
                        <div style="float: left">
                            <asp:Button ID="btnSearch" runat="server" Text="بحث" Style="margin-right: 4px;"
                                class="btn btn-info btn-fill pull-right" OnClick="btnSearch_Click" />
                            &nbsp;
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthText2" placeholder=" إبحث هنا ... "></asp:TextBox>
                        </div>
                        <div style="float: left">
                            <span>حدد الصنف : </span>
                            <asp:DropDownList ID="DLCategory" runat="server" ValidationGroup="g2" Width="150px" CssClass="form-control2 WidthText2" Style="font-size: 12px;">
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lblCategory" runat="server" ForeColor="Red" Font-Size="10px" Visible="false" Text="* حدد الصنف"></asp:Label>
                        </div>
                    </div>
                    <div class="panel-body">
                        <asp:Panel ID="pnlData" runat="server" Visible="False" Direction="RightToLeft">
                            <div class="table table-responsive">
                                <table class='table' style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>
                                                <div class="HideNow">
                                                    <uc1:WUCHeader runat="server" ID="WUCHeader" />
                                                </div>
                                                <div align="center" class="w">
                                                    <div>
                                                        <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GVProductByCategory" runat="server" AutoGenerateColumns="False" DataKeyNames="ProductID"
                                                    Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal" OnRowDataBound="GVProductByCategory_RowDataBound"
                                                    UseAccessibleHeader="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-Width="10px">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ProductID" HeaderText="ProductID" InsertVisible="False" ReadOnly="True"
                                                            SortExpression="ProductID" Visible="false" />
                                                        <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="رقم المنتج" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# Eval("IDNumberProduct")%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ينتمي إلى" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# Eval("AffiliationName")%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="صنف" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# Eval("CategoryName")%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="عنوان المنتج" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# Eval("ProductName")%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:CheckBoxField DataField="IsActive" HeaderText="حالة التفعيل" SortExpression="IsActive"
                                                            HeaderStyle-ForeColor="#CCCCCC" />
                                                        <asp:TemplateField HeaderText="المنتجات" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <span class="label label-success" style="font-size: 12px">
                                                                    <a href='PageManageProductByDetails.aspx?XID=<%# Eval("ProductID")%>&ID=<%# Eval("IDUniq")%>' title="عرض المنتجات" data-toggle="tooltip" target="_blank"><span class="fa fa-eye HideThis"></span>
                                                                        <%# Eval("CountProduct") %>  منتج
                                                                    </a>
                                                                </span>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="أُضيف من قبل" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# ClassQuaem.FAlBaheth((Int32) (Eval("IDAdmin")))%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="تاريخ الإضافة" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# ClassDataAccess.FChangeF((DateTime) (Eval("DateAddProduct")))%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="10px">
                                                            <ItemTemplate>
                                                                <a href='PageManageProduct.aspx?ID=<%# Eval("IDUniq")%>' title="تعديل" data-toggle="tooltip"
                                                                    class="btn btn-primary"><span class="fa fa-edit"></span></a>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                                    <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" NextPageText=" التالي  "
                                                        PreviousPageText=" السابق - " PageButtonCount="30" />
                                                    <PagerStyle CssClass="pagination-ys" BackColor="White" ForeColor="Red" HorizontalAlign="Right" Font-Size="Large" />
                                                    <RowStyle CssClass="rows"></RowStyle>
                                                    <RowStyle CssClass="rows"></RowStyle>
                                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                </asp:GridView>
                                                <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                                                <div>
                                                    <hr style='border: solid; border-width: 1px; width: 100%' />
                                                    <div class="container-fluid" dir="rtl" runat="server">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td>
                                                                    <div class="WidthMaglis24" align="center" runat="server" visible="false">
                                                                        الباحث الإجتماعي
                                                                        <br />
                                                                        <asp:Image ID="ImgAlBaheth" runat="server" Width='100px' Height='25' />
                                                                        <br />
                                                                        <asp:Label ID="lblAlBaheth" runat="server" Font-Size="11px"></asp:Label>
                                                                        <asp:DropDownList ID="DLAlBaheth" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true"
                                                                            CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                            <asp:ListItem Value=""></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="WidthMaglis24" align="center" style="font-family: 'Alwatan'; font-size: 20px;">
                                                                        مدير الجمعية
                                                                        <br />
                                                                        <asp:Image ID="ImgModer" runat="server" Width='100px' Height='25' />
                                                                        <br />
                                                                        <asp:Label ID="lblModerAlGmeiah" runat="server" Font-Size="20px"></asp:Label>
                                                                        <asp:DropDownList ID="DLModerAlGmeiah" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLModerAlGmeiah_SelectedIndexChanged"
                                                                            CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                            <asp:ListItem Value=""></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="WidthMaglis24" align="center" style="font-family: 'Alwatan'; font-size: 20px;">
                                                                        رئيس لجنة البحث الإجتماعية
                                                                        <br />
                                                                        <asp:Image ID="ImgRaeesLagnatAlBahath" runat="server" Width='100px' Height='25' />
                                                                        <br />
                                                                        <asp:Label ID="lblRaeesLagnatAlBahath" runat="server" Font-Size="20px"></asp:Label>
                                                                        <asp:DropDownList ID="DLRaeesLagnatAlBahath" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLRaeesLagnatAlBahath_SelectedIndexChanged"
                                                                            CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                            <asp:ListItem Value=""></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="WidthMaglis24" align="center" style="font-family: 'Alwatan'; font-size: 20px;">
                                                                        رئيس مجلس الإدارة
                                                                        <br />
                                                                        <asp:Image ID="ImgRaeesMaglesAlEdarah" runat="server" Width='100px' Height='25' />
                                                                        <br />
                                                                        <asp:Label ID="lblRaeesMaglesAlEdarah" runat="server" Font-Size="20px"></asp:Label>
                                                                        <asp:DropDownList ID="DLRaeesMaglesAlEdarah" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLRaeesMaglesAlEdarah_SelectedIndexChanged"
                                                                            CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                            <asp:ListItem Value=""></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="WidthMaglis24" align="center">
                                                                        <div runat="server" id="IDKhatm" align="left" style="margin-top: 0px">
                                                                            <img src="../../ImgSystem/ImgSignature/الختم.png" width="120" />
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <th>
                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                                <span style="font-size: 12px; padding-right: 5px">عدد الملفات : </span>
                                                <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                                <uc1:WUCFooterBottom runat="server" ID="WUCFooterBottom" />
                                            </th>
                                        </tr>
                                    </tfoot>
                                </table>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlNull" runat="server" Visible="False">
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <div align="center">
                                <h3 style="font-size: 20px">لا توجد نتائج
                                </h3>
                            </div>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </asp:Panel>
                        <asp:Panel ID="pnlSelect" runat="server" Visible="False">
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <div align="center">
                                <h3 style="font-size: 20px">يرجى تحديد البيانات 
                                </h3>
                            </div>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
        <script src="../css/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

