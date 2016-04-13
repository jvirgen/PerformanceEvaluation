$(document).ready(function () {
    $("#DisableOption").click(function () {

        
            $(this).parent().hide();
            $(this).parent().siblings(".DisableActions").show();
            $(this).css("text-decoration", "line-through");

    });

    $("#EnableOption").click(function () {
            $(this).parent.hide();
            $(this).parent().siblings(".EnableActions").show();
            $(this).css("text-decoration", "none");
    });
});