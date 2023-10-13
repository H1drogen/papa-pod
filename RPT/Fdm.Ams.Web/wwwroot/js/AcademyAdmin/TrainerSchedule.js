import ky from "https://cdn.jsdelivr.net/npm/ky@0.30.0/distribution/index.min.js";
import { objectToForm, toggleSaveResetButtons } from "./shared/helpers.js";

// Get all requests at the same time async
const [trainers, courseTypes, offices, venues, courseModuleTemplates, holidays] =
  await Promise.all([
    ky("/Trainer/GetAll").json(),
    ky("/CourseType/GetAll").json(),
    ky("/Office/GetAll").json(),
    ky("/Venue/GetAll").json(),
    ky("/CourseModuleTemplate/GetAll").json(),
    ky("/Holiday/GetAll").json(),
  ]);

// setup dayjs timezone plugin
dayjs.extend(window.dayjs_plugin_utc);
dayjs.extend(window.dayjs_plugin_timezone);
dayjs.extend(window.dayjs_plugin_customParseFormat);
dayjs.extend(window.dayjs_plugin_isBetween);

const getCourseTypeById = (id) => courseTypes.find((type) => type.id === id);
const getOfficeById = (id) => offices.find((type) => type.id === id);

// Format for fullcalendar
const formatEvents = async (start, end) => {
  const modules = await ky("/CourseModule/GetAll", {
    searchParams: { start, end },
  }).json();

  return await Promise.all(
    modules.map(async ({ pathwayId, ...event }) => {
      let color;
      let pathwayCode;

      if (pathwayId) {
        let pathwayTypeId;
        ({ pathwayTypeId, pathwayCode } = await ky(
          `/Course/GetById/${pathwayId}`
        ).json());

        ({ colour: color } = getCourseTypeById(pathwayTypeId));
      }

      return {
        id: event.id,
        title: event.name,
        start: event.startDate,
        end: event.endDate,
        resourceId: event.trainerId,
        color,
        extendedProps: {
          pathwayCode,
        },

        editable: true,
        startEditable: !pathwayId,
        durationEditable: !pathwayId,
        resourceEditable: true,
      };
    })
  );
};

const formatResources = async (start, end) => {
  const formatted = trainers.map((resource) => {
    const office = getOfficeById(resource.officeId);

    return {
      id: resource.id,
      title: `${resource.firstName} ${resource.lastName}`,
      group: office.name || "",
    };
  });

  formatted.push({ id: null, title: "Unassigned", group: null });

  return formatted;
};

const formatHolidays = (start, end) => {
  const getTrainerIdsByCountryId = (countryId) => {
    const officeIds = offices
      .filter((office) => countryId === office.countryId)
      .map((office) => office.id);

    return trainers
      .filter((trainer) => officeIds.includes(trainer.officeId))
      .map((trainer) => trainer.id);
  };
  
  return holidays.map((holiday) => ({
    id: holiday.id,
    title: holiday.name,
    start: holiday.startDate,
    end: holiday.endDate,
    display: "background",
    groupId: `holiday-${holiday.id}`,
    resourceIds: getTrainerIdsByCountryId(holiday.countryId),
  })).filter((holiday) => holiday.resourceIds.length > 0);
};

let eventList = [];

