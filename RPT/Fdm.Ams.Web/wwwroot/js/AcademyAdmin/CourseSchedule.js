import ky from "https://cdn.jsdelivr.net/npm/ky@0.30.0/distribution/index.min.js";

// Get all requests at the same time async
const [courseTypes, regions, templates, trainers] = await Promise.all([
    ky("/CourseType/GetAll").json(),
    ky("/Region/GetAll").json(),
    ky("/CourseTemplate/GetAll").json(),
    ky("/Trainer/GetAll").json(),
]);

// setup dayjs timezone plugin
dayjs.extend(window.dayjs_plugin_utc);
dayjs.extend(window.dayjs_plugin_timezone);
dayjs.extend(window.dayjs_plugin_customParseFormat);

//Logic added for new region dropdown in programme schedule page.
const courseScheduleRegionInput = document.getElementById("course-schedule-region");

for (const region of regions) {
    const regionOption = document.createElement("option");
    regionOption.value = region.id;
    regionOption.text = region.name;
    const courseScheduleRegionOption = regionOption.cloneNode(true);// cloneNodemethod creates a copy of a node, and returns the clone.
    courseScheduleRegionInput.add(courseScheduleRegionOption);
}

// Format for fullcalendar
const formatEvents = async (start, end) => {
    const modules = await ky("/CourseModule/GetAll", {
        searchParams: { start, end },
    }).json();

    return await Promise.all(
        modules.map(async ({ pathwayId, ...event }) => {
            let color;
            if (pathwayId) {
                const { pathwayTypeId } = await ky(
                    `/Course/GetById/${pathwayId}`
                ).json();

                ({ colour: color } = courseTypes.find(
                    (courseType) => courseType.id === pathwayTypeId
                ));
            }

            return {
                id: event.id,
                title: event.name,
                start: event.startDate,
                end: event.endDate,
                resourceId: pathwayId,
                extendedProps: {
                    trainer: event.trainerId,
                },
                color,
            };
        })
    );
};

const formatResources = async (start, end) => {
    const courses = await ky("/Course/GetAll", {
        searchParams: { start, end },
    }).json();

    const isAllRegions = courseScheduleRegionInput.value === "";
    const filteredCourses = courses.filter((course) => course.regionId === parseInt(courseScheduleRegionInput.value) || isAllRegions);

    return filteredCourses.map((resource) => ({
        id: resource.id,
        title: resource.pathwayCode,
    }));
};

let eventList = [];

