$(document).ready(function () {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        // Initial view: 'dayGridMonth', 'timeGridWeek', etc.
        initialView: 'dayGridMonth',
        // Initial date (optional)
        defaultDate: moment().format(),
        // Show multiple views:
        views: {
            dayGridMonth: { title: 'Month' },
            timeGridWeek: { title: 'Week' },
            timeGridDay: { title: 'Day' }
        },
        // ... other options as needed
    });
    calendar.render();
});