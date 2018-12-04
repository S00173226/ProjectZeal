$(document).ready(function () {
    $('.timepicker').timepicker({
        timeFormat: 'H:mm',
        interval: 15,
        minTime: '00:00:00',
        maxTime: '23:59:00',
        defaultTime: '10',
        dynamic: false,
        dropdown: true,
        scrollbar: true
    });
});