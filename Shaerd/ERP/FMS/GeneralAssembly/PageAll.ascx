<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageAll.ascx.cs" Inherits="Shaerd_ERP_FMS_GeneralAssembly_PageAll" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterSSM.ascx" TagPrefix="uc1" TagName="WUCFooterSSM" %>

<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

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
            var gv = document.getElementById("<%=GVGeneralAssemblyBill.ClientID%>");
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

<div class="page-header">
    <div class="container-fluid">
        <div class="pull-right">
            <asp:LinkButton ID="LBPrintAll" runat="server" class="btn btn-success" data-toggle="tooltip"
                title="طباعة" OnClick="LBPrintAll_Click">
                    <i class="fa fa-print"></i></asp:LinkButton>
            <asp:LinkButton ID="btnDelete1" runat="server" class="btn btn-danger" OnClick="btnDelete1_Click"
                OnClientClick="return ConfirmDelete();" title="حذف" data-toggle="tooltip"><span class="tip-bottom">
                    <i class="fa fa-trash-o"></i></span></asp:LinkButton>
            <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip" title="تحديث" OnClick="btnRefrish_Click">
            <i class="fa fa-refresh"></i></asp:LinkButton>
        </div>
        <div class="container-fluid">
            <h1>لوحة التحكم</h1>
            <ul class="breadcrumb">
                <li><a href="../">الرئيسية</a></li>
                <li><a href="#">قائمة ايصالات الإشتراكات </a></li>
            </ul>
        </div>
    </div>
    <div class="container-fluid">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <i class="fa fa-list"></i>قائمة ايصالات الإشتراكات
                </h3>
                <div style="float: left">
                    <asp:Button ID="btnSearch" runat="server" Text="بحث" title="بحث" data-toggle="tooltip"
                        ValidationGroup="g2" CssClass="btn btn btn-info pull-right" OnClick="btnSearch_Click" />
                    &nbsp;
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthText2" placeholder=" إبحث هنا ... "></asp:TextBox>
                </div>
                <div style="float: left; width:150px">
                    <asp:DropDownList ID="DLYears" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown"
                        Style="font-size: 12px;" AutoPostBack="true" OnSelectedIndexChanged="DLYears_SelectedIndexChanged">
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator12" runat="server"
                        ControlToValidate="DLYears" ErrorMessage="* حدد السنة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                        ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div style="float: left">
                    <h5 style="margin-top:10px">حدد السنة : </h5>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="panel-body">
                <div class="col-md-12">
                    <div class="form-group">
                        <h5><i class="fa fa-star"></i> فلترة العرض : </h5>
                        <div class="checkbox checkbox-primary">
                            <asp:CheckBox ID="CB1" runat="server" Text="م" Font-Size="12px" Checked="true" CssClass="styled" Width="40" />
                            <asp:CheckBox ID="CB2" runat="server" Text="الإسم" Font-Size="12px" Checked="true" CssClass="styled" Width="60" />
                            <asp:CheckBox ID="CB3" runat="server" Text="السجل المدني" Font-Size="12px" Checked="true" CssClass="styled" Width="100" />
                            <asp:CheckBox ID="CB4" runat="server" Text="رقم الجوال" Font-Size="12px" Checked="true" CssClass="styled" Width="80" />
                            <asp:CheckBox ID="CB5" runat="server" Text="نوع الصفة" Font-Size="12px" Checked="true" CssClass="styled" Width="100" />
                            <asp:CheckBox ID="CB6" runat="server" Text="تاريخ الانتساب" Font-Size="12px" Checked="true" CssClass="styled" Width="100" />
                            <asp:CheckBox ID="CB7" runat="server" Text="رقم الايصال" Font-Size="12px" Checked="true" CssClass="styled" Width="100" /><br />
                            <asp:CheckBox ID="CB8" runat="server" Text="تاريخ تجديد العضوية" Font-Size="12px" Checked="true" CssClass="styled" Width="140" />
                            <asp:CheckBox ID="CB9" runat="server" Text="تاريخ إنتهاء العضوية" Font-Size="12px" Checked="true" CssClass="styled" Width="140" />
                            <asp:CheckBox ID="CB10" runat="server" Text="رسوم العضوية" Font-Size="12px" Checked="true" CssClass="styled" Width="100" />
                            <asp:CheckBox ID="CB11" runat="server" Text="أٌضيف من قبل" Font-Size="12px" CssClass="styled" Width="100" />
                            <asp:CheckBox ID="CB12" runat="server" Text="تاريخ الإضافة" Font-Size="12px" Checked="true" CssClass="styled" Width="100" />
                        </div>
                    </div>
                </div>
                <asp:Panel ID="pnlData" runat="server" Visible="False">
                    <asp:Panel ID="pnl2" runat="server" Direction="RightToLeft">
                        <div class="table table-responsive">
                            <table class='table' style="width: 100%">
                                <thead>
                                    <tr>
                                        <th>
                                            <div class="HideNow">
                                                <uc1:WUCHeader runat="server" ID="WUCHeader" />
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                            </div>
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GVGeneralAssemblyBill" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
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
                                                    <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="الاسم" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# Eval("FirstName") %>
                                                            <div class="HideThis">
                                                                <%# ClassSaddam.FAmeenAlsondoq2((bool) (Eval("IsAllow_Ameen_Alsondoq_")))%>
                                                                <%# ClassSaddam.FCheckAllowRaeesMaglis2((bool) (Eval("IsAllow_Raees_AlMagles_")))%>
                                                            </div>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="A3" HeaderText="السجل المدني" HeaderStyle-ForeColor="#CCCCCC" />
                                                    <asp:BoundField DataField="PhoneNumber" HeaderText="رقم الجوال" HeaderStyle-ForeColor="#CCCCCC" />
                                                    <asp:BoundField DataField="The_Job_" HeaderText="نوع الصفة" HeaderStyle-ForeColor="#CCCCCC" />
                                                    <asp:BoundField DataField="bill_Number_" HeaderText="رقم الايصال" HeaderStyle-ForeColor="#CCCCCC" />
                                                    <asp:TemplateField HeaderText="تاريخ الانتساب" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# Eval("Date_Rigstry_", "{0:dd/MM/yyyy}") %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="تاريخ تجديد العضوية" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# Eval("_Date_Get_", "{0:dd/MM/yyyy}") %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="تاريخ إنتهاء العضوية" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# ClassDataAccess.FChangeWithAddYearsF((DateTime) Eval("_Date_Get_")) %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="رسوم العضوية" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# Eval("The_Mony_") %> ر.س
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="أٌضيف من قبل" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# ClassQuaem.FAlBaheth((Int32) Eval("IDAdmin")) %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="تاريخ الإضافة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# Eval("DateAdd_", "{0:dd/MM/yyyy}") %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <a href='PageView.aspx?ID=<%# Eval("bill_Number_")%>&XID=<%# Eval("IDUniq")%>' title="عرض الملف" data-toggle="tooltip"><span class="fa fa-eye"></span></a>
                                                            <a href='PageEdit.aspx?XID=<%# Eval("IDUniq")%>' title="تعديل" data-toggle="tooltip" style='<%# XDisplay %>'><span class="fa fa-edit"></span></a>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
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
                                            <asp:HiddenField ID="hfCount" runat="server" Value="0" />                                                  
                                        </td>
                                    </tr>
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <th>
                                            <div style="float: right">
                                                <span style="font-size: 12px; padding-right: 5px">العدد : </span>
                                                <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                            </div>
                                        </th>
                                    </tr>
                                </tfoot>
                            </table>
                            <div>
                                <div class="container-fluid" dir="rtl" runat="server">
                                    <hr style='border: solid; border-width: 1px; width: 100%' />
                                    <uc1:WUCFooterSSM runat="server" ID="WUCFooterSSM" />
                                </div>
                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                <uc1:WUCFooterBottom runat="server" ID="WUCFooterBottom" />
                            </div>
                        </div>
                    </asp:Panel>
                </asp:Panel>
                <asp:Panel ID="pnlNull" runat="server" Visible="False">
                    <hr />
                    <br />
                    <br />
                    <br />
                    <div align="center">
                        <h3 style="font-size: 20px">لا توجد نتائج
                        </h3>
                    </div>
                    <br />
                    <br />
                    <hr />
                </asp:Panel>
                <asp:Panel ID="pnlSelect" runat="server" Visible="False">
                    <hr />
                    <br />
                    <br />
                    <br />
                    <div align="center">
                        <h3 style="font-size: 20px">حدد السنة المراد الإستعلام عنها
                        </h3>
                    </div>
                    <br />
                    <br />
                    <hr />
                </asp:Panel>
            </div>
        </div>
    </div>
</div>
<script src="<%=ResolveUrl("~/Cpanel/css/chosen.jquery.js")%>" type="text/javascript"></script>
<script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
