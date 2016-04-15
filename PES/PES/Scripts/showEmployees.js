$(document).ready(function () {

   
    $("#showEnables").click(function () {
     
    var enableChecked = $("#showEnables").is(':checked');
    //var disableChecked = $('#showDisables').is(':checked');
    //var bothChecked = $('#showBoth').checked;

    if (enableChecked) {
       
        //call ajax
        $.ajax({
            type: "POST",
            url: "/Employee/GetEnabledEmployees",
            contentType: 'application/json; charset=utf-8',
            data: {status: "enabled"},
            dataType: "json",
            success: function (data) {
                if (data) {
               
                    //var length = data.length;
                    var row = "";
                    $('#table-employees').children('tbody').remove();

                    if (data.employees.length > 0) {
                        // Loop data from ajax call
                        for (var i = 0; i < data.employees.length; i++) {
                            var employee = data.employees[i];
                            row += "<tr><td>" + employee.FirstName + "</td><td>" + employee.LastName + "</td><td>"
                            + employee.Email + "</td><td>" + "" + "</td><td>" + "" + "</td></tr>";
                        }

                        if (row != "") {
                            $('#table-employees').append(row);
                        }
                    }
                }
            }
        });
    }
    //else if(disableChecked){
        //$('#table-employees').children('tbody').remove();
        //$.getJSON('/Employee/GetEmployeesProifile?profile=', function (data) {

            //ciclo
        
       
    
    //}
    })


    });
