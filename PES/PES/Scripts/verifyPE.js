// Events 
$("#loadFile").on("click", function (e) {
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
            override = confirm("This Performance Evaluation File has already been uploaded. Do you want to replace it?");
            if (override) {
                $("#Replace").val("true");
                $("#loadFile").attr("value","Overwrite File")
            }
            else {
                $("#Replace").val("false");
                $("#loadFile").attr("value", "Load File")
            }
        }
    });
}