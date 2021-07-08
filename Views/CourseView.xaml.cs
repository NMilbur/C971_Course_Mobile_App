using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Milburn_Courses.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseView : ContentPage
    {
        public CourseView(ViewModels.CourseViewModel cvm)
        {
            InitializeComponent();
            initCourseView(cvm);
        }

        private void initCourseView(ViewModels.CourseViewModel cvm)
        {
            if (cvm == null)
            {
                cvm = ViewModels.CourseViewModel.CurrentCourse;
            }

            course_name.Text = cvm.ClassName;
            course_date_range.Text = cvm.ClassDateRange;
            instructor_name.Text = cvm.InstructorName;
            phone.Text = cvm.Phone;
            email.Text = cvm.Email;
            notes.Text = cvm.Notes;

            oa_section.IsVisible = (!String.IsNullOrWhiteSpace(cvm.ObjectiveAssessmentName)) ? true : false;
            oa_name.Text = cvm.ObjectiveAssessmentName;
            oa_date_range.Text = cvm.OADateRange;

            pa_section.IsVisible = (!String.IsNullOrWhiteSpace(cvm.PerformanceAssessmentName)) ? true : false;
            pa_name.Text = cvm.PerformanceAssessmentName;
            pa_date_range.Text = cvm.PADateRange;

            assessment_title.IsVisible = (oa_section.IsVisible || pa_section.IsVisible) ? true : false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            initCourseView(ViewModels.CourseViewModel.CurrentCourse);
        }

        async void cancel_btn_Clicked(object sender, EventArgs e)
        {
            ViewModels.CourseViewModel.CurrentCourse = null;
            await Navigation.PopModalAsync();
        }

        async void edit_course_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddEditCourse(ViewModels.CourseViewModel.CurrentCourse)));
        }

        async void delete_course_btn_Clicked(object sender, EventArgs e)
        {
            await App.DBH.DeleteCourse(ViewModels.CourseViewModel.CurrentCourse.ID);
            ViewModels.CourseViewModel.CurrentCourse = null;
            await Navigation.PopModalAsync();
        }

        async void share_link_Clicked(object sender, EventArgs e)
        {
            await Share.RequestAsync(new ShareTextRequest { 
                Text = notes.Text,
                Title = $"{course_name.Text} Notes"
            });

            await Navigation.PopModalAsync();
        }
    }
}