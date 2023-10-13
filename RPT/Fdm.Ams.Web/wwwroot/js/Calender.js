// weekly calendar code --------------------------------------

//global variables ...............................
var rowElements = null; //the data pertaining to the visible rows currently on the page
var calenderContents = null; //the data pertaining to the draggable elements
var alldata = null; //all data retrieved from the database to be used to make rows
var visibleData = null; //the data printed into the calendar

var calendarTableFocus = ""; //the table containing all the data for each row i.e. 'Venues, Trainers, Courses'
var calendarTableIdProp = ""; //the property that will updated when dragging to different rows 'venueId, trainerId, pathwayId'
var calendarTableRow = ""; //the property of the calendarTableFocus that will be shwon on each row, i.e. .name, .id

var calenderStartDate = new Date(); //first date shown on the calendar
var calenderEndDate = new Date(); //last date shown on the calendar

var todaysDate = new Date(); 
var curr = new Date(); //used to form the layout of the calendar

//variables for page navigation 
var startIndex = 0;
var viewLength = 5;
var totalRowLength = 0;

//function reference for each template
var drawFunctionRef = null;

//.................................................
//retrieve the data (async allows the data to be loaded first)
async function callColumnAjax(inputurl) {
    return $.ajax({
        type: "GET",
        url: inputurl,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (data) {
            alldata = data;
            visibleData = data;
            rowElements = visibleData.slice(0, 5);
            totalRowLength = visibleData.length;
            setupRows();
            //FiveWeekLayout;
            $("#OneWeek").on("click", OneWeekLayout);
            $("#FiveWeeks").on("click", FiveWeekLayout);
            $("#TenWeeks").on("click", TenWeekLayout);
            populateCalendar();
            UpdateViewedRowsHtml();
        }
    });
}

//retrieve data that populates the calendar
async function callContentAjax(inputurl) {
    return $.ajax({
        type: "GET",
        url: inputurl,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (data) {
            calenderContents = data;
            populateCalendar();
        }
    }).done();
}

//gets the html syntax to apply the drag and drop functionality
getDragAndDropHtml = () => {
    return "class= 'slot' ondrop='drop(event)' ondragover='startHover(event)' ondragleave='endHover(event)'";
}

//set up the calendar slot id, important for saving to the database
setCalendarSlotId = (row, date) => {
    return "" + rowElements[row].id + "_" + dateToString(date);
}

//calls the await callAjax(), ensures that the data is retrieved before the page is loaded
async function retrieveData() {
    calendarTableFocus = document.getElementById("calendarTableFocus").innerHTML.trim();
    calendarTableIdProp = document.getElementById("calendarTableIdProp").innerHTML.trim();
    calendarTableRow = document.getElementById("calendarTableRow").innerHTML.trim();
    try {
        await callContentAjax("/CourseModule/GetAll");
        await callColumnAjax("/" + calendarTableFocus + "/GetAll");
    } catch (error) {
        console.warn("ERROR:");
        console.warn(error);
    }
}

//fills out the resource collumn with the rowElements[calendarTableRow] property
function setupRows() {
    $(".row-elements").empty();
    rowElements.forEach(element => {
        $(".row-elements").append("<div class='element'><h5>" + element[calendarTableRow] + "</h5></div>")
    })
}

//empty a html element of its contents
function removeChildrenElem(selector) {
    $(selector).empty();
}

// Get the Monday of the current week
function getCurrentMonday(inputDate) {
    let currentMonday = new Date();
    currentMonday.setTime(inputDate.getTime());
    currentMonday.setDate(inputDate.getDate() - inputDate.getDay() + 1);
    currentMonday.setMonth(currentMonday.getMonth());
    currentMonday.setFullYear(currentMonday.getFullYear());
    return currentMonday;
}

//return a string that holds the dates of the start of the week and end of the week. To be used in the calendar week header
function getWeekStartAndEndDate(currentMonday) {
    let endWeekDate = new Date();
    endWeekDate.setTime(currentMonday.getTime());
    endWeekDate.setDate(endWeekDate.getDate() + 4);
    weekDateString = "";

    weekDateString += getMonthTitle(currentMonday.getMonth()) + " " + currentMonday.getDate() + " - " + getMonthTitle(endWeekDate.getMonth()) + " " + endWeekDate.getDate();

    return weekDateString;
}

