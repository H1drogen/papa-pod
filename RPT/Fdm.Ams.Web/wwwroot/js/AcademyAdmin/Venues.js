$(document).ready(function () {
    $("#venueTable").DataTable({
        ajax: {
            url: "/Venue/GetAll",
            type: "GET",
            dataSrc: ""
        },
        columns: [
            { data: "Id" }
        ]
    });
});