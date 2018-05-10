
$(".btn-send-mail").on("click", function (e) {
    e.stopPropagation();
    var fechas = $('#start').attr('value');
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
            $('.spinner').hide();
            $("#VaFailedEmail").modal();
        })
        .always(function () { });
}


$('#cancelBotomModal').on("click", function (e) {    
        $("#cancelFormModal").submit();    
});
