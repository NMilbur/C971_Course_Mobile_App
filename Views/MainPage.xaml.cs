using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.LocalNotifications;


namespace Milburn_Courses
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            initDummyData();
        }

        private async void initDummyData()
        {
            if (await App.DBH.GetTermCount() == 0)
            {
                await App.DBH.SaveTerm(new Term()
                {
                    TermName = "Term 1",
                    StartDate = DateTime.Parse("2021-07-01"),
                    EndDate = DateTime.Parse("2021-10-31")
                });

                await App.DBH.SaveCourse(new Course()
                {
                    TermID = 1,
                    ClassName = "Mobile Development",
                    Status = "In Progress",
                    ClassStartDate = DateTime.Parse("2021-07-01"),
                    ClassEndDate = DateTime.Parse("2021-07-31"),
                    InstructorName = "Nicholas Milburn",
                    Phone = "619-723-7455",
                    Email = "nmilbu1@wgu.edu",
                    Notes = "This is a course on mobile development using Xamarin.",
                    ObjectiveAssessmentName = "OA Assessment",
                    OAStartDate = DateTime.Parse("2021-07-18"),
                    OAEndDate = DateTime.Parse("2021-07-24"),
                    PerformanceAssessmentName = "PA Assessment",
                    PAStartDate = DateTime.Parse("2021-07-25"),
                    PAEndDate = DateTime.Parse("2021-07-31"),
                    NotificationsEnabled = true
                });
            }
        }

        private async void initTermList()
        {
            List<Term> terms = await App.DBH.GetAllTerms();

            List<TermViewModel> termsViewList = new List<TermViewModel>();

            foreach (Term term in terms)
            {
                TermViewModel termView = new TermViewModel(term);

                termsViewList.Add(termView);
            }

            terms_list.ItemsSource = termsViewList;
        }

        private async void checkNotifications()
        {
            List<Course> courses = await App.DBH.GetAllCoursesWithNotifications();

            foreach (Course course in courses)
            {
                ViewModels.CourseViewModel cvm = new ViewModels.CourseViewModel(course);

                if (cvm.NotificationsEnabled)
                {
                    if (cvm.ClassStartDate == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("Reminder", $"{cvm.ClassName} starts today!", Int32.Parse($"1{cvm.ID}"));
                    }

                    if (cvm.ClassEndDate == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("Reminder", $"{cvm.ClassName} ends today!", Int32.Parse($"1{cvm.ID}"));
                    }

                    if (!String.IsNullOrWhiteSpace(cvm.ObjectiveAssessmentName))
                    {
                        if (cvm.OAStartDate == DateTime.Today)
                        {
                            CrossLocalNotifications.Current.Show("Reminder", $"{cvm.ObjectiveAssessmentName} starts today!", Int32.Parse($"2{cvm.ID}"));
                        }

                        if (cvm.OAEndDate == DateTime.Today)
                        {
                            CrossLocalNotifications.Current.Show("Reminder", $"{cvm.ObjectiveAssessmentName} is due today!", Int32.Parse($"2{cvm.ID}"));
                        }
                    }

                    if (!String.IsNullOrWhiteSpace(cvm.PerformanceAssessmentName))
                    {
                        if (cvm.PAStartDate == DateTime.Today)
                        {
                            CrossLocalNotifications.Current.Show("Reminder", $"{cvm.PerformanceAssessmentName} starts today!", Int32.Parse($"3{cvm.ID}"));
                        }

                        if (cvm.PAEndDate == DateTime.Today)
                        {
                            CrossLocalNotifications.Current.Show("Reminder", $"{cvm.PerformanceAssessmentName} is due today!", Int32.Parse($"3{cvm.ID}"));
                        }
                    }
                    
                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            TermViewModel.CurrentTerm = null;
            initTermList();
            checkNotifications();
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new TermAddEdit()));
        }

        async void terms_list_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (TermViewModel.CurrentTerm == null)
            {
                TermViewModel.CurrentTerm = (TermViewModel)e.Item;
            }

            await Navigation.PushModalAsync(new NavigationPage(new Views.TermView(TermViewModel.CurrentTerm)));
        }

    }
}
