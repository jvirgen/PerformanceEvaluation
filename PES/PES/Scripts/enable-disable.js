$(document).ready(function () {
    $('#DisableOption').click(function () {
        var email = parseInt($(this).parent().siblings().first().next().next().text());

        $.getJSON('/Employee/GetEmployeeStatus?email=' + email, function (data) {

            if (data.EndDate != null) {
                $(this).parent().siblings().text().css()
                $('#profileLabel').show();
                $('#selectedManager').show();
                // Remove current options dropdown
                $('#selectedManager').children().remove();
            }
            else if (data.EndDate == null) {
                $('#profileLabel').text("Director");
                $('#profileLabel').show();
                $('#selectedManager').show();
                // Remove current options dropdown
                $('#selectedManager').children().remove();
            }

        });
    });
});