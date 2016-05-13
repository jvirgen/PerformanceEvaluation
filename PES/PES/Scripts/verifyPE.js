// Events 
$("#SelectedYear").on("change", function (e) {
    var year = parseInt($('#SelectedYear').val());
    var period = parseInt($('#SelectedPeriod').val());
    var employee = parseInt($("#SelectedEmployee").val());
    var evaluator = parseInt($("#SelectedEvaluator").val());
    // Execute function 
    verifyPEfile(employee, evaluator, period, year);
});

$("#SelectedPeriod").on("change", function (e) {
    var year = parseInt($('#SelectedYear').val());
    var period = parseInt($('#SelectedPeriod').val());
    var employee = parseInt($("#SelectedEmployee").val());
    var evaluator = parseInt($("#SelectedEvaluator").val());
    // Execute function 
    verifyPEfile(employee, evaluator, period, year);
});

$("#SelectedEmployee").on("change", function (e) {
    var year = parseInt($('#SelectedYear').val());
    var period = parseInt($('#SelectedPeriod').val());
    var employee = parseInt($("#SelectedEmployee").val());
    var evaluator = parseInt($("#SelectedEvaluator").val());
    // Execute function 
    verifyPEfile(employee, evaluator, period, year);
});

$("#SelectedEvaluator").on("change", function (e) {
    var year = parseInt($('#SelectedYear').val());
    var period = parseInt($('#SelectedPeriod').val());
    var employee = parseInt($("#SelectedEmployee").val());
    var evaluator = parseInt($("#SelectedEvaluator").val());
    // Execute function 
    verifyPEfile(employee, evaluator, period, year);
});

function verifyPEfile(employee, evaluator, period, year) {

    $.getJSON('/PerformanceEvaluation/VerifyPE?employee='+ employee +'&&evaluator='+ evaluator +'&&period='+ period +'&&year=' + year, function (data) {

        if (data.exist = true && data.idPe != 0) {
            override = confirm("This Perfornace Evalauaton already has been uploaded. Do you want to replace it?");
            if (override) {
                //code here to replace
            }
            else {
                //Code here to do nothing
            }
        }
    });
}