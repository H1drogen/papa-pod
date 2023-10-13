import ky from "https://cdn.jsdelivr.net/npm/ky@0.31.0/distribution/index.min.js";

const country = await ky("/Country/GetAll").json();
const table = $("#table").DataTable({
  ajax: {
    url: "/Office/GetAll",
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
      data: "IsPopUp",
      title: "Is Pop-Up",
        render: (data, type, row) => (row.isPopUp ? "Yes" : "No"),
      className: "text-center py-1",
     },
    {
      data: "isActive",
      title: "Status",
      render: (data, type, row) => (row.isActive ? "Active" : "Inactive"),
      className: "text-center py-1",
    },
    {
      data: "countryId",
      render: (id) =>
              country.find((country) => country.id === id).name,
      title: "Country",
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
$("#create-new-btn").click(function (e) {
    e.preventDefault();
    $.ajax({
        url: '/Office/Create/',
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
                url: '/Office/GetByIdForEdit/',

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