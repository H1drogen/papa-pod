import ky from "https://cdn.jsdelivr.net/npm/ky@0.30.0/distribution/index.min.js";
import { objectToForm } from "./shared/helpers.js";

dayjs.extend(window.dayjs_plugin_weekOfYear);

//Initializing
const templateId = document.getElementById("update-template-id");
const calendarErrorMessage = document.getElementById("calendar-error-message");
const calendarErrorMessageTemp = document.getElementById(
    "calendar-error-message-temp-row"
);
const calendarEventMessage = document.getElementById(
    "calendar-event-duration-error-message"
);

// Unassign module logic
const unassignModuleEl = document.getElementById("update-unassign-module");
unassignModuleEl.addEventListener("click", () => {
    const eventStart = unassignModuleEl.dataset.start;
    const fullCalendarEvent = updateCalendar.getEvents();
    const filteredEvent = fullCalendarEvent.find((e) => e.start == eventStart);
    filteredEvent.remove();

    $("#update-unassign-modal").modal("hide");
});

const updateCalendarEl = document.getElementById("updateTemplateCalendar");
const updateCalendar = new FullCalendar.Calendar(updateCalendarEl, {
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
    headerToolbar: false,

    resources: [
        { id: 0, title: "Template" },
        { id: 1, title: "Temporary" },
    ],

    events: [],

    initialDate: "2022-01-01",
    validRange: {
        start: "2022-01-01",
        end: "2022-12-31",
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

        eventDurationEditable: false;
        const eventStartDate = arg.event.start;
        unassignModuleEl.dataset.start = eventStartDate;
        $("#update-unassign-modal").modal("show");
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

// Modules drag and drop
const draggableContainer = document.getElementById("update-new-modules");
new FullCalendar.Draggable(draggableContainer, {
    itemSelector: ".module",
    eventData: (eventEl) => ({
        id: parseInt(eventEl.dataset.id),
        title: eventEl.innerText,
        duration: { days: 5 },
        extendedProps: {
            originalSequence: eventEl.getAttribute("data-original-sequence"),
            originalDuration: eventEl.getAttribute("data-original-duration"),
        },
    }),
});

const cloneAndAppend = (id, name, originalSequence, originalDuration) => {
    const template = draggableContainer.children[0];
    const clone = template.cloneNode(true);

    clone.children[0].innerText = name;
    clone.setAttribute("data-id", id);
    clone.setAttribute("data-original-sequence", originalSequence);
    clone.setAttribute("data-original-duration", originalDuration);

    clone.classList.remove("d-none");
    draggableContainer.appendChild(clone);
};

// Data
const original = [];

const allModuleTemplates = await ky("/CourseModuleTemplate/GetAll").json();

const getModuleTemplatesByTemplate = async (id) =>
    allModuleTemplates.filter((template) => template.pathwayTemplateId === id);

// Get all course module templates
const newModuleTemplates = allModuleTemplates.filter(
    (template) => template.pathwayTemplateId === null
);

// populate calendar
const showUpdateTemplate = async () => {
    // get id from data of parent element
    const id = parseInt(templateId.value);

    const template = await ky(`/CourseTemplate/GetById/${id}`).json();
    const moduleTemplates = await getModuleTemplatesByTemplate(template.id);
    original.splice(0, original.length, ...moduleTemplates); // replacing original array with new list while preserving same array (constant)

    // populate modules
    for (const event of moduleTemplates) {
        const start = dayjs("2022-01-03").add(event.sequence, "week");

        const object = {
            id: parseInt(event.id),
            title: event.name,
            start: start.toDate(),
            end: start.add(event.duration, "day").toDate(),
            resourceId: 0,
            extendedProps: {
                originalSequence: event.sequence,
                originalDuration: event.duration,
            },
        };

        updateCalendar.addEvent(object, true);
    }

    for (const module of newModuleTemplates)
        cloneAndAppend(module.id, module.name);
};

// Saving
const saveUpdated = async () => {
    const calendarEvents = updateCalendar.getResourceById(0).getEvents();

    // get Element By Id
    const courseTemplateInput = document.getElementById(
        "update-course-module-templates"
    );

    // Moving to C#
    courseTemplateInput.value = JSON.stringify(calendarEvents);
};

// Submitting Form
const updateCourseTemplateForm = document.getElementById(
    "update-course-template-form"
);
updateCourseTemplateForm.addEventListener("submit", async (event) => {
    event.preventDefault();
    saveUpdated();

    const hasTemplateEvents = !!updateCalendar.getResourceById(0).getEvents()
        .length;
    const hasTempEvents = !!updateCalendar.getResourceById(1).getEvents().length;

    if (hasTempEvents) return (calendarErrorMessageTemp.hidden = false);

    if (!hasTemplateEvents) {
        calendarErrorMessage.hidden = false;
        return;
    }

    let name = document.querySelector('[name="Name"]').value;
    let description = document.querySelector('[name="Description"]').value;

    if (name.trim().length === 0 || description.trim().length === 0) return;

    updateCourseTemplateForm.submit();
});

showUpdateTemplate();
updateCalendar.render();