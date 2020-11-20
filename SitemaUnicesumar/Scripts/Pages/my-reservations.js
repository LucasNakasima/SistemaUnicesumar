$(document).ready(function () {

    $('.view-reserve').on('click', function (e) {
        e.preventDefault();

        let data = {
            id: $(this).data("id")
        }

        $("#load-modal").show();

        $.ajax({
            url: "/MyReservations/ViewReserve",
            type: "POST",
            dataType: "json",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                $('.modal-body').html(result);
            }
        });

    });

    $('.cancel-reserve').on('click', function (e) {
        e.preventDefault();

        let data = {
            id: $(this).data("id")
        }

        $("#load-modal").show();

        $.ajax({
            url: "/MyReservations/CancelReserveModal",
            type: "POST",
            dataType: "json",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                $('.modal-body').html(result);
            }
        });

    });

});