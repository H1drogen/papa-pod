const table = $("#course-types").DataTable({
    ajax: {
        url: "/CourseType/GetAll",
        dataSrc: "",
    },
    columns: [
        {
            data: "name",
            title: "Name",
            className: "text-center py-1",
            width: "auto",
        },
        {
            data: "abbreviation",
            title: "Abbreviation",
            className: "text-center py-1",
            width: "auto",
        },
        {
            data: "colour",
            title: "Colour",
            width: "auto",
            className: "text-center py-1",
            createdCell: (row, colour) => (row.style.backgroundColor = colour),
        },
        {
            data: "isConcludingPathway",
            title: "IsConcludingPathway",
            className: "text-center py-1",
            width: "auto",
        },

        {
            render: (_) =>
                `<button type="button" class="btn btn-secondary btn-sm px-4 editCourseType" >Manage</button>`,
            className: "py-2",
            width: "auto"
        },
    ],
    createdRow: (row, data) => $(row).attr("data-id", data.id),

});

const getManagebuttons = () => {
    const editButtons = document.querySelectorAll(".editCourseType");
    for (const editCourseType of editButtons) {
        editCourseType.addEventListener("click", (event) => {
            const id = event.target.parentElement.parentElement.dataset.id;

            $.ajax({
                url: '/CourseType/GetByIdForEdit/',
                data: { id: id },
                success: function (data) {
                    $(".modal-dialog-update").html(data);
                    $("#update-type-modal").modal("show");
                    $('#partialform').removeData("validator");
                    $('#partialform').removeData("unobtrusiveValidation");
                    $.validator.unobtrusive.parse('#partialform');
                }
            });
        });
    }
}
table.on('draw', () => getManagebuttons());
getManagebuttons();
