$(document).ready(function () {
});
var radioGeneral = $("#general");
var radioFilter = $("#filtered");
var comboFilter = $("#dropdownFilter");
var comboLocation = $("#dropdownLocation");
var comboManager = $("#dropdownManager");
var comboDirector = $("#dropdownDirector");

$(radioGeneral).click(function () {
    checkGeneral();
});

$(radioFilter).click(function () {
    checkFilter();
});

function checkGeneral() {
    $(radioGeneral).addClass("ckd");
    $(radioFilter).removeClass("ckd");
    showFilters();
}

function checkFilter() {
    $(radioFilter).addClass("ckd");
    $(radioGeneral).removeClass("ckd");
    showFilters();
}

function showFilters() {
    if ($(radioFilter).hasClass("ckd")) {
        filter = comboFilter.val();
        comboFilter.fadeIn();
        showFilterOptions(filter);
    }
    else {
        comboFilter.fadeOut();
        showFilterOptions(0);
    }
}

function showFilterOptions(filter) {
    if (filter == 1) {
        comboLocation.show();
        comboManager.hide();
        comboDirector.hide();
    }
    else if (filter == 2) {
        comboManager.show();
        comboLocation.hide();
        comboDirector.hide();
    }
    else if (filter == 3) {
        comboDirector.show();
        comboLocation.hide();
        comboManager.hide();
    }
    else {
        comboLocation.hide();
        comboManager.hide();
        comboDirector.hide();
    }
}

function genralReport() {
    
}

function locationReport(idLocation) {
}