//translates the month number returned from Date.getMonth() into the month text
function getMonthTitle(month) {
    let months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    return months[month];
}

//creates the month row by passing in first date in the calendar, and the number of days shown in the calendar.
//this will return a string containing the html element for the month row.
function generateMonthRow(currentMonday, dayslength) {
    let divString = "<div class = 'monthRow' style='";
    let monthLengthArray = [];

    let currentDate = new Date();
    currentDate.setTime(currentMonday.getTime());

    let yesterdayDate = new Date();
    yesterdayDate.setTime(currentDate.getTime());
    yesterdayDate.setDate(currentDate.getDate() - 1);

    let containerString = "<h3";
    if (currentDate.getMonth() % 2 == 0) {
        containerString += " style='background: #E0FFFF;'"
    }
    containerString += ">" + getMonthTitle(currentDate.getMonth()) + "</h3>";

    var dayCount = 0;
    for (var i = 0; i < dayslength; i++) {
        if (currentDate.getMonth() != yesterdayDate.getMonth()) {
            monthLengthArray.push(dayCount);
            dayCount = 0;
            if (i < dayslength - 2) {
                containerString += "<h3"
                if (currentDate.getMonth() % 2 == 0) {
                    containerString += " style='background: #E0FFFF;'"
                }

                containerString += "> " + getMonthTitle(currentDate.getMonth()) + "</h3>";
            }
        }
        if (i % 7 < 5) {
            dayCount++;
        }

        yesterdayDate.setTime(currentDate.getTime());
        currentDate.setDate(currentDate.getDate() + 1);
    }
    if (dayCount > 0) {
        monthLengthArray.push(dayCount)
    }
    divString += "display: grid; grid-template-columns: ";
    monthLengthArray.forEach(length => {
        divString += (length * 100 * 7/ (5*dayslength)) + "% "
    })
    divString += "; margin-bottom: 0; box-shadow: 1px 1px 5px rgb(230, 230, 230);' >";
    return divString + containerString + "</div>";
}

//creates a string to be used in the month title
function setMonthTitle(monthList, yearList) {
    let monthTitle = "";
    monthList.forEach((monthIndex, index) => {
        if (monthTitle.length != 0) {
            monthTitle += " - ";
        }
        monthTitle += getMonthTitle(monthIndex - 1);
        if (yearList.length > 1) {

            monthTitle += " " + (monthList[index] < monthList[0] ? yearList[1] : yearList[0]);
        }
    })
    if (yearList.length == 1) {
        monthTitle += " " + yearList[yearList.length-1];
    }
    $(".currMonth").text(monthTitle);
    return;
}

function setCalenderStartAndEndDate(currentMonday, length) {
    calenderStartDate.setDate(currentMonday.getDate());
    calenderStartDate.setMonth(currentMonday.getMonth());
    calenderStartDate.setFullYear(currentMonday.getFullYear());
    calenderEndDate.setTime(calenderStartDate.getTime());
    calenderEndDate.setDate(calenderEndDate.getDate() + ((7 * length) - 3));
}

//converts a date object to a string YY-MM-DD (this matches a date format for a JSON Data property)
function dateToString(date) {
    return date.getFullYear() + "-" + (("" + (date.getMonth()+1)).length == 1 ? "0" + (date.getMonth() + 1) : (date.getMonth() + 1)) + "-" + (("" + date.getDate()).length == 1 ? "0" + date.getDate() : date.getDate());
}

//adds days to a date object
function addDays(dateObj, days) {
    dateObj.setTime(dateObj.getTime() + (days * 1000 * 60 * 60 * 24));
}

//finds the difference in work days (Mon - Fri) 
function dateDifferenceInDays(startDateStr, endDateStr) {
    var startDate = new Date(startDateStr);
    const endDate = new Date(endDateStr);
    var workdayCount = 0;
    while (startDate <= endDate) {
        if ((startDate.getDay()+6) % 7 < 5) {
            workdayCount++;
        }
        startDate.setTime(startDate.getTime() + (1000 * 60 * 60 * 24));
    }
    return workdayCount;
}

