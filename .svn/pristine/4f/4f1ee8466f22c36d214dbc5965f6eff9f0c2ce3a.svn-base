$(document).ready(function () {

    // <!-- JQVMap -->
    //$('#world-map-gdp').vectorMap({
    //    map: 'world_en',
    //    backgroundColor: null,
    //    color: '#ffffff',
    //    hoverOpacity: 0.7,
    //    selectedColor: '#666666',
    //    enableZoom: true,
    //    showTooltip: true,
    //    values: sample_data,
    //    scaleColors: ['#E6F2F0', '#149B7E'],
    //    normalizeFunction: 'polynomial'
    //});


    // <!-- Doughnut Chart -->
    var options = {
        legend: false,
        responsive: false
    };

    new Chart(document.getElementById("canvas1"), {
        type: 'doughnut',
        tooltipFillColor: "rgba(51, 51, 51, 0.55)",
        data: {
            labels: [
                "Symbian",
                "Blackberry",
                "Other",
                "Android",
                "IOS"
            ],
            datasets: [{
                data: [15, 20, 30, 10, 30],
                backgroundColor: [
                    "#BDC3C7",
                    "#9B59B6",
                    "#E74C3C",
                    "#26B99A",
                    "#3498DB"
                ],
                hoverBackgroundColor: [
                    "#CFD4D8",
                    "#B370CF",
                    "#E95E4F",
                    "#36CAAB",
                    "#49A9EA"
                ]
            }]
        },
        options: options
    });
});