const calendarEl = document.getElementById("courseCalendar");
const calendar = new FullCalendar.Calendar(calendarEl, {
    resources: async (info, successCallback) =>
        successCallback(await formatResources(info.startStr, info.endStr)),
    events: async (info, successCallback) =>
        successCallback(await formatEvents(info.startStr, info.endStr)),
    initialView: "resourceTimeline6weeks",
    schedulerLicenseKey: "",
    editable: false,
    eventOverlap: false,
    filterResourcesWithEvents: true,
    weekends: false,
    displayEventTime: false,
    height: "auto",
    resourceAreaWidth: "10%",

    views: {
        resourceTimeline1week: {
            type: "resourceTimeline",
            duration: { weeks: 1 },
            buttonText: "1 Week",
            slotDuration: { days: 1 },
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
        today: {
            text: "Today",
            click: () => calendar.today(),
        },
        createButton: {
            text: "Create",
            click: () => {
                $("#create-course-module").modal("show");
                $("#alert-course-save-update").addClass("d-none");
                $("#alert-reset-save-update").addClass("d-none");
            },
        },
        saveButton: {
            text: "Save",
            click: () => {
                //Changes done to show banner successmessage when click on edit resourcer in pathwayschedule-start
                const resource = shuffleCalendar.getResourceById(0);
                $("#reset-button-slider").addClass("d-none");
                $("#save-button-slider").removeClass("d-none");
                $("#course-update").modal("show");
                $(".modal-update-course").hide();
                $(".modal-update-course-body").hide();
                $("#update-button-slider").addClass("d-none");
                $("#alert-course-update").addClass("d-none");
                document.getElementById("alert-course-save-update").innerHTML = "Pathway" + "  " + resource.title + " " + "has been updated successfully." + "<button type=button class=close data-dismiss=alert aria-label=Close><span aria-hidden=true>&times;</span></button>";
                //Changes done to show banner successmessage when click on edit resourcer in pathwayschedule - end
            },
        },
        resetButton: {
            text: "Reset",
            click: () => {
                //Changes done to show banner successmessage when click on edit resourcer in pathwayschedule-start
                const resource = shuffleCalendar.getResourceById(0);
                $("#reset-button-slider").removeClass("d-none");
                $("#course-update").modal("show");
                $(".modal-update-course").hide();
                $(".modal-update-course-body").hide();
                $("#update-button-slider").addClass("d-none");
                $("#save-button-slider").addClass("d-none");
                $("#alert-course-update").addClass("d-none");
                document.getElementById("alert-reset-save-update").innerHTML = "Pathway" + "  " + resource.title + " " + "has been reset successfully." + "<button type=button class=close data-dismiss=alert aria-label=Close><span aria-hidden=true>&times;</span></button>";
                //Changes done to show banner successmessage when click on edit resourcer in pathwayschedule-end
            },
        },
    },

    headerToolbar: {
        left: "resourceTimeline1week,resourceTimeline6weeks,resourceTimeline12weeks",
        center: "title",
        right: "prev,next today createButton saveButton,resetButton",
    },

    dateIncrement: { weeks: 1 },
    slotDuration: { days: 1 },
    slotLabelInterval: { weeks: 1 },
    slotLabelFormat: [
        {
            month: "long",
            week: "long",
        }, // top level of text
        (date) => {
            const start = dayjs(date.date.marker).format("MMM DD");
            const end = dayjs(date.date.marker).add(4, "days").format("MMM DD");
            return `${start} - ${end}`;
        }, // lower level of text
    ],

    eventClick: async (event) => {
        $("#alert-course-save-update").addClass("d-none");
        $("#alert-reset-save-update").addClass("d-none");
        $.ajax({
            type: "Get",
            dataType: "html",
            data: { "id": event.event._def.publicId },
            url: "/CourseModule/GetByIdForEditAsyncForCourseSchedule",

            success: function (data) {
                $('#course-schedule-update > .modal-dialog').html(data);
                $("#course-schedule-update").modal("show");
                $.validator.unobtrusive.parse('#course-schedule-form');
            }
        })
    },

    eventDidMount: (info) => createTooltip(info.el, info.event.title),
    eventContent: (info) => {
        const trainer = trainers.find(
            (trainer) => trainer.id === info.event.extendedProps.trainer
        );

        const content = `${info.event.title} <div class="small d-block">${trainer
            ? `<span>${trainer.firstName} ${trainer.lastName}</span>`
            : '<i class="fa fa-times" aria-hidden="true"></i>'
            }</div>`;

        return { html: content };
    },

    resourceLabelDidMount: (info) => {
        info.el.addEventListener("click", () => showUpdateCourse(info.resource.id));
        createTooltip(info.el, "Click to edit");
    },
    resourceLabelClassNames: ["fc-hover"],
});

//Logic for pathwaycode text before pathwaycode generation.
const generatePathwayCodeText = () => {
    const pathwayCodeElement = document.getElementById("pathway-code");
    pathwayCodeElement.innerText = "Code is being generated...";
};
//Logic for programtemplate,region,timezone dropdown validation in courseschedule modal.
const dropdownValidation = (isvalid) => {
    let errormsg =
        courseTemplateInput.value === ""
            ? (document.getElementById("template-error-message").innerText =
                "Programme Template  is required.")
            : region.value === ""
                ? (document.getElementById("region-error-message").innerText =
                    "Region is required.")
                : timezoneInput.value === ""
                    ? (document.getElementById("timezone-error-message").innerText =
                        "Timezone is required.")
                    : "";
    isvalid =
        courseTemplateInput.value === ""
            ? true
            : region.value === ""
                ? true
                : timezoneInput.value === ""
                    ? true
                    : false;
    if (errormsg != "" && isvalid) {
        return isvalid;
    }
};
const generatePathwayCode = async () => {
    // <region>-<double digit year>-<course abbreviation>-<course count>

    const region = document.getElementById("region");
    const course = document.getElementById("course-template");
    const date = document.getElementById("start-date");
    const year = dayjs(date.value || dayjs()).format("YY");

    const courseTemplate = await ky(
        `/CourseTemplate/GetById/${course.value}`
    ).json();

    const courseType = await ky(
        `/CourseType/GetById/${courseTemplate.pathwayTypeId}`
    ).json();

    const courseRegion = await ky(`/Region/GetById/${region.value} `).json();

    const unprefixedPathwayCode = `${courseRegion.abbreviation.toUpperCase()}-${year}-${courseType.abbreviation
        }`;

    const courses = await ky("/Course/GetAll").json();
    const matchingCourses = courses.filter((course) => {
        return course.pathwayCode.includes(unprefixedPathwayCode);
    });

    const pathwayCode = `${unprefixedPathwayCode}-${matchingCourses.length + 1}`;

    const code = document.getElementById("course-code");
    code.innerText = pathwayCode;
};

const objectToForm = (object) => {
    const form = new FormData();
    for (const key in object) form.append(key, object[key]);

    return form;
};

const createTooltip = (element, title) => {
    const options = {
        title: title,
        container: "body",
    };

    $(element).tooltip(options);
};

const saveMovedModules = async () => {
    for (const module of eventList) {
        const url = "/CourseModule/Put";
        await ky.put(url, { body: objectToForm(module) });

        eventList.shift();
    }

    // Disable save and reset button
    $(".fc-saveButton-button").first().prop("disabled", true);
    $(".fc-resetButton-button").first().prop("disabled", true);
    $("#alert-course-save-update").removeClass("d-none");
};

calendar.render();

$(".course-code-dependancy").change(() => {
    const office = document.getElementById('office').value;
    const template = document.getElementById('templates').value;
    const selectedstartdate = document.getElementById('start-date').value;
    const programme = document.getElementById('programme').value;
    const year = dayjs(selectedstartdate || dayjs()).format("YY");
    if (office != "" && template != "" && selectedstartdate != "" && programme != "")

        $.ajax({
            url: "/Course/GetCourseCode",
            type: "Get",
            data: { "officeId": office, "templateId": template, "year": year, "programmeId": programme },
            success: function (response) {
                const pathwayCode = document.getElementById("pathway-code");
                pathwayCode.innerText = response;
                document.getElementById("hidden-pathway-code").value = pathwayCode.innerText;
            }
        })
});

$(".fc-saveButton-button").first().prop("disabled", true);
$(".fc-resetButton-button").first().prop("disabled", true);

generatePathwayCodeText();

$(".fc-saveButton-button").first().prop("disabled", true);
$(".fc-resetButton-button").first().prop("disabled", true);

const shuffleEl = document.getElementById("courseShuffle");
const shuffleCalendar = new FullCalendar.Calendar(shuffleEl, {
    initialView: "resourceTimeline",
    themeSystem: "bootstrap",
    schedulerLicenseKey: "",
    height: "auto",
    resourceAreaWidth: "150px",
    editable: true,
    droppable: true,
    eventOverlap: false,
    weekends: false,
    displayEventTime: false,
    eventColor: "#343a40",
    customButtons: {
        today: {
            text: "Today",
            click: () => shuffleCalendar.today(),
        },
    },
    resources: [
        { id: 0, title: "Template" },
        { id: 1, title: "Temporary" },
    ],
    duration: { weeks: 6 },
    dateIncrement: { weeks: 3 },
    slotDuration: { days: 1 },
    slotLabelInterval: { weeks: 1 },
    slotLabelFormat: [
        {
            month: "long",
            week: "long",
            year: "2-digit",
        }, // top level of text
        (date) => {
            const start = dayjs(date.date.marker).format("MMM DD");
            const end = dayjs(date.date.marker).add(4, "days").format("MMM DD");
            return `${start} - ${end}`;
        },
    ],
    eventDrop: () => isTempEmpty(),
    eventContent: (arg) => arg.event.title,
});

const resetShuffle = () => {
    for (const event of shuffleCalendar.getEvents()) event.remove();
    $("#modal-save-update").prop("disabled", true);
    $("#modal-reset-update").prop("disabled", true);
};

const populateShuffle = (id) => {
    const resource = calendar.getResourceById(id);
    const events = resource.getEvents();

    const shuffleResource = shuffleCalendar.getResourceById(0);
    shuffleResource.setProp("title", resource.title);

    const initialDate = events[0].start;
    shuffleCalendar.setOption("initialDate", initialDate);

    for (const event of events) {
        const object = event.toPlainObject({ collapseColor: true });
        object.resourceId = 0;

        shuffleCalendar.addEvent(object, true);
    }
};

let lastOpenedUpdateCourse = null;
function showUpdateCourse(id) {
    //Changes done to show banner successmessage when click on edit resourcer in pathwayschedule-start
    const resource = calendar.getResourceById(id);
    $("#alert-course-save-update").addClass("d-none");
    $("#alert-reset-save-update").addClass("d-none");
    $(".modal-update-course").show();
    $(".modal-update-course-body").show();
    $("#reset-button-slider").addClass("d-none");
    $(".update-course-success").addClass("d-none");
    $("#course-update").modal("show");
    $("#update-button-slider").addClass("d-none");
    $("#save-button-slider").addClass("d-none");
    $("#alert-course-update").addClass("d-none");
    document.querySelector(".update-course-title").innerHTML = "Update " + resource.title + " " + "Pathway";

    //Changes done to show banner successmessage when click on edit resourcer in pathwayschedule - end

    shuffleCalendar.render();

    // hide main fullcalendar
    // the main calendar's disable overlap is preventing dropping on the shuffle calendar as it sees it above the main calendar
    calendarEl.classList.add("d-none");

    // don't do anything if same resource reopened - feature disabled
    // if (lastOpenedUpdateCourse === id) return;
    lastOpenedUpdateCourse = id;

    resetShuffle(id);
    populateShuffle(id);
}

function isTempEmpty() {
    const resource = shuffleCalendar.getResourceById(1);
    const events = resource.getEvents();

    if (!events.length) {
        $("#modal-save-update").prop("disabled", false)
        $("#modal-reset-update").prop("disabled", false)
        return;
    }

    $("#modal-save-update").prop("disabled", true)
    $("#modal-reset-update").prop("disabled", true)
}

function saveShuffled() {
    const resource = shuffleCalendar.getResourceById(0);
    const events = resource.getEvents();

    for (const event of events) {
        // get regular calendar event of the shuffled version of event
        const calEvent = calendar.getEventById(event.id);

        const isStartSame = dayjs(event.start).isSame(calEvent.start);
        const isEndSame = dayjs(event.end).isSame(calEvent.end);

        if (isStartSame && isEndSame) continue;

        calEvent.setStart(event.start);
        calEvent.setEnd(event.end);

        const data = {
            id: event.id,
            startDate: dayjs(event.start).toJSON(),
            endDate: dayjs(event.end).toJSON(),
        };

        eventList.push(data);

        $(".fc-saveButton-button").first().prop("disabled", false);
        $(".fc-resetButton-button").first().prop("disabled", false);
    }

    $("#course-update").modal("hide");//Changes done to show banner successmessage when click on edit resourcer in pathwayschedule - start
    document.getElementById("alert-course-update").innerHTML = "Pathway" + "  " + resource.title + " has been updated on the resource shuffle. Please click the " + "<strong>Save</strong>" + " " + "button to update the pathway schedule or click the" + "  " + "<strong>Reset</strong>" + " " + " button to reset the resource shuffle changes." + " " + " <button type=button class=close data-dismiss=alert aria-label=Close><span aria-hidden=true>&times;</span></button>";
    $("#alert-course-update").removeClass("d-none");//Changes done to show banner successmessage when click on edit resourcer in pathwayschedule - start
}
// Changes done to show banner successmessage when click on edit resourcer in pathwayschedule-start
const updateCourse = document.getElementById("modal-save-update");
updateCourse.addEventListener("click", () => {
    $("#update-button-slider").slideDown();
    $("#update-button-slider").removeClass("d-none");
    $(".modal-update-course").slideUp();
});
const confirmUpdateCourse = document.getElementById("confirm-update-course");
confirmUpdateCourse.addEventListener("click", () => {
    saveShuffled();
});

const confirmSaveUpdateCourse = document.getElementById("confirm-save-update-course");
confirmSaveUpdateCourse.addEventListener("click", () => {
    $("#course-update").modal("hide");
    saveMovedModules();
});
const cancelUpdateCourse = document.getElementById("cancel-update-course");
cancelUpdateCourse.addEventListener("click", () => {
    $(".modal-update-course").slideDown();
    $("#update-button-slider").slideUp();
    $("#update-button-slider").addClass("d-none");
    $("#modal-save-update").disabled = true;
});
const confirmResetCourse = document.getElementById("confirm-reset-course");
confirmResetCourse.addEventListener("click", () => {
    eventList = [];
    $(".fc-saveButton-button").first().prop("disabled", true);
    $(".fc-resetButton-button").first().prop("disabled", true);
    $("#course-update").modal("hide");
    $("#alert-reset-save-update").removeClass("d-none");
    calendar.refetchEvents();
});

const updateCourseReset = document.getElementById("modal-reset-update");
updateCourseReset.addEventListener("click", () => {
    $("#update-reset-button-slider").slideDown();
    $("#update-reset-button-slider").removeClass("d-none");
    $(".modal-update-course").slideUp();
});
const confirmUpdateResetCourse = document.getElementById("confirm-update-reset-course");
confirmUpdateResetCourse.addEventListener("click", () => {
    resetShuffle();
    populateShuffle(lastOpenedUpdateCourse);
    $(".modal-update-course").slideDown();
    $("#update-reset-button-slider").slideUp();
});
const cancelUpdateResetCourse = document.getElementById("cancel-update-reset-course");
cancelUpdateResetCourse.addEventListener("click", () => {
    $("#update-reset-button-slider").slideUp();
    $("#update-reset-button-slider").removeClass("d-none");
    $(".modal-update-course").slideDown();
});
//Changes done to show banner successmessage when click on edit resourcer in pathwayschedule - end

$("#course-update").on("hidden.bs.modal", () => {
    calendarEl.classList.remove("d-none");
    calendar.render();
});

$("#timezone").on("change", function () {
    var timezone = document.getElementById("timezone");
    var timezonetext = timezone.options[timezone.selectedIndex].text;
    $("#hidden-timezone").val(timezonetext);
});
$("#course-schedule-region").change(function () {
    calendar.refetchResources();
});