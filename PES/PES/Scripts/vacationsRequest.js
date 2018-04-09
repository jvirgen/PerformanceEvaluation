﻿$(document).ready(function () {
    $(function() {
        $('.daterange').daterangepicker({});
        statusColor();//changes the color of the status, <span> tag in VacationRequest view
        //editHolidayDay();
        //this event caches any modification on a start/end date field
        $(document).on('change', 'input.datesBox', getDaysRequested);
    });


    $('#haveProject').click(function () {
        this.checked ? $('#lead').prop('disabled', true) : $('#lead').prop('disabled', false);
        $("#lead").val("");
        if (this.checked) {
                $("#haveProject").val("false");
        } else {
                $("#haveProject").val("true");
          
            }
    });

    $('#sendRequest').click(function () {
        $("#startDates ").html($("#start").val());
        $("#startAndEnd ").attr("style", "color:black;");
        $("#returnDates").html($("#returnDay").val());
        $("#retrunDays ").attr("style", "color:black;");
        $(".activeSpan").attr("style", "color:dodgerblue");
    });

    $('#my-button').click(function () {
        $('#my-file').click();
        
    });
});


function addDate(btnAdd) {
    var last = $('.datesGroup')[$('.datesGroup').length - 1]; 
    $(last).after($(last).clone()); //clone
    var last2 = $('.datesGroup')[$('.datesGroup').length - 1]; 
    //save the counter as an integer on data atrribute (to increment by 1)
    //set with attr add 1 
    //updates inner array in name attribute of a determined start date, return date and lead 
    //updateEnumerationBoxes();

    
    //$($('.datesBox')[$('.datesBox').length - 1]).daterangepicker({
    //    startDate: getSysdate(),
    //    endDate: getSysdate()
    //});
    disableBtn();
    showBtn();
    getDaysRequested();
}

function removeDate(btnRemove) {
    $(btnRemove).parent().parent().parent().parent().prev('hr.divisor').remove();
    $(btnRemove).parent().parent().parent().parent().remove();//removeBtn->datesCont->subdatesGroup->datesGroup->remove()
    enableBtn();
    getDaysRequested();
  
    //updates inner array in name attribute of a determined start date, return date and lead name
    updateEnumerationBoxes();
}

function enableBtn(btnRemove) {
    $('.addBtn').last().prop('disabled', false);
}

function disableBtn() {
    for (var i = 0, l = $('.addBtn').length - 1; i < l; i++) {
        $('.addBtn').eq(i).prop('disabled', true);//disables all elements with "addBtn" class except the last one of them
    }
}

function showBtn() {
    for (var i = 0, l = $('.removeBtn').length - 1; i < l; i++) {
            $('.removeBtn').eq(i).removeClass('hidden');//shows all elements with "removeBtn" class
    }

    $('.removeBtn').eq($('.removeBtn').length - 1).addClass('hidden');//hides the last element of the collection of elements with "removeBtn" class
}

function statusColor() {
    var statusText = $('#status').text();

    if (statusText.localeCompare('PENDING') == 0) {
        $('#status').attr('class', 'label label-warning');
    }
    else if (statusText.localeCompare('REJECTED') == 0) {
        $('#status').attr('class', 'label label-danger');
    }
    else if (statusText.localeCompare('CANCELED') == 0) {
        $('#status').attr('class', 'label label-default');
    }
    else if (statusText.localeCompare('APPROVED') == 0) {
        $('#status').attr('class', 'label label-success');
    }
}

$('#start').datepicker({
    format: "mmm/d/yyyy",
    autoclose: true,
    todayHighlight: true
});

$('#End').datepicker({
    format: "d/MM/yyyy",
    autoclose: true,
    todayHighlight: true
});

$(".valida-fecha").on("change", function () {

    var Fecha1 = $("#start").val();

    var Fecha2 = $("#End").val();

    var star = moment(Fecha1);

    var end = moment(Fecha2);

    if (star != "" && end != "") {
        ValidateSameMonth(star, end);
    }
    else {
        alert("Llene los campos");
    }
});


/*function getDaysRequested() {
    $("#start").blur(function () {
        if (!isEmpty($("#start").val()) && !isEmpty($("#EndDateLabel").val())) {
            ValidateSameMonth($("#start").val(), $("#EndDateLabel").val());
        }
    });
    $("#EndDateLabel").blur(function () {
        if (!isEmpty($("#start").val()) && !isEmpty($("#EndDateLabel").val())) {
            ValidateSameMonth($("#start").val(), $("#EndDateLabel").val());
        }
    });
}*/

function ValidateSameMonth(start, end, count) {
    var sD = new Date(start);
    var eD = new Date(end);
    var count = 0;
    var flag = false;

    $.ajax({
        url: "/VacationRequest/ValidateSameMonth",
        data: { start: sD.toISOString(), end: eD.toISOString(), flag: flag }
    })
        .done(function (data) {

            if (!data) {

                $("#sameMonth").modal();
                $("#daysReq").text("0");
                $("#returnDay").val("");
                $('.datesBox').val("invalided date");
            } else {
                ValidateStartDate(start, end, count);
            }
        })
        .fail(function () {
        })
        .always(function () { });
}




function ValidateStartDate(start, end,  count) {
    var sD = new Date(start);

    var flag = false;

    $.ajax({
        url: "/VacationRequest/ValidateStartDate",
        data: { start: sD.toISOString(), flag: flag }
    })
        .done(function (data) {

            if (!data) {

                $("#OldDate").modal();
                $("#daysReq").text("0");
                $("#returnDay").val("");
                $('.datesBox').val("invalided date");
            } else {
                CountHolidaysAndValidateDates(start, end, count);
            }

        })
        .fail(function () {
        })
        .always(function () { });    
}