//finds the length between elements as a percentage of the size of the start element
//used for styling the draggable elements
function lengthBetweenElements(id, startDateStr, endDateStr) {
    startEl = document.getElementById(id + "_" + startDateStr);
    endEl = document.getElementById(id + "_" + endDateStr)

    if (startEl != null && endEl != null) {
        const startLeft = startEl.getBoundingClientRect().x;
        const startWidth = startEl.getBoundingClientRect().width;
        const endLeft = endEl.getBoundingClientRect().x;
        const endWidth = endEl.getBoundingClientRect().width;
        return (((endLeft + endWidth) - startLeft) / startWidth)*100;
    }
    return 0;
}

//defines the draggable element style depending on whether the start is off the calendar, the end is off the calendar, or only the middle is on the calendar
//returns a string with a style html element containing calculated length
elementStyle = (id, startDateStr, endDateStr) => {
    var startDate = new Date(startDateStr);
    const endDate = new Date(endDateStr);

    var classString = "class='drag-item in-calender";
    if ((startDate < calenderStartDate && startDateStr != dateToString(calenderStartDate)) && ((endDate >= calenderStartDate && endDate <= calenderEndDate) || endDateStr == dateToString(calenderStartDate))) {
        length = lengthBetweenElements(id, dateToString(calenderStartDate), endDateStr);
        classString += " end-in-calender";
    } else if (endDate > calenderEndDate && ((startDate <= calenderEndDate && startDate >= calenderStartDate) || startDateStr == dateToString(calenderEndDate) || startDateStr == dateToString(calenderStartDate))) {
        length = lengthBetweenElements(id, startDateStr, dateToString(calenderEndDate));
        classString += " start-in-calender";
    } else if (startDate < calenderStartDate && endDate > calenderEndDate) {
        length = lengthBetweenElements(id, dateToString(calenderStartDate), dateToString(calenderEndDate));
        classString += " middle-in-calender";
    } else {
        length = lengthBetweenElements(id, startDateStr, endDateStr);
    } 
    return classString + "' style='" + "width: " + length + "%;'";
}

//generates the target slot to insert the draggable element. If the end date is on the calendar but the start date is off the calendar, then the calendarStartDate is used instead
function getTargetSlotDateID(id, startDateStr, endDateStr) {
    let startDate = new Date(startDateStr);
    let endDate = new Date(endDateStr);
    if ((startDate < calenderStartDate && endDate >= calenderStartDate) || endDateStr == dateToString(calenderStartDate)) {
        return id + "_" + calenderStartDate.getFullYear() + "-" + (("" + (calenderStartDate.getMonth()+1)).length == 1 ? "0" + (calenderStartDate.getMonth() + 1) : (calenderStartDate.getMonth() + 1)) + "-" + (("" + calenderStartDate.getDate()).length == 1 ? "0" + calenderStartDate.getDate() : calenderStartDate.getDate());
    }
    return id + "_" + startDateStr;
}

//The calendar is populated with the draggable elements using the database
populateCalendar = () => {
    $(".unassigned-side-scroll").empty();
    $(".slot").empty();
    calenderContents.forEach(element => {
        let courseLength = dateDifferenceInDays(element.startDate.substring(0, 10), element.endDate.substring(0, 10));
        if (element[calendarTableIdProp] != null && element.startDate != null && element.startDate != null) {
            let targetId = getTargetSlotDateID(element[calendarTableIdProp], element.startDate.substring(0, 10), element.endDate.substring(0, 10));
            let classAndStyleString = elementStyle(element[calendarTableIdProp], element.startDate.substring(0, 10), element.endDate.substring(0, 10));


            $("#" + targetId).empty().append("<div id='" + element.id + "_" + courseLength + "_" + element.startDate.substring(10) + "_" + element.endDate.substring(10) + "' "
                + classAndStyleString + " "
                + "draggable = 'true' ondragstart = 'dragstart(event)' ondragend = 'dragend(event)' "
                + ">CourseID: "
                + element.id
                + "<br><button class='edit-course-module' draggable='false' onclick='editButtonClick(event)'><i class='fa fa-pencil-square-o' aria-hidden='true' draggable = 'false'></i></button>"
                + "</div>");
        } else {
            $(".unassigned-side-scroll").append("<div id='" + element.id + "_" + courseLength + "_" + element.startDate.substring(10) + "_" + element.endDate.substring(10) + "' "
                + "class='drag-item' style='width: 200px; height: 100%;' "
                + "draggable='true' ondragstart='dragstart(event)' ondragend='dragend(event)' "
                + ">CourseID: "
                + element.id
                + "<br><button class='edit-course-module' draggable='false' onclick='editButtonClick(event)'><i class='fa fa-pencil-square-o' aria-hidden='true'></i></button>"
                + "</div>");
        } 
    })
}


