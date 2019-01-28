$('#UploadBtn').attr('disabled', true);
$('#overlay').hide();
$('#mytable').hide();

$('#UploadBtn').click(function () {
    var tables = $.fn.dataTable.fnTables(true);
    $(tables).each(function () {
        $(this).dataTable().fnClearTable();
        $(this).dataTable().fnDestroy();
    });

    $('#mytable thead tr').empty();
    $('#mytable tbody tr').empty();
    $('#overlay').show();
    $('#UploadBtn').attr('disabled', true);
    var fileUpload = $("#Files").get(0);
    var files = fileUpload.files;
    var fileData = new FormData();
    fileData.append(files[0].name, files[0]);

    $.ajax({
        xhr: function () {
            var xhr = new XMLHttpRequest();
            xhr.addEventListener("progress", function (e) {
                if (xhr.status == 200) {
                    $('#overlay').hide();
                    $('#mytable').animate({ width: '100%', height: '100%' }, 1000).show();
                }
            });
            return xhr;
        },
        url: '/Home/UploadFiles',
        type: "POST",
        contentType: false,
        processData: false,
        data: fileData,
        success: function (response) {
            var t = response.JsonString;
            if (t =="false") {
                $('#mytable').css({ 'box-shadow': 'none' });
                $('#modalbtn').click();
                $('#Files').val(null);
            }
            else
                $('#mytable').css({ 'box-shadow': '-1px 8px 27px 5px rgba(0,0,0,0.5)' });

            $('#overlay').hide();
            var result = JSON.parse(response.JsonString);
            var resultTable = result[0];
            var l = Object.keys(resultTable).length
            var tablehead = "<tr>";
            $.each(resultTable, function (key, val) {
                tablehead += "<th>" + val + "</th>";
            });
            tablehead += "</tr>";
            $('#mytable thead').append(tablehead);

            var tablebody = "";
            for (var i = 1; i < result.length; i++) {
                tablebody = "";
                resultTable = result[i];
                tablebody += "<tr class='tablerow'>";
                $.each(resultTable, function (key, val) {
                    tablebody += "<td>" + val + "</td>";
                });

                tablebody += "</tr>";
                $('#mytable tbody').append(tablebody);
            }
            $('#UploadBtn').attr('disabled', true);
            $('#mytable').DataTable();
        },
        error: function (err) {
            alert("Something Went Wrong.....");
        }
    });
});

$('#Files').change(function () {
    var fileName = $('#Files').val();
    if (fileName != null && fileName != "")
        $('#UploadBtn').attr('disabled', false);
});
