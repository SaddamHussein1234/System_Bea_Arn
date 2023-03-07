<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Chart_Index_spline_symbols_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <script src="../../code/highcharts.js"></script>
        <script src="../../code/modules/series-label.js"></script>
        <script src="../../code/modules/exporting.js"></script>
        <script src="../../code/modules/export-data.js"></script>

        <div id="container" style="min-width: 310px; height: 400px; margin: 0 auto"></div>

        <script type="text/javascript">
            Highcharts.chart('container', {
                chart: {type: 'spline'},
                title: {text: 'إحصائية '},
                subtitle: {text: 'تقنية المعلومات لاجلك'},
                xAxis: {categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun','Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']},
                yAxis: {title: {text: ''},labels: {formatter: function () {return this.value + '°';}}},
                tooltip: {crosshairs: true,shared: true},
                plotOptions: {spline: {marker: {radius: 4,lineColor: '#666666',lineWidth: 1}}},
                series: [{
                    name: 'مثال1', marker: { symbol: 'square' },
                    data: [7.2, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2,26.5, 23.3, 18.3, 13.9, 9.6]
                },
                {
                    name: 'مثال2', marker: { symbol: 'diamond' },
                    data: [3.9, 4.2, 5.7, 8.5, 11.9, 15.2, 17.0, 16.6, 14.2, 10.3, 6.6, 4.8]
                },
                {
                    name: 'مثال3', marker: { symbol: 'circle' },
                    data: [13, 16, 18, 23, 28, 33, 32, 30, 26, 22, 18, 14]
                }]
            });
		</script>
    </form>
</body>
</html>