//Draw the html for each calendar template
function drawOneWeekLayout(curr) {
    removeChildrenElem(".date");
    console.log("DRAW ONE");
    let today = new Date();


    // Get the Monday of the current week
    let currentMonday = (curr.getDay()==1 ? curr : getCurrentMonday(curr));
    setCalenderStartAndEndDate(currentMonday, 1);

    //For the month title
    let currMonthList = [];
    let currYearList = [];

    // Labels for each day
    let days = ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"];

    let htmlString = ""

    // loop 7 times to get all days of the week
    htmlString += "<div class='OneWeekGrid'>"
    for (var i = 0; i < 7; i++) {
        // apply formatting
        let style = "";
        if (i < 5) {
            htmlString += "<div class='day-element'>"
            // add to HTML doc
            htmlString += "<div class='top'";
            if (currentMonday.getDate() == today.getDate() && currentMonday.getMonth() == today.getMonth() && currentMonday.getFullYear() == today.getFullYear()) {
                style +="background-color: #FFB6C1;"
            } else if (currentMonday.getMonth() % 2 == 0) {
                style += "background: #E0FFFF;";
            }
            if (style != "") {
                htmlString += "style='" + style + "'"
            }
            htmlString += ">" +
                "<span>" + days[(currentMonday.getDay() + 6) % 7] + "</span>" +
                "<h4>" + currentMonday.getDate() + "</h4>" +
                "</div>";

            for (var row = 0; row < rowElements.length; row++) {
                htmlString += "<div class='currDateCol'";
                if (style != "") {
                    htmlString += " style='" + style + "'";
                }
                htmlString += ">"
                    + "<div id='" + setCalendarSlotId(row, currentMonday) + "' " + getDragAndDropHtml() + "></div>"
                    + "</div>";
            }
            //checks what month and year correspond to the week for the month title
            if (!currMonthList.includes(currentMonday.getMonth() + 1)) {
                currMonthList.push(currentMonday.getMonth() + 1);
            }
            if (!currYearList.includes(currentMonday.getFullYear())) {
                currYearList.push(currentMonday.getFullYear());
            }
        }
        // increment for the next day
        addDays(currentMonday, 1);
        htmlString += "</div>";
    }

    htmlString += "</div>";

    $(".date").append(htmlString);
    setMonthTitle(currMonthList, currYearList);

    //return the current day so that calender day can be tracked
    return currentMonday;
}

//Draw the 5 week layout
function drawFiveWeekLayout(curr) {
    removeChildrenElem(".date");
    console.log("DRAW FIVE");
    // Get the Monday of the current week
    let currentMonday = (curr.getDay() == 1 ? curr : getCurrentMonday(curr));
    setCalenderStartAndEndDate(currentMonday, 5);

    let today = new Date();

    //For the month title
    let currMonthList = [];
    let currYearList = [];

    // Labels for each day
    let days = ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"];

    let htmlString = ""

    htmlString += generateMonthRow(currentMonday, 35);

    // loop 7 times to get all days of the week
    htmlString += "<div class='fiveWeekGrid'>"

    for (var week = 0; week < 5; week++) {
        htmlString += "<div class='currWeekCol'>"
        htmlString += "<h5>" + getWeekStartAndEndDate(currentMonday) + "</h5>"
        htmlString += "<div class='OneWeekGrid'>"
        for (var row = 0; row < rowElements.length; row++) {
            for (var i = 0; i < 7; i++) {
                let style = "";
                if (i < 5) {
                    // add to HTML doc
                    htmlString += "<div class='currDateCol' ";
                    if (currentMonday.getDay() == 5) {
                        style += "border-right: none;";
                    }
                    if (currentMonday.getDate() == today.getDate() && currentMonday.getMonth() == today.getMonth() && currentMonday.getFullYear() == today.getFullYear()) {
                        style += "background-color: #FFB6C1;"
                    } else if (currentMonday.getMonth() % 2 == 0) {
                        style += "background: #E0FFFF;";
                    }
                    if (style != "") {
                        htmlString += "style='" + style + "'"
                    }
                    htmlString += ">" +
                        "<div id='" + setCalendarSlotId(row, currentMonday) + "' " + getDragAndDropHtml() + "></div>" +
                        "</div>";


                    //checks what month and year correspond to the week for the month title
                    if (!currMonthList.includes(currentMonday.getMonth() + 1)) {
                        currMonthList.push(currentMonday.getMonth() + 1);
                    }
                    if (!currYearList.includes(currentMonday.getFullYear())) {
                        currYearList.push(currentMonday.getFullYear());
                    }
                }
                // increment for the next day
                addDays(currentMonday, 1);
            }
            if (row != rowElements.length - 1) {
                addDays(currentMonday, -7);
            }
        }
        
        htmlString += "</div></div>"
    }
    htmlString += "</div>"

    $(".date").append(htmlString);
    setMonthTitle(currMonthList, currYearList);

    //return the current day so that calender day can be tracked
    return currentMonday;
}

