$(document).ready(function () {

    var year = parseInt($('#selectedYear').val());
    var period = parseInt($('#selectedPeriod').val());

    // Events 
    $("#selectedYear").on("change", function (e) {
        // Execute function 

        // obtener true o false
        verifyPEfile();
    });

    $("#selectedPeriod").on("change", function (e) {
        // Execute function 

        verifyPEfile();
    });
}
);

function verifyPEfile(valorbooleana) {

    $.getJSON('/Employee/GetPE?option=' + option ,function (data) {

        if (booleana) {
      
        }
        else {
            //si no es igual, sobreescribir
        }
    });
}