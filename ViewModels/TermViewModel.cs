using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Milburn_Courses
{
    public class TermViewModel
    {
        private Term _term;

        public TermViewModel(Term term)
        {
            this._term = term;
        }

        public string TermName { get {  return _term.TermName; } }
        public DateTime StartDate { get {  return _term.StartDate; } }
        public DateTime EndDate { get { return _term.EndDate; } }
        public int ID { get {  return _term.ID; } }
        public string DateRange { get {  return $"{StartDate.ToString("MM/dd/yyyy")} - {EndDate.ToString("MM/dd/yyyy")}"; } }

        public Term Term
        {
            get => _term;
        }

        public static TermViewModel CurrentTerm { get; set; }
    }
}
