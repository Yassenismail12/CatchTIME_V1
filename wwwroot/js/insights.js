document.addEventListener('DOMContentLoaded', function () {
    // Productivity Chart
    var ctx = document.getElementById('productivityChart').getContext('2d');
    var productivityChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
            datasets: [{
                label: 'Productivity',
                data: [65, 59, 80, 81, 56, 55, 40],
                backgroundColor: 'rgba(60, 145, 230, 0.2)',
                borderColor: 'rgba(60, 145, 230, 1)',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });

    // Tasks Completion Charts
    var dayCtx = document.getElementById('dayCompletionChart').getContext('2d');
    var monthCtx = document.getElementById('monthCompletionChart').getContext('2d');
    var yearCtx = document.getElementById('yearCompletionChart').getContext('2d');

    var dayCompletionChart = new Chart(dayCtx, {
        type: 'bar',
        data: {
            labels: ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'],
            datasets: [{
                label: 'Tasks Completed',
                data: [12, 19, 3, 5, 2, 3, 7],
                backgroundColor: 'rgba(60, 145, 230, 0.2)',
                borderColor: 'rgba(60, 145, 230, 1)',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });

    var monthCompletionChart = new Chart(monthCtx, {
        type: 'bar',
        data: {
            labels: ['Week 1', 'Week 2', 'Week 3', 'Week 4'],
            datasets: [{
                label: 'Tasks Completed',
                data: [20, 30, 50, 40],
                backgroundColor: 'rgba(60, 145, 230, 0.2)',
                borderColor: 'rgba(60, 145, 230, 1)',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });

    var yearCompletionChart = new Chart(yearCtx, {
        type: 'bar',
        data: {
            labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
            datasets: [{
                label: 'Tasks Completed',
                data: [50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 160],
                backgroundColor: 'rgba(60, 145, 230, 0.2)',
                borderColor: 'rgba(60, 145, 230, 1)',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });

    // Tab functionality for Tasks Completion
    function openTab(evt, tabName) {
        var i, tabcontent, tablinks;
        tabcontent = document.getElementsByClassName("tabcontent");
        for (i = 0; i < tabcontent.length; i++) {
            tabcontent[i].style.display = "none";
        }
        tablinks = document.getElementsByClassName("tablink");
        for (i = 0; i < tablinks.length; i++) {
            tablinks[i].className = tablinks[i].className.replace(" active", "");
        }
        document.getElementById(tabName).style.display = "block";
        evt.currentTarget.className += " active";
    }

    // Set default tab to open
    document.querySelector('.tablink').click();

    // Recommendations
    var recommendations = [
        "Take regular breaks to stay productive.",
        "Use a task management tool to stay organized.",
        "Set realistic goals and deadlines.",
        "Review and reflect on your progress regularly.",
        "Stay positive and motivated."
    ];

    var recommendationsList = document.getElementById('recommendationsList');
    recommendations.forEach(function (recommendation) {
        var listItem = document.createElement('li');
        listItem.textContent = recommendation;
        recommendationsList.appendChild(listItem);
    });
});
