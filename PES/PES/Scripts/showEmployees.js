$(document).ready(function () {
    var enableCheck = $('#showEnables').checked;
    var disableCheck = $('#showDisables').checked;

    if (enableCheck) {
        $('#table-employees').children('tbody').remove();
        $.getJSON('/Employee/GetEnabledEmployees', function (data) {

        
        });
    }
    else if(disableCheck){
        $('#table-employees').children('tbody').remove();
        $.getJSON('/Employee/GetEmployeesProifile?profile=', function (data) {

            //ciclo
        
        });
    
    }

    });
});