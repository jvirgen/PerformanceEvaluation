$(document).ready(function () {
    $("#selectedEmployeeA").children().remove();
    $("#selectedEmployeeB").children().remove();

    $("#ManagerA-table").DataTable();
    $("#ManagerB-table").DataTable();

    var transferProfiles = parseInt($("#selectedProfile").val());

    if (transferProfiles == 3) {
        $("#EmployeeALabel").text("Director A");
        $("#EmployeeBLabel").text("Director B");
        $('#selectedEmployeeA').append("<option value=>" + "Select a director" + "</option>");
        $('#selectedEmployeeB').append("<option value=>" + "Select a director" + "</option>");
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
        $('#selectedEmployeeA').append("<option value=>" + "Select a manager" + "</option>");
        $('#selectedEmployeeB').append("<option value=>" + "Select a manager" + "</option>");
        $.getJSON('/Employee/GetEmployeesByProifile?profile=' + transferProfiles, function (data) {
            for (var i = 0; i < data.employees.length; i++) {
                var employee = data.employees[i];
                $('#selectedEmployeeA').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
                $('#selectedEmployeeB').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
            }
        });
    }
    
    //$('#selectedEmployeeA').trigger(showSelectedEmployees(parseInt($("#selectedEmployeeA").val()), 1));
    //showSelectedEmployees(parseInt($("#selectedEmployeeB").val()), 2);

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

    $('#ManagerA-table').DataTable().clear("tbody");
    //$("#ManagerB-table").DataTable().clear().draw();

    if (transferProfiles == 3) {
        $("#EmployeeALabel").text("Director A");
        $("#EmployeeBLabel").text("Director B");
        $('#selectedEmployeeA').append("<option value=>" + "Select a director" + "</option>");
        $('#selectedEmployeeB').append("<option value=>" + "Select a director" + "</option>");
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
        $('#selectedEmployeeA').append("<option value=>" + "Select a manager" + "</option>");
        $('#selectedEmployeeB').append("<option value=>" + "Select a manager" + "</option>");
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
    if ($(this).hasClass("selectedToMove")) {
        $(this).removeClass("selectedToMove");
    }
    else {
        $(this).addClass("selectedToMove");
    }
});

function moveToB() {
    var rowElements = $("#ManagerA-table").find(".selectedToMove").parents("tr");
    $("#ManagerB-table").find("tr:last").after(rowElements);
}

function moveToA() {
    var rowElements = $("#ManagerB-table").find(".selectedToMove").parents("tr");
    $("#ManagerA-table").find("tr:last").after(rowElements);
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

    