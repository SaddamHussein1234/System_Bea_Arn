<%@ Page Language='C#' AutoEventWireup='true' CodeFile='test.aspx.cs' Inherits='Cpanel_test' %>

<!DOCTYPE html>

<html xmlns='http://www.w3.org/1999/xhtml'>
<head runat='server'>
    <title></title>
    <script>
        Highcharts.chart('container', {
            chart: {
                type: 'spline'
            },
            title: {
                text: 'Monthly Average Temperature'
            },
            subtitle: {
                text: 'Source: WorldClimate.com'
            },
            xAxis: {
                categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
                    'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
            },
            yAxis: {
                title: {
                    text: 'Temperature'
                },
                labels: {
                    formatter: function () {
                        return this.value + '°';
                    }
                }
            },
            tooltip: {
                crosshairs: true,
                shared: true
            },
            plotOptions: {
                spline: {
                    marker: {
                        radius: 4,
                        lineColor: '#666666',
                        lineWidth: 1
                    }
                }
            },
            series: [{
                name: 'Tokyo',
                marker: {
                    symbol: 'square'
                },
                data: [7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, {
                    y: 26.5,
                    marker: {
                        symbol: 'url(https://www.highcharts.com/samples/graphics/sun.png)'
                    }
                }, 23.3, 18.3, 13.9, 9.6]

            }, {
                name: 'London',
                marker: {
                    symbol: 'diamond'
                },
                data: [{
                    y: 3.9,
                    marker: {
                        symbol: 'url(https://www.highcharts.com/samples/graphics/snow.png)'
                    }
                }, 4.2, 5.7, 8.5, 11.9, 15.2, 17.0, 16.6, 14.2, 10.3, 6.6, 4.8]
            }]
        });
    </script>
</head>
<body>
    <form id='form2' runat='server'>
        <div class="col-lg-3 col-md-3 col-sm-6" style="background-color:aquamarine">
            <a href="Default.aspx" data-toggle="tooltip" title="الرئيسية">
                <div class="tile">
                    <div class="tile-heading">
                        الرئيسية <span class="pull-right"></span>
                    </div>
                    <div class="tile-body">
                        <i class="fa fa-home">Saddam</i>
                        <h2 class="pull-right">Hussein</h2>
                    </div>
                    <div class="tile-footer">Hi</div>
                </div>
            </a>
        </div>
        <hr /><hr /><hr /><hr />
        <div>
            Enter Value:
            <asp:TextBox ID="txtnumberw" runat="server" /><br />
            <asp:Button ID="btnClick" runat="server" Text="Convert" OnClick="btnClick_Click" Visible="false" /><br />
            <label id="lblmsg" runat="server" />
        </div>
        <hr /><hr /><hr /><hr />
        <asp:DropDownList ID="cboCurrency" runat="server">
            <asp:ListItem></asp:ListItem>
            <asp:ListItem Value="0">Syrian Pound</asp:ListItem>
            <asp:ListItem Value="1">UAE Dirham</asp:ListItem>
            <asp:ListItem Value="2">Saudi Riyal</asp:ListItem>
            <asp:ListItem Value="3">Tunisian Dinar</asp:ListItem>
            <asp:ListItem Value="4">Gram</asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:TextBox ID="txtNumber" runat="server" OnTextChanged="txtNumber_TextChanged" AutoPostBack="true"></asp:TextBox>
        <br />
        <asp:TextBox ID="txtEnglishWord" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
        <br />
        <asp:TextBox ID="txtArabicWord" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
        <hr /><hr /><hr /><hr />
        <asp:Repeater ID="RPTMostafeed" runat="server">
            <ItemTemplate>
                <%# Eval("NameMostafeed") %><br />
            </ItemTemplate>
        </asp:Repeater>
        <hr /><hr /><hr />
        <asp:Label ID="lblDecrypt" runat="server"></asp:Label>
        <hr /><hr /><hr />
        <asp:Label ID="lblDecamal" runat="server" Text="0"></asp:Label>
        <hr /><hr /><hr />
    </form>
</body>
</html>
