let planNames = ["Cyber", "Physics", "ITM"];
let planIDs = [0, 1, 2];
let plan = {};
let catalog = {};
let requirements = {};

fetch('/Home/GetCourseFinder')
    .then(response => response.json())
    .then(data => {        
        
        catalog = data;

    })
    .catch(error => {
        console.error('Error:', error);
    });

fetch('/Home/GetRequirements')
    .then(response => response.json())
    .then(data => {        
        
        requirements = data;
        
    })
    .catch(error => {
        console.error('Error:', error);
    });

fetch('/Home/GetPlan')
    .then(response => response.json())
    .then(data => {        
        plan = data;
        renderPlan();
        renderReqs();
        renderCourseFinder();
    })
    .catch(error => {
        console.error('Error:', error);
    });

// fetch('/Home/GetData')
//     .then(response => response.json())
//     .then(data => {
//         //console.log(data);
//     })
//     .catch(error => {
//         console.error('Error:', error);
//     });

document.addEventListener("DOMContentLoaded", function() {
    //renderPlan();
    //renderCourseFinder();
    //renderReqs();
    renderManagePlans();
});
// window.onload = function () {
//     renderPlan();
//     renderCourseFinder();
//     renderReqs();
//     renderManagePlans();
// };

function renderManagePlans() {
    let $subMenu = $(".submenu-content");
    $subMenu.html("");

    for (let i = 0; i < planNames.length; i++) {
        let $a = $(
            `<a href="#" onclick="renderAgain(${planIDs[i]})">${planNames[i]}</a>`
        );
        $subMenu.append($a);
    }
}

/* Plan */
// Give each course a unique ID for drag and drop
let id = 0;
let totalHours = 0.0;

function renderPlan() {
    $("#plan-name").text(plan.name);
    $("#major").html(`<span class="tag">Major</span> ${plan.major}`);
    $("#catalog").html(`<span class="tag">Catalog</span> ${catalog.year}`);

    let $elem = $(".plan-grid");

    let courses = organizeCourses(plan);

    Object.keys(courses).forEach((year) => {
        Object.keys(courses[year]).forEach((term) => {
            let trueYear = Number(year);
            if (term != "Fall") {
                trueYear += 1;
            }

            let semester = `${term}-${trueYear}`;
            let dnd =
                "ondragover='onDragOver(event);' ondragleave='onDragLeave(event);' ondrop='onDrop(event);'";
            let $div = $(`<div id=${semester} class="semester" ${dnd}></div>`);

            let hours = 0.0;
            courses[year][term].forEach((course) => {
                let name = catalog.courses[course].name;
                let $course = $(
                    `<p id="${id}" draggable="true" ondragstart="onDragStart(event)"><span class="tag">${course}</span> ${name}</p>`
                );
                $div.append($course);
                hours += catalog.courses[course].credits;
                id += 1;
            });
            totalHours += hours;

            let $header = $(`<div class="semester-info"></div>`);
            $header.append($(`<h3>${term} ${trueYear}</h3>`));
            $header.append($(`<span class="tag">${hours}</span>`));

            $div.prepend($header);

            $elem.append($div);
        });
    });

    // update total hours
    $("#total-hours").html(`<span class="tag">Total Hours</span> ${totalHours}`);
}

function organizeCourses(plan) {
    let courses = {};
    let terms = ["Fall", "Spring", "Summer"];

    Object.keys(plan.courses).forEach((courseId) => {
        let course = plan.courses[courseId];
        let year = course.year;
        if (course.term != "Fall") {
            year -= 1;
        }

        if (!courses[year]) {
            courses[year] = {};
            terms.forEach((term) => {
                courses[year][term] = [];
            });
        }

        courses[year][course.term].push(course.id);
    });

    return courses;
}

/* Requirements */
function getName(courseId) {
    return catalog.courses[courseId].name;
}