const calendarEl = document.getElementById("trainerCalendar");
const calendar = new FullCalendar.Calendar(calendarEl, {
  resources: async (info, successCallback) =>
    successCallback(await formatResources(info.startStr, info.endStr)),
  eventSources: [
    async (info) => await formatEvents(info.startStr, info.endStr),
    {
      id: "holidays",
      events: async (info) => await formatHolidays(info.startStr, info.endStr),
      backgroundColor: "#39ff14",
        textColor: "#000000",
        overlap: false,
        className: "overflow-hidden",
        resourceEditable: false,
       
    },
  ],
  initialView: "resourceTimeline6weeks",
  schedulerLicenseKey: "",

  eventOverlap: true,
  weekends: false,
  displayEventTime: false,
  height: "auto",
  resourceGroupField: "group",

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
    saveButton: {
      text: "Save",
      click: () => {
        if (confirm("Do you want to save?")) saveMovedModules();
      },
    },
    resetButton: {
      text: "Reset",
      click: () => {
        if (!confirm("Do you want to reset your unsaved changes?")) return;

        eventList = [];
        toggleSaveResetButtons(false);
        resetActivity();
        calendar.refetchEvents();
        alert("All event unsaved changes have been reset");
      },
    },
    toggleExtra: {
      text: "Activities",
      click: () => toggleSidebars(),
    },
  },

  headerToolbar: {
    left: "toggleExtra resourceTimeline1week,resourceTimeline6weeks,resourceTimeline12weeks",
    center: "title",
    right: "prev,next today saveButton,resetButton",
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

  eventClick: async (info) => {
    if (info.event.display === "background") return;

    const isTemplate = !info.event.extendedProps.pathwayCode;
    if (!isTemplate) return regularEditModal(info.event.id);

    const isInEventList = eventList.find((event) => event.id === info.event.id);

    if (!isInEventList) {
      const module = await ky(`/CourseModule/GetById/${info.event.id}`).json();

      const fullCalendarEvent = calendar.getEventById(info.event.id);
      fullCalendarEvent.setExtendedProp("description", module.description);
      fullCalendarEvent.setExtendedProp(
        "preparationNotes",
        module.preparationNotes
      );

      pushActivityToEventList(fullCalendarEvent);
    }

    activityEditModal(info.event.id);
  },

  eventDrop: (info) => {
    const { event } = info;
    const events = calendar.getEvents();

    // check if there is another event in the calendar which conflicts with the start or end date with the same resource
    const isOverlapping = events.some((otherEvent) => {
      if (otherEvent.id === info.event.id) return false; // don't conflict with self
      if (!event.extendedProps.pathwayCode) return false; // activities can overlap without warning
      if (event._def.resourceIds[0] === "null") return false; // unassigned can overlap without warning

      const start = dayjs(otherEvent.start);
      const end = dayjs(otherEvent.end);

      const isSameResource =
        otherEvent._def.resourceIds[0] === event._def.resourceIds[0];
      const isOverlappingStart = start.isBetween(event.start, event.end);
      const isOverlappingEnd = end.isBetween(event.start, event.end);

      return isSameResource && (isOverlappingStart || isOverlappingEnd);
    });

    if (isOverlapping) {
      const isConfirmed = confirm(
        "This trainer is already assigned during this time. Confirm assigning another course at the same time?"
      );
      if (!isConfirmed) return info.revert();
    }

    const data = {
      id: info.event.id,
      startDate: dayjs(info.event.start).toJSON(),
      endDate: dayjs(info.event.end).toJSON(),
      trainerId:
        info.newResource != null
          ? parseInt(info.newResource.id)
          : parseInt(info.event._def.resourceIds[0]),
      venueId: info.event.extendedProps.venueId,
      name: info.event.title,
      description: info.event.extendedProps.description,
      preparationNotes: info.event.extendedProps.preparationNotes,
      template: !!info.event.extendedProps.template,
    };

    // remove event from eventList if it exist
    const index = eventList.findIndex((event) => event.id === data.id);
    if (index !== -1) eventList.splice(index, 1);

    eventList.push(data);

    toggleSaveResetButtons(true);
  },
  eventReceive: (info) => {
    pushActivityToEventList(info.event);
    toggleSaveResetButtons(true);
  },

  eventDidMount: (info) => createTooltip(info.el, info.event.title),
  eventWillUnmount: (info) => destroyTooltip(info.el),
  eventContent: (info) => {
    const prefix = info.event.extendedProps.pathwayCode
      ? `<span class="ml-1">${info.event.extendedProps.pathwayCode}</span> - `
      : "";

    return { html: `${prefix}${info.event.title}` };
  },

  eventAllow: (dropInfo, draggedEvent) => {
    const isActivity = draggedEvent.startEditable;
    const isUnassigned = dropInfo.resource.id === "null";

    if (isUnassigned && isActivity) return false;

    return true;
  },
});
const resetActivity = () => {
  for (const event of calendar.getEvents()) event.remove();
};

const createTooltip = (element, title) => {
  const options = {
    title: title,
    container: "body",
  };

  $(element).tooltip(options);
};

const destroyTooltip = (element) => $(element).tooltip("dispose");