function CountHolidaysAndValidateDates(start, end, count) {
    var sD = new Date(start);

    var eD = new Date(end);

        $.ajax({
            url: "/VacationRequest/CountHolidaysAndValidateDates",
            data: { start: sD.toISOString(), end: eD.toISOString(), count: count }
        })
            .done(function (data) {

                $("#daysReq").val(validateDaysRequested(getWorkableDays(start, end) - data, this));
                $("#daysRequest").val(validateDaysRequested(getWorkableDays(start, end) - data, this));
            })
            .fail(function () {
            })
            .always(function () { });

}

function validateDaysRequested(daysReq, input) {
    if ($('#daysVac').text() < daysReq) {
        $(input).val('');
        $("#modalNoEnoughDays").modal();
        return 0;
    }
    else {
        getReturnDate();
        return daysReq;
    }
}

function validateReturnDate(returnDate) {
    $.ajax({
        url: "/VacationRequest/ValidateResultDate",
        data: { returnDate: returnDate.toISOString() }
    })
        .done(function (data) {

            $("#returnDay").val(data.date);
        })
        .fail(function () {

        })
        .always(function () {});
}

function getReturnDate() {
    //var returnDay = new Date();
    var dates = '';
    var endDate = null;
    $('.datesBox').each(function (i, input) {
        dates = $(input).val();
        if (dates != '') {
            endDate = dates;
        }
    });
    var returnDay = new Date(endDate);
    validateReturnDate(returnDay);
}

function getSysdate() {
    var d = new Date();
    var month = d.getMonth() + 1;
    var day = d.getDate();

    var output = (month < 10 ? '0' : '') + month + '/' + (day < 10 ? '0' : '') + day + '/' + d.getFullYear();

    return output;
}

// Expects start date to be before end date
// start and end are Date objects
function getWorkableDays(start, end) {
    // Copy date objects so don't modify originals
    var s = new Date(+start);
    var e = new Date(+end);

    // Set time to midday to avoid daylight saving and browser quirksss
    s.setHours(12, 0, 0, 0);
    e.setHours(12, 0, 0, 0);

    // Get the difference in whole days
    var totalDays = Math.round((e - s) / 8.64e7);

    // Get the difference in whole weeks
    var wholeWeeks = totalDays / 7 | 0;

    // Estimate business days as number of whole weeks * 5
    var days = wholeWeeks * 5;

    // If not even number of weeks, calc remaining weekend days
    if (totalDays % 7) {
        s.setDate(s.getDate() + wholeWeeks * 7);

        while (s < e) {
            s.setDate(s.getDate() + 1);

            // If day isn't a Sunday or Saturday, add to business days
            if (s.getDay() != 0 && s.getDay() != 6) {               
                ++days;
            }
        }
    }

        return days;       
}

//Document this if i dont finish it-----------------------------------------------
var Item = document.getElementById("sendRequest2");

Item.addEventListener("mouseover", ValidateVacationDaysManager, false)

function ValidateVacationDaysManager() {
    var idManager = $("#lead").val();
    var flag = false;


    $.ajax({
        url: "/VacationRequest/ValidateEmployeeVacationsManager",
        data: {IdEmployee: idManager}
    })
        .done(function (data) {

            if (data === true) {

            } else {
                $("#VaEmMa").modal();
            }

        })
        .fail(function () {
        })
        .always(function () { });
}
//Document this if i dont finish it-----------------------------------------------


/*

DESCRIPTION
this function searches for the following fields: datesBox, returnBox, leadBox, projectBox that may repeat many times depending on how many dates the user is requesting for vacations. Once found the elements proceeds to update their names according to the structure agreed "subRequest[n]"

*/
function updateEnumerationBoxes() {
    //here are defined the body of the name, we set two elements: subRequest[ and ].date, ].lead_name, ].returnDate respectively
    var datesPieces = $($('.datesBox')[0]).attr('name').replace(/[0-9]/g, ' ').split(' ');
    var returnPieces = $($('.returnBox')[0]).attr('name').replace(/[0-9]/g, ' ').split(' ');
    var leadPieces = $($('.leadBox')[0]).attr('name').replace(/[0-9]/g, ' ').split(' ');
    var projectPiece = $($('.projectBox')[0]).attr('name').replace(/[0-9]/g, ' ').split(' ');
    var projectHiddenPiece = $($('.projectHiddenBox')[0]).attr('name').replace(/[0-9]/g, ' ').split(' ');

    if ($('.datesBox').length > 1) {
        $('.datesBox').each(function (i, element) {//for each element we set the numeration based on the "i" variable
            $($('.datesBox')[i]).attr('name', datesPieces[0] + i + datesPieces[1]);
            $($('.returnBox')[i]).attr('name', returnPieces[0] + i + returnPieces[1]);
            $($('.leadBox')[i]).attr('name', leadPieces[0] + i + leadPieces[1]);
            $($('.projectBox')[i]).attr('name', projectPiece[0] + i + projectPiece[1]);
            $($('.projectHiddenBox')[i]).attr('name', leadPieces[0] + i + leadPieces[1]);
        });
    }
    else {//if only one set of elements is present there is no need to enumerate, all are set to 0
        $($('.datesBox')[0]).attr('name', datesPieces[0] + 0 + datesPieces[1]);
        $($('.returnBox')[0]).attr('name', returnPieces[0] + 0 + returnPieces[1]);
        $($('.leadBox')[0]).attr('name', leadPieces[0] + 0 + leadPieces[1]);
        $($('.projectBox')[0]).attr('name', projectPiece[0] + 0 + projectPiece[1]);
        $($('.projectHiddenBox')[0]).attr('name', leadPieces[0] + 0 + leadPieces[1]);
    }
}


