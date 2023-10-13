import ky from "https://cdn.jsdelivr.net/npm/ky@0.31.0/distribution/index.min.js";
const offices = await ky("/Office/GetAll").json();
const table = $("#trainers").DataTable({
    ajax: {
        url: "/Trainer/GetAll",
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
            data: "firstName",
            title: "First Name",
            className: "text-center py-1",
            width: "auto",
        },
        {
            data: "lastName",
            title: "Last Name",
            className: "text-center py-1",
            width: "auto",
        },
        {
            data: "email",
            title: "Email",
            className: "text-center py-1",
            width: "auto",
        },
        {
            data: "username",
            title: "Username",
            className: "text-center py-1",
            width: "auto",
        },
        {
            data: "teamName",
            title: "Team Name",
            className: "text-center py-1",
            width: "auto",
        },
        {
            data: "officeId",
            render: (id) =>
                offices.find((office) => office.id === id).name,
            title: "Office",
            className: "text-center py-1",
            width: "auto",
        },
        {
            data: "active",
            render: (data, type, row) => (row.active ? "Active" : "Inactive"),
            title: "Status",
            className: "text-center py-1",
            width: "auto",
        },
        {
            title: "Action",
            render: (_) =>
                `<button type="button" class="btn btn-secondary btn-sm px-4 manage">Manage</button>`,
            className: "py-2",
            width: "auto",
        },
    ],
    createdRow: (row, data) => $(row).attr("data-id", data.id),
});

$("#create-new-btn").click(function (e) {
    e.preventDefault();
    $.ajax({
        url: '/Trainer/Create/',
        success: function (data) {
            $('#create-modal> .modal-dialog').html(data);
            $("#create-modal").modal("show");
            $.validator.unobtrusive.parse("#partial-form");
        }
    });
});
const getManagebuttons = () => {
    const manageButtons = document.querySelectorAll(".manage");
    for (const manage of manageButtons) {
        manage.addEventListener("click", (event) => {
            const id = event.target.parentElement.parentElement.dataset.id;

            $.ajax({
                url: '/Trainer/GetByIdForEdit/',

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