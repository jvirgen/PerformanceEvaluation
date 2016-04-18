$(document).ready(function () {
  
    $("#showEnables").click(function () {
     
        var enableChecked = $("#showEnables").is(':checked');
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

                        //var length = data.length;
                        var row = "";
                        $('#table-employees').children('tbody').remove();

                        if (data.employees.length > 0) {
                            // Loop data from ajax call
                            for (var i = 0; i < data.employees.length; i++) {
                                var employee = data.employees[i];
                                row += "<tr><td>" + employee.FirstName + "</td><td>" + employee.LastName + "</td><td>"
                                + employee.Email + "</td><td>" + employee.Profile.Name + "</td><td>" + employee.Manager.FirstName + "" + employee.Manager.LastName + "</td></tr>";
                            }

                            if (row != "") {
                                $('#table-employees').append(row);
                            }
                        }
                    }
                }
            });
        }
    })

    $("#showDisables").click(function () {

        var disableChecked = $('#showDisables').is(':checked');

        if (disableChecked) {
            //call ajax
            $.ajax({
                url: "/Employee/GetFilteredEmployees",
                type: "POST",
                data: { status: $("#showDisables").val() },
                dataType: "json",
                success: function (data) {
                    if (data) {

                        //var length = data.length;
                        var row = "";
                        $('#table-employees').children('tbody').remove();

                        if (data.employees.length > 0) {
                            // Loop data from ajax call
                            for (var i = 0; i < data.employees.length; i++) {
                                var employee = data.employees[i];
                                row += "<tr><td>" + employee.FirstName + "</td><td>" + employee.LastName + "</td><td>"
                                + employee.Email + "</td><td>" + employee.Profile.Name + "</td><td>" + employee.Manager.FirstName + "" + employee.Manager.LastName + "</td></tr>";
                            }

                            if (row != "") {
                                $('#table-employees').append(row);
                            }
                        }
                    }
                }
            });
        }
    })

    $("#showBoth").click(function () {

        var bothChecked = $("#showBoth").is(':checked');

        if (bothChecked) {
            //call ajax
            $.ajax({
                url: "/Employee/GetFilteredEmployees",
                type: "POST",
                data: { status: $("#showBoth").val() },
                dataType: "json",
                success: function (data) {
                    if (data) {

                        //var length = data.length;
                        var row = "";
                        $('#table-employees').children('tbody').remove();

                        if (data.employees.length > 0) {
                            // Loop data from ajax call
                            for (var i = 0; i < data.employees.length; i++) {
                                var employee = data.employees[i];
                                row += "<tr><td>" + employee.FirstName + "</td><td>" + employee.LastName + "</td><td>"
                                + employee.Email + "</td><td>" + employee.Profile.Name + "</td><td>" + employee.Manager.FirstName + "" + employee.Manager.LastName + "</td></tr>";
                            }

                            if (row != "") {
                                $('#table-employees').append(row);
                            }
                        }
                    }
                }
            });
        }
    })

    });
