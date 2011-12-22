﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using DCExternalSite.Web;
using System.ServiceModel.DomainServices.Client;
using DCExternalSite.DCServiceReference;
using DCExternalSite.DCServiceReference0;
using DCExternalSite.Web.Services;
//using DCExternalSite.BLL;

namespace DCExternalSite.Views
{
    public partial class PatientList : Page
    {
                
        public PatientList()
        {
            InitializeComponent();
            this.Title = ApplicationStrings.PatientPageTitle;

            WebContext.Current.Authentication.LoggedOut += (se, ev) =>
                {
                    NavigationService.Navigate(new Uri("/Home", UriKind.Relative));
                };
        }
        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!WebContext.Current.User.IsAuthenticated)
            {
                this.NavigationService.Navigate(new Uri("/Home", UriKind.Relative));
                return;
            }            
        }

        private void patientDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }

        private void examinationDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }

        
         private void imagePrint_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
         {
            
             App app = (App)Application.Current;

             app.FirstName = FirstNameTextBlock.Text;
             app.Surname = SurnameTextBlock.Text;
             app.BirthDate = BirthDateTextBlock.Text;
             app.Consultation = (dataForm1.FindNameInContent("ConsultationTextBox") as TextBox).Text;
             app.Recommendation = (dataForm1.FindNameInContent("RecommendationTextBox") as TextBox).Text;
             app.Conclusion = (dataForm1.FindNameInContent("ConclusionTextBox") as TextBox).Text;
             app.Protocol = (dataForm1.FindNameInContent("ProtocolTextBox") as TextBox).Text;
             app.StartTime = (dataForm1.FindNameInContent("StartTimeTextBlock") as TextBlock).Text;
             app.ExaminationTypeName = (dataForm1.FindNameInContent("ExaminationTypeNameTextBlock") as TextBlock).Text;
             app.ExaminationTypePrice = (dataForm1.FindNameInContent("ExaminationTypePriceTextBlock") as TextBlock).Text;
             app.EmployeeFirstName = (dataForm1.FindNameInContent("EmployeeFirstNameTextBlock") as TextBlock).Text;
             app.EmployeeSurname = (dataForm1.FindNameInContent("EmployeeSurnameTextBlock") as TextBlock).Text;
             app.EmployeeSpecialty = (dataForm1.FindNameInContent("EmployeeSpecialtyTextBlock") as TextBlock).Text;
             app.EmployeeCategory = (dataForm1.FindNameInContent("EmployeeCategoryTextBlock") as TextBlock).Text;
             
             this.NavigationService.Navigate(new Uri("/PrintPage", UriKind.Relative));
             
         }

    }     
}