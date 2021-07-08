using System;
using System.Collections.Generic;
using System.Text;

namespace Milburn_Courses.ViewModels
{
    public class CourseViewModel
    {
        private Course _course;

        public CourseViewModel(Course course)
        {
            this._course = course;
        }

        public int ID { get { return _course.ID;  } }
        public int TermID { get { return _course.TermID; } }
        public string ClassName { get { return _course.ClassName; } }
        public string Status { get { return _course.Status; } }
        public DateTime ClassStartDate { get { return _course.ClassStartDate; } }
        public DateTime ClassEndDate { get { return _course.ClassEndDate; } }
        public string ClassDateRange { get { return $"{ClassStartDate.ToString("MM/dd/yyyy")} - {ClassEndDate.ToString("MM/dd/yyyy")}"; } }

        public string InstructorName { get { return _course.InstructorName; } }
        public string Phone { get { return _course.Phone; } }
        public string Email { get { return _course.Email; } }
        public string Notes { get { return _course.Notes; } }
        public string ObjectiveAssessmentName { get { return _course.ObjectiveAssessmentName; } }
        public DateTime OAStartDate { get { return _course.OAStartDate; } }
        public DateTime OAEndDate { get { return _course.OAEndDate; } }
        public string OADateRange { get { return $"{OAStartDate.ToString("MM/dd/yyyy")} - {OAEndDate.ToString("MM/dd/yyyy")}"; } }

        public string PerformanceAssessmentName { get { return _course.PerformanceAssessmentName; } }
        public DateTime PAStartDate { get { return _course.PAStartDate; } }
        public DateTime PAEndDate { get { return _course.PAEndDate; } }
        public string PADateRange { get { return $"{PAStartDate.ToString("MM/dd/yyyy")} - {PAEndDate.ToString("MM/dd/yyyy")}"; } }
        public bool NotificationsEnabled { get { return _course.NotificationsEnabled; } }

        public static CourseViewModel CurrentCourse { get; set; }
    }
}
