$(document).ready(function () {
    $('#selectedProfile').change(function () {
        var profile = parseInt($('#selectedProfile').val());

        $.getJSON('/Employee/GetEmployeesProifile/' + profile, function (data) {

            if (profile == 1) {
                $('#profileLabel').val("Manager");
            }
            else if (profile == 2) {
                $('#profileLabel').val("Director");
            }
            else if (profile == 3) {
                $('#profileLabel').hide();
                $('#selectedManager').hide();
            }

            // Remove current options dropdown
            $('#selectedManager').children().remove();

            $('#selectedManager').show();

            // Loop data from ajax call
            for (var i = 0; i < data.employees.length; i++) {
                var employee = data.employees[i];

                $('#selectedManager').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");

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