const unassignModule = async () => {
  try {
    const data = {
      id: $("#modal-save-trainer").attr("data-id"),
      venueId: $("#current-venue-id").attr("data-id"),
    };
    await ky.post(`/CourseModule/PutUnAssign`, {
      body: objectToForm(data),
    });

    $("#trainer-schedule-update").modal("hide");
    alert("Course unassigned from current trainer");
    calendar.refetchEvents();
  } catch (e) {
    alert(e);
  }
};

const saveMovedModules = async () => {
  for (const module of eventList) {
    if (module.template) {
      await createCourse(module);
      continue;
    }
    await saveCourse(module);
  }

  eventList = [];

  toggleSaveResetButtons(false);

  calendar.refetchEvents();
  alert("All event changes have been saved");
};

const saveCourse = async (listItem) => {
  let body;

  // remove fields if not an activity before updating
  // obtuse logic to selectively destructure
  // - can't directly assign to module because vars for deleting values don't exist
  if (!!listItem.description) {
    const { template, ...data } = listItem;
    body = data;
  } else {
    const { name, description, preparationNotes, template, ...data } = listItem;
    body = data;
  }

    const url = `/CourseModule/Put${body.trainerId ? "" : "UnAssign"}`;
  await ky.put(url, { body: objectToForm(body) });
};

const createCourse = async (listItem) => {
  const { id, template, ...module } = listItem;
  const event = calendar.getEventById(id);

  const body = objectToForm(module);
  await ky.post("/CourseModule/Post", { body });

  event.remove();
};

const pushActivityToEventList = (event) => {
  const data = {
    id: event.id,
    startDate: dayjs(event.start).toJSON(),
    endDate: dayjs(event.end).toJSON(),
    trainerId: parseInt(event._def.resourceIds[0]),
    venueId: null,
    name: event.title,
    description: event.extendedProps.description,
    preparationNotes: event.extendedProps.preparationNotes,
    template: !!event.extendedProps.template,
    edited: false,
  };

  eventList.push(data);
};

const regularEditModal = async (id) => {
  // moved here in from duplicate eventClick (#237)
  $.ajax({
    type: "Get",
    dataType: "html",
    data: { id },
    url: "/CourseModule/GetByIdForEdit",

    success: function (data) {
      $("#trainer-schedule-update > .modal-dialog").html(data);
      $("#trainer-schedule-update").modal("show");
      $.validator.unobtrusive.parse("#trainer-schedule-form");
    },
  });
};

const activityEditModal = async (activityId) => {
  const module = eventList.find((module) => module.id === activityId);

  const id = document.getElementById("activity-id");
  const venue = document.getElementById("activity-venue");
  const name = document.getElementById("activity-name");
  const description = document.getElementById("activity-description");
  const preparationNotes = document.getElementById("activity-prep");

  id.value = module.id;
  venue.value = module.venueId;
  name.value = module.name;
  description.value = module.description;
  preparationNotes.value = module.preparationNotes;

  $("#trainer-schedule-update-activity").modal("show");

  const activityStartDate = document.getElementById("activity-start");
  const activityEndDate = document.getElementById("activity-end");
  const formatDate = (date) => dayjs(date).format("DD/MM/YYYY");
  activityStartDate.innerHTML = formatDate(module.startDate);
  activityEndDate.innerHTML = formatDate(module.endDate);
};

calendar.render();

$("#modal-save-trainer").click(() => {
  const confirmed = confirm("Are you sure you want to save?");
  if (confirmed) saveModule();
});

$("#unassign").click(() => {
  const confirmed = confirm("Are you sure you want to unassign this module?");
  if (confirmed) unassignModule();
});

$("#delete-btn").click(() => $("#confirm-delete").slideDown());
$("#confirm-delete-btn").click(() => deleteModule());

$("#trainer-schedule-update").on("hidden.bs.modal", () => {
  $("#confirm-delete").hide();

  const saveButton = document.getElementById("modal-save-trainer");
  saveButton.innerText = "Save changes";
  saveButton.disabled = false;
});
$("#confirm-delete").hide();

$(".form-select").select2({ width: "50%" });

toggleSaveResetButtons(false);


// Toggle extra sidebar
const extraSidebar = document.getElementById("extra-modules-sidebar");
const sidebar = document.querySelector(".aside__toggle-bar");

const isAsideOpen = () =>
  document.querySelector(".admin-aside").offsetWidth === 280;

const toggleSidebars = () => {
  const isClosed = extraSidebar.offsetWidth === 0;

  if (isClosed) return openExtraSidebar();
  closeExtraSidebar();
};

