$(document).ready(function () {
    $("#DisableOption").click(function () {

        
            $(this).parent().hide();
            $(this).parent().siblings(".DisableActions").show();
            $(this).css("text-decoration", "line-through");

            //var url = "/Employee/DisableEmployeeInList";
            //$.post("/");

    });

    $("#EnableOption").click(function () {
            $(this).parent.show();
            $(this).parent().siblings(".EnableActions").show();
            $(this).css("text-decoration", "none");
    });
});