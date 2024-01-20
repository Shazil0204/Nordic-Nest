// Data for the chart
var data = {
    labels: ["Traveling", "Food", "Clothing", "shopping"],
    datasets: [{
        data: [20, 35, 15, 50],
        backgroundColor: ["#3490dc", "#38a169", "#f6e05e", "#ff0000"],
    }]
};

// Chart configuration
var config = {
    type: 'doughnut', // Doughnut chart is similar to a pie chart
    data: data,
};

// Create the chart
var myChart = new Chart(
    document.getElementById('budgetChart'),
    config
);