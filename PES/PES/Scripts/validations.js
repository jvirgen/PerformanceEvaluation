﻿$(document).ready(function () {
    var profile = parseInt($('#selectedProfile').val());
    // Execute validate profile function
    $.getJSON('/Employee/GetEmployeesProifile?profile=' + profile, function (data) {

        if (profile == 1) {
            $('#profileLabel').text("Manager");
            //Remove span option
            $("#select2-selectedManager-container").text("Select a Manager--");
            // Remove current options dropdown
            $('#selectedManager').children().remove();
            // Loop data from ajax call
            for (var i = 0; i < data.employees.length; i++) {
                var employee = data.employees[i];
                $('#selectedManager').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
            }
            $('#dropdownManager').show();



        }
        else if (profile == 2) {
            $('#profileLabel').text("Director");
            //Remove span option
            $("#select2-selectedManager-container").text("Select a Director--");
            // Remove current options dropdown
            $('#selectedManager').children().remove();
            // Loop data from ajax call
            for (var i = 0; i < data.employees.length; i++) {
                var employee = data.employees[i];
                $('#selectedManager').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
            }
            $('#dropdownManager').show();

        }
        else if (profile == 3) {
            // Remove current options dropdown
            $('#selectedManager').children().remove();
            $('#dropdownManager').hide();
        }
    });

    $('#selectedProfile').change(function () {
        var profile = parseInt($('#selectedProfile').val());

        $.getJSON('/Employee/GetEmployeesProifile?profile=' + profile, function (data) {

            if (profile == 1) {
                $('#profileLabel').text("Manager");
                //$('#profileLabel').show();
                //$('#selectedManager').show();
                //Remove span option
                $("#select2-selectedManager-container").text("Select a Manager--");
                // Remove current options dropdown
                $('#selectedManager').children().remove();
                // Loop data from ajax call
                for (var i = 0; i < data.employees.length; i++) {
                    var employee = data.employees[i];
                    $('#selectedManager').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
                }
                $('#dropdownManager').show();

               

            }
            else if (profile == 2) {
                $('#profileLabel').text("Director");
                //$('#profileLabel').show();
                //$('#selectedManager').show();
                //Remove span option
                $("#select2-selectedManager-container").text("Select a Director--");
                // Remove current options dropdown
                $('#selectedManager').children().remove();
                // Loop data from ajax call
                for (var i = 0; i < data.employees.length; i++) {
                    var employee = data.employees[i];
                    $('#selectedManager').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
                }
                $('#dropdownManager').show();
                
            }
            else if (profile == 3) {
                //$('#profileLabel').hide();
                // Remove current options dropdown
                $('#selectedManager').children().remove();
                $('#dropdownManager').hide();
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