async function renderReqs() {
    let planned = new Set(Object.keys(plan.courses));

    let $acc = $("#accordion");

    // if (requirements.categories == null) {
    //     requirements.categories = {}; 
    // }
    let sections = Object.keys(requirements.categories);

    sections.forEach((s) => {
        let section = requirements.categories[s];
        let $header = $(`<h3>${s}</h3>`);
        let $div = $("<div></div>");
        let courses = section.courses;
        courses.forEach((course) => {
            let $courseDiv = $(`<div class="reqCourse"></div>`);
            $courseDiv.append(
                $(
                    `<p draggable="true" ondragstart="onDragStart(event)"><span class="tag">${course}</span> ${getName(
                        course
                    )}</p>`
                )
            );

            if (planned.has(course)) {
                $courseDiv.append($(`<img src="checkmark.png" alt="checkmark">`));
            }
        
            $div.append($courseDiv);
        });

        $acc.append($header);
        $acc.append($div);
    });

    // JQuery accordion
    $(function () {
        $("#accordion").accordion({
            collapsible: true,
        });
    });
}

/* Course Finder */
function renderCourseFinder() {
    let courses = catalog.courses;
    let tbody = $("#courses-body");

    for (let courseId in courses) {
        let course = courses[courseId];
        let $newRow = $(
            '<tr draggable="true" ondragstart="onDragStart(event)"></tr>'
        );
        $newRow.append($(`<td>${course.credits}</td>`));
        $newRow.append($(`<td><span class="tag">${course.id}</span></td>`));
        $newRow.append($(`<td>${course.name}</td>`));
        $newRow.append($(`<td>${course.description}</td>`));
        tbody.append($newRow);
    }

    let number = $("#courses-body tr").length;
    updateNumberOfEntries(number);
}

function updateNumberOfEntries(totalVisibleEntries) {
    if (totalVisibleEntries === 0) {
        $("#numEntries").text("No entries found");
        return;
    }
    $("#numEntries").text(
        `Showing 1 to ${totalVisibleEntries} of ${totalVisibleEntries} entries`
    );
}

function searchCourses() {
    let input = document.getElementById("searchBar").value.toUpperCase();
    let table = document.getElementById("coursesTable");
    let rows = table.getElementsByTagName("tr");
    let visibleCount = 0;

    for (let i = 1; i < rows.length; i++) {
        let matchFound = false;
        for (let j = 0; j < rows[i].cells.length; j++) {
            let cellText = rows[i].cells[j].innerText.toUpperCase();
            if (cellText.includes(input)) {
                matchFound = true;
                visibleCount++;
                break;
            }
        }
        if (matchFound) {
            rows[i].style.display = "";
        } else {
            rows[i].style.display = "none";
        }
    }
    updateNumberOfEntries(visibleCount);
}

function sortTable(column) {
    var table,
        rows,
        switching,
        i,
        x,
        y,
        shouldSwitch,
        dir,
        switchcount = 0;
    table = document.getElementById("coursesTable");
    switching = true;

    dir = "asc";

    while (switching) {
        switching = false;
        rows = table.rows;

        for (i = 1; i < rows.length - 1; i++) {
            shouldSwitch = false;

            x = rows[i].getElementsByTagName("TD")[column];
            y = rows[i + 1].getElementsByTagName("TD")[column];

            if (dir == "asc") {
                if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                    shouldSwitch = true;
                    break;
                }
            } else if (dir == "desc") {
                if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
                    shouldSwitch = true;
                    break;
                }
            }
        }
        if (shouldSwitch) {
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;

            switchcount++;
        } else {
            if (switchcount == 0 && dir == "asc") {
                dir = "desc";
                switching = true;
            }
        }
    }
}

function savePlan() {
    let courses = {};
  
    $(".semester").each(function() {
      let semesterID = $(this).attr("id").split("-");
      let semester = semesterID[0];
      let year = semesterID[1];
  
      $(this).find("p span").each(function() {
        let tag = $(this).text();
        courses[tag] = {
          id: tag,
          term: semester,
          year: year
        };
      })
    })

    return courses;
  
    // $.ajax({
    //     url: "something",
    //     type: "POST",
    //     data: JSON.stringify(courses),
    //     contentType: "application/json",
    //     dataType: "json",
    //     error: function (response) {
    //         alert(response.responseText);
    //     },
    //     success: function (response) {
    //         alert(response);
    //     }
    // });
  }