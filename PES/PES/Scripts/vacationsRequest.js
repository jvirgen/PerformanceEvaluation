$(document).ready(function () {
    $(function() {
        $('input[name="daterange"]').daterangepicker();

        statusColor();//changes the color of the status span tag
    });
});

function addDate(btnAdd) {
    $('#datesGroup').after('<hr class="divisor">' +
                            '<div id="datesGroup" class="form-group">' +
                                '<div id="subDatesGroup" class="form-group">' +
                                    '<div id="datesCont" class="container flexEnd">' +
                                        '<div class="col-md-3 text-center">' +
                                            '<label for="start">Start Date - End Date</label>' +
                                            '<input id="start" type="text" name="daterange" class="form-control text-center" data-val="true" data-val-required="Start date is required"/>' +
                                            '<span class="field-validation-valid text-danger" data-valmsg-for="start" data-valmsg-replace="true"></span>' +
                                        '</div>' +
                                        '<div class="col-md-3">' +
                                            '<label>Return Date</label>' +
                                            '<input type="text" class="form-control" id="return" disabled="disabled"/>' +
                                        '</div>' +
                                        '<div class="col-md-1">' +
                                            '<button onclick="addDate(this)" type="button" class="btn btn-default addBtn">' +
                                                'Add Date' +
                                            '</button>' +
                                        '</div>' +
                                    '</div>' +
                                '</div>' +
                                '<div id="subDatesGroup" class="form-group">' +
                                    '<div id="datesCont" class="container flexCenter">' +
                                        '<div id="leadName" class="col-md-4">' +
                                            '<label for="lead" class="control-label">Lead Name</label>' +
                                            '<div>' +
                                                '<input id="lead" type="text" class="form-control" data-val="true" data-val-required="Lead name is required" />' +
                                            '</div>' +
                                            '<span class="field-validation-valid text-danger" data-valmsg-for="lead" data-valmsg-replace="true"></span>' +
                                        '</div>' +
                                        '<div id="checkarea" class="col-md-2 checkbox">' +
                                            '<label id="checktext">' +
                                                '<input type="checkbox" id="unpaid"/>I do not have a project' +
                                            '</label>' +
                                        '</div>' +
                                        '<div class="removeBtn col-md-1">' +
                                            '<button onclick="removeDate(this)" type="button" class="btn btn-danger">Remove Date</button>' +
                                        '</div>' +
                                    '</div>' +
                                '</div>' +
                            '</div>');

    $('input[name="daterange"]').daterangepicker();

    disableBtn();
    showBtn();
}

function removeDate(btnRemove) {
    $(btnRemove).parent().parent().parent().parent().prev('hr.divisor').remove();
    $(btnRemove).parent().parent().parent().parent().remove();//removeBtn->datesCont->subdatesGroup->datesGroup->remove()
    enableBtn();
}

function enableBtn(btnRemove) {
    $('.addBtn').last().prop('disabled', false);
}

function disableBtn() {
    for (var i = 0, l = $('.addBtn').length - 1; i < l; i++) {
        $('.addBtn').eq(i).prop('disabled', true);
    }
}

function showBtn() {
    for (var i = 0, l = $('.removeBtn').length - 1; i < l; i++) {
            $('.removeBtn').eq(i).removeClass('hidden');
    }

    $('.removeBtn').eq($('.removeBtn').length - 1).addClass('hidden');
}

function statusColor() {
    statusText = $('#status').text();

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