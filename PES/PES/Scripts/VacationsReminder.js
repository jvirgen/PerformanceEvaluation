$(document).ready(function (){
    $("#tableViewReminded").DataTable({
        "order": [[0, "dec"]],
        "columnDefs": [{
            "targets": 2,
            "orderable": false
        }]
    });;    
});

$("#tableViewReminded .btn-send-reminder").on("click", function (e) {
    e.stopPropagation();
    var id = $(this).attr('id');
    sendedImail(id);
    //$("#VaReEmail").modal();
});

function sendedImail(id) {
    $.ajax({
        url: "/VacationRequest/SendReminderEmail",
        data: { userid: id }
    })
        .done(function (data) {
            if (data) {
                $("#VaReEmail").modal();
            } else {
                $("#VaFailedEmail").modal();
            }           

        })
        .fail(function () {
            $("#VaFailedEmail").modal();
        })
        .always(function () { });
}


