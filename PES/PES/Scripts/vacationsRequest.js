$(document).ready(function () {
    var btnAdd = $('#addDate');
    btnAdd.click(function () {
        $('#datesGroup').after('<div id="datesGroup" class="form-group"><div id="datesCont" class="container flexEnd"><div class="col-md-3 text-center"><label for="start">Start Date - End Date</label><input id="start" type="text" name="daterange" class="form-control text-center" data-val="true" data-val-required="Start date is required"><span class="field-validation-valid text-danger" data-valmsg-for="start" data-valmsg-replace="true"></span></div><div class="col-md-2"><label>Return Date</label><input type="text" class="form-control" id="return"></div><div class="col-md-7"><button id="addDate" type="button" class="btn btn-default" data-dismiss="modal">Add Date</button></div></div> <!-- container end --></div> <!-- form-group end -->');
    });
});