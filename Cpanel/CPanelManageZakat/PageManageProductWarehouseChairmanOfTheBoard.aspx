<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelManageZakat/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageProductWarehouseChairmanOfTheBoard.aspx.cs" Inherits="Cpanel_CPanelManageZakat_PageManageProductWarehouseChairmanOfTheBoard" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterWSM.ascx" TagPrefix="uc1" TagName="WUCFooterWSM" %>


<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../GridView.css?v=2.2" rel="stylesheet" type="text/css" />
    <link href="../css/chosen.css" rel="stylesheet" />
    <style type="text/css">
        .bl {
            color: #fff;
        }

        .fo {
            font-size: 12px;
        }

        @media screen and (min-width: 768px) {
            .WidthText2 {
                Width: 200px;
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
                Width: 24%;
                padding-right: 5px;
            }

            .WidthText4 {
                float: right;
                Width: 50%;
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
        }
    </style>

    <script type="text/javascript">
        function insertConfirmation() {
            var answer = confirm("هل تريد الإستمرار ؟")
            if (answer) {
                return true;
            }
            else {
                return false;
            }
        }
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
            var gv = document.getElementById("<%=GVWarehouseCashier.ClientID%>");
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
                return confirm(" هل أنت متأكد من الإستمرار ؟");
            }
        }
    </script>

    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnAllow.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <%--<a href="PageManageProductShippingWarehouse.aspx" data-toggle="tooltip" title="إضافة أمر شحن للمستودع" class="btn btn-primary"><i class="fa fa-plus"></i></a>--%>
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث" OnClick="btnRefrish_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
                    <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                        title="طباعة" OnClientClick="return insertConfirmation();">
                    <i class="fa fa-print"></i></asp:LinkButton>

                </div>
                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="Default.aspx">الرئيسية</a></li>
                        <li><a href="PageManageProductWarehouseChairmanOfTheBoard.aspx">قائمة فواتير الدعم العيني </a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>قائمة فواتير الدعم العيني - موافقة رئيس المجلس
                        </h3>
                        <div style="float: left">
                            <asp:Button ID="btnAllow" class="btn btn-info" runat="server" Text="الموافقة على السجلات المحددة" OnClick="btnAllow_Click"
                                title="الموافقة على السجلات المحددة" data-toggle="tooltip" OnClientClick="return ConfirmDelete();" />
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="col-sm-12">
                            <div id="IDMessageSuccess" runat="server" visible="false" class="alert  alert-success alert-dismissible" role="alert">
                                <span class="badge badge-pill badge-success">عملية ناجحة</span>
                                تمت العملية بنجاح ... 
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                        </div>
                        <asp:Panel ID="pnlData" runat="server" Direction="RightToLeft" Visible="False">
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
                                                        <asp:TextBox ID="txtTitle" runat="server" class="form-control" Text="قائمة سندات الدعم العيني التي تحتاج إلى موافقة رئيس مجلس الإدارة" placeholder='عنوان البحث' Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GVWarehouseCashier" runat="server" AutoGenerateColumns="False" DataKeyNames="_ID_Item"
                                                    Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal" OnRowDataBound="GVWarehouseCashier_RowDataBound"
                                                    UseAccessibleHeader="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-Width="10px">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkAll" runat="server" onclick='checkAll(this);' />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="_ID_Item" HeaderText="_ID_Item" InsertVisible="False" ReadOnly="True"
                                                            SortExpression="_ID_Item" Visible="false" />
                                                        <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="بيانات الفاتورة" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <span style="font-size: 11px">رقم الفاتورة <%# Eval("_bill_Number_")%>
                                                                </span>
                                                                <br />
                                                                <span style="font-size: 11px" class="HideThis">
                                                                     <%# ClassSaddam.FAmeenAlsondoq2((bool) (Eval("_IsAmmenAlSondoq_")))%>
                                                                   , <%# ClassSaddam.FRaeesMaglis2((bool) (Eval("_IsRaeesMaglisAlEdarah_")))%>
                                                                   , <%# ClassSaddam.FAmeenAlmostodaa2((bool) (Eval("_IsStorekeeper_")))%>
                                                                </span>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="بإسم" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <span style="font-size: 12px">
                                                                    <%# Eval("_Name_Donor_")%>
                                                                </span>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="المشروع" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <span style="font-size: 12px">
                                                                    <%# ClassQuaem.FSupportType(Convert.ToInt32(Eval("_ID_Project_")))%>
                                                                </span>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="المبلغ الإجمالي" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCountTotalPrice" runat="server" Font-Size="12px" 
                                                                    Text=' <%# ClassWarehouse_Zakat.FSumWarehouse_Zakat_By_Bill(Convert.ToInt32(Eval("_bill_Number_")),Convert.ToInt32(Eval("_ID_Project_")))%>'></asp:Label>
                                                                ريال
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="أُضيف من قبل" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <span style="font-size: 12px">
                                                                    <%# Eval("FirstName")%>
                                                                </span>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="تاريخ الإضافة" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <span>
                                                                    <%# ClassDataAccess.FChangeF((DateTime) (Eval("_Date_UpDate_")))%>
                                                                </span>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="16">
                                                            <ItemTemplate>
                                                                <a href='PageDeedDonationInKindDetails.aspx?ID=<%# Eval("_bill_Number_")%>&IDP=<%# Eval("_ID_Project_")%>&Type=Moder' title="عرض التفاصيل" data-toggle="tooltip"
                                                                    class="btn btn-info"><span class="fa fa-eye"></span></a>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                    <HeaderStyle CssClass="Colorloading" Font-Bold="True" ForeColor="White" />
                                                    <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" NextPageText=" التالي  "
                                                        PreviousPageText=" السابق - " PageButtonCount="30" />
                                                    <PagerStyle CssClass="pagination-ys" BackColor="White" ForeColor="Red" HorizontalAlign="Right" Font-Size="Large" />
                                                    <RowStyle CssClass="rows"></RowStyle>
                                                    <RowStyle CssClass="rows"></RowStyle>
                                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                </asp:GridView>
                                                <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                                            </td>
                                        </tr>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <th>
                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                                <span style="font-size: 12px; padding-right: 5px">العدد : </span>
                                                <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                / <span style="font-size: 12px; padding-right: 5px">المجموع : </span>
                                                <asp:Label ID="lblTotalPrice" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                <small>
                                                    <asp:Label ID="lblMonyType" runat="server" Style='color: Red; font-size: 12px'></asp:Label>
                                                </small>
                                                <div align="Left" class="HideThis">
                                                    <img src='/Img/IconTrue.png' style='width: 20px' />
                                                    <span style="font-size: 11px">إطلع</span>
                                                    <img src='/Img/IconFalse.png' style='width: 20px' />
                                                    <span style="font-size: 11px">لم يطلع</span>
                                                </div>
                                            </th>
                                        </tr>
                                    </tfoot>
                                </table>
                                <div>
                                    <hr style='border: solid; border-width: 1px; width: 100%' />
                                    <div class="container-fluid" dir="rtl" runat="server">
                                        <uc1:WUCFooterWSM runat="server" ID="WUCFooterWSM" />
                                    </div>
                                    <hr style='border: solid; border-width: 1px; width: 100%' />
                                    <uc1:WUCFooterBottom runat="server" ID="WUCFooterBottom" />
                                </div>
                            </div>

                        </asp:Panel>
                        <asp:Panel ID="pnlNull" runat="server" Visible="False">
                            <br />
                            <br />
                            <br />
                            <br />
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

