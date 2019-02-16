$(function () {
    var dateFormat = "dd/mm/yy",
        from = $(".from")
            .datepicker({
                dateFormat:'dd/mm/yy',
                defaultDate: "+1w",
                changeMonth: true,
                showAnim: "blind"
            })
            .on("change", function () {
                to.datepicker("option", "minDate", getDate(this));
            }),
        to = $(".to").datepicker({
            dateFormat:'dd/mm/yy',
            defaultDate: "+1w",
            changeMonth: true,
            showAnim: "blind"
        })
            .on("change", function () {
                from.datepicker("option", "maxDate", getDate(this));
            });

    function getDate(element) {
        var date;
        try {
            date = $.datepicker.parseDate(dateFormat, element.value);  
        } catch (error) {
            date = null;
        }

        return date;
    }
});