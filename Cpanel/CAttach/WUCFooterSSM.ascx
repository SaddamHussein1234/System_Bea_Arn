<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCFooterSSM.ascx.cs" Inherits="Cpanel_CAttach_WUCFooterSSM" %>

<table style="width: 100%; font-family: 'Co'; font-size:12px;">
    <tr>
        <td>
            <div class="WidthMaglis" align="center" runat="server" visible="false">
                الباحث الإجتماعي
                <br />
                <asp:Image ID="ImgAlBaheth" runat="server" Width='100px' Height='25' />
                <br />
                <asp:Label ID="lblAlBaheth" runat="server"></asp:Label>
                <asp:DropDownList ID="DLAlBaheth" runat="server" ValidationGroup="g2" Width="100%"
                    CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                    <asp:ListItem Value=""></asp:ListItem>
                </asp:DropDownList>
            </div>
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
                <span style="font-size:14px;"> لجنة البحث الاجتماعي </span>
                <br />
                <asp:Image ID="ImgRaeesLagnatAlBahath" runat="server" Width='100px' Height='25' />
                <br />
                <asp:Label ID="lblRaeesLagnatAlBahath" runat="server"></asp:Label>
                <asp:DropDownList ID="DLRaeesLagnatAlBahath" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLRaeesLagnatAlBahath_SelectedIndexChanged"
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
