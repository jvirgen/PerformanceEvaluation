$(document).ready(function () {
  
    $("#showEnables").click(function () {
     
        var enableChecked = $("#showEnables").is(':checked');
        $("#showDisables").attr('checked', false);
        $("#showBoth").attr('checked', false);
        //var status = $("#showEnables").val();
        //var bothChecked = $('#showBoth').checked;

        if (enableChecked) {
            //call ajax
            $.ajax({
                url: "/Employee/GetFilteredEmployees",
                type: "POST",
                data: { status: $("#showEnables").val() },
                dataType: "json",
                success: function (data) {
                    if (data) {
                        $("#Id").each(function (index) {
                            for (var i = 0; i < data.length; i++) {
                                if($(this).val == data.EmployeeId){
                                    $(this).hide();
                                }
                            }
                        });
                    }
                }
            });
        }
    })

    $("#showDisables").click(function () {

        var disableChecked = $('#showDisables').is(':checked');
        $("#showEnables").attr('checked', false);
        $("#showBoth").attr('checked', false);

        if (disableChecked) {
            //call ajax
            $.ajax({
                url: "/Employee/GetFilteredEmployees",
                type: "POST",
                data: { status: $("#showDisables").val() },
                dataType: "json",
                success: function (data) {
                    if (data) {
                        $("#Id").each(function (index) {
                            for (var i = 0; i < data.length; i++) {
                                if ($(this).val == data.EmployeeId) {
                                    $(this).hide();
                                }
                            }
                        });
                    }
                }
            });
        }
    })

    $("#showBoth").click(function () {

        var bothChecked = $("#showBoth").is(':checked');
        $("#showDisables").attr('checked', false);
        $("#showEnables").attr('checked', false);

        if (bothChecked) {
            //call ajax
            $.ajax({
                url: "/Employee/GetFilteredEmployees",
                type: "POST",
                data: { status: $("#showBoth").val() },
                dataType: "json",
                success: function (data) {
                    if (data) {
                        $("#Id").each(function (index) {
                            for (var i = 0; i < data.length; i++) {
                                if ($(this).val == data.EmployeeId) {
                                    $(this).hide();
                                }
                            }
                        });
                    }
                }
            });
        }
    })

    });
