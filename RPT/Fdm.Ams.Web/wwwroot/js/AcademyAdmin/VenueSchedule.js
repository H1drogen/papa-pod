import ky from "https://cdn.jsdelivr.net/npm/ky@0.30.0/distribution/index.min.js"; // modern http client


// Get all requests at the same time async
const [venues, courses, courseTypes, offices] = await Promise.all([
    ky("/Venue/GetAll").json(),
    ky("/Course/GetAll").json(),
    ky("/CourseType/GetAll").json(),
    ky("/Office/GetAll").json(),
]);

const getCourseTypeById = (id) => courseTypes.find((type) => type.Id === id);
const getOfficeById = (id) => offices.find((type) => type.Id === id);

// Format for fullcalendar
const formatEvents = async (start, end) => {
    const modules = await ky("/CourseModule/GetAll", {
        searchParams: { start, end },
    }).json();

    return await Promise.all(
        modules.map(async ({ CourseId, ...event }) => {
            let color;
            let courseCode;

            if (CourseId) {
                let CourseTypeId;
                ({ CourseTypeId, CourseCode: courseCode } = await ky(
                    `/Course/GetById/${CourseId}`
                ).json());

                ({ Colour: color } = getCourseTypeById(CourseTypeId));
            }

            return {
                id: event.Id,
                title: event.Name,
                start: event.StartDate,
                end: event.EndDate,
                resourceId: event.VenueId,
                color,
                extendedProps: {
                    courseCode,
                },
            };
        })
    );
};

const formatResources = async (start, end) => {
    venues.push({ Id: null, Name: "Unassigned" });

    return venues.map((resource) => {
        const office = getOfficeById(resource?.OfficeId);

        return {
            id: resource.Id,
            title: resource.Name,
            group: office?.Name,
        };
    });
};

// setup dayjs timezone plugin
dayjs.extend(window.dayjs_plugin_utc);
dayjs.extend(window.dayjs_plugin_timezone);
dayjs.extend(window.dayjs_plugin_customParseFormat);

