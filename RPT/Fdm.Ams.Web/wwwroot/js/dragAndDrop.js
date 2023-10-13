
//global variables .......................................
let dragged = null; //the node thats being dragged
let origin = null; //the last valid position to drop the 

//original start position and style in the case the drag fails
let startSlot = null;
let startWidth = "";
let startClass = ""

//new start date, end date and id for updating the database
let newStartDateStr = "";
let newEndDateStr = ""
let newTargetId = ""

//a set of slot ids where the draggable cannot be dropped
let nonDroppableIds = new Set();
let draggedCoverIds = new Set(); //a set of slot ids that will contain a non-droppable cover

//controls the changing between different classes representing state
controlClassList = (className) => {
    switch (className) {
        case ("is-being-dragged"):
            dragged.classList.add("is-being-dragged");
            dragged.classList.remove("is-hovering");
            dragged.classList.remove("is-hovering-not-calender");
            dragged.classList.remove("in-calender");
            return;
        case ("is-hovering"):
            dragged.classList.add("is-hovering");
            dragged.classList.remove("is-hovering-not-calender");
            return;
        case ("is-hovering-not-calender"):
            dragged.classList.remove("is-hovering");
            dragged.classList.remove("in-calender");
            dragged.classList.add("is-hovering-not-calender");
            return;
        case ("in-calender"):
            dragged.classList.add("in-calender");
            dragged.classList.remove("is-hovering");
            dragged.classList.remove("is-hovering-not-calender");
            dragged.classList.remove("is-being-dragged");
            return;
        case ("EXCEPT-is-hovering"):
            dragged.classList.remove("is-hovering-not-calender");
            dragged.classList.remove("is-being-dragged");
            return;
        case ("ALL"):
            dragged.classList.remove("in-calender");
            dragged.classList.remove("is-hovering");
            dragged.classList.remove("is-hovering-not-calender");
            dragged.classList.remove("is-being-dragged");
            return;
    } 
}

//takes a start and end date, then adds those dates and the ones in between to the nonDroppableIds set
populateDateStringSet = (id, startDateStr, endDateStr) => {
    currentDateObj = new Date(startDateStr);
    endDateObj = new Date(endDateStr);
    while (currentDateObj <= endDateObj) {
        nonDroppableIds.add(id + "_" + dateToString(currentDateObj));
        currentDateObj.setTime(currentDateObj.getTime() + (1000 * 60 * 60 * 24));
    }
}

//populate set representing ids covered by a draggable element.
populateDraggedCoverIds = (uniqueid) => {
    id = uniqueid.split("_")[0]
    currentDateObj = new Date(uniqueid.split("_")[1]);
    draggedCoverIds.clear()
    for (let i = 0; i < dragged.id.split("_")[1]; i++) {
        draggedCoverIds.add(id + "_" + dateToString(currentDateObj));
        if (currentDateObj.getDay() == 5) {
            currentDateObj.setTime(currentDateObj.getTime() + (3 * 1000 * 60 * 60 * 24));
        } else {
            currentDateObj.setTime(currentDateObj.getTime() + (1000 * 60 * 60 * 24));
        }
    }
}

//if overlapping with a nondroppable position, the dragged element will be moved a minimal distance to find a valid position and placed
// if not, then the hovering position is not updated
findValidPosition = (currentHoveredId) => {
    populateDraggedCoverIds(currentHoveredId);

    let doesIdContainDraggableId = () => {
        let bool = false;
        draggedCoverIds.forEach(id => {
            if (nonDroppableIds.has(id)) {
                bool = true;
            }
        })
        return bool;
    };
    DateObj = new Date(currentHoveredId.split("_")[1]);

    var limitCount = dragged.id.split("_")[1];

    while (doesIdContainDraggableId() == true) {
        if (DateObj.getDay() == 1) {
            DateObj.setTime(DateObj.getTime() - (3 * 1000 * 60 * 60 * 24));
        } else {
            DateObj.setTime(DateObj.getTime() - (1000 * 60 * 60 * 24));
        }
        populateDraggedCoverIds(currentHoveredId.split("_")[0] + "_" + dateToString(DateObj));
        limitCount--;
        
        if (limitCount < 0 || DateObj < calenderStartDate) {
            return;
        }
    }
    origin = document.getElementById([...draggedCoverIds][0]);
}

