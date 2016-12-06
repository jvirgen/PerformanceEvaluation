$(function () {

    if ($('#Tableview').length) 
    {
        $('#Tableview').DataTable({
            dom: '<"row"<"col-md-6"B><"col-md-6"f>> rtp',
            buttons: [
                'excel', 'pdf'],
            pagingType: "full_numbers"
        });
    }
    

    //To upload the excel
    $(document).on('change', ':file', function () { $("#lateness").submit(); });
    $("#saveFile").on("click", function () { $("#myModal").modal(); });

    $("#confirm").on("click", function () {
        $.post("/Lateness/SaveLatenessExcel", function (done) {
            if(done == "True")
            {
                var message = "<div class=\"alert alert-success\">" +
                                "<a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a>" +
                                "The lateness report has been saved successfully!" +
                              "</div>";
                $("body table:last").before(message);
            } else {
                var message = "<div class=\"alert alert-danger\">" +
                                "<a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a>" +
                                "You already save this file, reload the file or select a new file." +
                              "</div>";
                $(".alert").remove();
                $("body table:last").before(message);
            }
        });
    });
});