//Draw the 10 week layout
function drawTenWeekLayout(curr) {
        removeChildrenElem(".date");
        console.log("DRAW TEN");
        // Get the Monday of the current week
        let currentMonday = (curr.getDay() == 1 ? curr : getCurrentMonday(curr));
        setCalenderStartAndEndDate(currentMonday, 10);

        let today = new Date();

        //For the month title
        let currMonthList = [];
        let currYearList = [];

        // Labels for each day
        let days = ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"];

        let htmlString = ""

        htmlString += generateMonthRow(currentMonday, 70);

        // loop 7 times to get all days of the week
        htmlString += "<div class='tenWeekGrid'>"

        for (var week = 0; week < 10; week++) {
            htmlString += "<div class='currWeekCol'>"
            htmlString += "<h6>" + getWeekStartAndEndDate(currentMonday) + "</h6>"
            htmlString += "<div class='OneWeekGrid'>"
            for (var row = 0; row < rowElements.length; row++) {
                for (var i = 0; i < 7; i++) {
                    // apply formatting
                    let style = "";
                    if (i < 5) {
                        // add to HTML doc
                        htmlString += "<div class='currDateCol";
                        if (currentMonday.getDay() == 5) {
                            style += "border-right: none;";
                        }

                        htmlString += "' ";
                        if (currentMonday.getDate() == today.getDate() && currentMonday.getMonth() == today.getMonth() && currentMonday.getFullYear() == today.getFullYear()) {
                            style += "background-color: #FFB6C1;"
                        } else if (currentMonday.getMonth() % 2 == 0) {
                            style += "background: #E0FFFF;";
                        }

                        if (style != "") {
                            htmlString += "style='" + style + "'"
                        }
                        htmlString += ">" +
                            "<div id='" + setCalendarSlotId(row, currentMonday) + "' " + getDragAndDropHtml() + "></div>" +
                            "</div>";

                        //checks what month and year correspond to the week for the month title
                        if (!currMonthList.includes(currentMonday.getMonth() + 1)) {
                            currMonthList.push(currentMonday.getMonth() + 1);
                        }
                        if (!currYearList.includes(currentMonday.getFullYear())) {
                            currYearList.push(currentMonday.getFullYear());
                        }
                    }
                    // increment for the next day
                    addDays(currentMonday, 1);
                }
                if (row != rowElements.length - 1) {
                    addDays(currentMonday, -7);
                }
            }
            htmlString += "</div></div>"
        }
        htmlString += "</div>"

        $(".date").append(htmlString);
        setMonthTitle(currMonthList, currYearList);

        //return the current day so that calender day can be tracked
        return currentMonday;
}


