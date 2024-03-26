let plan = {
    student: "joe",
    name: "myPlan",
    major: "Computer Science",
    currYear: 2022,
    currTerm: "Spring",
    courses: {
        "CS-1210": {
            id: "CS-1210",
            year: 2021,
            term: "Fall",
        },
        "MATH-1710": {
            id: "MATH-1710",
            year: 2022,
            term: "Spring",
        },
        "EGCP-1010": {
            id: "EGCP-1010",
            year: 2021,
            term: "Fall",
        },
        "CS-1220": {
            id: "CS-1220",
            year: 2022,
            term: "Spring",
        },
        "BTGE-1725": {
            id: "BTGE-1725",
            year: 2022,
            term: "Spring",
        },
        "PHYS-2110": {
            id: "PHYS-2110",
            year: 2022,
            term: "Spring",
        },
        "CS-2210": {
            id: "CS-2210",
            year: 2022,
            term: "Fall",
        },
        "PHYS-2120": {
            id: "PHYS-2120",
            year: 2022,
            term: "Fall",
        },
        "CS-3310": {
            id: "CS-3310",
            year: 2023,
            term: "Spring",
        },
        "CS-3350": {
            id: "CS-3350",
            year: 2023,
            term: "Spring",
        },
        "MATH-2520": {
            id: "MATH-2520",
            year: 2023,
            term: "Spring",
        },
        "EGCP-3210": {
            id: "EGCP-3210",
            year: 2023,
            term: "Spring",
        },
        "CS-3410": {
            id: "CS-3410",
            year: 2023,
            term: "Fall",
        },
        "EGCP-4310": {
            id: "EGCP-4310",
            year: 2023,
            term: "Fall",
        },
        "CS-3610": {
            id: "CS-3610",
            year: 2024,
            term: "Spring",
        },
        "CS-3220": {
            id: "CS-3220",
            year: 2024,
            term: "Spring",
        },
        "CS-4810": {
            id: "CS-4810",
            year: 2024,
            term: "Fall",
        },
        "EGGN-4010": {
            id: "EGGN-4010",
            year: 2024,
            term: "Fall",
        },
        "EGGN-3110": {
            id: "EGGN-3110",
            year: 2025,
            term: "Spring",
        },
        "CS-3510": {
            id: "CS-3510",
            year: 2025,
            term: "Spring",
        },
    },
    catYear: 2021,
};
let catalog = {
    year: 2021,
    courses: {
        "CS-1210": {
            id: "CS-1210",
            name: "C++ Programming",
            description: "Feeble effort to teach programming",
            credits: 2,
        },
        "CS-1220": {
            id: "CS-1220",
            name: "Object-Oriented DesignD",
            description: "Why do we still teach C++",
            credits: 3,
        },
        "CS-2210": {
            id: "CS-2210",
            name: "Data Structures Using Java",
            description: "What an awesome prof!!!!",
            credits: 3,
        },
        "CS-3210": {
            id: "CS-3210",
            name: "Programming Language Survey",
            description: "Three cheers for Prof Dude",
            credits: 3,
        },
        "CS-3220": {
            id: "CS-3220",
            name: "Web Applicationss",
            description: "Who won the Medal of Honor at Gettysburg",
            credits: 3,
        },
        "CS-3310": {
            id: "CS-3310",
            name: "Operating Systems",
            description: "Forget Windows; Let's do Linux!",
            credits: 3,
        },
        "CS-3350": {
            id: "CS-3350",
            name: "Foundations of Computer Security",
            description: "Authentication",
            credits: 3,
        },
        "CS-3410": {
            id: "CS-3410",
            name: "Algorithms",
            description: "The heart of Computer Science",
            credits: 3,
        },
        "CS-3510": {
            id: "CS-3510",
            name: "Compiler Theory & Practice",
            description: "The BEST!  Way Cool!",
            credits: 3,
        },
        "CS-3610": {
            id: "CS-3610",
            name: "Database Organization & Design",
            description: "What's a left join?",
            credits: 3,
        },
        "CS-4410": {
            id: "CS-4410",
            name: "Parallel Computing",
            description: "Impossible",
            credits: 3,
        },
        "CS-4430": {
            id: "CS-4430",
            name: "Machine Learning for Intelligent Agents",
            description: "AI",
            credits: 3,
        },
        "CS-4710": {
            id: "CS-4710",
            name: "Computer Graphics",
            description: "Just games",
            credits: 3,
        },
        "CS-4730": {
            id: "CS-4730",
            name: "Virtual Reality Applications",
            description: "Is it real?",
            credits: 3,
        },
        "CS-4810": {
            id: "CS-4810",
            name: "Software Engineering I",
            description: "Love Senior Design!",
            credits: 3,
        },
        "CS-4820": {
            id: "CS-4820",
            name: "Software Engineering II",
            description: "To infinity and beyond",
            credits: 4,
        },
        "CY-1000": {
            id: "CY-1000",
            name: "Introduction to Cybersecurity",
            description: "Attack!",
            credits: 3,
        },
        "CY-2310": {
            id: "CY-2310",
            name: "Cyber Forensics",
            description: "Investigate!",
            credits: 3,
        },
        "CY-3320": {
            id: "CY-3320",
            name: "Linux System Programming",
            description: "Control!",
            credits: 3,
        },
        "CY-3420": {
            id: "CY-3420",
            name: "Cyber Defense",
            description: "Defend!",
            credits: 3,
        },
        "CY-4310": {
            id: "CY-4310",
            name: "Cyber Operations",
            description: "Adversarial Mindset",
            credits: 3,
        },
        "CY-4330": {
            id: "CY-4330",
            name: "Software Security",
            description: "buffer overflow",
            credits: 3,
        },
        "CY-4810": {
            id: "CY-4810",
            name: "Secure Software Engineering I",
            description: "Love Senior Design!",
            credits: 3,
        },
        "CY-4820": {
            id: "CY-4820",
            name: "Secure Software Engineering II",
            description: "To infinity and beyond",
            credits: 4,
        },
        "EGCP-1010": {
            id: "EGCP-1010",
            name: "Digital Logic Design",
            description: "Cool course with AND, OR, and NOT",
            credits: 3,
        },
        "EGCP-3010": {
            id: "EGCP-3010",
            name: "Advanced Digital Logic Design",
            description: "I AM ROBOT",
            credits: 3,
        },
        "EGCP-2120": {
            id: "EGCP-2120",
            name: "Microcontrollers",
            description: "They are really tiny",
            credits: 3,
        },
        "EGCP-3210": {
            id: "EGCP-3210",
            name: "Computer Architecture",
            description: "Build the Pipeline!",
            credits: 3,
        },
        "EGCP-4210": {
            id: "EGCP-4210",
            name: "Advanced Computer Architecture",
            description: "We love Tomasulo",
            credits: 3,
        },
        "EGCP-4310": {
            id: "EGCP-4310",
            name: "Computer Networks",
            description: "Networking is very importing for finding a job",
            credits: 3,
        },
        "EGGN-3110": {
            id: "EGGN-3110",
            name: "Professional Ethics",
            description: "Politicians need to take this course!",
            credits: 3,
        },
        "EGGN-4010": {
            id: "EGGN-4010",
            name: "Senior Seminar",
            description: "Wrong Major!",
            credits: 0,
        },
        "MATH-1710": {
            id: "MATH-1710",
            name: "Calculus I",
            description: "A weedout course",
            credits: 5,
        },
        "MATH-1720": {
            id: "MATH-1720",
            name: "Calculus II",
            description: "For the few who passed Calc I",
            credits: 5,
        },
        "MATH-2520": {
            id: "MATH-2520",
            name: "Discrete Math w/ Prob",
            description: "We should always be discrete",
            credits: 3,
        },
        "MATH-3500": {
            id: "MATH-3500",
            name: "Number Theory",
            description: "Why?",
            credits: 3,
        },
        "MATH-3610": {
            id: "MATH-3610",
            name: "Linear Algebra",
            description: "As opposed to non-linear algegra?",
            credits: 3,
        },
        "MATH-3760": {
            id: "MATH-3760",
            name: "Numerical Analysis",
            description: "Painful!",
            credits: 3,
        },
        "PHYS-2110": {
            id: "PHYS-2110",
            name: "General Physics I",
            description: "Distance, Velocity, Acceleration",
            credits: 4,
        },
        "PHYS-2120": {
            id: "PHYS-2120",
            name: "General Physics II",
            description: "Why do we take this again?",
            credits: 4,
        },
        "BTGE-1725": {
            id: "BTGE-1725",
            name: "The Bible and the Gospel",
            description: "Truly why we are here",
            credits: 3,
        },
        "BTGE-2730": {
            id: "BTGE-2730",
            name: "Old Testament Literature",
            description: "In the Beginning!",
            credits: 3,
        },
        "BTGE-2740": {
            id: "BTGE-2740",
            name: "New Testament Literature",
            description: "In the Beginning Part 2!",
            credits: 3,
        },
        "BTGE-3755": {
            id: "BTGE-3755",
            name: "Theology I",
            description: "Let's learn about God",
            credits: 3,
        },
        "BTGE-3765": {
            id: "BTGE-3765",
            name: "Theology II",
            description: "Let's learn more about God",
            credits: 3,
        },
        "CHEM-1050": {
            id: "CHEM-1050",
            name: "Chemistry for Engineers",
            description: "The lab is fun!",
            credits: 3.5,
        },
    },
};
let requirements = {
    categories: {
        Core: {
            courses: [
                "CS-1210",
                "CS-1220",
                "CS-2210",
                "CS-3210",
                "CS-3220",
                "CS-3310",
                "CS-3410",
                "CS-3510",
                "CS-3610",
                "CS-4810",
                "CS-4820",
                "CY-1000",
                "CY-3420",
                "EGCP-1010",
                "EGCP-3210",
                "EGCP-4310",
                "EGGN-3110",
                "EGGN-4010",
                "MATH-2520",
            ],
        },
        Electives: {
            courses: [
                "CY-3320",
                "CY-4310",
                "CY-4330",
                "CS-4430",
                "CS-4710",
                "CS-4730",
                "EGCP-3010",
                "EGCP-4210",
                "MATH-3610",
            ],
        },
        Cognates: {
            courses: [
                "CHEM-1050",
                "MATH-1710",
                "MATH-1720",
                "PHYS-2110",
                "PHYS-2120",
            ],
        },
        GenEds: {
            courses: [
                "BTGE-1725",
                "BTGE-2730",
                "BTGE-2740",
                "BTGE-3755",
                "BTGE-3765",
            ],
        },
    },
};
let planNames = ["Cyber", "Physics", "ITM"];
let planIDs = [0, 1, 2];

window.onload = function () {
    renderPlan();
    renderCourseFinder();
    renderReqs();
    renderManagePlans();
};

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

            let semester = `${term.toLowerCase()}${trueYear}`;
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

function renderReqs() {
    let $acc = $("#accordion");
    let sections = Object.keys(requirements.categories);

    sections.forEach((s) => {
        let section = requirements.categories[s];
        let $header = $(`<h3>${s}</h3>`);
        let $div = $("<div></div>");
        let courses = section.courses;
        courses.forEach((course) => {
            $div.append(
                $(
                    `<p draggable="true" ondragstart="onDragStart(event)"><span class="tag">${course}</span> ${getName(
                        course
                    )}</p>`
                )
            );
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