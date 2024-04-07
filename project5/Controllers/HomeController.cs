using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using project5.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using project5.Data;
using System.Security.Claims;

namespace project5.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly project5Context _context;

        public HomeController(ILogger<HomeController> logger, project5Context context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            var allUsers = _context.Users.Select(u => new
            {
                UserName = u.UserName,
                Id = u.Id
            }).ToList();

            return View(allUsers);
        }


        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Faculty")]
        public IActionResult Faculty()
        {
            var students = from user in _context.Users
                           join userRole in _context.UserRoles on user.Id equals userRole.UserId
                           join role in _context.Roles on userRole.RoleId equals role.Id
                           where role.Name == "Student"
                           select new
                           {
                               UserName = user.UserName,
                               Id = user.Id
                           };

            return View(students.ToList());
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> GetCourseFinder()
        {
            // Assuming _context is your database context
            // First, get the courseIDs for the given year

            int year = 2023; // Example value, replace with your actual year

            var courseIDs = await _context.Catalogcourse
                                .Where(cc => cc.year == year)
                                .Select(cc => cc.courseID)
                                .ToListAsync();

            // Now, fetch the corresponding courses details
            var courseDetails = await _context.courses
                                    .Where(c => courseIDs.Contains(c.courseID))
                                    .Select(c => new
                                    {
                                        c.courseID,
                                        c.name,
                                        c.description,
                                        c.credits
                                    })
                                    .ToListAsync();

            // Reshape the data into the desired format
            var catalog = new
            {
                year = year,
                courses = courseDetails.ToDictionary(
                    c => c.courseID,
                    c => new
                    {
                        id = c.courseID,
                        name = c.name,
                        description = c.description,
                        credits = c.credits
                    })
            };

            // Return the result as JSON
            return Json(catalog);
        }

        public async Task<IActionResult> GetRequirements()

        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string currentUserID = userId; //"bfd1569a-59fa-4d69-8b05-26d38408be87"; // Replace with the actual user ID
            int currentMajorID = 1; // Replace with the actual major ID

            var requirementsQuery = from r in _context.Requirements
                                    join mp in _context.majorplan on r.majorID equals mp.majorID
                                    join p in _context.Plan on mp.planID equals p.planID
                                    join u in _context.Users on p.UserID equals u.Id
                                    where u.Id == currentUserID && mp.majorID == currentMajorID
                                    select new
                                    {
                                        r.courseID,
                                        r.category
                                    };

            var requirementsList = await requirementsQuery.ToListAsync();

            // Adjusted structure to match the desired JSON format
            var requirements = new Dictionary<string, object>()
    {
        { "categories", new Dictionary<string, object>()
            {
                { "Core", new { courses = new List<string>() } },
                { "Electives", new { courses = new List<string>() } },
                { "Cognates", new { courses = new List<string>() } },
                { "Gen Eds", new { courses = new List<string>() } }
            }
        }
    };

            foreach (var requirement in requirementsList)
            {
                var categoryDict = requirements["categories"] as Dictionary<string, object>;
                if (categoryDict.TryGetValue(requirement.category, out var categoryObj))
                {
                    var category = categoryObj as dynamic;
                    category.courses.Add(requirement.courseID);
                }
            }

            if (requirementsList.Count == 0)
            {
                return Json(new { message = "No results found" });
            }
            else
            {
                return Json(requirements);
            }
        }


        public async Task<IActionResult> GetPlan()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string currentUserID = userId; // "bfd1569a-59fa-4d69-8b05-26d38408be87"; // Replace with the actual user ID
            int currentPlanID = 1; // Replace with the actual plan ID

            // Get student name
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == currentUserID);
            string studentName = user != null ? user.UserName : string.Empty;

            // Get plan name and major ID
            var plan = await _context.Plan
                .Where(p => p.planID == currentPlanID)
                .Select(p => new { p.name, p.planID })
                .FirstOrDefaultAsync();
            string planName = plan?.name ?? string.Empty;
            int planID = plan?.planID ?? 0;

            var majorplan = await _context.majorplan
                .Where(p => p.planID == planID)
                .Select(p => new { p.majorID })
                .FirstOrDefaultAsync();
            int majorID = majorplan?.majorID ?? 0;

            // Get major name
            var major = await _context.Major.FirstOrDefaultAsync(m => m.majorID == majorID);
            string majorName = major != null ? major.major : string.Empty;

            // Get current year and term
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;
            string currentTerm = GetCurrentTerm(currentMonth);

            // Get courses data
            var courses = await _context.plancourses
                .Where(pc => pc.planID == currentPlanID)
                .Select(pc => new { pc.courseID, pc.yearTaken, pc.termTaken })
                .ToListAsync();

            var coursesData = new Dictionary<string, object>();
            foreach (var course in courses)
            {
                coursesData[course.courseID] = new
                {
                    id = course.courseID,
                    year = course.yearTaken,
                    term = course.termTaken
                };
            }

            var planData = new
            {
                student = studentName,
                name = planName,
                major = majorName,
                currYear = currentYear,
                currTerm = currentTerm,
                courses = coursesData,
                catYear = 2021,
            };

            return Json(planData);
        }

        public string GetCurrentTerm(int month)
        {
            if (month >= 1 && month <= 5)
            {
                return "Spring";
            }
            else if (month == 6 || month == 7)
            {
                return "Summer";
            }
            else
            {
                return "Fall";
            }
        }

        public async Task<IActionResult> GetData()
        {
            int year = 2023; // Example value, replace with your actual year

            var courseIDs = await _context.Catalogcourse
                                .Where(cc => cc.year == year)
                                .Select(cc => cc.courseID)
                                .ToListAsync();

            // Now, fetch the corresponding courses details
            var courseDetails = await _context.courses
                                    .Where(c => courseIDs.Contains(c.courseID))
                                    .Select(c => new
                                    {
                                        c.courseID,
                                        c.name,
                                        c.description,
                                        c.credits
                                    })
                                    .ToListAsync();

            // Reshape the data into the desired format
            var catalog = new
            {
                year = year,
                courses = courseDetails.ToDictionary(
                    c => c.courseID,
                    c => new
                    {
                        id = c.courseID,
                        name = c.name,
                        description = c.description,
                        credits = c.credits
                    })
            };

            // Return the result as JSON
            //  return Json(catalog);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string currentUserID = userId; // "bfd1569a-59fa-4d69-8b05-26d38408be87"; // Replace with the actual user ID
            int currentMajorID = 1; // Replace with the actual major ID

            var requirementsQuery = from r in _context.Requirements
                                    join mp in _context.majorplan on r.majorID equals mp.majorID
                                    join p in _context.Plan on mp.planID equals p.planID
                                    join u in _context.Users on p.UserID equals u.Id
                                    where u.Id == currentUserID && mp.majorID == currentMajorID
                                    select new
                                    {
                                        r.courseID,
                                        r.category
                                    };

            var requirementsList = await requirementsQuery.ToListAsync();

            // Assuming courseID is a string and requirements is correctly set up for string IDs
            var requirements = new Dictionary<string, Dictionary<string, List<string>>>()
{
    {"categories", new Dictionary<string, List<string>>()
        {
            {"Core", new List<string>()},
            {"Electives", new List<string>()},
            {"Cognates", new List<string>()},
            {"GenEds", new List<string>()}
        }
    }
};

            foreach (var requirement in requirementsList)
            {
                switch (requirement.category)
                {
                    case "Core":
                        requirements["categories"]["Core"].Add(requirement.courseID);
                        break;
                    case "Electives":
                        requirements["categories"]["Electives"].Add(requirement.courseID);
                        break;
                    case "Cognates":
                        requirements["categories"]["Cognates"].Add(requirement.courseID);
                        break;
                    case "Gen Eds":
                        requirements["categories"]["GenEds"].Add(requirement.courseID);
                        break;
                }
            }


            //return Json(requirements);

            //string currentUserID = "bfd1569a-59fa-4d69-8b05-26d38408be87"; // Replace with the actual user ID
            int currentPlanID = 1; // Replace with the actual plan ID

            // Get student name
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == currentUserID);
            string studentName = user != null ? user.UserName : string.Empty;

            // Get plan name and major ID
            var plan = await _context.Plan
                .Where(p => p.planID == currentPlanID)
                .Select(p => new { p.name, p.planID })
                .FirstOrDefaultAsync();
            string planName = plan?.name ?? string.Empty;
            int planID = plan?.planID ?? 0;

            var majorplan = await _context.majorplan
                .Where(p => p.planID == planID)
                .Select(p => new { p.majorID })
                .FirstOrDefaultAsync();
            int majorID = majorplan?.majorID ?? 0;

            // Get major name
            var major = await _context.Major.FirstOrDefaultAsync(m => m.majorID == majorID);
            string majorName = major != null ? major.major : string.Empty;

            // Get current year and term
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;
            string currentTerm = GetCurrentTerm(currentMonth);

            // Get courses data
            var courses = await _context.plancourses
                .Where(pc => pc.planID == currentPlanID)
                .Select(pc => new { pc.courseID, pc.yearTaken, pc.termTaken })
                .ToListAsync();

            var coursesData = new Dictionary<string, object>();
            foreach (var course in courses)
            {
                coursesData[course.courseID] = new
                {
                    id = course.courseID,
                    year = course.yearTaken,
                    term = course.termTaken
                };
            }

            var planData = new
            {
                student = studentName,
                name = planName,
                major = majorName,
                currYear = currentYear,
                currTerm = currentTerm,
                courses = coursesData,
                catYear = 2021,
            };



            //return Json(planData);

            return Json(new { catalog, requirements, planData });



        }

    }
}