// define button functionality for each calendar template
OneWeekLayout = function () {
    drawFunctionRef = OneWeekLayout;

    $("#OneWeek").prop("disabled", true);
    $("#FiveWeeks").prop("disabled", false);
    $("#TenWeeks").prop("disabled", false);

    $("#goToDateSub").unbind();
    $("#next").unbind();
    $("#prev").unbind();

    removeChildrenElem(".date");

    if (calenderContents != null && rowElements != null) {
        curr = drawOneWeekLayout(curr);
        populateCalendar();
        curr.setDate(curr.getDate() - 6);

        // go to date code
        $("#goToDateSub").click(function () {
            let goToDate = new Date(document.getElementById("goToDateVal").value);
            curr.setTime(goToDate.getTime());
            curr = drawOneWeekLayout(curr);
            populateCalendar();
            document.getElementById("goToDateVal").valueAsDate = todaysDate;
            curr.setDate(curr.getDate() - 6);
        });

        // next button code
        $("#next").click(function () {
            curr.setDate(curr.getDate() + 8);
            curr = drawOneWeekLayout(curr);
            populateCalendar();
            curr.setDate(curr.getDate() - 6);
        });

        // previous button code
        $("#prev").click(function () {
            // rerun the loop to for the previous week
            curr.setDate(curr.getDate() - 6);
            //draw the layout
            curr = drawOneWeekLayout(curr);
            populateCalendar();
            curr.setDate(curr.getDate() - 6);
        });
    } else {
        console.warn("ERROR: calendarContents or rowElements is null");
        setTimeout(OneWeekLayout, 20);
    }
    
}

FiveWeekLayout = function () {
    drawFunctionRef = FiveWeekLayout;

    $("#OneWeek").prop("disabled", false);
    $("#FiveWeeks").prop("disabled", true);
    $("#TenWeeks").prop("disabled", false);

    $("#goToDateSub").unbind();
    $("#next").unbind();
    $("#prev").unbind();

    if (calenderContents != null && rowElements != null) {
        removeChildrenElem(".date");

        curr = drawFiveWeekLayout(curr);
        populateCalendar();
        curr.setDate(curr.getDate() - 34);

        // go to date code
        $("#goToDateSub").click(function () {
            let goToDate = new Date(document.getElementById("goToDateVal").value);
            curr.setTime(goToDate.getTime());
            curr = drawFiveWeekLayout(curr);
            populateCalendar();
            document.getElementById("goToDateVal").valueAsDate = todaysDate;
            curr.setDate(curr.getDate() - 34);
        });

        // next button code
        $("#next").click(function () {
            curr.setDate(curr.getDate() + 8);
            curr = drawFiveWeekLayout(curr);
            populateCalendar();
            curr.setDate(curr.getDate() - 34);
        });

        // previous button code
        $("#prev").click(function () {
            curr.setDate(curr.getDate() - 6);
            curr = drawFiveWeekLayout(curr);
            populateCalendar();
            curr.setDate(curr.getDate() - 34);
        });
    } else {
        console.warn("ERROR: calendarContents or rowElements null");
        setTimeout(FiveWeekLayout, 20);
    }
}

TenWeekLayout = function () {
    drawFunctionRef = TenWeekLayout;

    $("#OneWeek").prop("disabled", false);
    $("#FiveWeeks").prop("disabled", false);
    $("#TenWeeks").prop("disabled", true);

    $("#goToDateSub").unbind();
    $("#next").unbind();
    $("#prev").unbind();

    removeChildrenElem(".date");

    if (calenderContents != null && rowElements != null) {
        curr = drawTenWeekLayout(curr);
        populateCalendar();
        curr.setDate(curr.getDate() - 69);

        // go to date codes
        $("#goToDateSub").click(function () {
            let goToDate = new Date(document.getElementById("goToDateVal").value);
            curr.setTime(goToDate.getTime());
            curr = drawTenWeekLayout(curr);
            populateCalendar();
            document.getElementById("goToDateVal").valueAsDate = todaysDate;
            curr.setDate(curr.getDate() - 69);
        });

        // next button code
        $("#next").click(function () {
            curr.setDate(curr.getDate() + 8);
            curr = drawTenWeekLayout(curr);
            populateCalendar();
            curr.setDate(curr.getDate() - 69);
        });

        // previous button code
        $("#prev").click(function () {
            curr.setDate(curr.getDate() - 6);
            curr = drawTenWeekLayout(curr);
            populateCalendar();
            curr.setDate(curr.getDate() - 69);
        });
    } else {
        console.warn("ERROR: calendarContents or rowElements is null");
        setTimeout(TenWeekLayout, 20);
    }
}

