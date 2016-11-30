$(document).ready(function () {

});

function addDate(btnAdd) {
    $('#datesGroup').after('<div id="datesGroup" class="form-group"><div id="datesCont" class="container flexEnd"><div class="col-md-3 text-center"><label for="start">Start Date - End Date</label><input id="start" type="text" name="daterange" class="form-control text-center" data-val="true" data-val-required="Start date is required"><span class="field-validation-valid text-danger" data-valmsg-for="start" data-valmsg-replace="true"></span></div><div class="col-md-2"><label>Return Date</label><input type="text" class="form-control" id="return"></div><div class="col-md-1"><button onclick="addDate(this)" type="button" class="btn btn-default addBtn">Add Date</button></div><div id="removeBtn" class="col-md-1"><button onclick="removeDate(this)" type="button" class="btn btn-danger">Remove Date</button></div></div></div>');

    $(function () {
        $('input[name="daterange"]').daterangepicker();
    });

    disableBtn();
}

function removeDate(btnRemove) {
    $(btnRemove).parent().parent().parent().remove();//removeBtn->datesCont->datesGroup->remove()
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

function validateFields() {
    var validator = $("#form").validate();
    validator.element("#title");
    validator.element("#lead");
    validator.element("#date");
    validator.element("#returnDate");
    validator.element("#comments");
}