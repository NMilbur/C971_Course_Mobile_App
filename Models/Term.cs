using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Milburn_Courses
{
    [Table("Term")]
    public class Term
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string TermName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