//updated the values in the page navigation index
UpdateViewedRowsHtml = () => {
    $("#startIndex").html(startIndex + 1);
    $("#endIndex").html(Math.min(startIndex + viewLength, totalRowLength));
    $("#totalRowElements").html(totalRowLength);
    UpdateNavigationButtonAccess();
}

//updates the permissions of the navigation button
UpdateNavigationButtonAccess = () => {
    if (startIndex > totalRowLength - viewLength - 1) {
        $("#nextNav").prop("disabled", true);
    } else {
        $("#nextNav").prop("disabled", false);
    }
    if (startIndex < viewLength) {
        $("#prevNav").prop("disabled", true);
    } else {
        $("#prevNav").prop("disabled", false);
    }
}

//date range checks on the go-to-date function
UpdatePermittedDateRange = () => {
    var maxDate = new Date();
    var minDate = new Date();
    maxDate.setFullYear(todaysDate.getFullYear() + 1000);
    minDate.setFullYear(todaysDate.getFullYear() - 1000);

    console.log(dateToString(maxDate));
    console.log(dateToString(minDate));

    document.querySelectorAll("input[type='date']").forEach((dateInput) => {
        dateInput.setAttribute('max', maxDate.toISOString().slice(0, 10));
        dateInput.setAttribute('min', minDate.toISOString().slice(0, 10));
    });
}

//Checks if the input of drop down filter and updates the calendar
async function UpdateCalendarFilter(region) {
    if (region == "") {
        visibleData = alldata;
        totalRowLength = visibleData.length;
        rowElements = visibleData.slice(0, Math.min(totalRowLength, 5));
        setupRows();
        populateCalendar();
        UpdateViewedRowsHtml();
        return;
    }
    var regionId = region.split('.')[0];
    console.log(regionId);
    console.log("FILTER");
    console.log(region);


    $.ajax({
        type: "GET",
        url: '/Venue/GetActiveVenuesByRegionId',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        data: { regionID: regionId },
        success: function (data) {
            console.log(data);
            visibleData = data;
            totalRowLength = visibleData.length;
            rowElements = visibleData.slice(0, Math.min(5, totalRowLength));
            setupRows();
            populateCalendar();
            UpdateViewedRowsHtml();
        }
    });
}

//_-----------------------------------------------------------------------------
//CODE BEGINS HERE

//the date is firstly retrieved, populating the all date and calendarContents variable
retrieveData();

//updates the go-to-date permitted date range
UpdatePermittedDateRange();

//sets the initial value of the go-to-date input to todays date
document.getElementById("goToDateVal").valueAsDate = todaysDate;

//disables the previous option in the page navigator 
if (startIndex < viewLength) {
    $("#prevNav").prop("disabled", true);
}

//functionlity for the page navigatior
$("#nextNav").click(function () {
    removeChildrenElem(".row-elements");
    removeChildrenElem(".date");
    startIndex += viewLength;
    rowElements = visibleData.slice(startIndex, startIndex + viewLength);
    setupRows();
    drawFunctionRef();
    populateCalendar();
    UpdateViewedRowsHtml();
});
$("#prevNav").click(function () {
    removeChildrenElem(".row-elements");
    removeChildrenElem(".date");
    startIndex -= viewLength;
    rowElements = visibleData.slice(startIndex, startIndex + viewLength);
    setupRows();
    drawFunctionRef();
    populateCalendar();
    UpdateViewedRowsHtml();
});

//bidning for calendar filter
$("#dropdownList").change(() => {
    UpdateCalendarFilter($("#dropdownList option:selected").val());
})

//validity check when focused moved away from the go-to-date input field
$("#goToDateVal").focusout(function () {
    var inputDate = new Date($(this).val());
    var maxDate = new Date();
    var minDate = new Date();
    maxDate.setFullYear(todaysDate.getFullYear() + 1000);
    minDate.setFullYear(todaysDate.getFullYear() - 1000);

    console.log("Input date: ", inputDate);

    if (inputDate > maxDate) {
        $(this).val(maxDate.toISOString().slice(0, 10));
    } else if (inputDate < minDate) {
        $(this).val(minDate.toISOString().slice(0, 10));
    } else if (inputDate == null || inputDate == undefined || inputDate == "Invalid Date") {
        $(this).val(todaysDate.toISOString().slice(0, 10));
    }
});

//Sets the default button layout to 5 weeks
$(FiveWeekLayout);