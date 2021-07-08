using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Milburn_Courses.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditCourse : ContentPage
    {
        public AddEditCourse()
        {
            InitializeComponent();
        }

        public AddEditCourse(ViewModels.CourseViewModel cvm)
        {
            InitializeComponent();
            initCourseEdit(cvm);
        }

        public void initCourseEdit(ViewModels.CourseViewModel cvm)
        {
            course_id.Text = cvm.ID.ToString();
            class_name.Text = cvm.ClassName;
            status.SelectedItem = cvm.Status;
            class_start_date.Date = cvm.ClassStartDate;
            class_end_date.Date = cvm.ClassEndDate;
            instructor_name.Text = cvm.InstructorName;
            phone.Text = cvm.Phone;
            email.Text = cvm.Email;
            notes.Text = cvm.Notes;
            objective_assessment_name.Text = cvm.ObjectiveAssessmentName;
            oa_start_date.Date = cvm.OAStartDate;
            oa_end_date.Date = cvm.OAEndDate;
            performance_assessment_name.Text = cvm.PerformanceAssessmentName;
            pa_start_date.Date = cvm.PAStartDate;
            pa_end_date.Date = cvm.PAEndDate;
            notification.IsChecked = cvm.NotificationsEnabled;
        }

        async void save_btn_Clicked(object sender, EventArgs e)
        {
            bool isValid = true;

            if (String.IsNullOrWhiteSpace(class_name.Text))
            {
                isValid = false;
                await DisplayAlert("Error", "Please enter a name for the course.", "Ok");
            
            } else if (String.IsNullOrWhiteSpace(instructor_name.Text))
            {
                isValid = false;
                await DisplayAlert("Error", "Please enter a name for the instructor.", "Ok");
            
            } else if (String.IsNullOrWhiteSpace(phone.Text))
            {
                isValid = false;
                await DisplayAlert("Error", "Please enter a phone number for the instructor.", "Ok");
            
            } else if (String.IsNullOrWhiteSpace(email.Text))
            {
                isValid = false;
                await DisplayAlert("Error", "Please enter an email for the instructor.", "Ok");

            } else if (class_start_date.Date >= class_end_date.Date)
            {
                isValid = false;
                await DisplayAlert("Error", "The class start date must be before the end date.", "Ok");
            } else if ((!String.IsNullOrWhiteSpace(objective_assessment_name.Text) && (oa_start_date.Date >= oa_end_date.Date)) ||
                (!String.IsNullOrWhiteSpace(performance_assessment_name.Text) && (pa_start_date.Date >= pa_end_date.Date)))
            {
                isValid = false;
                await DisplayAlert("Error", "All assessment start dates must be before the end dates.", "Ok");
            
            } else if (!isValidEmail(email.Text))
            {
                isValid = false;
                await DisplayAlert("Error", "The email address entered is improperly formatted.", "Ok");
            }

            if (isValid) { 
                await App.DBH.SaveCourse(new Course
                {
                    ID = (!String.IsNullOrWhiteSpace(course_id.Text)) ? Int32.Parse(course_id.Text) : 0,
                    TermID = TermViewModel.CurrentTerm.ID,
                    ClassName = class_name.Text,
                    Status = status.SelectedItem.ToString(),
                    ClassStartDate = class_start_date.Date,
                    ClassEndDate = class_end_date.Date,
                    InstructorName = instructor_name.Text,
                    Phone = phone.Text,
                    Email = email.Text,
                    Notes = notes.Text,
                    ObjectiveAssessmentName = objective_assessment_name.Text,
                    OAStartDate = oa_start_date.Date,
                    OAEndDate = oa_end_date.Date,
                    PerformanceAssessmentName = performance_assessment_name.Text,
                    PAStartDate = pa_start_date.Date,
                    PAEndDate = pa_end_date.Date,
                    NotificationsEnabled = notification.IsChecked
                });

                if (!String.IsNullOrWhiteSpace(course_id.Text))
                {
                    ViewModels.CourseViewModel.CurrentCourse = new ViewModels.CourseViewModel(await App.DBH.GetCourse(Int32.Parse(course_id.Text)));
                }

                await Navigation.PopModalAsync();
                
            } 
        }

        private bool isValidEmail(string email)
        {
            try
            {
                MailAddress compEmail = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        async void cancel_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void clear_oa_Clicked(object sender, EventArgs e)
        {
            objective_assessment_name.Text = null;
            oa_start_date.Date = DateTime.Today;
            oa_end_date.Date = DateTime.Today;
        }

        private void clear_pa_Clicked(object sender, EventArgs e)
        {
            performance_assessment_name.Text = null;
            pa_start_date.Date = DateTime.Today;
            pa_end_date.Date = DateTime.Today;
        }
    }
}