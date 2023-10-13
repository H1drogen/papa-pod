import ky from "https://cdn.jsdelivr.net/npm/ky@0.31.0/distribution/index.min.js";

const table = $("#create-programme").DataTable({
    ajax: {
        url: "/Programme/GetAll",
        dataSrc: "",
    },
    pagingType: "simple_numbers",
    scrollX: false,
    scrollY: "calc(100vh - 350px)",
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
            className: "text-center py1",
        },
        {
            data: "description",
            title: "Description",
            width: "auto",
            className: "text-center py-1",
        },
        {
            data: "abbreviation",
            title: "Abbreviation",
            width: "auto",
            className: "text-center py-1",
        },
        {
            render: (_) =>
                `<button type="button" class="btn btn-secondary btn-sm px-4 manage">Manage</button>`,
            width: "auto",
            className: "text-center py-2"
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
                url: '/Programme/GetByIdForEdit/',
                data: { id: id },
                success: function (data) {
                    $(".modal-dialog-update").html(data);
                    $("#update-type-modal").modal("show");
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