//obtain the ids of all locations where the currently dragged element cannot be dropped and add the covers
populateUndroppableSections = (id) => {
    let currentElement = calenderContents.find((element) => {
        return element.id == id;
    });

    calenderContents.forEach(element => {
        var sameIdCheck;
        if (id != element.id) {
            sameIdCheck = Number((currentElement.venueId === element.venueId && currentElement.venueId != null) + (currentElement.pathwayId === element.pathwayId && currentElement.pathwayId != null) + (currentElement.trainerId === element.trainerId && currentElement.trainerId != null));
            if (currentElement[calendarTableIdProp] !== element[calendarTableIdProp] || (currentElement[calendarTableIdProp] === element[calendarTableIdProp] && sameIdCheck > 1) || (currentElement[calendarTableIdProp] == null && sameIdCheck > 0)) {
                if (sameIdCheck > 0) {
                    for (let id = 1; id <= alldata.length; id++) {
                        populateDateStringSet(id, element.startDate.substring(0, 10), element.endDate.substring(0, 10));
                    }
                }
            } 
        }
    });

    nonDroppableIds.forEach(calendarSlotId => {
        var fillSlot = document.getElementById(calendarSlotId);
        if (fillSlot != null) {
            if (fillSlot.innerHTML.includes("<div class='undroppableSlot drag-item'></div>") == false) {
                $("#" + calendarSlotId).append("<div class='undroppableSlot drag-item'></div>");
            }
        }

    });

    calenderContents.forEach(element => {
        if (currentElement != element) {
            populateDateStringSet(element[calendarTableIdProp], element.startDate.substring(0, 10), element.endDate.substring(0, 10));
        } 
    });



}
//all covers are removed
unpopulateUndroppableSections = () => {
    $(".undroppableSlot").remove();
    nonDroppableIds.clear();
}

//controls the changing of class relating to style
controlStyle = (className) => {
    dragged.classList.remove("end-in-calender");
    dragged.classList.remove("middle-in-calender");
    if (className == "start-in-calender") {
        dragged.classList.add("start-in-calender")
    } else {
        dragged.classList.remove("start-in-calender");
    }
}

//gets the new end date from the start date and course module length
function getEndDate(startDateStr, count) {
    var returnDate = new Date(startDateStr);

    while (count > 1 && startDateStr != undefined && startDateStr != null) {
        if (returnDate.getDay() % 7 < 5) {
            count--;
        }
        returnDate.setTime(returnDate.getTime() + (1000 * 60 * 60 * 24));
    }
    return returnDate;
}

//styles the dragged element as it is moved
function dragStyling(startDateStr, courseLength) {
    if (startDateStr == undefined) {
        return;
    }

    controlStyle();
    const startDate = new Date(startDateStr);

    var endDate = getEndDate(startDateStr, courseLength);

    newStartDateStr = startDateStr;
    newEndDateStr = dateToString(endDate);

    if ((startDate < calenderStartDate && startDateStr != dateToString(calenderStartDate)) && ((endDate >= calenderStartDate && endDate <= calenderEndDate) || newEndDateStr == dateToString(calenderStartDate))) {
        dragged.style.width = lengthBetweenElements(origin.id.split("_")[0], dateToString(calenderStartDate), newEndDateStr) + "%";
        controlStyle("end-in-calender");
    } else if (endDate > calenderEndDate && ((startDate <= calenderEndDate && startDate >= calenderStartDate) || startDateStr == dateToString(calenderEndDate) || startDateStr == dateToString(calenderStartDate))) {
        dragged.style.width = lengthBetweenElements(origin.id.split("_")[0], startDateStr, dateToString(calenderEndDate))  + "%";
        controlStyle("start-in-calender");
    } else {
        dragged.style.width = lengthBetweenElements(origin.id.split("_")[0], startDateStr, newEndDateStr) + "%";
        controlStyle();
    }
}

//toggle the visibility of the edit icon for when the draggable element is moved
function ToggleEditIconHidden(isHidden) {
    document.querySelectorAll(".edit-course-module").forEach(el => {
        el.hidden = isHidden;
    })
}

