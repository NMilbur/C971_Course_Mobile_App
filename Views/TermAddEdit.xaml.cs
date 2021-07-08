using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Milburn_Courses
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermAddEdit : ContentPage
    {
        public TermAddEdit()
        {
            InitializeComponent();
        }

        public TermAddEdit(TermViewModel tvm)
        {
            InitializeComponent();
            initEditTermForm(tvm);
        }

        public void initEditTermForm(TermViewModel tvm)
        {
            term_id.Text = tvm.ID.ToString();
            term_name.Text = tvm.TermName;
            start_date.Date = tvm.StartDate;
            end_date.Date = tvm.EndDate;
        }

        async void save_btn_Clicked(object sender, EventArgs e)
        {
            bool isValid = true;

            if (String.IsNullOrWhiteSpace(term_name.Text))
            {
                isValid = false;
                await DisplayAlert("Error", "Please enter a term name.", "Ok");
            }

            if (start_date.Date >= end_date.Date)
            {
                isValid = false;
                await DisplayAlert("Error", "The start date must be before end date.", "Ok");
            }

            if (isValid)
            {
                await App.DBH.SaveTerm(new Term
                {
                    ID = (!String.IsNullOrWhiteSpace(term_id.Text)) ? Int32.Parse(term_id.Text) : 0,
                    TermName = term_name.Text,
                    StartDate = start_date.Date,
                    EndDate = end_date.Date
                });

                if (!String.IsNullOrWhiteSpace(term_id.Text))
                {
                    TermViewModel.CurrentTerm = new TermViewModel(await App.DBH.GetTerm(Int32.Parse(term_id.Text)));
                }

                await Navigation.PopModalAsync();
            }
        }

        async void cancel_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}