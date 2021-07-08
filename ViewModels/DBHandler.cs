using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Milburn_Courses
{
    public class DBHandler
    {
        readonly SQLiteAsyncConnection connection;

        public DBHandler(string path)
        {
            connection = new SQLiteAsyncConnection(path);
            connection.CreateTablesAsync<Term, Course>().Wait();
        }

        public Task<List<Term>> GetAllTerms()
        {
            return connection.Table<Term>()
                .OrderBy(term => term.StartDate)
                .ToListAsync();
        }

        public Task<Term> GetTerm(int termId)
        {
            return connection.Table<Term>()
                .Where(term => term.ID == termId)
                .FirstAsync();
        }

        public Task<int> GetTermCount()
        {
            return connection.Table<Term>()
                .CountAsync();
        }

        public Task SaveTerm(Term term)
        {
            return (term.ID != 0) ? connection.UpdateAsync(term) : connection.InsertAsync(term);
        }

        public Task DeleteTerm(int termID)
        {
            connection.Table<Course>()
                .Where(c => c.TermID == termID)
                .DeleteAsync();

            return connection.Table<Term>()
                .Where(t => t.ID == termID)
                .DeleteAsync();  
        }

        public Task<List<Course>> GetTermCourses(int termId)
        {
            return connection.Table<Course>()
                .Where(c => c.TermID == termId)
                .OrderBy(c => c.ClassStartDate)
                .ToListAsync();
        }

        public Task<List<Course>> GetAllCoursesWithNotifications()
        {
            return connection.Table<Course>()
                .Where(c => c.NotificationsEnabled == true)
                .ToListAsync();
        }

        public Task<Course> GetCourse(int classId)
        {
            return connection.Table<Course>()
                .Where(course => course.ID == classId)
                .FirstAsync();
        }

        public Task SaveCourse(Course course)
        {
            return (course.ID != 0) ? connection.UpdateAsync(course) : connection.InsertAsync(course);
        }

        public Task DeleteCourse(int courseID)
        {
            return connection.Table<Course>()
                .Where(c => c.ID == courseID)
                .DeleteAsync();
        }
    }
}
