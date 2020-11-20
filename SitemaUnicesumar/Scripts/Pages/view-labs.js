$(document).ready(function () {

    $(document).on('click', '.btn-schedule', function (e) {

        e.preventDefault();

        let data = {
            id: $(this).data("id")
        }

        $("#load-modal").show();

        $.ajax({
            url: "/ViewLabs/ManageScheduleLab",
            type: "POST",
            dataType: "json",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            success: function (result) {

                $('.modal-body').html(result);

                let dayNow = new Date().getDate();
                let monthNow = new Date().getMonth() + 1;
                let yearNow = new Date().getFullYear();   

                $("#Date").val(yearNow + "-" + monthNow + "-" + dayNow);
                $("#DateStart").val(yearNow + "-" + monthNow + "-" + dayNow);
                $("#DateEnd").val(yearNow + "-" + monthNow + "-" + dayNow);
            }
        });

    });

    $("#btn-filter").on("click", function () {

        $("#load-animate").show();
        $(".div-lab").css("opacity", "0.4");
        $(".div-filter").css("opacity", "0.4");
        $(".modal").css("opacity", "0.4");

        let dateReserve = $("#DateReserve").val();
        let parseDate = dateReserve.split("-");
        let dataFilter = new Date(parseDate[0], parseDate[1] - 1, parseDate[2]);
        let dayNow = new Date().getDate();
        let monthNow = new Date().getMonth();
        let yearNow = new Date().getFullYear();    

        if (dataFilter < new Date(yearNow, monthNow, dayNow)) {
            $("#load-animate").hide();
            $(".div-lab").css("opacity", "");
            $(".div-filter").css("opacity", "");
            $(".modal").css("opacity", "");

            return alert("A data desejada não pode ser menor que o dia atual");
        }

        let data = {
            HeadOfficeId: $("#HeadOfficeId").val(),
            BlockId: $("#BlockId").val(),
            LaboratoryCategoryId: $("#LaboratoryCategoryId").val(),
            LaboratoryCapacityId: $("#LaboratoryCapacityId").val(),
        }

        $.ajax({
            url: "/ViewLabs/Filter",
            type: "POST",
            dataType: "json",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                $('#laboratoryView').html(result);
                $("#load-animate").hide();
                $(".div-lab").css("opacity", "");
                $(".div-filter").css("opacity", "");
                $(".modal").css("opacity", "");
            }
        });
    });

    $(document).on("focusout", "#Date", function (e) {

        e.preventDefault();
        let dateReserve = $("#Date").val();
        let parseDate = dateReserve.split("-");
        let dataFilter = new Date(parseDate[0], parseDate[1] - 1, parseDate[2]);
        let dayNow = new Date().getDate();
        let monthNow = new Date().getMonth();
        let yearNow = new Date().getFullYear();

        if (dataFilter < new Date(yearNow, monthNow, dayNow)) {
            $("#load-animate").hide();
            $(".div-lab").css("opacity", "");
            $(".div-filter").css("opacity", "");
            $(".modal").css("opacity", "");

            let dayNow = new Date().getDate();
            let monthNow = new Date().getMonth() + 1;
            let yearNow = new Date().getFullYear();

            $("#Date").val(yearNow + "-" + monthNow + "-" + dayNow);

            return alert("A data desejada não pode ser menor que o dia atual");
        }
    });

    $(document).on("focusout", "#DateStart", function (e) {

        e.preventDefault();
        let dateReserve = $("#DateStart").val();
        let parseDate = dateReserve.split("-");
        let dataFilter = new Date(parseDate[0], parseDate[1] - 1, parseDate[2]);
        let dayNow = new Date().getDate();
        let monthNow = new Date().getMonth();
        let yearNow = new Date().getFullYear();

        if (dataFilter < new Date(yearNow, monthNow, dayNow)) {
            $("#load-animate").hide();
            $(".div-lab").css("opacity", "");
            $(".div-filter").css("opacity", "");
            $(".modal").css("opacity", "");

            let dayNow = new Date().getDate();
            let monthNow = new Date().getMonth() + 1;
            let yearNow = new Date().getFullYear();

            $("#DateStart").val(yearNow + "-" + monthNow + "-" + dayNow);

            return alert("A data desejada não pode ser menor que o dia atual");
        }
    });

    $(document).on("focusout", "#DateEnd", function (e) {

        e.preventDefault();
        let dateReserve = $("#DateEnd").val();
        let parseDate = dateReserve.split("-");
        let dataFilter = new Date(parseDate[0], parseDate[1] - 1, parseDate[2]);
        let dayNow = new Date().getDate();
        let monthNow = new Date().getMonth();
        let yearNow = new Date().getFullYear();

        if (dataFilter < new Date(yearNow, monthNow, dayNow)) {
            $("#load-animate").hide();
            $(".div-lab").css("opacity", "");
            $(".div-filter").css("opacity", "");
            $(".modal").css("opacity", "");

            let dayNow = new Date().getDate();
            let monthNow = new Date().getMonth() + 1;
            let yearNow = new Date().getFullYear();

            $("#DateEnd").val(yearNow + "-" + monthNow + "-" + dayNow);

            return alert("A data desejada não pode ser menor que o dia atual");
        }
    });

    $(document).on("change", "#TypeReserveId", function (e) {

        e.preventDefault();

        let typeReserve = $("#TypeReserveId").val();

        if (typeReserve == 0) {
            $(".div-date").show();
            $(".div-date-start").hide();
            $(".div-date-end").hide();
        }
        else if (typeReserve == 1) {
            $(".div-date").hide();
            $(".div-date-start").show();
            $(".div-date-end").show();
        }
    });

});