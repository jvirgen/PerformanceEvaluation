$(document).ready(function () {

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

function verifyPEfile() {

    $.getJSON('/PerformanceEvaluation/VerifyPE?year=' + year + "&period=" + period + "&employee=" + employee + "&evaluator=" + evaluator, function (data) {

        if (booleana) {
            // si es igual, preguntar        
        }
        else {
            //si no es igual, sobreescribir
        }
    });
}