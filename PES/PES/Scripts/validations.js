$(document).ready(function () {
    getProfile();
});

var currentProfile = parseInt($('#CurrentProfile_ProfileId').val());
var profile = parseInt($('#selectedProfile').val());
var manager = parseInt($('#selectedManager').val());
var comboProfile = $('#selectedProfile');
var comboManager = $('#selectedManager');
var email = $('#Email').val();
var newProfile = 0;

comboProfile.change(function () {
    getManagersByProfile(profile, email);
});

function getProfile() {
    if (profile == 1) {
        // Resource was selected
        newProfile = 2; // Manager
    } else if (profile == 2) {
        // Manager was selected
        newProfile = 3; // Director
    } else if (profile == 3) {
        // Director was selected
        newProfile = 2; // Director
    }
    getManagersByProfile(newProfile, email);
}

function getManagersByProfile(newProfile, email) {
    // Execute validate profile function
    $.getJSON('/Employee/GetEmployeesProifile?profile=' + newProfile + "&email=" + email, function (data) {
        if (profile == 1) {
            $('#profileLabel').text("New Manager");

            // Remove current options dropdown
            comboManager.children().remove();

            // Loop data from ajax call
            for (var i = 0; i < data.employees.length; i++) {
                var employee = data.employees[i];
                $('#selectedManager').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
            }
            $('#dropdownManager').show();
        }
        else if (profile == 2) {
            $('#profileLabel').text("New Director");

            // Remove current options dropdown
            comboManager.children().remove();

            // Loop data from ajax call
            for (var i = 0; i < data.employees.length; i++) {
                var employee = data.employees[i];
                $('#selectedManager').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
                //$('#selectedNewManager').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
            }
            $('#dropdownManager').show();
        }
        else if (profile == 3) {
            // Remove current options dropdown
            comboManager.children().remove();
            $('#dropdownManager').hide();
        }
    });
}