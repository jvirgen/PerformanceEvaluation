﻿function select(x) {
    x.style.color = "#428bca";
}
function unselect(x) {
    x.style.color = "#333333";
}

function getValues(x) {
    var period = $(x).find("#period").val();
    var year = $(x).siblings().find("#year").val();
    getReport(period, year);
}

function getReport(period, year) {
    var tableOptions = {
        dom: '<"row"<"col-md-12"B>><"row"<"col-md-6"l><"col-md-6"f>>rtip',
        buttons: [
            'excel', 'pdf'],
        lengthMenu: [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]],
        pageLength: 10,
        order: [[6, "desc"]],
    };

    $.ajax({
        url: "/PerformanceEvaluation/GetHistoricalReport",
        data: {
            period : period,
            year : year
        }
    })
    .done(function (data) {
        if (data) {
            //success ajax
            // Populate table with data from ajax call
            $("#performancePartialPeriods").html("");
            $("#performancePartialPeriods").html(data).fadeOut("fast").fadeIn("slow");

            // Re init datatable
            $("#TableviewPeriods").DataTable(tableOptions);
        }
        else {
            // Error
            alert("Error while getting employees");
        }
    })
    .fail(function (jqxhr, textStatus, error) {
        alert("Error while getting employees. Please try again later.");
    })
    .always(function () {
        //alert("finished");
    });
}