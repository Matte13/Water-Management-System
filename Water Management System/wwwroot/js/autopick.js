$("#compid").change(runme());


function runme() {

    $.ajax({
        url: '/WaterManagment/GetCrop',
        type: 'GET',
        async: true,
        cache: false,


        data: { id: $("#compid").val() },

        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            var s = '';

            for (var i = 0; i < result.length; i++) {

                s += '<option value="' + result[i].id + '">' +
                    result[i].name + '</option>';
            }
            $("#CropDropdown").html(s);

        }
    });
}