//Checks that the target item is not also a drag-item, by checking the class name.
// If the target is of the class "drag-item", the parent of the target will instead be used.
checkTarget = (ev) => {
    var slot = ev.target;
    if (slot != null) {
        try {
            if (slot.classList.contains("drag-item") || (slot.classList.contains("edit-course-module")) || (slot.classList.contains("fa fa-pencil-square-o")) || (slot == undefined)) {
                slot = origin;
            }
        } catch (e) {
            console.warn(e);
        }
    } else {
        console.warn();
    }
    return slot;
}

//when the edit button is clicked, an edit form will be loaded
editButtonClick = (ev) => {
    let target = ev.target;
    while (target.classList.contains("drag-item") == false) {
        target = target.parentElement;
    }
    let targetId = target.id.split("_")[0];
    $("#inputStartDate").focusout(function () {
    var inputDate = new Date($(this).val());
    console.log($(this).val());
    if (inputDate.getDay() == 6) {
        addDays(inputDate, 2);
        $(this).val(inputDate.toISOString().slice(0, 10));
    } else if (inputDate.getDay() == 0) {
        addDays(inputDate, 1);
        $(this).val(inputDate.toISOString().slice(0, 10));
    } 
});
    $.ajax({
        url: '/CourseModule/GetByIdForEdit',
        type: 'GET',
        data: {id: targetId},
        success: function (data) {
            console.log("START EDITING");
            $(".update-modal").html(data);
            $("#update-modal").modal("show");
            $("#update-course-module-form").removeData("validator");
            $("#update-course-module-form").removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse("#update-course-module-form");
            $("#inputStartDate").focusout(function () {
                var inputDate = new Date($(this).val());
                var inputTime = $(this).val().slice(10, 23);
                if (inputDate.getDay() == 6 || inputDate.getDay() == 0) {
                    addDays(inputDate, (inputDate.getDay() + 2) % 5);
                    $(this).val(inputDate.toISOString().slice(0, 10) + inputTime);
                } 
            });
            $("#inputEndDate").focusout(function () {
                var inputDate = new Date($(this).val());
                var inputTime = $(this).val().slice(10, 23);
                if (inputDate.getDay() == 6 || inputDate.getDay() == 0) {
                    addDays(inputDate, (inputDate.getDay() + 2) % 5 - 3);
                    $(this).val(inputDate.toISOString().slice(0, 10) + inputTime);
                }
            });
        }
    });
}

toggleUpdateForm = () => {
    $("#update-modal").modal("hide");
    $(".update-modal").empty()
    //$("#edit-form-submit[data-dismiss=modal]").trigger();
} 

//validity check for inputs. If invalid, will send error message to edit form and prevent update
isinputDataInvalid = (input) => {
    const inputStartDate = new Date(input.startDate);
    const inputEndDate = new Date(input.endDate);

    if (inputStartDate > inputEndDate) {
        $("#endDateError").text("End date must be after the start date");
        return true;
    }


    const doesScheduleConflict = calenderContents.some((el) => {
        if (el.id != input.id) {
            const minValidDate = new Date(el.startDate);
            const maxValidDate = new Date(el.endDate);

            console.log("INPUT VALUES: ", input.trainerId, " ", input.pathwayId, " ", input.venueId);
            return (el.trainerId == input.trainerId && input.trainerId != "" && ((minValidDate <= inputStartDate && maxValidDate >= inputStartDate)
                    || (minValidDate <= inputEndDate && maxValidDate >= inputEndDate) || (inputStartDate <= minValidDate && inputEndDate >= maxValidDate)))
                || (el.pathwayId == input.pathwayId && input.pathwayId != "" && ((minValidDate <= inputStartDate && maxValidDate >= inputStartDate)
                    || (el.startDate <= inputEndDate && maxValidDate >= inputEndDate) || (inputStartDate <= minValidDate && inputEndDate >= maxValidDate)))
                || (el.venueId == input.venueId && input.venueId != "" && ((minValidDate <= inputStartDate && maxValidDate >= inputStartDate)
                    || (minValidDate <= inputEndDate && maxValidDate >= inputEndDate) || (inputStartDate <= minValidDate && inputEndDate >= maxValidDate)));
        }
    });

    if (doesScheduleConflict == true) {
        $("#invalid-input-message").text("Current configuration causes schedule conflicts");
        return true;
    }
    return false;
}

