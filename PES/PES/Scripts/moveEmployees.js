$(document).ready(function () {
    $("#ManagerA-table").DataTable();
    $("#ManagerB-table").DataTable();

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

    // Events
    $('#selectedEmployeeA').change(function (e) {
        // Execute function 

        var employee = parseInt($("#selectedEmployeeA").val());
        var option = "selectedEmployeeA";
        showSelectedEmployees(employeeId);
});

    $('#selectedEmployeeB').change(function (e) {
        // Execute function 

        var employeeId = parseInt($("#selectedEmployeeB").val());
        var option = "selectedEmployeeB";
        showSelectedEmployees(employeeId);
    });
});

function showSelectedEmployees(employeeId) {
    $.ajax({
        url: "/Employee/GetEmployeesByManager",
        data: {
            employeeId: employeeId
        }
    })
    .done(function (data) {
        if (data) {
            //success ajax
            // Populate table with data from ajax call
            $("#divtable-content").html("");
            $("#divtable-content").html(data).fadeOut("fast").fadeIn("slow");

            // Re init datatable
            $("#ManagerA-table").DataTable();
            $("#ManagerB-table").DataTable();
            
            //if (option = "selectedEmployeeA") {
            //    $("#divtable-content").html("");
            //    $("#divtable-content").html(data).fadeOut("fast").fadeIn("slow");

            //    // Re init datatable
            //    $("#ManagerA-table").DataTable();
            //}
            //else {
            //    $("#divtable2-content").html("");
            //    $("#divtable2-content").html(data).fadeOut("fast").fadeIn("slow");
                
            //    // Re init datatable
            //    $("#ManagerB-table").DataTable();
            //}
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