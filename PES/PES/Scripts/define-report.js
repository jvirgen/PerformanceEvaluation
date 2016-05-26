$(document).ready(function () {
});
var radioGeneral = $("#general");
var radioLocation = $("#location");
var combo = $("#dropdownLocation");

$(radioGeneral).click(function () {
    checkGeneral();
});

$(radioLocation).click(function () {
    checkLocation();
});

$(combo).change(function () {
    locationReport($(this).val());
});

function checkGeneral() {
    $(radioGeneral).addClass("ckd");
    $(radioLocation).removeClass("ckd");
    showCombo();
    genralReport();
}

function checkLocation() {
    $(radioLocation).addClass("ckd");
    $(radioGeneral).removeClass("ckd");
    showCombo();
    locationReport($(combo).val());
}
function showCombo() {
    if ($(radioLocation).hasClass("ckd")) {
        combo.fadeIn();
    }
    else {
        combo.fadeOut();
    }
}

function genralReport() {
}

function locationReport(idLocation) {
}