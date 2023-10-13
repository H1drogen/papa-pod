import ky from "https://cdn.jsdelivr.net/npm/ky@0.31.0/distribution/index.min.js";
let countries = null;
$.ajax({
    url: "/Country/GetAll",
    success: function (result) {
        countries = result;
    }
});

const table = $("#create-holiday").DataTable({
    ajax: {
        url: "/Holiday/GetAll",
        dataSrc: "",
    },
    pagingType: "simple_numbers",
    sScrollX: "100%",
    sScrollXInner: "100%",
    scrollY: "100%",
    scrollCollapse: true,
    pageLength: 20,
    lengthMenu: [
        [20, 40, 60, -1],
        [20, 40, 60, "ALL"],
    ],
    language: { searchPlaceholder: "Search", search: "" },
    initComplete: function (settings, json) {
        $("body").find(".dataTables_scrollBody").addClass("scrollbar");
    },

    columns: [
        {
            data: "name",
            title: "Name",
            width: "auto",
            className: "text-center py-1",
        },
        {
            data: "description",
            title: "Description",
            width: "auto",
            className: "text-center py-1",
        },
        {
            data: "countryId",
            render: (id) =>
                countries.find((country) => country.id === id).name,
            title: "Country Name",
            width: "auto",
            className: "text-center py-1",
        },
        {
            data: "startDate",
            title: "Start Date",
            width: "auto",
            render: (data) => moment(data).format("DD/MM/YYYY"),
            className: "text-center py-1",
        },
        {
            data: "endDate",
            title: "End Date",
            width: "auto",
            render: (data) => moment(data).format("DD/MM/YYYY"),
            className: "text-center py-1",
        },
        {
            render: (_) =>
                `<button type="button" class="btn btn-secondary btn-sm px-4 manage">Manage</button>`,
            width: "auto",
            className: "text-center py-2",
        },

    ],

    createdRow: (row, data) => $(row).attr("data-id", data.id),
});
const getManagebuttons = () => {
    const manageButtons = document.querySelectorAll(".manage");
    for (const manage of manageButtons) {
        manage.addEventListener("click", (event) => {
            const id = event.target.parentElement.parentElement.dataset.id;

            $.ajax({
                url: '/Holiday/GetByIdForEdit/',

                data: { id: id },
                success: function (data) {
                    $(".update-modal").html(data);
                    $("#update-modal").modal("show");
                    $("#partial-form").removeData("validator");
                    $("#partial-form").removeData("unobtrusiveValidation");
                    $.validator.unobtrusive.parse("#partial-form");
                }
            });
        });
    }
}
table.on("draw", () => getManagebuttons());
getManagebuttons();