let eventList = [];
var calendarE1 = document.getElementById("venueCalendar");
let calendar = new FullCalendar.Calendar(calendarE1, {
    initialView: "resourceTimeline6weeks",
    resourceGroupField: "group",
    resources: async (info, successCallback) =>
        successCallback(await formatResources(info.startStr, info.endStr)),
    events: async (info, successCallback) =>
        successCallback(await formatEvents(info.startStr, info.endStr)),
    schedulerLicenseKey: "",
    eventStartEditable: false,
    eventDurationEditable: false,
    eventOverlap: true,
    editable: true,
    height: "auto",
    views: {
        resourceTimeline1week: {
            type: "resourceTimeline",
            dateIncrement: { weeks: 1 },
            duration: { weeks: 1 },
            buttonText: "1 Week",
            slotDuration: { days: 5 },
            slotLabelInterval: { day: 1 },
            slotLabelFormat: [
                {
                    month: "long",
                    week: "long",
                }, // top level of text
                {
                    weekday: "short",
                    day: "2-digit",
                }, // lower level of text
            ],
        },
        resourceTimeline6weeks: {
            type: "resourceTimeline",
            duration: { weeks: 6 },
            buttonText: "6 Weeks",
        },
        resourceTimeline12weeks: {
            type: "resourceTimeline",
            duration: { weeks: 12 },
            buttonText: "12 Weeks",
        },
    },
    customButtons: {
        prev: {
            text: "Previous Week",
            click: function () {
                if (
                    eventList.length > 0 &&
                    confirm(
                        "There are unsaved changes on the current page. Do you want to save?"
                    )
                ) {
                    putAllEvents(this);
                    calendar.prev();
                } else if (eventList.length == 0) {
                    calendar.prev();
                } else {
                    alert("Please reset your changes");
                }
            },
        },
        next: {
            text: "Next Week",
            click: function () {
                if (
                    eventList.length > 0 &&
                    confirm(
                        "There are unsaved changes on the current page. Do you want to save?"
                    )
                ) {
                    putAllEvents(this);
                    calendar.prev();
                } else if (eventList.length == 0) {
                    calendar.next();
                } else {
                    alert("Please reset your changes");
                }
            },
        },
        saveButton: {
            text: "Save",
            click: function () {
                if (confirm("Do you want to save?")) {
                    putAllEvents(this);
                }
            },
        },
        today: {
            text: "Today",
            click: function () {
                calendar.today();
            },
        },
        resetButton: {
            text: "Reset",
            click: function () {
                if (confirm("Do you want to reset your unsaved changes?")) {
                    eventList = [];
                    $(".fc-saveButton-button").first().prop("disabled", true);
                    $(".fc-resetButton-button").first().prop("disabled", true);
                    alert("All event unsaved changes have been reset");
                    calendar.refetchEvents();
                }
            },
        },
    },
    headerToolbar: {
        left: "resourceTimeline1week,resourceTimeline6weeks,resourceTimeline12weeks",
        center: "title",
        right: "prev,next today saveButton resetButton",
    },
    eventDrop: function (event) {
        var data = {
            Id: event.event._def.publicId,
            StartDate: event.event._instance.range.start.toDateString(
                "YYYY/MM/DD HH:mm:ss A"
            ),
            EndDate: event.event._instance.range.end.toDateString(
                "YYYY/MM/DD HH:mm:ss A"
            ),
            VenueId:
                event.newResource != null
                    ? parseInt(event.newResource._resource.id)
                    : parseInt(event.event._def.resourceIds[0]),
            TrainerId: event.event.extendedProps.TrainerId,
        };
        eventList.push(data);
        $(".fc-saveButton-button").first().prop("disabled", false);
        $(".fc-resetButton-button").first().prop("disabled", false);
    },
    eventClick: function (event) {
        $.ajax({
            type: "GET",
            url: "/CourseModule/GetById/",
            data: { id: event.event._def.publicId },
            success: async function (receivedData) {
                $("#trainerDropdown").val(receivedData.TrainerId);
                $("#trainerDropdown").trigger("change");
                $("#current-trainer-id").attr("data-id", receivedData.TrainerId);
                $("#venue-id").attr("data-id", receivedData.VenueId);

                $("#venue-schedule-update").modal("show");
                $("#delete-btn").attr("data-id", parseInt(receivedData.Id));
                $("#modal-save-venue").attr("data-id", parseInt(receivedData.Id));
                $("#content-name").val(receivedData.Name);
                $("#content-description").val(receivedData.Description);
                $("#content-prep").val(receivedData.PreparationNotes);

                const moduleStartDate = document.getElementById("module-start-date");
                const moduleEndDate = document.getElementById("module-end-date");
                const courseStartDate = document.getElementById("course-start-date");
                const courseEndDate = document.getElementById("course-end-date");

                const formatDate = (date) => dayjs(date).format("DD/MM/YYYY");

                const course = await ky(
                    `/Course/GetById/${receivedData.CourseId}`
                ).json();

                moduleStartDate.innerHTML = formatDate(receivedData.StartDate);
                moduleEndDate.innerHTML = formatDate(receivedData.EndDate);
                courseStartDate.innerHTML = formatDate(course.StartDate);
                courseEndDate.innerHTML = formatDate(course.EndDate);
            },
        });
    },
    slotDuration: { days: 1 },
    slotLabelInterval: { weeks: 1 },
    slotLabelFormat: [
        {
            month: "long",
            week: "long",
        }, // top level of text
        function (date) {
            const start = moment(date.date).format("MMM DD");
            const end = moment(date.date).add(4, "days").format("MMM DD");
            return `${start} - ${end}`;
        }, // lower level of text
    ],
    resourceAreaWidth: "10%",
    weekends: false,
    displayEventTime: false,

    eventDidMount: function (info) {
        createTooltip(info.el, info.event.title);
    },
    eventWillUnmount: function (info) {
        destroyTooltip(info.el);
    },

    eventContent: function (arg) {
        return {
            html: `<span class="ml-1">${arg.event.extendedProps.courseCode}</span> - ${arg.event.title}`,
        };
    },
});

const createTooltip = (element, title) => {
    const options = {
        title: title,
        container: "body",
    };

    $(element).tooltip(options);
};

const destroyTooltip = (element, enabled = true) => {
    $(element).tooltip("dispose");
};

