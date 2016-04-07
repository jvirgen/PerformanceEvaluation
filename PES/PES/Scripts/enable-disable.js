$(document).ready(function () {
    $("#DisableOption").click(function () {
            $(this).parent().hide();
            $(this).parent().siblings(".DisableActions").show();
    });

    $("#EnableOption").click(function () {
            $(this).parent.hide();
            $(this).parent().siblings(".EnableActions").show();
    });
});