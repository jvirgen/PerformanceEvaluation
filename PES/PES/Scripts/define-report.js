$(document).ready(function () {
    $("#Tableview").DataTable();

    // Events
    $("#general").on("click", function (e) {
        // Execute function 

        // Filter 
        var filter = ;
        // Option
        var option = ;

        validateFilters(filter, option);
    });

    $("#dropdownLocation").on("click", function (e) {
        // Execute function 

        // Filter 
        var filter = ;
        // Option
        var option = ;

        validateFilters(filter, option);
    });

    $("#dropdownManager").on("click", function (e) {
        // Execute function 

        // Filter 
        var filter = ;
        // Option
        var option = ;

        validateFilters(filter, option);
    });

    $("#dropdownDirector").on("click", function (e) {
        // Execute function 

        // Filter 
        var filter = ;
        // Option
        var option = ;

        validateFilters(filter, option);
    });
});
var radioGeneral = $("#general");
var radioFilter = $("#filter");
var comboFilter = $("#dropdownFilter");
var comboLocation = $("#dropdownLocation");
var comboManager = $("#dropdownManager");
var comboDirector = $("#dropdownDirector");

$(radioGeneral).click(function () {
    checkGeneral();
});

$(radioFilter).click(function () {
    checkLocation();
});

comboFilter.change(function () {
    showoFilterOptions(comboFilter.val());
});

function generateReport(idDropDown) {
    if (idDropDown == 1) {
        filteredReport(comboFilter.val(), comboLocation.val());
    }
    else if (idDropDown == 2) {
        filteredReport(comboFilter.val(), comboManager.val());
    }
    else {
        filteredReport(comboFilter.val(), comboDirector.val());
    }
}

function checkGeneral() {
    $(radioGeneral).addClass("ckd");
    $(radioFilter).removeClass("ckd");
    showCombo();
}

function checkLocation() {
    $(radioFilter).addClass("ckd");
    $(radioGeneral).removeClass("ckd");
    showCombo();
}

function showCombo() {
    if ($(radioFilter).hasClass("ckd")) {
        comboFilter.fadeIn();
        showoFilterOptions(comboFilter.val());
    }
    else {
        comboFilter.fadeOut();
        showoFilterOptions(0);
    }
}

function showoFilterOptions(idFilter) {
    if (idFilter == 1) {
        comboManager.hide();
        comboDirector.hide();
        $("#labelOptions").text("Select Location");
        comboLocation.show();
        filteredReport(idFilter, comboLocation.val());
    }
    else if (idFilter == 2) {
        comboLocation.hide();
        comboDirector.hide();
        $("#labelOptions").text("Select Manager");
        comboManager.show();
        filteredReport(idFilter, comboManager.val());
    }
    else if (idFilter == 3) {
        comboLocation.hide();
        comboManager.hide();
        $("#labelOptions").text("Select Director");
        comboDirector.show();
        filteredReport(idFilter, comboDirector.val());
    }
    else {
        comboLocation.hide();
        comboManager.hide();
        comboDirector.hide();
        $("#labelOptions").text("");
    }
}

function validateFilters(FilterId, OptionId) {
    $.ajax({
        url: "/Employee/GetEmployeesByFilter",
        data: {
            FilterId: FilterId,
            OptionId: OptionId
        }
    })
    .done(function (data) {
        if (data) {
            //success ajax
            // Populate table with data from ajax call
            $("#performancePartial").html("");
            $("#performancePartial").html(data).fadeOut("fast").fadeIn("slow");

            // Re init datatable
            $("#Tableview").DataTable();
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

function generalReport() {
}

function filteredReport(idFilter, idOption) {
}
