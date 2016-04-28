$(document).ready(function () {
    $("#selectedEmployeeA").children().remove();
    $("#selectedEmployeeB").children().remove();

    $("#ManagerA-table").DataTable();
    $("#ManagerB-table").DataTable();

    var transferProfiles = parseInt($("#selectedProfile").val());

    if (transferProfiles == 3) {
        $("#EmployeeALabel").text("Director A");
        $("#EmployeeBLabel").text("Director B");
        $.getJSON('/Employee/GetEmployeesByProifile?profile=' + transferProfiles, function (data) {
            for (var i = 0; i < data.employees.length; i++) {
                var employee = data.employees[i];
                $('#selectedEmployeeA').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
                $('#selectedEmployeeB').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
            }
        });
    }
    else {
        $("#EmployeeALabel").text("Manager A");
        $("#EmployeeBLabel").text("Manager B");
        $.getJSON('/Employee/GetEmployeesByProifile?profile=' + transferProfiles, function (data) {
            for (var i = 0; i < data.employees.length; i++) {
                var employee = data.employees[i];
                $('#selectedEmployeeA').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
                $('#selectedEmployeeB').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
            }
        });
    }

    var employeeId1 = parseInt($("#selectedEmployeeA").val());
    var employeeId2 = parseInt($("#selectedEmployeeB").val());

    //showSelectedEmployees(employeeId1, 1);
    //showSelectedEmployees(employeeId2, 2);

    // Events
    $('#selectedEmployeeA').change(function (e) {
        // Execute function 

        var employeeId = parseInt($("#selectedEmployeeA").val());
        var option = 1;
        showSelectedEmployees(employeeId, option);
    });

    $('#selectedEmployeeB').change(function (e) {
        // Execute function 

        var employeeId = parseInt($("#selectedEmployeeB").val());
        var option = 2;
        showSelectedEmployees(employeeId, option);
    });
});

$("#selectedProfile").change(function () {
    var transferProfiles = parseInt($("#selectedProfile").val());

    $("#selectedEmployeeA").children().remove();
    $("#selectedEmployeeB").children().remove();

    if (transferProfiles == 3) {
        $("#EmployeeALabel").text("Director A");
        $("#EmployeeBLabel").text("Director B");
        $.getJSON('/Employee/GetEmployeesByProifile?profile=' + transferProfiles, function (data) {
            for (var i = 0; i < data.employees.length; i++) {
                var employee = data.employees[i];
                $('#selectedEmployeeA').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
                $('#selectedEmployeeB').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
            }
        });
    }
    else {
        $("#EmployeeALabel").text("Manager A");
        $("#EmployeeBLabel").text("Manager B");
        $.getJSON('/Employee/GetEmployeesByProifile?profile=' + transferProfiles, function (data) {
            for (var i = 0; i < data.employees.length; i++) {
                var employee = data.employees[i];
                $('#selectedEmployeeA').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
                $('#selectedEmployeeB').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
            }
        });
    }   
});

function showSelectedEmployees(employeeId, option) {
    $.ajax({
        url: "/Employee/GetEmployeesByManager",
        data: {
            employeeId: employeeId,
            option: option
        }
    })
    .done(function (data) {
        if (data) {
            //success ajax
            
            if (option == 1) {
                // Populate table with data from ajax call
                $("#ManagerA-table").html(data).fadeOut("fast").fadeIn("slow");
                // Re init datatable
                $("#ManagerA-table").DataTable();              
            }
            else {
                $("#ManagerB-table").html(data).fadeOut("fast").fadeIn("slow");
                $("#ManagerB-table").DataTable();
            }
        }
        else {
            // Error
            alert("Error while getting employees");
        }
    })
    .fail(function (jqxhr, textStatus, error) {
        alert("Error while getting employees. Please try again later.");
    })
    .always(function () {
        //alert("finished");
    });
}

$(".checkbox").on("click", function () {
    $(this).addClass("selectedToMove");
});

function moveToB(){
    var rowElements = $("#ManagerA-table").find(".selectedToMove").parents("tr");
    rowElements.remove();
    $("#ManagerB-table").find("tr:last").insertAfter(rowElements);
    rowElements.removeClass("selectedToMove");
}

function moveToA() {
    var rowElements = $("#ManagerB-table").find(".selectedToMove").parents("tr");
    rowElements.remove();
    $("#ManagerA-table").find("tr:first").insertAfter(rowElements);
    rowElements.removeClass("selectedToMove");
}

function moveEmployeeToB(employee) {
    var employee = $("#idEmployee").val();

    $.ajax({
        url: "/Employee/MoveEmployee",
        data: {
            employee: employee
        }
    })
    .done(function (data) {
        if (data) {
            //success ajax
            // Populate table with data from ajax call
            $("#table-content").html("");
            $("#table-content").html(data);
            
            // si toca

            // Re init datatable
            $("#firstTable").DataTable();
            $("#secondTable").DataTable();
        }
        else {
            // Error
            alert("Error while getting employees");
        }
    })
    .fail(function (jqxhr, textStatus, error) {
        alert("Error while getting employees. Please try again later.");
    })
    .always(function () {
        //alert("finished");
    });
}

    function moveEmployeeToA(employee) {
        $.ajax({
            url: "/Employee/MoveEmployeeToA",
            data: {
                employee: employee
            }
        })
        .done(function (data) {
            if (data) {
                //success ajax
                // Populate table with data from ajax call
                $("#table-content").html("");
                $("#table-content").html(data).fadeOut("fast").fadeIn("slow");

                // Re init datatable
                $("#firstTable").DataTable();
                $("#secondTable").DataTable();
            }
            else {
                // Error
                alert("Error while getting employees");
            }
        })
        .fail(function (jqxhr, textStatus, error) {
            alert("Error while getting employees. Please try again later.");
        })
        .always(function () {
            //alert("finished");
        });
    }

    function moveAllEmployeesToA(employees) {
        $.ajax({
            url: "/Employee/MoveAllEmployeesToA",
            data: {
                employees: employees
            }
        })
        .done(function (data) {
            if (data) {
                //success ajax
                // Populate table with data from ajax call
                $("#table-content").html("");
                $("#table-content").html(data).fadeOut("fast").fadeIn("slow");

                // Re init datatable
                $("#firstTable").DataTable();
                $("#secondTable").DataTable();
            }
            else {
                // Error
                alert("Error while getting employees");
            }
        })
        .fail(function (jqxhr, textStatus, error) {
            alert("Error while getting employees. Please try again later.");
        })
        .always(function () {
            //alert("finished");
        });
    }

    function moveAllEmployeesToB(employees) {
        $.ajax({
            url: "/Employee/MoveAllEmployeesToB",
            data: {
                employees: employees
            }
        })
        .done(function (data) {
            if (data) {
                //success ajax
                // Populate table with data from ajax call
                $("#table-content").html("");
                $("#table-content").html(data).fadeOut("fast").fadeIn("slow");

                // Re init datatable
                $("#firstTable").DataTable();
                $("#secondTable").DataTable();
            }
            else {
                // Error
                alert("Error while getting employees");
            }
        })
        .fail(function (jqxhr, textStatus, error) {
            alert("Error while getting employees. Please try again later.");
        })
        .always(function () {
            //alert("finished");
        });
    }