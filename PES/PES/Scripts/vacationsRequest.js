$(document).ready(function () {
    $(function() {
        $('.daterange').daterangepicker({});

        statusColor();//changes the color of the status, <span> tag in VacationRequest view
            
        $(document).on('change', 'input.datesBox', getDaysRequested);
    });
});

//function insertNewDates() {
//    // Get date group element
//    var dateGroup = $("#dateGroup-0");
//    // Clone into another div
//    var parentDiv = $("#datesGroups");
//    parentDiv.append('<div class="dateGroup" id="dateGroup-1">');
//    $("#dateGroup-1").append('<div id="subDatesGroup-1" class="form-group">');
//    $("#subDatesGroup-1").append('<div id="datesCont-1" class="container flexEnd">');
//    $("#datesCont-1").append('<div class="col-md-3 text-center" id="data-1">');
//    $("#data-1").append(' <label for="start" id="lable-1">Start Date - End Date</label>');
//    $("#lable-1").append('<input type="text" name="subRequest[' + add() + '].date" class="daterange" /></div></div></div></div>');
//}

var add = (function () {
    var counter = 0;
    return function () { return counter += 1; }
    })();

function addDate(btnAdd) {
    var last = $('.datesGroup')[$('.datesGroup').length - 1];
    $(last).after($(last).clone());

    $($('.datesBox')[$('.datesBox').length - 1]).daterangepicker({
        startDate: getSysdate(),
        endDate: getSysdate()
    });

    disableBtn();
    showBtn();
    getDaysRequested();
}

function removeDate(btnRemove) {
    $(btnRemove).parent().parent().parent().parent().prev('hr.divisor').remove();
    $(btnRemove).parent().parent().parent().parent().remove();//removeBtn->datesCont->subdatesGroup->datesGroup->remove()
    enableBtn();
    getDaysRequested();
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

function getDaysRequested() {
    var total = 0;
    var dates = '';
    var start = null;
    var end = null;

    $('.datesBox').each(function (i, input) {
        dates = $(input).val();
        if (dates != '') {
        start = moment(dates.split(" - ")[0]);
        end = moment(dates.split(" - ")[1]);

            total += getWorkableDays(start, end);
            //total += end.diff(start, 'days');
        }
    });

    $("#daysReq").text(validateDaysRequested(total, this));
}

function validateDaysRequested(daysReq, input) {
    if ($('#daysVac').text() < daysReq) {
            $(input).val('');
        alert('no more vacations days available');

        return 0;
    }
    else {
        return daysReq;
    }
}

function getReturnDate() {

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

    // Set time to midday to avoid daylight saving and browser quirks
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