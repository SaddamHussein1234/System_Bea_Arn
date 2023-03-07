<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PageMonthlyStipend.aspx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Chart_PageMonthlyStipend" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <script src="/Chart/code/highcharts.js"></script>
        <script src="/Chart/code/modules/series-label.js"></script>
        <script src="/Chart/code/modules/exporting.js"></script>
        <script src="/Chart/code/modules/export-data.js"></script>
        <div id="container" style="min-width: 310px; height: 400px; margin: 0 auto"></div>

        <script type="text/javascript">
            Highcharts.chart('container', {
                chart: {
                    type: 'spline'
                },
                title: {
                    text: '<%= XNameCompany %>'
                },
                subtitle: {
                    text: 'SADDAM HUSSEIN'
                },
                xAxis: {
                    categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
                        'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
                },
                yAxis: {
                    title: {
                        text: ''
                    },
                    labels: {
                        formatter: function () {
                            return this.value;
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
                    name: 'رواتب',
                    marker: {
                        symbol: 'square'
                    },
                    data: [<%= FCountMonthlyStipend("1")%>, <%= FCountMonthlyStipend("2")%>, <%= FCountMonthlyStipend("3")%>, <%= FCountMonthlyStipend("4")%>, <%= FCountMonthlyStipend("5")%>, <%= FCountMonthlyStipend("6")%>,
                        <%= FCountMonthlyStipend("7")%>, <%= FCountMonthlyStipend("8")%>, <%= FCountMonthlyStipend("9")%>, <%= FCountMonthlyStipend("10")%>, <%= FCountMonthlyStipend("11")%>, <%= FCountMonthlyStipend("12")%>]

                }, {
                        name: 'إنتداب',
                        marker: {
                            symbol: 'diamond'
                        },
                        data: [<%= FCountMandate("1")%>, <%= FCountMandate("2")%>, <%= FCountMandate("3")%>, <%= FCountMandate("4")%>, <%= FCountMandate("5")%>,
                        <%= FCountMandate("6")%>,<%= FCountMandate("7")%>, <%= FCountMandate("8")%>, <%= FCountMandate("9")%>,
                        <%= FCountMandate("10")%>, <%= FCountMandate("11")%>, <%= FCountMandate("12")%>]
                    }
                    , {
                        name: 'إضافي',
                        marker: {
                            symbol: 'diamond'
                        },
                        data: [<%= FCountOverTime("1")%>, <%= FCountOverTime("2")%>, <%= FCountOverTime("3")%>, <%= FCountOverTime("4")%>, <%= FCountOverTime("5")%>,
                        <%= FCountOverTime("6")%>,<%= FCountOverTime("7")%>, <%= FCountOverTime("8")%>, <%= FCountOverTime("9")%>,
                        <%= FCountOverTime("10")%>, <%= FCountOverTime("11")%>, <%= FCountOverTime("12")%>]
                }]
            });
        </script>
    </form>
</body>
</html>
