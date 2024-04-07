/*
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace project5.Models
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=james.cedarville.edu;Database=cs3220_sp24;User Id=cs3220_sp24;Password=OqagokbAg9DzKZGb;";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                // Get current user ID from session (replace with actual session handling)
                int currentUserID = 1;

                // Query to get current plan ID
                string planIDQuery = "SELECT TOP 1 planID FROM HMS_Plan WHERE userID = @UserID ORDER BY planID ASC";
                SqlCommand planIDCommand = new SqlCommand(planIDQuery, connection);
                planIDCommand.Parameters.AddWithValue("@UserID", currentUserID);
                int currentPlanID = (int)planIDCommand.ExecuteScalar();

                // Query to get major ID
                string majorIDQuery = "SELECT TOP 1 majorID FROM HMS_MajorPlan WHERE planID = @PlanID";
                SqlCommand majorIDCommand = new SqlCommand(majorIDQuery, connection);
                majorIDCommand.Parameters.AddWithValue("@PlanID", currentPlanID);
                int currentMajorID = (int)majorIDCommand.ExecuteScalar();

                // Query to get requirements
                string requirementsQuery = "SELECT R.majorID, R.courseID, R.Category FROM HMS_Reqs R JOIN HMS_MajorPlan MP ON R.majorID = MP.majorID JOIN HMS_Plan P ON MP.planID = P.planID JOIN HMS_User U ON P.userID = U.userID WHERE U.userID = @UserID AND MP.majorID = @MajorID";
                SqlCommand requirementsCommand = new SqlCommand(requirementsQuery, connection);
                requirementsCommand.Parameters.AddWithValue("@UserID", currentUserID);
                requirementsCommand.Parameters.AddWithValue("@MajorID", currentMajorID);

                Dictionary<string, List<string>> requirements = new Dictionary<string, List<string>>()
                {
                    { "Core", new List<string>() },
                    { "Electives", new List<string>() },
                    { "Cognates", new List<string>() },
                    { "GenEds", new List<string>() }
                };

                using (SqlDataReader reader = requirementsCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string category = reader["Category"].ToString();
                        string courseID = reader["courseID"].ToString();

                        if (requirements.ContainsKey(category))
                        {
                            requirements[category].Add(courseID);
                        }
                    }
                }

                // Query to get student name
                string studentNameQuery = "SELECT full_name FROM HMS_User WHERE userID = @UserID";
                SqlCommand studentNameCommand = new SqlCommand(studentNameQuery, connection);
                studentNameCommand.Parameters.AddWithValue("@UserID", currentUserID);
                string studentName = studentNameCommand.ExecuteScalar().ToString();

                // Query to get plan name and major name
                string planDataQuery = "SELECT p.name AS planName, mp.majorID AS majorID FROM HMS_Plan AS p INNER JOIN HMS_MajorPlan AS mp ON p.planID = mp.planID WHERE p.planID = @PlanID";
                SqlCommand planDataCommand = new SqlCommand(planDataQuery, connection);
                planDataCommand.Parameters.AddWithValue("@PlanID", currentPlanID);
                SqlDataReader planDataReader = planDataCommand.ExecuteReader();

                string planName = "";
                string majorName = "";

                if (planDataReader.Read())
                {
                    planName = planDataReader["planName"].ToString();
                    int majorID = Convert.ToInt32(planDataReader["majorID"]);

                    // Query to get major name
                    string majorNameQuery = "SELECT Major FROM HMS_Major WHERE majorID = @MajorID";
                    SqlCommand majorNameCommand = new SqlCommand(majorNameQuery, connection);
                    majorNameCommand.Parameters.AddWithValue("@MajorID", majorID);
                    majorName = majorNameCommand.ExecuteScalar().ToString();
                }

                planDataReader.Close();

                // Get current year and term
                int currentYear = DateTime.Now.Year;
                string currentMonth = DateTime.Now.Month.ToString();
                string currentTerm = "";

                if (currentMonth == "1" || currentMonth == "2" || currentMonth == "3" || currentMonth == "4" || currentMonth == "5")
                {
                    currentTerm = "Spring";
                }
                else if (currentMonth == "6" || currentMonth == "7")
                {
                    currentTerm = "Summer";
                }
                else
                {
                    currentTerm = "Fall";
                }

                // Query to get courses
                string coursesQuery = "SELECT CourseID, yearTaken, termTaken FROM HMS_PlanCourses WHERE planID = @PlanID";
                SqlCommand coursesCommand = new SqlCommand(coursesQuery, connection);
                coursesCommand.Parameters.AddWithValue("@PlanID", currentPlanID);

                Dictionary<string, Dictionary<string, string>> courses = new Dictionary<string, Dictionary<string, string>>();

                using (SqlDataReader reader = coursesCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string courseID = reader["CourseID"].ToString();
                        string yearTaken = reader["yearTaken"].ToString();
                        string termTaken = reader["termTaken"].ToString();

                        courses.Add(courseID, new Dictionary<string, string>()
                        {
                            { "id", courseID },
                            { "year", yearTaken },
                            { "term", termTaken }
                        });
                    }
                }

                // Query to get catalog
                int catalogYear = 2021; // Assuming catalog year is fixed
                string catalogQuery = "SELECT HMS_CatalogCourse.courseID, HMS_Courses.name, HMS_Courses.description, HMS_Courses.credits FROM HMS_CatalogCourse JOIN HMS_Courses ON HMS_CatalogCourse.courseID = HMS_Courses.courseID WHERE HMS_CatalogCourse.year = @CatalogYear";
                SqlCommand catalogCommand = new SqlCommand(catalogQuery, connection);
                catalogCommand.Parameters.AddWithValue("@CatalogYear", catalogYear);

                Dictionary<string, Dictionary<string, object>> catalog = new Dictionary<string, Dictionary<string, object>>();

                using (SqlDataReader reader = catalogCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string courseID = reader["courseID"].ToString();
                        string name = reader["name"].ToString();
                        string description = reader["description"].ToString();
                        string credits = reader["credits"].ToString();

                        catalog.Add(courseID, new Dictionary<string, object>()
                        {
                            { "id", courseID },
                            { "name", name },
                            { "description", description },
                            { "credits", credits }
                        });
                    }
                }

                // Query to get plan names and IDs
                string planNamesQuery = "SELECT planID, name FROM HMS_Plan WHERE userID = @UserID";
                SqlCommand planNamesCommand = new SqlCommand(planNamesQuery, connection);
                planNamesCommand.Parameters.AddWithValue("@UserID", currentUserID);

                List<string> planNames = new List<string>();
                List<int> planIDs = new List<int>();

                using (SqlDataReader reader = planNamesCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int planID = Convert.ToInt32(reader["planID"]);
                        string name = reader["name"].ToString();

                        planNames.Add(name);
                        planIDs.Add(planID);
                    }
                }

                // Output JSON
                var jsonData = new
                {
                    backfromthedb = currentPlanID,
                    session = currentUserID,
                    requirements = requirements,
                    plan = new
                    {
                        student = studentName,
                        name = planName,
                        major = majorName,
                        currYear = currentYear,
                        currTerm = currentTerm,
                        courses = courses
                    },
                    catalog = new
                    {
                        year = catalogYear,
                        courses = catalog
                    },
                    planNames = planNames,
                    planIDs = planIDs
                };

                string json = JsonConvert.SerializeObject(jsonData, Formatting.Indented);
                Console.WriteLine(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
*/