//This function will be recursive, as it ensures that the events are called serially
function putAllEvents(buttonClicked) {
    if (eventList.length > 0) {
        //This checks length of event list. If there are elements, the function will continue to call itself
        var event = eventList.shift(); //This moves out the first element in the array

        var url = "/CourseModule/Put";
        if (isNaN(event.VenueId))
            //This checks if the event is an "Unassign"
            url = url + "UnAssign";

        $.ajax({
            type: "PUT",
            url: url,
            data: event,
            success: function () {
                putAllEvents(buttonClicked);
            },
        });
    } else if ($(buttonClicked).attr("id") == "modal-save-venue") {
        //This will run if the modal save changes button was clicked
        $.ajax({
            type: "PUT",
            url: "/CourseModule/Put",
            data: {
                Id: $(buttonClicked).attr("data-id"),
                Name: $("#content-name").val(),
                Description: $("#content-description").val(),
                PreparationNotes: $("#content-prep").val(),
                TrainerId: $("#trainerDropdown").find(":selected").val(),
            },
            success: function (response) {
                if (response.success) {
                    calendar.refetchEvents();
                    $("#venue-schedule-update").modal("hide");
                    alert("Successfully edited");
                } else {
                    alert(response.responseText);
                }
            },
        });
    } else if ($(buttonClicked).attr("id") == "confirm-delete-btn") {
        //This will run if the confirm delete button has been clicked
        $.ajax({
            type: "Delete",
            url: "/CourseModule/Delete",
            data: {
                id: $("#delete-btn").attr("data-id"),
            },

            success: function () {
                $("#venue-schedule-update").modal("hide");
                alert("event has been deleted");
                calendar.refetchEvents();
            },
        });
    } else if ($(buttonClicked).attr("id") == "unassign") {
        $.ajax({
            url: "/CourseModule/PutUnAssign",
            data: {
                Id: $("#modal-save-venue").attr("data-id"),
                TrainerId: $("#current-trainer-id").attr("data-id"),
            },
            success: function () {
                calendar.refetchEvents();
                $("#venue-schedule-update").modal("hide");
            },
        });
    } else {
        //If the calendar save button is used, it will run this snippet instead
        $(".fc-saveButton-button").first().prop("disabled", true);
        $(".fc-resetButton-button").first().prop("disabled", true);
        alert("All event changes have been saved");
    }
}

$("#unassign").click(function () {
    if (confirm("Are you sure you want to unassign this module?")) {
        putAllEvents(this);
    }
});

$("#modal-save-venue").click(function () {
    if (confirm("Are you sure you want to save?")) {
        const saveButton = document.getElementById("modal-save-venue");
        saveButton.innerText = "Saving...";
        saveButton.disabled = true;
        putAllEvents(this);
    }
});

$("#delete-btn").click(function () {
    $("#confirm-delete").slideDown();
});

$("#confirm-delete-btn").click(function () {
    if (
        confirm(
            "WARNING!!!! \n" +
            "\nYou are about to delete this course module\n\nIf you delete this," +
            "this can't be restored\n\nIf you have changed your mind, please cancel this action"
        )
    ) {
        putAllEvents(this);
    }
});
$.ajax({
    type: "GET",
    url: "/Trainer/Get",
    dataType: "json",
    success: function (data) {
        data.forEach((trainerData) => {
            var div_data =
                "<option value=" +
                trainerData.Id +
                ">" +
                trainerData.Name +
                "</option>";
            $("#trainerDropdown").append(div_data);
        });
    },
});

$("#venue-schedule-update").on("hidden.bs.modal", function () {
    $("#confirm-delete").hide();

    const saveButton = document.getElementById("modal-save-venue");
    saveButton.innerText = "Save changes";
    saveButton.disabled = false;
});

$("#trainerDropdown").select2({
    width: "50%",
});

$("#confirm-delete").hide();

calendar.render();

$(".fc-saveButton-button").first().prop("disabled", true);
$(".fc-resetButton-button").first().prop("disabled", true);
*/
// weekly calendar code --------------------------------------

function removeChildrenElem(selector) {
    $(selector).empty();
}

function setMonthTitle(monthList, yearList) {
    let months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    let monthTitle = "";
    monthList.forEach((monthIndex, index) => {
        if (monthTitle.length != 0) {
            monthTitle += " - ";
        }
        monthTitle += months[monthIndex - 1];
        if (yearList.length > 1) {
            monthTitle += " " + yearList[index];
        }
    })
    if (yearList.length == 1) {
        monthTitle += " " + yearList[0];
    }
    $(".currMonth").text(monthTitle);
    return;
}

