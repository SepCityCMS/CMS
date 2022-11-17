function StateBox(AccessToken, CountryCode, controlToPopulate) {
    var IP2Location = {
        CountryCode: CountryCode
    };

    $.ajax({
        url: "https://www.sepcity.com/api/ProvincesInCountry",
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        beforeSend: function (request) {
            request.setRequestHeader("Authorization", "BEARER " + AccessToken);
        },
        success: function (data) {
            controlToPopulate.find("option").remove();
            controlToPopulate.append($("<option>", {
                value: "",
                text: "--- Select a State/Province ---"
            }));
            if (CountryCode == "us") {
                $("#DistanceText").text("miles");
            } else {
                $("#DistanceText").text("kilometers");
            }
            $.each(data.Provinces, function (index, province) {
                controlToPopulate.append($("<option>", {
                    value: province,
                    text: province
                }));
            });
        },
        data: JSON.stringify(IP2Location)
    });
}

//https://stackoverflow.com/questions/9656523/jquery-autocomplete-with-callback-ajax-json
//function CityBox(AccessToken, CountryCode, Province, controlToPopulate) {
//    var IP2Location = {
//        CountryCode: CountryCode,
//        Province: Province
//    }

//    controlToPopulate.autocomplete({
//        source: function (request, response) {
//            $.ajax({
//                url: "https://new.sepcity.com/api/CitiesInProvince",
//                type: 'POST',
//                dataType: 'json',
//                contentType: "application/json",
//                beforeSend: function (request) {
//                    request.setRequestHeader("Authorization", "BEARER " + AccessToken);
//                },
//                data: JSON.stringify(IP2Location),
//                success: function (data) {
//                    response(data);
//                }
//            });
//        },
//        minLength: 3,
//        select: function (event, ui) {
//            log(ui.item ?
//                "Selected: " + ui.item.label :
//                "Nothing selected, input was " + this.value);
//        },
//        open: function () {
//            $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
//        },
//        close: function () {
//            $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
//        }
//    });
//}