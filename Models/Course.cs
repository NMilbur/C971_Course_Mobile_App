using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Milburn_Courses
{
    [Table("Course")]
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int TermID { get; set; }
        public string ClassName { get; set; }
        public string Status { get; set; }
        public DateTime ClassStartDate { get; set; }
        public DateTime ClassEndDate { get; set; }
        public string InstructorName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public string ObjectiveAssessmentName { get; set; }
        public DateTime OAStartDate { get; set; }
        public DateTime OAEndDate { get; set; }
        public string PerformanceAssessmentName { get; set; }
        public DateTime PAStartDate { get; set; }
        public DateTime PAEndDate { get; set; }
        public bool NotificationsEnabled { get; set; }
    }
}
