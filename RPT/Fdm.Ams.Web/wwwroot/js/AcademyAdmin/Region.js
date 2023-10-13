const table = $("#table").DataTable({
    ajax: {
        url: "/Region/GetAll",
        dataSrc: "",
    },
    columns: [
        {
            data: "name",
            title: "Name",
            className: "text-center py-1",
        },
        {
            data: "abbreviation",
            title: "Abbreviation",
            className: "text-center py-1",
        },
        {
            data: "isActive",
            title: "Status",
            render: (data, type, row) => (row.isActive ? "Active" : "Inactive"),
            className: "text-center py-1",
        },
        {
            title: "Action",
            render: (_) =>
                `<button type="button" class="btn btn-secondary btn-sm px-4 manage">Manage</button>`,
            className: "text-center py-1",
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
                url: '/Region/GetByIdForEdit/',

                data: { id: id },
                success: function (data) {
                    $(".update-modal").html(data);
                    $("#update-region").modal("show");
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