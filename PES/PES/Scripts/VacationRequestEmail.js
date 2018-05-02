
$(".btn-send-mail").on("click", function (e) {
    e.stopPropagation();
    var fechas = $('#start').attr('value')
    var stringFechas = fechas.toString();
    var id = $(this).attr('id');
    var VacDaysReq = $(this).attr('value');
    sendedImail(id, VacDaysReq, stringFechas);
    $('.spinner').css('display', 'block');
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
            $('.spinner').hide();
            if (data) {
                $("#VaReEmail").modal();
            } else {
                $("#VaFailedEmail").modal();
            }

        })
        .fail(function () {
            stopAnimation('.spinner');
            $("#VaFailedEmail").modal();
        })
        .always(function () { });
}


$('#cancelBotomModal').on("click", function (e) {
    var Fecha = $('#start').val();
    var starDate = moment(Fecha.substring(0, 10));
    var today = moment(new Date());
    var compareToday = today.add(2,'days');
    var uncomingDate = true;
    //True es no puede cancelar si es false si puede cancelar
    if (starDate < compareToday) {
        uncomingDate = true;
        $("#VaFailedEmail").modal();
    }
    else {
        uncomingDate = false;
        $("#cancelFormModal").submit();
    }
});
