document.addEventListener('DOMContentLoaded', function () {
    const ctx2 = document.getElementById('chartColumnCanvas').getContext('2d');

    const chartData2 = {
        labels: chartData.labels,
        datasets: [{
            label: "Daily Money Usage",
            data: chartData.data,
            backgroundColor: function (context) {
                var value = context.dataset.data[context.dataIndex];
                return value <= 50 ? 'blue' :
                    value <= 100 ? 'green' :
                        value <= 200 ? 'yellow' :
                            value <= 500 ? 'orange' :
                                'purple';
            },
            borderColor: 'white',
            borderWidth: 2,
        }],
    };

    var chartColumn = new Chart(ctx2, {
        type: "bar",
        data: chartData2,
        options: {
            maintainAspectRatio: false,
            layout: {
                padding: {
                    bottom: 30
                }
            },
            scales: {
                x: {
                    ticks: {
                        autoSkip: true,
                        maxRotation: 0,
                        minRotation: 0
                    }
                },
                y: {
                    beginAtZero: true
                }
            },
            plugins: {
                tooltip: {
                    callbacks: {
                        label: function (context) {
                            const label = context.dataset.label || '';
                            return label + ': ' + context.parsed.y + ' DKK';
                        }
                    }
                }
            },
        },
    });
});
