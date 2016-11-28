$(function () {

    $('input[name="period"]').click(function () {
        var period = $('input[name="period"]:checked').val();
        $.ajax({
            type: "POST",
            url: "/Lateness/GetLatenessByFilter/",
            data: { period: period },
            success: function (data) {
                $("#table-content").html("").hide("fast");
                $("#table-content").html(data).fadeOut("normal").fadeIn(1000);

                $('#Tableview').DataTable({
                    dom: '<"row"<"col-md-6"B><"col-md-6"f>>',
                    buttons: [
                        'excel', 'pdf'],
                    pageLength: -1 
                });
            
            }
        });
    });

    $('#Tableview').DataTable({
        dom: '<"row"<"col-md-6"B><"col-md-6"f>>',
        buttons: [
            'excel', 'pdf'],
        pageLength: -1
    });
});