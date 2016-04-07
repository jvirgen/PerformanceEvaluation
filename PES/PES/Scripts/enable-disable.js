$(document).ready(function () {
    $('#DisableOption').click(function () {
        var email = parseInt($(this).parent().siblings().first().next().next());

        $.getJSON('/Employee/GetEmployeesProifile?email=' + profile, function (data) {

            if (profile == 1) {
                $('#profileLabel').text("Manager");
                $('#profileLabel').show();
                $('#selectedManager').show();
                // Remove current options dropdown
                $('#selectedManager').children().remove();
                // Loop data from ajax call
                for (var i = 0; i < data.employees.length; i++) {
                    var employee = data.employees[i];
                    $('#selectedManager').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
                }

            }
            else if (profile == 2) {
                $('#profileLabel').text("Director");
                $('#profileLabel').show();
                $('#selectedManager').show();
                // Remove current options dropdown
                $('#selectedManager').children().remove();
                // Loop data from ajax call
                for (var i = 0; i < data.employees.length; i++)
                {
                    var employee = data.employees[i];
                    $('#selectedManager').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
                }
            }
            else if (profile == 3) {
                $('#profileLabel').hide();
                $('#selectedManager').hide();
            }
        });
    });