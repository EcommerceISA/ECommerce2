$(document).ready(
    function () {
        $("#StateId").change(
            function () {
                $("#CityId").empty();
                $("#CityId").append('<option value = "0">[Select a city...]</option>');
                $.ajax({
                    type: 'POST',
                    url: UrlCities,
                    dataType: 'json',
                    data: { stateId: $("#StateId").val() },
                    success:
                    function (data) {
                        $.each(data,
                            function (i, data2) {
                                $("#CityId").append(
                                    '<option value="'
                                    + data2.CityId + '">'
                                    + data2.Name + '</option>');
                            });
                    },
                    error: function (ex) {
                        alert('Failed to retrieve cities.' + ex);
                    }
                });

                return false;
            })

        $("#CityId").change(
            function () {
                $("#CompanyId").empty();
                $("#CompanyId").append('<option value = "0">[Select a company...]</option>');
                $.ajax({
                    type: 'POST',
                    url: UrlCompanies,
                    dataType: 'json',
                    data: { cityId: $("#CityId").val() },
                    success:
                    function (data) {
                        $.each(data,
                            function (i, data2) {
                                $("#CompanyId").append(
                                    '<option value="'
                                    + data2.CompanyId + '">'
                                    + data2.Name + '</option>');
                            });
                    },
                    error: function (ex) {
                        alert('Failed to retrieve companies.' + ex);
                    }
                });

                return false;
            })

    });