// close main sidebar and open extra sidebar
const openExtraSidebar = () => {
  if (isAsideOpen()) sidebar.click();
  extraSidebar.style.width = "200px";
};

// close main sidebar and open extra sidebar
const closeExtraSidebar = () => {
  if (!isAsideOpen()) sidebar.click();
  extraSidebar.style.width = "0px";
};

// populate extra modules
const draggableContainer = document.getElementById("extra-modules-sidebar");
new FullCalendar.Draggable(draggableContainer, {
  itemSelector: ".module",
  eventData: (eventEl) => {
    const template = courseModuleTemplates.find(
      (template) => template.id === parseInt(eventEl.dataset.id)
    );

    return {
      id: dayjs().unix(), // template id !== module id, prevents clashes
      title: eventEl.innerText,
      duration: { days: 5 },
      extendedProps: {
        venueId: null,
        preparationNotes: template.preparationNotes,
        description: template.description,
        template: true,
      },

      editable: true,
      startEditable: true,
      durationEditable: true,
      resourceEditable: true,
    };
  },
});

const extraTemplate = document.getElementById("extra-module-template");
const cloneAndAppend = (id, name, description) => {
  const clone = extraTemplate.cloneNode(true);

  clone.dataset.id = id;
  clone.innerText = name;

  createTooltip(clone, description);

  clone.classList.remove("d-none");
  draggableContainer.appendChild(clone);
};

const extraModules = courseModuleTemplates.filter(
  (module) => module.isExtra && !module.pathwayTemplateId
);

for (const module of extraModules) {
  cloneAndAppend(module.id, module.name, module.description);
}

$("#activity-delete").click(() => $("#activity-delete-accordion").slideDown());
$("#trainer-schedule-update-activity").on("hidden.bs.modal", () => {
  $("#activity-delete-accordion").hide();
});
$("#activity-delete-accordion").hide();

const deleteActivity = document.getElementById("activity-delete-confirm");
deleteActivity.addEventListener("click", async () => {
  const id = document.getElementById("activity-id").value;
  const module = eventList.find((module) => module.id === id);

  // if existing
  if (!module.template) await ky.delete(`/CourseModule/Delete/${id}`);

  eventList.splice(eventList.indexOf(module), 1);
  calendar.getEventById(id).remove();

  $("#trainer-schedule-update-activity").modal("hide");

  if (!eventList.length) toggleSaveResetButtons(false);
});

const activityForm = document.getElementById("activity-form");
activityForm.addEventListener("submit", (event) => {
  event.preventDefault();

  const id = document.getElementById("activity-id").value;
  const existing = eventList.find((module) => module.id === id);

  const venue = document.getElementById("activity-venue").value;
  const name = document.getElementById("activity-name").value;
  const description = document.getElementById("activity-description").value;
  const preparationNotes = document.getElementById("activity-prep").value;

  const calendarEvent = calendar.getEventById(id);

  calendar.getEventById(id).setProp("title", name);
  calendarEvent.setExtendedProp("description", description);
  calendarEvent.setExtendedProp("preparationNotes", preparationNotes);
  calendarEvent.setExtendedProp("venueId", venue);

  const data = {
    ...existing,
    id: id,
    name: name,
    description: description,
    preparationNotes: preparationNotes,
    venueId: venue,
  };

  // remove from eventList and push new
  const moduleIndex = eventList.findIndex((module) => module.id === id);
  eventList.splice(moduleIndex, 1);
  eventList.push(data);

  toggleSaveResetButtons(true);

  $("#trainer-schedule-update-activity").modal("hide");
});

$("#trainer-schedule-update-activity").on("hide.bs.modal", () => {
  const id = document.getElementById("activity-id").value;
  const moduleIndex = eventList.findIndex((module) => module.id === id);

  if (eventList[moduleIndex]?.template) return;

  // check that it has edited as undefined, meaning it is edited
  const isEdited = eventList[moduleIndex]?.edited;
  if (isEdited || isEdited === undefined) return;

  // remove from eventList
  eventList.splice(moduleIndex, 1);
});

// handle marking as edited
activityForm.addEventListener("change", () => {
  const id = document.getElementById("activity-id").value;
  const moduleIndex = eventList.findIndex((module) => module.id === id);

  eventList[moduleIndex].edited = true;
});
