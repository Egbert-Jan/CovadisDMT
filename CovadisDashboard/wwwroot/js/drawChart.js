google.charts.load('current', { 'packages': ['corechart'] });
google.charts.setOnLoadCallback(drawChart);

function drawChart() {
    
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Correct/Incorrect');
    data.addColumn('number', 'Uptime');
    data.addRows([
        ['Correct', parseInt(document.getElementById('correct').innerHTML)],
        ['Incorrect', parseInt(document.getElementById('incorrect').innerHTML)]
    ]);

    var options = {
        title: 'Uptime/downtime'
    };

    var chart = new google.visualization.PieChart(document.getElementById('piechart'));

    chart.draw(data, options);
}