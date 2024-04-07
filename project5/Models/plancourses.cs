using System.ComponentModel.DataAnnotations;

namespace project5.Models;

public class plancourses
{
    [Key]
    public int planID { get; set; }

    public string courseID { get; set; }

    public int yearTaken { get; set; }

    public string termTaken { get; set; }

}