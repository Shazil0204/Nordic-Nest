//// Data for the chart
//var data = {
//    labels: ["Traveling", "Food", "Clothing", "shopping", "cartoons"],
//    datasets: [{
//        data: [20, 35, 15, 50, 20],
//        backgroundColor: ["#3490dc", "#38a169", "#f6e05e", "#ff0000", "#343434"],
//    }]
//};

//// Chart configuration
//var config = {
//    type: 'doughnut', // Doughnut chart is similar to a pie chart
//    data: data,
//};

//// Create the chart
//var myChart = new Chart(
//    document.getElementById('budgetChart'),
//    config
//);

// Define a function to initialize the progress bars
function initializeProgressBars(labels, data, backgroundColors) {
    // Data for the chart
    var chartData = {
        labels: labels,
        datasets: [{
            data: data,
            backgroundColor: backgroundColors,
        }]
    };

    // Chart configuration
    var config = {
        type: 'doughnut', // Doughnut chart is similar to a pie chart
        data: chartData,
    };

    // Create the chart
    var myChart = new Chart(
        document.getElementById('budgetChart'),
        config
    );
}