function OneWeekLayout() {
    console.log("ONE WEEK LAYOUT");
    // get current date
    
}

function FiveWeekLayout() {
    // get current date
    let curr = new Date();

    // Get the Monday of the current week
    let currentMonday = new Date();
    currentMonday.setDate(curr.getDate() - curr.getDay() + 1);

    let currMonthList = [];
    let currYearList = [];
}

$(() => {
    let curr = new Date();

    // Get the Monday of the current week
    let currentMonday = new Date();
    currentMonday.setDate(curr.getDate() - curr.getDay() + 1);

    let currMonthList = [];
    let currYearList = [];

    // Labels for each day
    let days = ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"];

    // loop 7 times to get all days of the week
    let i = 0;
    for (i; i < 7; i++) {
        console.log(i);
        // apply formatting
        if (i < 5) {
            // add to HTML doc
            $(".date").append(
                "<div class='currDateCol' >" +
                "<span>" + days[(currentMonday.getDay() + 6) % 7] + "</span>" +
                "<h4>" + currentMonday.getDate() + "</h4>" +
                "<div id='SLOT" + days[(currentMonday.getDay() + 6) % 7] + currentMonday.getDate() + "' class='slot' ondrop='drop(event)' ondragover='allowDrop(event)'></div>" +
                "</div>"
            );

            if (!currMonthList.includes(currentMonday.getMonth() + 1)) {
                currMonthList.push(currentMonday.getMonth() + 1);
            }
            if (!currYearList.includes(currentMonday.getFullYear())) {
                currYearList.push(currentMonday.getFullYear());
            }
        }

        // increment for the next day
        currentMonday.setDate(currentMonday.getDate() + 1);
    }

    setMonthTitle(currMonthList, currYearList);

    // next button code
    $("#nextWeek").on("click", function () {
        console.log("NEXT WEEK FUNCTION");
        // remove current children elements of specified element
        removeChildrenElem(".date");
        currMonthList = [];
        currYearList = [];
        // rerun the loop to get the next week
        let i = 0;
        for (i; i < 7; i++) {
            if (i < 5) {
                $(".date").append(
                    "<div class='currDateCol new' >" +
                    "<span>" + days[(currentMonday.getDay() + 6) % 7] + "</span>" +
                    "<h4>" + currentMonday.getDate() + "</h4>" +
                    "<div id='SLOT" + days[(currentMonday.getDay() + 6) % 7] + currentMonday.getDate() + "' class='slot' ondrop='drop(event)' ondragover='allowDrop(event)'></div>" +
                    "</div>"
                );

                if (!currMonthList.includes(currentMonday.getMonth() + 1)) {
                    currMonthList.push(currentMonday.getMonth() + 1);
                }
                if (!currYearList.includes(currentMonday.getFullYear())) {
                    currYearList.push(currentMonday.getFullYear());
                }
            }
            // increment for the next day
            currentMonday.setDate(currentMonday.getDate() + 1);
        }
        setMonthTitle(currMonthList, currYearList);
    });

    // previous button code
    $("#prevWeek").on("click", function () {
        console.log("PREV WEEK FUNCTION");
        // remove current children elements of specified element
        removeChildrenElem(".date");
        currMonthList = []
        currYearList = [];

        // rerun the loop to get the next week
        currentMonday.setDate(currentMonday.getDate() - 14);
        let i = 0;
        for (i; i < 7; i++) {
            if (i < 5) {
                $(".date").append(
                    "<div class='currDateCol new' >" +
                    "<span>" + days[(currentMonday.getDay() + 6) % 7] + "</span>" +
                    "<h4>" + currentMonday.getDate() + "</h4>" +
                    "<div id='SLOT" + days[(currentMonday.getDay() + 6) % 7] + currentMonday.getDate() + "' class='slot' ondrop='drop(event)' ondragover='allowDrop(event)'></div>" +
                    "</div>"
                );
                if (!currMonthList.includes(currentMonday.getMonth() + 1)) {
                    currMonthList.push(currentMonday.getMonth() + 1);
                }
                if (!currYearList.includes(currentMonday.getFullYear())) {
                    currYearList.push(currentMonday.getFullYear());
                }
            }

            // increment for the next day
            currentMonday.setDate(currentMonday.getDate() + 1);
        }
        setMonthTitle(currMonthList, currYearList);
    });
});