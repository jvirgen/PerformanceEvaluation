function enableDisable(employeeId, option) {
    // Call ajax
    $.ajax({
        url: '/Employee/EnableDisableEmployee',
        type: "POST",
        data: { idEmployee: employeeId, option: option }
    })
    .done(function (data) {
        // Get data from json
        // Call was successfully executed
        if (data.success) {
            var element = $('#Id'+ employeeId).siblings('td:last').children('.enableDisable');
            var rowElement = element.parent().siblings();
            var actionsElement = element.parent();

            if (option == "enable") {

                if (rowElement.hasClass("line-through")) {
                    rowElement.removeClass("line-through");
                }

                element.text("Disable");
                element.attr("data-option", "disable");

                //Hide row
                rowElement.fadeOut();
                actionsElement.fadeOut();
            }
            else { // Disable
                if (!rowElement.hasClass("line-through")) {
                    rowElement.addClass("line-through");
                }

                element.text("Enable");
                element.attr("data-option", "enable");

                //Hide row
                rowElement.fadeOut();
                actionsElement.fadeOut();
            }
        } else {
            alert("There was an error.");
        }
    })
    .fail(function () {
    });
}

//$(".enableDisable").on("click", function (e) {
//    // Link element clicked
//    var element = $(e.target);

//    var option = element.attr("data-option");
//    var employeeId = element.attr("data-employee-id");

//    // Call ajax
//    $.ajax({
//        url: '/Employee/EnableDisableEmployee',
//        type: "POST",
//        data: { idEmployee: employeeId, option: option }
//    })
//    .done(function (data) {
//        // Get data from json
//        // Call was successfully executed
//        if (data.success) {
//            var rowElement = element.parent().siblings();

//            if (option == "enable") {

//                if (rowElement.hasClass("line-through")) {
//                    rowElement.removeClass("line-through");
//                }

//                element.text("Disable");
//                element.attr("data-option", "disable");

//                // Enable edit button
//                var editElement = rowElement.parent().find(".edit-button").first();
//                editElement.attr("disabled", false);
//            }
//            else { // Disable
//                if (!rowElement.hasClass("line-through")) {
//                    rowElement.addClass("line-through");
//                }

//                element.text("Enable");
//                element.attr("data-option", "enable");

//                // Disable edit button
//                var editElement = rowElement.parent().find(".edit-button").first();
//                editElement.attr("disabled", true);
//            }
//        } else {
//            alert("There was an error.");
//        }
//    })
//    .fail(function () {
//    });
//})
