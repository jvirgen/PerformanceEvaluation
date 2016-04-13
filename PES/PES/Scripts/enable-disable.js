$(".disable_action").click(function (e) {
    e.preventDefault();

    $.ajax({
        url: '/Employee/DisableEmployeeInList/' + this.href,
        type: "POST",
        success: function (data) {
            $(this).parent().hide();
            $(this).parent().siblings(".DisableActions").show();
            $(this).css("text-decoration", "line-through");
        }
    });

});

$("#EnableOption").click(function () {
            $(this).parent.show();
            $(this).parent().siblings(".EnableActions").show();
            $(this).css("text-decoration", "none");
});
