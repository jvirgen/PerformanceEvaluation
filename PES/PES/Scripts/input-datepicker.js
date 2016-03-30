$(document).ready(function () {
    $(".jqueryui-marker-datepicker").datepicker({
        dateFormat: "dd/mm/y",
        changeMonth: true,
        changeYear: true,
        showOn: "button"
    }).css("display", "inline-block")
    .next("button").button({
        icons: { primary: "ui-icon-calendar" },
        label: "Select a date",
        text: false
    });
    $(".jqueryui-marker-datepicker").datepicker("setDate", new Date()).attr('readonly', 'readonly');
    $(".jqueryui-marker-datepicker").readonlyDatepicker(true);
});