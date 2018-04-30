
$(".btn-send-mail").on("click", function (e) {
    e.stopPropagation();
    var id = $(this).attr('id');
    var VacDaysReq = $(this).attr('value');
    sendedImail(id, VacDaysReq);
    //$("#VaReEmail").modal();
});

function sendedImail(id, VacDaysReq) {
    $.ajax({
        url: "/VacationRequest/SendConfirmationHR",
        data: {
            userid: id,
            VacationDays: VacDaysReq
        }
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
