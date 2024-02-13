// Define a function to initialize the progress bars
function initializeProgressBars(labels, data, backgroundColors) {
    document.addEventListener("DOMContentLoaded", function () {
        var ctx = document.getElementById("donutChart").getContext("2d");
        var donutChart = new Chart(ctx, {
            type: "doughnut",
            data: {
                labels: labels,
                datasets: [
                    {
                        data: data,
                        backgroundColor: backgroundColors,
                    },
                ],
            },
            options: {
                cutout: "30%",
                plugins: {
                    legend: {
                        display: false,
                    },
                },
            },
        });
    });

}