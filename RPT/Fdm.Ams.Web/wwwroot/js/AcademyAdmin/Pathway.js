import ky from "https://cdn.jsdelivr.net/npm/ky@0.31.0/distribution/index.min.js";

const courseTypes = await ky("/CourseType/GetAll").json();
const regions = await ky("/Region/GetAll").json();
const programmes = await ky("/Programme/GetAll").json();

const table = $("#pathways").DataTable({
    ajax: {
        url: "/Course/GetAll",
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
            data: "pathwayCode",
            title: "Pathway Code",
            width: "auto",
            className: "text-center",
        },
        {
            data: "startDate",
            title: "Start Date",
            render: (data) => moment(data).format("DD/MM/YYYY"),
            width: "auto",
            className: "text-center",
        },
        {
            data: "endDate",
            title: "End Date",
            render: (data) => moment(data).format("DD/MM/YYYY"),
            width: "auto",
            className: "text-center",
        },
        {
            data: "createdBy",
            title: "Created By",
            width: "auto",
            className: "text-center",
        },
        {
            data: "isPond",
            render: (data, type, row) => (row.isPond ? "Yes" : "No"),
            title: "Is Pond",
            width: "auto",
            className: "text-center",
        },
        {
            data: "cancelled",
            render: (data, type, row) => (row.cancelled ? "Yes" : "No"),
            title: "Cancelled",
            width: "auto",
            className: "text-center",
        },
        {
            data: "maxCapacity",
            title: "Max Capacity",
            width: "auto",
            className: "text-center",
        },
        {
            data: "pathwayTypeId",
            render: (id) =>
                courseTypes.find((courseType) => courseType.id === id).name,
            title: "Pathway Type",
            width: "auto",
            className: "text-center",
        },
        {
            data: "regionId",
            render: (id) => regions.find((region) => region.id === id).name,
            title: "Region",
            width: "auto",
            className: "text-center",
        },
        {
            data: "programmeId",
            render: (id) => programmes.find((programme) => programme.id === id).name,
            title: "Programme",
            width: "auto",
            className: "text-center",
        },
        {
            render: (_) => `<button type="button" class="btn btn-secondary btn-sm px-4 manage">View</button>`,
            title: "Action",
            width: "auto",
            className: "text-center",
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
                url: '/Course/GetDetails/',

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