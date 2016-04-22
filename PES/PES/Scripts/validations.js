$(document).ready(function () {
    var profile = parseInt($('#selectedProfile').val());
    var email = $('#Email').val();
    var newProfile = 0;
    
    if (profile == 1) {
        // Resource was selected
        newProfile = 2; // Manager
    } else if (profile == 2) {
        // Manager was selected
        newProfile = 3; // Director
    } else if (profile == 3) {
        // Director was selected
        newProfile = 2; // Director
    }

    // Execute validate profile function
    $.getJSON('/Employee/GetEmployeesProifile?profile=' + newProfile + "&email=" + email ,function (data) {

        if (profile == 1) {
            $('#profileLabel').text("New Manager");
            // Remove current options dropdown
            $('#selectedManager').children().remove();
            $('#selectedNewManager').children().remove();
            // Loop data from ajax call
            for (var i = 0; i < data.employees.length; i++) {
                var employee = data.employees[i];
                $('#selectedManager').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
                $('#selectedNewManager').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
            }
            $('#dropdownManager').show();
            $('#dropdownNewManager').show();



        }
        else if (profile == 2) {
            $('#profileLabel').text("New Director");
            // Remove current options dropdown
            $('#selectedManager').children().remove();
            $('#selectedNewManager').children().remove();
            $('#selectedManager').append("<option value='0'>Select a Director</option>");
                $('#selectedNewManager').append("<option value='0'>Select a Director</option>");
            // Loop data from ajax call
            for (var i = 0; i < data.employees.length; i++) {
                var employee = data.employees[i];
                $('#selectedManager').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
                $('#selectedNewManager').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
            }
            $('#dropdownManager').show();
            $('#dropdownNewManager').show();
        }
        else if (profile == 3) {
            // Remove current options dropdown
            $('#selectedManager').children().remove();
            $('#selectedNewManager').children().remove();
            $('#dropdownManager').hide();
            $('#dropdownNewManager').hide();
        }
            
    });

    $('#selectedProfile').change(function () {
        var profile = parseInt($('#selectedProfile').val());
        var email = $('#Email').val();
        var newProfile = 0;

        if (profile == 1) {
            // Resource was selected
            newProfile = 2; // Manager
        } else if (profile == 2) {
            // Manager was selected
            newProfile = 3; // Director
        } else if (profile == 3) {
            // Director was selected
            newProfile = 2; // Director
        }

        $.getJSON('/Employee/GetEmployeesProifile?profile=' + newProfile + "&email=" + email, function (data) {

            if (profile == 1) {
                $('#profileLabel').text("New Manager");
                // Remove current options dropdown
                $('#selectedManager').children().remove();
                $('#selectedNewManager').children().remove();
                // Loop data from ajax call
                for (var i = 0; i < data.employees.length; i++) {
                    var employee = data.employees[i];
                    $('#selectedManager').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
                    $('#selectedNewManager').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
                }
                $('#dropdownManager').show();
                $('#dropdownNewManager').show();

            }
            else if (profile == 2) {
                $('#profileLabel').text("New Director");
                 //Remove current options dropdown
                $('#selectedManager').children().remove();
                $('#selectedNewManager').children().remove();
                // Loop data from ajax call
                for (var i = 0; i < data.employees.length; i++) {
                    var employee = data.employees[i];
                    $('#selectedManager').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
                    $('#selectedNewManager').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
                }
                $('#dropdownManager').show();
                $('#dropdownNewManager').show();
                
            }
            else if (profile == 3) {
                //$('#profileLabel').hide();
                // Remove current options dropdown
                if (data.hasOrg > 1) {
                    $('#selectedManager').children().remove();
                    $('#dropdownManager').hide();
                    // Remove current options dropdown
                    $('#selectedNewManager').children().remove();
                    // Loop data from ajax call
                    for (var i = 0; i < data.employees.length; i++) {
                        var employee = data.employees[i];
                        $('#selectedNewManager').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
                    }
                    $('#dropdownNewManager').show();
                }
                else {
                    $('#selectedManager').children().remove();
                    $('#selectedNewManager').children().remove();
                    $('#dropdownManager').hide();
                    $('#dropdownNewManager').hide();
                }
            }
        });
    });

    //$.ajax({
    //  url: '',
    //  data: datos_formulario,
    //  type: 'GET',
    //  dataType: 'json',
    //  success: function(datos) {
    //    $('#resultados').text(JSON.stringify(datos, null, 4));
    //    $('#respuesta').text(datos.respuesta).fadeIn('slow');
    //  }
    //});
});