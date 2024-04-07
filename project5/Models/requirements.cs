using System.ComponentModel.DataAnnotations;

namespace project5.Models;

public class Requirements
{
    [Key]
    public int majorID { get; set; }

    public string courseID { get; set; }

    public string category { get; set; }

}
