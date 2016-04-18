//$(".disable_action").click(function (e) {
//    e.preventDefault();

//    $.ajax({
//        url: '/Employee/DisableEmployeeInList/' + this.href,
//        type: "POST",
//        success: function (data) {
//            $(this).parent().hide();
//            $(this).parent().siblings(".DisableActions").show();
//            $(this).css("text-decoration", "line-through");
//        }
//    });

//});
$(document).ready(function () {

    if ($(".enableDisable").attr("data-option") == "disable") {
        $(".enableDisable").parent().siblings().removeClass("line-through");
    }
    else if ($(".enableDisable").attr("data-option") == "enable") {
        $(".enableDisable").parent().siblings().addClass("line-through");
    }

    $(".enableDisable").click(function (e) {
        // Link element clicked
        var element = $(e.target);

        var option = element.attr("data-option");
        var employeeId = element.attr("data-employee-id");

        // Call ajax
        $.ajax({
            url: '/Employee/EnableDisableEmployee',
            type: "POST",
            data: { idEmployee: employeeId, option: option}
        })
        .done(function(data){
            // Get data from json
            // Call was successfully executed
            if (data.success) {
                var rowElement = element.parent().siblings();

                if (option == "enable") {

                    if (rowElement.hasClass("line-through")) {
                        rowElement.removeClass("line-through");
                    }

                    element.text("Disable");
                    element.attr("data-option", "disable");

                    // Enable edit button
                    var editElement = rowElement.parent().find(".edit-button").first();
                    editElement.attr("disabled", false);
                }
                else { // Disable
                    if (!rowElement.hasClass("line-through")) {
                        rowElement.addClass("line-through");
                    }

                    element.text("Enable");
                    element.attr("data-option", "enable");

                    // Disable edit button
                    var editElement = rowElement.parent().find(".edit-button").first();
                    editElement.attr("disabled", true);
                }
            } else {
                alert("There was an error.");
            }
        })
        .fail(function(){
        });
    })
});

    //$("#EnableOption").click(function () {
    //            $(this).parent.show();
    //            $(this).parent().siblings(".EnableActions").show();
    //            $(this).css("text-decoration", "none");
    //});
