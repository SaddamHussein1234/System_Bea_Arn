<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/DMS/MPCPanel.master" AutoEventWireup="true" CodeFile="PageAll.aspx.cs" Inherits="Cpanel_ERP_DMS_InComingGeneral_PageAll" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>
<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/ERP/WUCFooterBottomERP.ascx" TagPrefix="uc1" TagName="WUCFooterBottomERP" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterSSM.ascx" TagPrefix="uc1" TagName="WUCFooterSSM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function Confirmation() {
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
        function DisableButton() {
            document.getElementById("<%=btnGet.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

    <style type="text/css">
        .ShowDiv {
            display: block;
        }

        .HideDiv {
            display: none;
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
            var gv = document.getElementById("<%=GVInComingGeneral.ClientID%>");
            var chk = gv.getElementsByTagName("input");
            for (var i = 0; i < chk.length; i++) {
                if (chk[i].checked && chk[i].id.indexOf("chkAll") == -1) {
                    count++;
                }
            }
            if (count == 0) {
                alert("لم تقم بالتحديد على أي ملف");
                return false;
            }
            else {
                return confirm(" هل أنت متأكد من الإستمرار ؟");
            }
        }
    </script>

    <script type="text/javascript">
        function ShowAttention() {
            $("#IDAttention").modal('show');
        }

        $(function () {
            $("#btnShow").click(function () {
                ShowAttention();
            });
        });
    </script>

    <script type="text/javascript">
        function ShowWhatsAppTemplate() {
            $("#IDWhatsApp").modal('show');
        }

        $(function () {
            $("#btnShow").click(function () {
                ShowWhatsAppTemplate();
            });
        });
    </script>

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <a href="PageAdd.aspx" data-toggle="tooltip" title="إضافة جديد" class="btn btn-info"><i class="fa fa-plus"></i></a>
                    <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click" Visible="false" title="طباعة">
                    <i class="fa fa-print"></i></asp:LinkButton>
                    <asp:Button ID="btnDelete" runat="server" Text="حذف الملفات المحددة" title="حذف الملفات المحددة" Visible="false"
                        data-toggle="tooltip" CssClass="btn btn-danger" OnClientClick="return ConfirmDelete();" OnClick="LBDelete_Click" />
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="btnRefrish_Click"
                        title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
                </div>
                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="/Cpanel/ERP/DMS/">الرئيسية</a></li>
                        <li><a href="PageAll.aspx">الوارد العام</a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <img runat="server" id="IDLogo" visible="false" alt="" style="margin: 50px 0 0 0;" />
                            <i class="fa fa-list"></i>قائمة الوارد العام
                        </h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-sm-12">
                            <div id="IDMessageWarning" runat="server" visible="false" class="alert  alert-warning alert-dismissible" role="alert">
                                <span class="badge badge-pill badge-warning">تحذير</span>
                                <asp:Label ID="lblMessageWarning" runat="server"></asp:Label>
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div id="IDMessageSuccess" runat="server" visible="false" class="alert  alert-success alert-dismissible" role="alert">
                                <span class="badge badge-pill badge-success">عملية ناجحة</span>
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <h5><strong><i class="fa fa-star"></i> سيتم الفلترة حسب القوائم المحددة : </strong></h5>
                        <div class="col-sm-3">
                            <div class="checkbox checkbox-primary">
                                <asp:CheckBox ID="Check_Years" runat="server" CssClass="styled" Text="سنوات الإرشيف" Width="100%" Checked="true" Enabled="false" />
                            </div>
                            <script type="text/javascript">
                                $(function () {
                                    $(document.getElementById("<%=Check_Years.ClientID %>")).click(function () {
                                        if ($(this).is(":checked")) {
                                            $("#IDYears").show();
                                        } else {
                                            $("#IDYears").hide();
                                        }
                                    });
                                });
                            </script>
                        </div>
                        <div class="col-sm-3">
                            <div class="checkbox checkbox-primary">
                                <asp:CheckBox ID="Check_Category" runat="server" CssClass="styled" Text="الفئات" Width="100%" Checked="true" />
                            </div>
                            <script type="text/javascript">
                                $(function () {
                                    $(document.getElementById("<%=Check_Category.ClientID %>")).click(function () {
                                        if ($(this).is(":checked")) {
                                            $("#IDCategory").show();
                                        } else {
                                            $("#IDCategory").hide();
                                        }
                                    });
                                });
                            </script>
                        </div>
                        <div class="col-sm-3">
                            <div class="checkbox checkbox-primary">
                                <asp:CheckBox ID="Check_Nature" runat="server" CssClass="styled" Text="طبيعة المعاملة" Width="100%" Checked="true" />
                            </div>
                            <script type="text/javascript">
                                $(function () {
                                    $(document.getElementById("<%=Check_Nature.ClientID %>")).click(function () {
                                        if ($(this).is(":checked")) {
                                            $("#IDNature").show();
                                        } else {
                                            $("#IDNature").hide();
                                        }
                                    });
                                });
                            </script>
                        </div>
                        <div class="col-sm-3">
                            <div class="checkbox checkbox-primary">
                                <asp:CheckBox ID="Check_Importance" runat="server" CssClass="styled" Text="أهمية المعاملة" Width="100%" Checked="true" />
                            </div>
                            <script type="text/javascript">
                                $(function () {
                                    $(document.getElementById("<%=Check_Importance.ClientID %>")).click(function () {
                                        if ($(this).is(":checked")) {
                                            $("#IDImportance").show();
                                        } else {
                                            $("#IDImportance").hide();
                                        }
                                    });
                                });
                            </script>
                        </div>
                        <hr style='border: solid; border-width: 1px; width: 100%' />
                        <div class="clearfix"></div>
                        <asp:Panel ID="IDFilter" runat="server" ScrollBars="Auto" Height="320">
                            <div class="col-sm-3">
                                <div id="IDYears" class="col-sm-12" style="display: none; <%= FCheck("Years") %>">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> سنوات الإرشيف : </h5>
                                        <div class="checkbox checkbox-primary">
                                            <asp:CheckBoxList ID="CBYears" runat="server"
                                                RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div id="IDCategory" class="col-sm-12" style="display: none; <%= FCheck("Category") %>">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> الفئات : </h5>
                                        <div class="checkbox checkbox-primary">
                                            <asp:CheckBoxList ID="CBCategory" runat="server"
                                                RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div id="IDNature" class="col-sm-12" style="display: none; <%= FCheck("Nature") %>">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> طبيعة المعاملة : </h5>
                                        <div class="checkbox checkbox-primary">
                                            <asp:CheckBoxList ID="CBNature" runat="server"
                                                RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                                <div id="IDImportance" class="col-sm-12" style="display: none; <%= FCheck("Importance") %>">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> أهمية المعاملة : </h5>
                                        <div class="checkbox checkbox-primary">
                                            <asp:CheckBoxList ID="CBImportance" runat="server"
                                                RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i>من تاريخ : </h5>
                                        <div class="input-group date ">
                                            <asp:TextBox ID="txtDateFrom" runat="server" placeholder="من تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="VGCustomer" Style="direction: ltr;"></asp:TextBox>
                                            <asp:Label ID="lblDateFrom" runat="server" Text="حدد التاريخ * " ForeColor="Red" Visible="false"></asp:Label>
                                            <span class="input-group-btn">
                                                <button class="btn btn-default" type="button">
                                                    <i class="fa fa-calendar"></i>
                                                </button>
                                            </span>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator12" runat="server"
                                                ControlToValidate="txtDateFrom" ErrorMessage="* حدد التاريخ" ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGCustomer" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i>إلى تاريخ : </h5>
                                        <div class="input-group date ">
                                            <asp:TextBox ID="txtDateTo" runat="server" placeholder="إلى تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="VGCustomer" Style="direction: ltr;"></asp:TextBox>
                                            <asp:Label ID="lblDateTo" runat="server" Text="حدد التاريخ * " ForeColor="Red" Visible="false"></asp:Label>
                                            <span class="input-group-btn">
                                                <button class="btn btn-default" type="button">
                                                    <i class="fa fa-calendar"></i>
                                                </button>
                                            </span>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                                ControlToValidate="txtDateTo" ErrorMessage="* حدد التاريخ" ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGCustomer" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i>بحث : </h5>
                                        <asp:TextBox ID="txtSearch" runat="server" class="form-control" placeholder="إبحث هنا ... " ValidationGroup="VGCustomer"></asp:TextBox>
                                    </div>
                                    <br />
                                    <asp:LinkButton ID="btnGet" runat="server" Style="margin-right: 4px;"
                                        class="btn btn-info btn-fill " ValidationGroup="VGCustomer" OnClick="btnGet_Click">
                                        بحث حسب الفلترة
                                    </asp:LinkButton>
                            </div>
                            <div class="clearfix"></div>
                            <hr />
                        </asp:Panel>
                        <div class="clearfix"></div>
                        <asp:Panel ID="pnlData" runat="server" Visible="False" Direction="RightToLeft">
                            <div class="table table-responsive" runat="server" id="pnlDataPrint" dir="rtl">
                                <table class='table' style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>
                                                <div class="hide">
                                                    <uc1:WUCHeader runat="server" ID="WUCHeader" />
                                                </div>
                                                <div align="center" class="w">
                                                    <div class="col-md-2 HideThis">
                                                        <div class="heading-elements">
                                                            <div id="rec_q_edit" class=" heading-btn">
                                                                <button type="button" class="btn btn-info dropdown-toggle" data-toggle="dropdown" aria-expanded="false"><i class="fa fa-magic flip-x"></i> تحرير سريع</button>
                                                                <ul class="dropdown-menu dropdown-menu-right">
                                                                    <li class="dropdown dropdown-submenu dropdown-submenu-left mt2" data-status_name="عرض الخطاب">
                                                                        <asp:LinkButton ID="LBView" runat="server" Width="100%" OnClientClick="return ConfirmDelete();"
                                                                            OnClick="LBView_Click"
                                                                            CssClass="button_multi_order_action" data-toggle="tooltip" title="تنفيذ الطلب">
                                                                        <i class="fa fa-eye"></i> 
                                                                        عرض الخطاب
                                                                        </asp:LinkButton>
                                                                    </li>
                                                                    <li class="dropdown dropdown-submenu dropdown-submenu-left mt2" data-status_name="تعديل الخطاب">
                                                                        <asp:LinkButton ID="LBEdit" runat="server" Width="100%" OnClientClick="return ConfirmDelete();"
                                                                            OnClick="LBEdit_Click"
                                                                            CssClass="button_multi_order_action" data-toggle="tooltip" title="تنفيذ الطلب">
                                                                        <i class="fa fa-edit"></i> 
                                                                        تعديل الخطاب
                                                                        </asp:LinkButton>
                                                                    </li>
                                                                    <li class="dropdown dropdown-submenu dropdown-submenu-left mt2" data-status_name="حذف الخطاب">
                                                                        <asp:LinkButton ID="LBDelete" runat="server" Width="100%" OnClientClick="return ConfirmDelete();"
                                                                            OnClick="LBDelete_Click"
                                                                            CssClass="button_multi_order_action" data-toggle="tooltip" title="تنفيذ الطلب">
                                                                        <i class="fa fa-trash"></i> 
                                                                        حذف الخطاب
                                                                        </asp:LinkButton>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-8">
                                                        <asp:TextBox ID="txtTitle" runat="server" class="form-control" Text="قائمة بيانات العملاء" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-2 HideThis">
                                                        <asp:LinkButton ID="LBGetFilter" runat="server"
                                                            OnClick="LBGetFilter_Click" data-toggle="tooltip" title="جلب قائمة الفلترة"> <i class="fa fa-refresh"></i> عرض الفلترة</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GVInComingGeneral" runat="server" AutoGenerateColumns="False" 
                                                    DataKeyNames="_ID_Item_,_ID_Year_,_Number_File_"
                                                    OnPageIndexChanging="GVCustomers_PageIndexChanging"
                                                    Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal" PageSize="1"
                                                    UseAccessibleHeader="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="10px">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblchkAll" runat="server" Text="-" Visible="false"></asp:Label>
                                                                <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblchk" runat="server" Text="-" Visible="false"></asp:Label>
                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="م" HeaderStyle-Width="10px" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ر/الخطاب" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <a href='PageView.aspx?ID=<%# Eval("_Number_File_")%>&IDYears=<%# Eval("_ID_Year_")%>' title="عرض الخطاب" data-toggle="tooltip">
                                                                    <span><%# Eval("_Number_File_")  %></span>
                                                                </a>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="الفئة" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# Eval("CategoryAr") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="الجهة" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# Eval("_Name_Ar_") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="الموضوع" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# Eval("_The_Title_") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="طبيعة المعاملة" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# Eval("NatureAr") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="أهمية المعاملة" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# Eval("ImportanceAr") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="المرفقات" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate> 
                                                                <a href='javaScript:void(0)' data-toggle="modal" data-target="#IDAttachments<%# Eval("_ID_Item_") %>" 
                                                                    title="عرض المرفقات" class="btn btn-info"><i class="fa fa-file-pdf-o"></i> عرض</a>
                                                                <div id="IDAttachments<%# Eval("_ID_Item_") %>" class="modal fade in modal_New_Style">
                                                                    <div class="modal-dialog modal-lg" >
                                                                        <div class="modal-content">
                                                                            <div class="modal-header no-border">
                                                                                <button type="button" class="close" data-dismiss="modal">×</button>
                                                                            </div>
                                                                            <div class="modal-body" id="modal_ajax_content">
                                                                                <div class="page-container">
                                                                                    <div class="page-content">
                                                                                        <div class=" panel-body">
                                                                                            <label>
                                                                                                <i class="fa fa-star"></i> 
                                                                                                مرفقات <%# Eval("_The_Title_") %> : 
                                                                                            </label>
                                                                                            <div align="center">
                                                                                                <div class="" dir="rtl">
                                                                                                    <table class='table table-bordered table-condensed' style="width: 100%" aria-multiline="true">
                                                                                                        <thead>
                                                                                                            <tr class="th">
                                                                                                                <th class="StyleTD">م</th>
                                                                                                                <th class="StyleTD">عنوان الملف</th>
                                                                                                                <th class="StyleTD">نوع الملف</th>
                                                                                                                <th class="StyleTD">حجم الملف</th>
                                                                                                                <th class="StyleTD">بتاريخ</th>
                                                                                                                <th class="StyleTD">العرض</th>
                                                                                                            </tr>
                                                                                                        </thead>
                                                                                                        <tbody>
                                                                                                            <tr>
                                                                                                                <td style="width: 10px;" class="StyleTD">
                                                                                                                    <span style="margin-right: 5px; font-size: 11px"> 1 </span>
                                                                                                                </td>
                                                                                                                <td class="StyleTD">
                                                                                                                    <span style="font-size: 12px"><%# Eval("_The_Title_")%></span>
                                                                                                                </td>
                                                                                                                <td class="StyleTD">
                                                                                                                    <span style="font-size: 12px"><%# Eval("_Type_File_").ToString().Replace(".", "") %></span>
                                                                                                                </td>
                                                                                                                <td class="StyleTD">
                                                                                                                    <span style="font-size: 12px"><%# ClassSaddam.FormatSize(Convert.ToInt32(Eval("_Size_File_"))) %></span>
                                                                                                                </td>
                                                                                                                <th class="StyleTD th">
                                                                                                                    <span style="font-size: 12px"><%# Eval("_CreatedDate_", "{0:MM/dd/yyyy}") %></span>
                                                                                                                </th>
                                                                                                                <td class="StyleTD">
                                                                                                                    <a class="btn btn-default btn-sm download_button" style="<%# ClassSaddam.FCheckNullFile(Eval("_Src_").ToString()) %>"
                                                                                                                        href="/<%# Eval("_Src_") %>" data-file="pdf" data-fancybox data-type="iframe" title="عرض الملف" data-toggle="tooltip">
                                                                                                                        <i class="fas fa-file-pdf"></i>
                                                                                                                        <div>
                                                                                                                            <span>عرض الملف </span><small><%# ClassSaddam.FGetTypeFileOutTitle((string)Eval("_Type_File_")) %> </small>
                                                                                                                        </div>
                                                                                                                    </a>
                                                                                                                    <a class="btn btn-default btn-sm download_button" style="<%# ClassSaddam.FCheckNotNullFile(Eval("_Src_").ToString()) %>" data-file="pdf">
                                                                                                                        <i class="fas fa-file-pdf"></i>
                                                                                                                        <div><span>بدون ملف مرفق</span></div>
                                                                                                                    </a>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </tbody>
                                                                                                        <tfoot>
                                                                                                            <tr>
                                                                                                                <th colspan="2"></th>
                                                                                                            </tr>
                                                                                                        </tfoot>
                                                                                                    </table>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="modal-footer">
                                                                                            <button type="button" class="btn btn-default -mb-3" data-dismiss="modal">اغلاق</button>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="بتاريخ" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# Eval("_CreatedDate_", "{0:dd/MM/yyyy}") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="مزيد من التفاصيل" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LBViewAdmin" runat="server" CommandName='<%# Eval("_LastSeeBy_") %>' 
                                                                    CommandArgument='<%# Eval("_CreatedBy_") %>' ToolTip='<%# Eval("_LastSeeDate_") %>' OnClick="LBViewAdmin_Click"
                                                                    title="عرض المزيد ؟" data-toggle="tooltip"><span class="fa fa-user"></span> عرض</asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="16">
                                                            <ItemTemplate>
                                                                <a href="PageAdd.aspx?ID=<%# Eval("_ID_Item_") %>" data-toggle="tooltip" title="تعديل الخطاب">
                                                                    <i class="fa fa-edit"></i>
                                                                </a>
                                                                <a href='PageView.aspx?ID=<%# Eval("_Number_File_")%>&IDYears=<%# Eval("_ID_Year_")%>' class="hide"
                                                                    title="عرض الخطاب" data-toggle="tooltip"><span class="fa fa-eye"></span></a>
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
                                                <span>العدد : </span>
                                                <asp:Label ID="lblCount" runat="server" Text="0"></asp:Label>
                                                <div class="hide2">
                                                    <hr style='border: solid; border-width: 1px; width: 100%' />
                                                    <uc1:WUCFooterSSM runat="server" ID="WUCFooterSSM" />
                                                    <hr style='border: solid; border-width: 1px; width: 100%' />
                                                    <div class="HideNow">
                                                        <uc1:WUCFooterBottom runat="server" ID="WUCFooterBottom" />
                                                    </div>
                                                </div>
                                            </th>
                                        </tr>
                                    </tfoot>
                                </table>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlNull" runat="server" Visible="False">
                            <hr />
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
                            <br />
                            <br />
                            <br />
                            <br />
                        </asp:Panel>
                        <asp:Panel ID="pnlSelect" runat="server" Visible="False">
                            <hr />
                            <div align="center">
                                <h3 style="font-size: 20px">حدد البيانات
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
                            <br />
                            <br />
                            <br />
                            <br />
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            $('.date').datetimepicker({
                pickTime: false
            });

            $('.time').datetimepicker({
                pickDate: false
            });

            $('.datetime').datetimepicker({
                pickDate: true,
                pickTime: true
            });
        </script>
        <asp:HiddenField ID="HFIDStore" runat="server" />
        <asp:HiddenField ID="HFIDDomain" runat="server" />
        <asp:HiddenField ID="HFIDSiteName" runat="server" />

        <asp:HiddenField ID="HFListName" runat="server" />
        <asp:HiddenField ID="HFSocial_Media" runat="server" />
        <asp:HiddenField ID="HFMarketing_Campaign" runat="server" />
        <asp:HiddenField ID="HFInterest" runat="server" />
        <asp:HiddenField ID="HFCustomer_Case" runat="server" />
        <asp:HiddenField ID="HFDeal_Size" runat="server" />
        <asp:HiddenField ID="HFAge" runat="server" />
        <span runat="server" id="IDCreatedByStyle"></span>
        <link href="../jquery.fancybox.min.css" rel="stylesheet" />
        <script src="../jquery.fancybox.min.js"></script>
</asp:Content>

