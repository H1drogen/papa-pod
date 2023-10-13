import ky from "https://cdn.jsdelivr.net/npm/ky@0.31.0/distribution/index.min.js";

const courseTypes = await ky("/CourseType/GetAll").json();
const regions = await ky("/Region/GetAll").json();

const table = $("#course-templates").DataTable({
    ajax: {
        url: "/CourseTemplate/GetAll",
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
            data: "pathwayTypeId",
            render: (id) =>
                courseTypes.find((courseType) => courseType.id === id).name,
            title: "Pathway Type",
            width: "auto",
            className: "text-center py-1",
        },
        {
            data: "regionId",
            render: (id) =>
                regions.find((region) => region.id === id).name,
            title: "Region",
            width: "auto",
            className: "text-center py-1",
        },
        {
            data: "id",
            render: (id) =>
                `<a type="button" href="/CourseTemplate/GetByIdForEdit/${id}" class="btn btn-secondary btn-sm px-4 text-white">Manage</a>`,
            width: "auto",
            className: "text-center py-2",
        },
    ],
    createdRow: (row, data) => $(row).attr("data-id", data.id),
});