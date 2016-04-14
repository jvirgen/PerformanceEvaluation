$(document).ready(function () {
    var enableCheck = $('#showEnables').checked;
    var disableCheck = $('#showDisables').checked;

    if (enableCheck) {
        

        //call ajax
        $.ajax({
            type: "POST",
            url: "Employees/GetEnabledEmployees",
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data)
                    $('#table-employees').children('tbody').remove();
            }        
        });
    }
    else if(disableCheck){
        //$('#table-employees').children('tbody').remove();
        //$.getJSON('/Employee/GetEmployeesProifile?profile=', function (data) {

            //ciclo
        
       
    
    }

    });
