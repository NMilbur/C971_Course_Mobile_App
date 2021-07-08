using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Milburn_Courses.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermView : ContentPage
    {
        public TermView(TermViewModel tvm)
        {
            InitializeComponent();
        }

        private void initTermData(TermViewModel tvm)
        {
            term_name.Text = null;
            date_range.Text = null;

            term_name.Text = tvm.TermName;
            date_range.Text = tvm.DateRange;
        }

        private async void initCourseList(TermViewModel tvm)
        {
            List<Course> courses = await App.DBH.GetTermCourses(tvm.ID);
            List<ViewModels.CourseViewModel> cvm = new List<ViewModels.CourseViewModel>();

            foreach (Course course in courses)
            {
                cvm.Add(new ViewModels.CourseViewModel(course));
            }

            course_list.ItemsSource = cvm;

            add_course_btn.IsEnabled = (cvm.Count() < 6) ? true : false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModels.CourseViewModel.CurrentCourse = null;
            initTermData(TermViewModel.CurrentTerm);
            initCourseList(TermViewModel.CurrentTerm);
        }

        async void course_list_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (ViewModels.CourseViewModel.CurrentCourse == null)
            {
                ViewModels.CourseViewModel.CurrentCourse = (ViewModels.CourseViewModel)e.Item;
            }

            await Navigation.PushModalAsync(new NavigationPage(new CourseView(ViewModels.CourseViewModel.CurrentCourse)));
        }

        async void edit_term_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new TermAddEdit(TermViewModel.CurrentTerm)));
        }

        async void delete_term_btn_Clicked(object sender, EventArgs e)
        {
            await App.DBH.DeleteTerm(TermViewModel.CurrentTerm.ID);
            TermViewModel.CurrentTerm = null;
            await Navigation.PopModalAsync();
        }

        async void cancel_btn_Clicked(object sender, EventArgs e)
        {
            TermViewModel.CurrentTerm = null;
            await Navigation.PopModalAsync();
        }

        async void add_course_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddEditCourse()));
        }
    }
}