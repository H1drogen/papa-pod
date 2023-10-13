import ky from "https://cdn.jsdelivr.net/npm/ky@0.30.0/distribution/index.min.js";
import { objectToForm } from "./shared/helpers.js";

dayjs.extend(window.dayjs_plugin_weekOfYear);

//Initializing
const calendarErrorMessage = document.getElementById("calendar-error-message");
const calendarErrorMessageTemp = document.getElementById("calendar-error-message-temp-row");
const calendarEventMessage = document.getElementById("calendar-event-duration-error-message");

// Modules drag and drop
const draggableContainer = document.getElementById("created-modules");
new FullCalendar.Draggable(draggableContainer, {
    itemSelector: ".module",
    eventData: (eventEl) => ({
        id: eventEl.dataset.id,
        title: eventEl.innerText,
        duration: { days: 5 },
    }),
});

// Populate module templates list
const cloneAndAppend = (id, name) => {
    const template = draggableContainer.children[0];
    const clone = template.cloneNode(true);

    clone.children[0].innerText = name;
    clone.dataset.id = id;

    clone.classList.remove("d-none");
    draggableContainer.appendChild(clone);
};

// Populate calendar
const allModuleTemplates = await ky("/CourseModuleTemplate/GetAll").json();

const moduleTemplates = allModuleTemplates.filter(
    (template) => template.pathwayTemplateId === null
);

moduleTemplates.forEach((module) => cloneAndAppend(module.id, module.name));

// Unassign module logic
const unassignModuleEl = document.getElementById("unassign-module-template");
unassignModuleEl.addEventListener("click", () => {
    const eventStart = unassignModuleEl.dataset.start;
    const fullCalendarEvent = calendar.getEvents();
    const filteredEvent = fullCalendarEvent.find((e) => e.start == eventStart);
    filteredEvent.remove();

    $("#unassign-module").modal("hide");
});

// Calendar Setup
const calendarEl = document.getElementById("course-template-calendar");
const calendar = new FullCalendar.Calendar(calendarEl, {
    initialView: "resourceTimeline",
    themeSystem: "bootstrap",
    schedulerLicenseKey: "",
    height: "auto",
    resourceAreaWidth: "120px",
    editable: true,
    droppable: true,
    eventOverlap: false,
    weekends: false,
    displayEventTime: false,
    eventColor: "#343a40",
    headerToolbar: false,

    initialDate: "2022-01-01",
    resources: [
        { id: 0, title: "Template" },
        { id: 1, title: "temp" },
    ],
    validRange: {
        start: "2022-01-01",
        end: "2022-05-09",
    },
    duration: { weeks: 52 },
    slotDuration: { days: 1 },
    slotLabelInterval: { weeks: 1 },
    slotLabelFormat: [
        (date) => dayjs(date.date.marker).subtract(1, "week").week(),
    ],

    eventContent: (arg) => arg.event.title,
    eventClick: (arg) => {
        calendarErrorMessage.hidden = true;
        calendarErrorMessageTemp.hidden = true;
        calendarEventMessage.hidden = true;

        const eventStartDate = arg.event.start;

        unassignModuleEl.dataset.start = eventStartDate;
        $("#unassign-module").modal("show");
    },

    eventDidMount: (info) => {
        calendarErrorMessage.hidden = true;
        calendarErrorMessageTemp.hidden = true;
        calendarEventMessage.hidden = true;

        const options = {
            title: info.event.title,
            container: "body",
        };
        const element = info.el;
        $(element).tooltip(options);
    },

    eventWillUnmount: (info) => {
        $(info.el).tooltip("dispose");
    },

    eventResize: (info) => {
        const duration = dayjs(info.event.end).diff(
            dayjs(info.event.start),
            "days"
        );

        calendarEventMessage.hidden = true;

        if (duration <= 5) return;
        calendarEventMessage.hidden = false;
        info.revert();
    },
});

// Submitting Form
const courseTemplateForm = document.getElementById("create-form");
courseTemplateForm.addEventListener("submit", async (event) => {
    event.preventDefault();

    const hasTemplateEvents = !!calendar.getResourceById(0).getEvents().length;
    const hasTempEvents = !!calendar.getResourceById(1).getEvents().length;

    if (hasTempEvents) {
        calendarErrorMessageTemp.hidden = false;
        return;
    }

    if (!hasTemplateEvents) {
        calendarErrorMessage.hidden = false;
        return;
    }

    let name = document.querySelector('[name="Name"]').value.trim();
    let description = document.querySelector('[name="Description"]').value.trim();
    let regionId = document.querySelector('[name="RegionId"]').value;
    let pathwayTypeId = document.querySelector('[name="PathwayTypeId"]').value;

    const events = calendar.getResourceById(0).getEvents();
    const inputCourseModules = document.getElementById("create-course-module-templates");

    if (name.length == 0 || pathwayTypeId == "" || regionId == "" || description.length == 0) {
        return
    };

    inputCourseModules.value = JSON.stringify(events);

    courseTemplateForm.submit();
});

calendar.render();