
$(".btn-send-mail").on("click", function (e) {
    e.stopPropagation();
    var fechas = $('#start').attr('value')
    var stringFechas = fechas.toString();
    var id = $(this).attr('id');
    var VacDaysReq = $(this).attr('value');
    sendedImail(id, VacDaysReq, stringFechas);
    //$("#VaReEmail").modal();
});

function sendedImail(id, VacDaysReq, stringFechas) {
    $.ajax({
        url: "/VacationRequest/SendConfirmationHR",
        data: {
            userid: id,
            VacationDays: VacDaysReq,
            Sfechas: stringFechas
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
