using System.ComponentModel.DataAnnotations;

namespace project5.Models
{
    public class courses
    {
        [Key]
        public string courseID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public float credits { get; set; }
    }
}
