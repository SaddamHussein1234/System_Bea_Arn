<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCFooterMony.ascx.cs" Inherits="Cpanel_CAttach_WUCFooterMony" %>
<table style="width: 100%; font-family: 'Co'; font-size:12px;">
    <tr>
        <td>
            <div class="WidthMaglis24" align="center">
                <span style="font-size:14px;">مدير الجمعية</span>
                                                                    <br />
                <asp:Image ID="ImgModer" runat="server" Width='100px' Height='25' />
                <br />
                <asp:Label ID="lblModerAlGmeiah" runat="server"></asp:Label>
                <asp:DropDownList ID="DLModerAlGmeiah" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLModerAlGmeiah_SelectedIndexChanged"
                    CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                    <asp:ListItem Value=""></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="WidthMaglis24" align="center">
                المشرف المالي
                <br />
                <asp:Image ID="ImgAmeenAlSondoq" runat="server" Width='100px' Height='25' />
                <br />
                <asp:Label ID="lblAmeenAlSondoq" runat="server"></asp:Label>
                <asp:DropDownList ID="DLAmeenAlSondoq" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLAmeenAlSondoq_SelectedIndexChanged"
                    CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                    <asp:ListItem Value=""></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="WidthMaglis24" align="center">
                <span style="font-size:14px;"> رئيس مجلس الإدارة </span>
                                                                    <br />
                <asp:Image ID="ImgRaeesMaglesAlEdarah" runat="server" Width='100px' Height='25' />
                <br />
                <asp:Label ID="lblRaeesMaglesAlEdarah" runat="server"></asp:Label>
                <asp:DropDownList ID="DLRaeesMaglesAlEdarah" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLRaeesMaglesAlEdarah_SelectedIndexChanged"
                    CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                    <asp:ListItem Value=""></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="WidthMaglis24" align="center">
                <div runat="server" id="IDKhatm" align="left" style="margin-top: 0px">
                    <img src="/ImgSystem/ImgSignature/الختم.png" width="100" />
                </div>
            </div>
        </td>
    </tr>
</table>