$(document).ready(function () {
    $("DisableOption").click(function () {
        var email = ($(this).parent().siblings().first().next().next().text().trim());

        $.getJSON('/Employee/GetEmployeeStatus?email=' + email, function (data) {

            if (data.employees.EndDate != null) {
                $(this).text("Enable");
                $(this).parent().siblings().text().css({ "text-decoration": "line-through" });
            }
            else if (data.employees.EndDate == null) {
                $(this).text("Disable");
                $(this).parent().siblings().text().css({ "text-decoration": "none" });
               
            }

        });
    });
});