//when the edit form is submitted, it will do an input value check before submitting
function editFormSubmit(ev) {
    console.log(ev);
    ev.preventDefault();
    let inputData = null;
    let id = $("#inputId").val();
    $.ajax({
        type: "GET",
        url: `/CourseModule/GetById/` + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (data) {
            inputData = data;
            inputData.Name = $("#inputName").val();
            inputData.Description = $("#inputDescription").val();
            inputData.PreparationNotes = $("#inputPreparationNotes").val();
            inputData.startDate = $("#inputStartDate").val();
            inputData.Duration = $("#inputDuration").val();
            inputData.endDate = $("#inputEndDate").val();
            inputData.trainerId = $("#inputTrainerId").val();
            inputData.pathwayId = $("#inputPathwayId").val();
            inputData.venueId = $("#inputVenueId").val();
            if (isinputDataInvalid(inputData) == true) {
                return;
            }

            $.ajax({
                type: "PUT",
                url: "/CourseModule/Put/" + id,
                data: inputData,
                async: true,
                success: function (data) {
                    console.log("SUCCESS");
                }
            }).done(function (data) {
                if (inputData.trainerId == "" || inputData.pathwayId == "" || inputData.venueId == "") {
                    $.ajax({
                        type: "PUT",
                        url: "/CourseModule/PutUnAssign/" + id,
                        data: inputData,
                        async: true,
                    }).done(function (data) {
                        callContentAjax("/CourseModule/GetAll");
                        toggleUpdateForm();
                    });
                } else {
                    callContentAjax("/CourseModule/GetAll");
                    toggleUpdateForm();
                }
            }).fail(function (data) {
                console.log("FAIL");
                console.log(data);
            })
        }
    });
}


function deleteFormSubmit(ev) {
    let id = $("#inputId").val();
    $.ajax({
        type: "DELETE",
        url: "/CourseModule/DeleteById/" + id,
        async: true,
        success: function (data) {
            console.log("SUCCESSFULLY DELETED");
        }
    }).done(function (data) {
        callContentAjax("/CourseModule/GetAll");
    })

}


//when a draggable element is hovering over a slot. Performed every tick
//  -> updates the origin slot and the draggable styling and checks for a valid position
startHover = (ev) => {
    ev.preventDefault();
    var slot = checkTarget(ev)

    if ((slot !== null) || (slot != undefined)) {
        findValidPosition(slot.id);
        if (origin != null || origin != undefined) {
            dragStyling(origin.id.split("_")[1], dragged.id.split("_")[1]);

            if (origin.classList.value == "slot" && origin.innerHTML == "") {
                controlClassList("is-hovering");
                origin.appendChild(dragged);
            } else if (origin.classList.value != "slot") {
                controlClassList("is-hovering-not-calender");
                origin.appendChild(dragged);
            } else if (origin == dragged.parentElement) {
                controlClassList("is-hovering");
            }
        } else {
            console.warn("origin is null or undefined");
        }
    } else {
        console.warn("target is null or undefined");
    }
}

//when a draggable element stops hovering over a slot, updates s
endHover = (ev) => {
    ev.preventDefault();
    controlClassList("EXCEPT-is-hovering");
}

//when the user starts dragging an element
//  ->  updates the draggable var
//  ->  updates the origin var
//  ->  hides the edit icon
//  ->  populates the nondroppable sections
//  ->  sets the initial position variables
dragstart = (ev) => {
    console.clear();
    dragged = ev.target;
    ToggleEditIconHidden(true);

    origin = ev.srcElement;
    while (origin.classList.contains("drag-item") || origin.classList.contains("edit-course-module") || origin.classList.contains("fa fa-pencil-square-o")) {
        origin = origin.parentElement;
    }

    if (origin == null || origin == undefined || dragged.classList.contains("edit-course-module") || dragged.classList.contains("fa fa-pencil-square-o")) {
        return;
    }
    populateUndroppableSections(dragged.id.split("_")[0]);

    startSlot = ev.srcElement.parentElement;
    startClass = dragged.classList.value;
    startWidth = dragged.style.width;
    ev.dataTransfer.setData("text", dragged.id);
    controlClassList("is-being-dragged"); 
}

