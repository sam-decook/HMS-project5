/* Drag and drop simply manipulates the DOM.
 * It used to be simple, now it's unweildy
 *
 * Each course in the plan has a unique ID
 * If a course is moved around within the plan
 * - The course ID is stored in the event
 * - The total semester credits are decreased
 * If a course is added from the requirements or course finder
 * - The course tag (ie. CS-1210) is stored in the event
 * 
 * When a course is dropped into a semeser, check if data is a number.
 * - True: update total credits of the source semester, detach course
 * - False: create a new course, update total credits
 * - Always add the course to the destination semester
 */

// Takes a string of HTML and returns the text inside of a span tag
function getCourseId(html) {
    let pieces = html.split("<");
    for (let i = 0; i < pieces.length; i++) {
        if (pieces[i].includes("span")) {
            return pieces[i].split(">")[1];
        }
    }
}

function getHours(courseTag) {
    return Number(catalog.courses[courseTag].credits);
}

function onDragStart(event) {
    console.log(event);
    event.dataTransfer.effectAllowed = "move";
    event.dataTransfer.dropEffect = "move";

    if (event.target.hasAttribute("id")) {
        event.dataTransfer.setData("text", event.target.id);
    } else {
        let tag = getCourseId(event.target.innerHTML);
        event.dataTransfer.setData("text", tag);
    }
}

function onDragOver(event) {
    event.preventDefault();
    event.dataTransfer.dropEffect = "move";
    event.currentTarget.style.backgroundColor = "var(--primary)";
}

function onDragLeave(event) {
    event.preventDefault();
    event.currentTarget.style.backgroundColor = "white";
}

function onDrop(event) {
    let $course;
    let data = event.dataTransfer.getData("text");
    let hours = 0;

    if (isNaN(Number(data))) {
        let tag = event.dataTransfer.getData("text");
        let name = catalog.courses[tag].name;

        $course = $(`<p id="${id}" draggable="true" ondragstart="onDragStart(event)"><span class="tag">${tag}</span> ${name}</p>`);
        id += 1;

        hours = getHours(tag);
        totalHours += hours;

        $("#total-hours").html(`<span class="tag">Total Hours</span> ${totalHours}`)
        addCheckmark(tag);

    } else {
        let courseId = event.dataTransfer.getData("text");

        let tag = getCourseId($("#" + courseId).html());
        hours = getHours(tag);

        let $prevSemester = $("#" + courseId).closest(".semester");
        let prevSemHours = $prevSemester.find(".semester-info span").text();
        $prevSemester.find(".semester-info span").text(Number(prevSemHours) - hours);

        $course = $("#" + courseId).detach();
    }

    event.dataTransfer.dropEffect = "move";
    event.currentTarget.style.backgroundColor = "white";

    let $current = $("#" + event.currentTarget.id);
    $current.append($course);

    let currHours = $("#" + event.currentTarget.id + " div span").text();
    $("#" + event.currentTarget.id + " div span").text(Number(currHours) + hours);
}

function deleteCourse(event) {
    event.dataTransfer.dropEffect = "move";

    let courseId = event.dataTransfer.getData("text");
    let tag = getCourseId($("#" + courseId).html());
    hours = getHours(tag);
    totalHours -= hours;

    let $prevSemester = $("#" + courseId).closest(".semester");
    let prevSemHours = $prevSemester.find(".semester-info span").text();
    $prevSemester.find(".semester-info span").text(Number(prevSemHours) - hours);

    $course = $("#" + courseId).detach();
    $("#total-hours").html(`<span class="tag">Total Hours</span> ${totalHours}`)

    removeCheckmark(tag);
}

function addCheckmark(tag) {
    $(".reqCourse").each(function() {
        if ($(this).find("span").text() == tag) {
            $(this).append($(`<img src="checkmark.png" alt="checkmark">`));
        }
    })
}

function removeCheckmark(tag) {
    $(".reqCourse").each(function() {
        if ($(this).find("span").text() == tag) {
            $(this).find("img").detach();
        }
    })
}