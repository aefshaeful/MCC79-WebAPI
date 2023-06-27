using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Education
{
    public class UpdateEducationDto
    {
        [Required]
        public Guid Guid { get; set; }
        [Required]
        public string Major { get; set; }
        [Required]
        public string Degree { get; set; }
        [Range(0, 4, ErrorMessage = "GPA is only from 0 to 4")]
        public double Gpa { get; set; }
        [Required]
        public Guid UniversityGuid { get; set; }
    }
}