//when the dragging stops
//  ->  empties the draggable and origin variables
//  ->  makes the edit button visible again
//  ->  updates the styling to 'in-calendar'
//  ->  clears the nonDroppableIds and draggedCoverIds set

dragend = (ev) => {
    ToggleEditIconHidden(false);
    unpopulateUndroppableSections();
    if (origin != null) {
        if (origin.classList.value == "slot") {
            controlClassList("in-calender");
        } else {
            controlClassList("ALL");
        }
    } else {
        controlClassList("ALL");
    }
    origin = null;
    dragged.querySelector(".edit-course-module").hidden = false;
    dragged = null;
    nonDroppableIds.clear();
    draggedCoverIds.clear();
}

// when the dragged element is dropped
//  ->  gets the properties from the dragged and origin element ids
//  ->  calls a put request
//  ->  reloads the calendar Element variable
//  ->  if put fails, return dragged element to its initial position
drop = (ev) => {
    ev.preventDefault();
    if ((origin != null || origin != undefined) && (dragged != null && dragged != undefined)) {
        const id = Number(dragged.id.split("_")[0]);
        const calendarTableId = Number(origin.id.split("_")[0]);

        const startTime = dragged.id.split("_")[2];
        const endTime = dragged.id.split("_")[3];
        var inputData = null;
        var slot = checkTarget(ev);
        var dragItem = dragged;
        $.ajax({
            type: "GET",
            url: `/CourseModule/GetById/` + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (data) {
                inputData = data;
                inputData[calendarTableIdProp] = calendarTableId;
                inputData.startDate = newStartDateStr + startTime;
                inputData.endDate = newEndDateStr + endTime;
                $.ajax({
                    type: "PUT",
                    url: "/CourseModule/Put/" + id,
                    data: inputData,
                    async: true,
                    success: function (data) {
                        console.log("SUCCESS");
                    }
                }).done(function (data) {
                    callContentAjax("/CourseModule/GetAll");
                }).fail(function () {
                    slot.innerHTML = "";
                    dragItem.classList.value = startClass;
                    dragItem.style.width = startWidth;
                    startSlot.appendChild(dragItem);
                })
            }
        });
    } else {
        console.warn("DROP: origin or dragged is null/undefined");
    }
}

// when dragged element is hovering in the unassigned.
//  ->  calls same functionality as startHover except with different styling choices
startHoverUnassigned = (ev) => {
    ev.preventDefault();
    origin = checkTarget(ev);
    dragged.style.width = "200px"
    if (origin.classList.value != "slot") {
        controlClassList("EXCEPT-is-hovering");
        origin.appendChild(dragged);
    } else if (origin == dragged.parentElement) {
        controlClassList("is-hovering");
    }
}

// when dragged element is dropped in the unassigned.
//  ->  calls same functionality as above except makes a PutUnassign request
dropUnassigned = (ev) => {
    ev.preventDefault();
    console.log("DROP UNASSIGNED START");
    const id = Number(dragged.id.split("_")[0]);
    var inputData = null;
    var slot = checkTarget(ev);
    var dragItem = dragged;
    $.ajax({
        type: "GET",
        url: `/CourseModule/GetById/` + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (data) {
            inputData = data;
            inputData[calendarTableIdProp] = null;
            console.log(inputData);
            $.ajax({
                type: "PUT",
                url: "/CourseModule/PutUnAssign/" + id,
                data: inputData,
                async: true,
                success: function (data) {
                    console.log("SUCCESS UNASSIGNED");
                }
            }).done(function (data) {
                console.log(data);
                callContentAjax("/CourseModule/GetAll");
            }).fail(function () {
                if (slot.classList.contains("unassigned-side-scroll")) {
                    slot.removeChild(dragItem);
                } else {
                    slot.empty();
                }
                dragItem.classList.value = startClass;
                dragItem.style.width = startWidth;
                startSlot.appendChild(dragItem);
            })
        }
    });
}