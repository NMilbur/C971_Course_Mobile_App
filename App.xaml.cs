using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Milburn_Courses
{
    public partial class App : Application
    {
        static DBHandler dbh;

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
            //MainPage = new AddEditClass();
        }

        public static DBHandler DBH
        {
            get
            {
                if (dbh == null)
                {
                    
                    string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

                    dbh = new DBHandler(System.IO.Path.Combine(path, "ClassManagerDB.db3"));
                }

                return dbh;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
