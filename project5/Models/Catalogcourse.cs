using System.ComponentModel.DataAnnotations;

namespace project5.Models
{
    public class Catalogcourse
    {
        [Key]
        public int year { get; set; }
        public string courseID { get; set; }
        
    }
}
