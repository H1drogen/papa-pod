const table = $("#course-module-templates").DataTable({
  ajax: {
    // We are calling the custom GetByNullPathwayTemplateId here instead of GetAll to get list of course module templates with null PathwayTemplateIds
    url: "/CourseModuleTemplate/GetByNullPathwayTemplateId",
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
      className: "text-center py-1",
      width: "auto",
    },
    {
      data: "description",
      title: "Description",
      className: "text-center py-1",
      width: "30%",
    },
    {
      data: "duration",
      title: "Duration (Days)",
      className: "text-center py-1",
      width: "auto",
    },
    {
      data: "isExtra",
      render: function (data, type, row) {
        if (row.isExtra == true) {
          return "Activity";
        } else {
          return "Course";
        }
      },
      title: "Type",
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

const getManagebuttons = () => {
    const manageButtons = document.querySelectorAll(".manage");
    for (const manage of manageButtons) {
        manage.addEventListener("click", (event) => {
            const id = event.target.parentElement.parentElement.dataset.id;

            $.ajax({
                url: '/CourseModuleTemplate/GetByIdForEdit/',

                data: { id: id },
                success: function (data) {
                    $(".update-modal").html(data);
                    $("#update-template-modal").modal("show");
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