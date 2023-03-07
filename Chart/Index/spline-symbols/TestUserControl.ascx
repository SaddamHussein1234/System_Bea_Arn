<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TestUserControl.ascx.cs" Inherits="Chart_Index_spline_symbols_TestUserControl" %>

<%--<script src="../../code/highcharts.js"></script>
<script src="../../code/modules/series-label.js"></script>
<script src="../../code/modules/exporting.js"></script>
<script src="../../code/modules/export-data.js"></script>--%>
<script src="../Chart/code/highcharts.js"></script>
    <script src="../Chart/code/modules/series-label.js"></script>
    <script src="../Chart/code/modules/exporting.js"></script>
    <script src="../Chart/code/modules/export-data.js"></script>

<div id="container" style="min-width: 310px; height: 400px; margin: 0 auto"></div>

<script type="text/javascript">
    Highcharts.chart('container', {
        chart: { type: 'spline' },
        title: { text: 'إحصائية المعرض' },
        subtitle: { text: 'تقنية المعلومات لاجلك' },
        xAxis: { categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'] },
        yAxis: { title: { text: 'SADDAM HUSSEIN' }, labels: { formatter: function () { return this.value + '°'; } } },
        tooltip: { crosshairs: true, shared: true },
        plotOptions: { spline: { marker: { radius: 4, lineColor: '#666666', lineWidth: 1 } } },
        series: [{
            name: 'سيارات', marker: { symbol: 'square' },
            //data: [7.2, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, {y: 26.5,marker: {symbol: 'url(https://www.highcharts.com/samples/graphics/sun.png)'}}, 23.3, 18.3, 13.9, 9.6]
            data: [7.2, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6]
        }, {
            name: 'عقود', marker: { symbol: 'diamond' },
            //data: [{y: 3.9,marker: {symbol: 'url(https://www.highcharts.com/samples/graphics/snow.png)'}}, 4.2, 5.7, 8.5, 11.9, 15.2, 17.0, 16.6, 14.2, 10.3, 6.6, 4.8]
            //data: [3.9, 4.2, 5.7, 8.5, 11.9, 15.2, 17.0, 16.6, 14.2, 10.3, 6.6, 4.8]
            data: [<%=Label1.Text %>, 4.2, 5.7, 8.5, 11.9, 15.2, 17.0, 16.6, 14.2, 10.3, 6.6, 4.8]
                }]
            });
		</script>
<hr />
<hr />
<asp:Label ID="Label1" runat="server"></asp:Label>