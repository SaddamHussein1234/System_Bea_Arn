<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Test" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterBill.ascx" TagPrefix="uc1" TagName="WUCFooterBill" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        html
        {
            background-color: Gray;
            font: 14px Georgia,Serif,Garamond;
        }
        h1
        {
            color: Green;
            font-size: 18px;
            border-bottom: Solid 1px orange;
        }
        .clear
        {
            clear: both;
        }
        .lbl
        {
            color: green;
            font-weight: bold;
        }
        .upperColumn
        {
            margin: auto;
            width: 600px;
            border-bottom: Solid 1px orange;
            background-color: white;
            padding: 10px;
        }
        .bottomColumn
        {
            margin: auto;
            width: 600px;
            background-color: white;
            padding: 10px;
        }
    </style>
    <title>File Upload</title>

    <script type="text/javascript">
        var size = 2;
        var id = 0;

        function ProgressBar() {
            if (document.getElementById('<%=ImageFile.ClientID %>').value != "") {
                document.getElementById("divProgress").style.display = "block";
                document.getElementById("divUpload").style.display = "block";
                id = setInterval("progress()", 20);
                return true;
            }
            else {
                alert("Select a file to upload");
                return false;
            }

        }

        function progress() {
            size = size + 1;
            if (size > 299) {
                clearTimeout(id);
            }
            document.getElementById("divProgress").style.width = size + "pt";
            document.getElementById("<%=lblPercentage.ClientID %>").firstChild.data = parseInt(size / 3) + "%";
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="upperColumn">
        <h1>
            File Upload Example</h1>
                <asp:Label ID="lblImageFile" Text="Load Image" AssociatedControlID="ImageFile" runat="server"
                    CssClass="lbl" />
                <asp:FileUpload ID="ImageFile" runat="server" />
                <br />
                <br />
                <asp:Button ID="btnAddImage" runat="server" Text="Add Image" OnClientClick="return ProgressBar()"
                    OnClick="btnAddImage_Click" />
                <asp:Button ID="btnShowImage" Text="Show Image" runat="server" OnClick="btnShowImage_Click" />
                <div id="divUpload" style="display: none">
                    <div style="width: 300pt; text-align: center;">
                        Uploading...</div>
                    <div style="width: 300pt; height: 20px; border: solid 1pt gray">
                        <div id="divProgress" runat="server" style="width: 1pt; height: 20px; background-color: orange;
                            display: none">
                        </div>
                    </div>
                    <div style="width: 300pt; text-align: center;">
                        <asp:Label ID="lblPercentage" runat="server" Text="Label"></asp:Label></div>
                    <br />
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text=""></asp:Label>
                </div>
            </div>
            <br class="clear" />
            <div class="bottomColumn">
                <asp:DataList ID="dlImageList" RepeatColumns="3" runat="server">
                    <ItemTemplate>
                        <asp:Image ID="imgShow" ImageUrl='<%# Eval("Name","~/ImgSystem/FilesDMS/{0}")%>' Style="width: 200px"
                            runat="server" AlternateText='<%# Eval("Name") %>' />
                        <br />
                        <%# Eval("Name") %>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <hr />
        </div>
        <hr>
        <style>
            #leftbox {
                width: 150px;
                height: 150px;
                border: 1px solid red;
                float: left;
                margin-left: 10px;
            }

            #rightbox {
                width: 150px;
                height: 150px;
                border: 1px solid black;
                float: left;
                margin-left: 10px;
            }
        </style>
        <script>
            function drag(ev) {
                ev.dataTransfer.setData("Text", ev.target.id);
            }
            function allowdrop(ev) {
                ev.preventDefault();
            }
            function drop(ev) {
                ev.preventDefault();
                var data = ev.dataTransfer.getData("Text");
                ev.target.appendChild(document.getElementById(data));
            }
        </script>
        <div>
            <h3>Drag and Drop the Folder image firstly into the Red box after that in black box(Example 2)</h3>
            <div id="leftbox" ondrop="drop(event)" ondragover="allowdrop(event)"></div>
            <div id="rightbox" ondrop="drop(event)" ondragover="allowdrop(event)"><img src="Img/BGR.jpg" width="150" draggable="true" ondragstart="drag(event)" id="image"></div>
        </div>
        <br /><hr />
        <asp:Label ID="lblEnc" runat="server" Text="0"></asp:Label><br />
        <asp:Label ID="lblDec" runat="server" Text="0"></asp:Label>
        <hr /><hr />
        <asp:TextBox ID="txtQuery" runat="server" TextMode="MultiLine" Rows="15" Width="500"></asp:TextBox><br />
        <asp:LinkButton ID="LBSubmet" runat="server" OnClick="LBSubmet_Click">Submet</asp:LinkButton>
        <hr /><hr />
        <asp:Image ID="ImgQRCode" runat="server" alt='QR Code' Visible="false" />
        <asp:Repeater ID="RPTCashing" runat="server">
            <ItemTemplate>
                <div class='page'>
                    <header class="hide">
                        <img src='/view/image/LogoTitleNew2.jpg' style='width: 100%; height: 100px;' />
                    </header>
                    <div class="">
                        <div align="center" class="w">
                            <table style="width: 100%; background-color: #ffffff; color: #393939">
                                <tr>
                                    <td style="border: thin double #808080; border-width: 1px; width: 45%">
                                        <div align="center">
                                            <asp:Label ID="txtTitle" runat="server" Text="سند صرف" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:Label>
                                        </div>
                                    </td>
                                    <td style="border: thin double #808080; border-width: 1px; width: 20%; font-family: 'Alwatan'; font-size: 18px;">
                                        <span style="padding-right: 10px; font-size: 18px;">رقم الفاتورة /  </span>
                                        <%# Eval("_bill_Number_") %>
                                    </td>
                                    <td rowspan="2" style="border: thin double #808080; border-width: 1px; width: 35%">
                                        <div align='center' class="w">

                                            <img src='<%# Class_QRScan.FGetQRCodePath(XNAmeServer +
                                                "/ar/Cashing/PageView.aspx?ID=" + Eval("_bill_Number_") + "&IDUniq=" + Request.QueryString["IDUniq"] 
                                                    , ImgQRCode) %>' alt='Loding' style='Height:90px; Width:90px;' />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="border: thin double #808080; border-width: 1px; width: 35%">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td align="left" style="width: 20%; font-size: 12px">التاريخ / 
                                                </td>
                                                <td style="width: 80%">
                                                    <%# Eval("_CreatedDate_", "{0:yyyy/MM/dd}") + "مـ - " /*+ ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(Eval("_CreatedDate_"))) + "هـ" */%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 30%;">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 46%;">
                                            <span style="font-size: 13px">بموجبة يتم الصرف / 
                                            </span>
                                            <asp:DropDownList ID="DLType" runat="server" ValidationGroup="GPrint">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="للسيد">للسيد</asp:ListItem>
                                                <asp:ListItem Value="للسيدة">للسيدة</asp:ListItem>
                                                <asp:ListItem Value="للسادة">للسادة</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                                ControlToValidate="DLType" ErrorMessage="* إجباري" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="REVPrint" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:Label ID="lblType" runat="server" Visible="false"></asp:Label>
                                            <div style="font-family: 'Alwatan'; font-size: 17px" align="center">
                                                <asp:Label ID="lblFromDonor" runat="server"></asp:Label>
                                                <%# Eval("_Name_") %>
                                            </div>
                                        </td>
                                        <td style="width: 33%; display: none;">
                                            <table style="font-size: 12px; margin: 10px; width: 90%; display: none;">
                                                <tr>
                                                    <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">مدخل البيانات :
                                                                <%# Eval("NameAdmin") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">بتاريخ :
                                                                <%# Eval("_ModifiedDate_", "{0:yyyy/MM/dd}") %>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="IDUpdate" visible="false" style="display: none">
                                                    <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">
                                                        <asp:Label ID="lblDataEntryEdit" runat="server" Font-Size="12px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px; display: none">بتاريخ :
                                                                <asp:Label ID="lblDateEntryEdit" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 70%;">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 20%; border: thin double #808080; border-width: 1px; padding: 5px" align="center">
                                            <asp:Label ID="lblTotalPrice" runat="server" Text='<%# Eval("_The_Mony_") %>' Style='color: Red; font-size: 13px'></asp:Label>
                                            <asp:Label ID="Label150" runat="server" Text='<%# XMony %>' Style='color: Red; font-size: 12px'></asp:Label>
                                        </td>
                                        <td style="width: 80%; border: thin double #808080; border-width: 1px;" align="center">
                                            <asp:Label ID="lblSumWord" runat="server" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"
                                                Text='<%# FConvertToWord(Eval("_The_Mony_").ToString()) %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%; border: thin double #808080; border-width: 1px; padding: 5px" align="center">طريقة الدفع 
                                        </td>
                                        <td style="width: 80%; border: thin double #808080; border-width: 1px;" align="center">
                                            <%# Convert.ToBoolean(Eval("_IsCash_Money_")) ?
                                                "<input id='CBCash_Money_' type='checkbox' Checked='" + Eval("_IsCash_Money_") + "' disabled /> <span>نقداً </span>" : ""
                                            %>
                                            <%# Convert.ToBoolean(Eval("_IsShayk_Bank_")) ?
                                                "<input id='CBShayk_Bank' type='checkbox' Checked='" + Eval("_IsShayk_Bank_") + "' disabled /> <span style='font-size: 11px;'>شيك رقم : </span>" +
                                                " / رقم الشيك : " + Eval("_Number_Shayk_Bank_").ToString() + "- بتاريخ : " + Eval("_Date_Shayk_", "{0:yyyy/MM/dd}") + 
                                                "- على : " + Eval("_For_Bank_") : ""
                                            %>
                                            <%# Convert.ToBoolean(Eval("_Transfer_On_Account_")) ?
                                                "<input id='CBShayk_Bank' type='checkbox' Checked='" + Eval("_Transfer_On_Account_") + "' disabled /> <span style='font-size: 11px;'>إيداع بنكي على :</span>" +
                                                Eval("_For_Bank_Transfer_").ToString() + " / حساب رقم : " + Eval("_Number_Account_") + 
                                                "- تاريخ :" + Eval("_Date_Bank_Transfer_", "{0:yyyy/MM/dd}") : ""
                                            %>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <div style="margin: 10px">
                        <span style="font-size:12px;">وذلك لغرض / <%# Eval("_Name_Ar_") + " / " + Library_CLS_Arn.OM.Repostry.Repostry_Main_Items_.FGetNameByID(new Guid(Eval("_ID_Sub_Item_").ToString())) %> </span>
                        / 
                        <span style="font-size:12px;"><%# Eval("_Note_Bill_") %> </span>
                    </div>
                    <%# Convert.ToBoolean(Eval("_IDAmmenAlSondoq_")) && Convert.ToBoolean(Eval("_IDRaeesMaglisAlEdarah_")) ? 
                    "<div align='left' style='margin-top: -60px'><img src='/ImgSystem/ImgSignature/الختم.png' /></div>" 
                    : 
                    "<div align='left' style='margin-top: -60px'><img src='/Cpanel/loader.gif' width='113' /></div>" %>
                    <table style="width: 100%; margin-top: -60px; font-size: 12px">
                        <tr>
                            <td style="width: 33%; border: thin double #808080; border-width: 1px;" align="center">
                                <div style="margin: 0 0 5px 0;">
                                    <span style="font-family: 'Alwatan'; font-size: 17px">أمين الصندوق</span>
                                    <div align="right" style="margin-right: 5px;">
                                        الإسم :
                                        <%# Eval("NameAmmenAlSondoq") %><br />
                                        التوقيع :
                                        <%# Convert.ToBoolean(Eval("_IDAmmenAlSondoq_")) ?
                                            "<img src='/" + Eval("ImgAmmenAlSondoq") + "' alt='' Width='100' Height='30px' />"
                                            :
                                            "<img src='/Cpanel/loaderMin.gif' alt='' Width='30' Height='30px' />"
                                        %>
                                        <%# Eval("_IDAmmen_Date_Allow_", "{0:yyyy/MM/dd}") %>
                                    </div>
                                </div>
                            </td>
                            <td style="width: 33%; border: thin double #808080; border-width: 1px;" align="center">
                                <div style="margin: 0 0 5px 0;">
                                    <span style="font-family: 'Alwatan'; font-size: 17px">رئيس مجلس الإدارة</span>
                                    <div align="right" style="margin-right: 5px;">
                                        الإسم :
                                        <%# Eval("NameRaeesMaglis") %><br />
                                        التوقيع :
                                        <%# Convert.ToBoolean(Eval("_IDRaeesMaglisAlEdarah_")) ?
                                            "<img src='/" + Eval("ImgRaeesMaglis") + "' alt='' Width='100' Height='30px' />"
                                            :
                                            "<img src='/Cpanel/loaderMin.gif' alt='' Width='30' Height='30px' />"
                                        %>
                                        <%# Eval("_IDRaees_Date_Allow_", "{0:yyyy/MM/dd}") %>
                                    </div>
                                </div>
                            </td>
                            <td style="width: 33%; border: thin double #808080; border-width: 1px;" align="center">
                                <div style="margin: 0 0 5px 0;">
                                    <span style="font-family: 'Alwatan'; font-size: 17px">المستلم</span>
                                    <div align="right" style="margin-right: 5px;">
                                        الإسم :
                                        <%# Eval("_Name_") %>
                                    </div>
                                    <asp:Label ID="txtCoustmoer" runat="server" Text="وقع صورة طبق الأصل"
                                            Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div class="hide">
                        <uc1:wucfooterbill runat="server" id="WUCFooterBill" />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
     </form>
    </body>
</html>