using System.ComponentModel.DataAnnotations;

namespace project5.Models;

public class majorplan
{
    [Key]
    public int userPlanID { get; set; }

    public int planID { get; set; }

    public int majorID { get; set; }

}