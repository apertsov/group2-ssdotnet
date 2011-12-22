using System;
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
using System.Windows.Printing;

namespace DCExternalSite.Views
{
    public partial class PrintPage : Page
    {
        public PrintPage()
        {
            InitializeComponent();
            App app = (App)Application.Current;
            P_Patient.Inlines.Add(" " + app.FirstName + " " + app.Surname);
            P_ExaminationTypeName.Inlines.Add(app.ExaminationTypeName);
            P_BirthDate.Inlines.Add(" " +  app.BirthDate + "р.");
            P_ExaminationTypePrice.Inlines.Add(" " + app.ExaminationTypePrice + " грн.");
            P_Consultation.Inlines.Add(" " + app.Consultation);
            P_Recommendation.Inlines.Add(" " + app.Recommendation);
            P_Conclusion.Inlines.Add(" " + app.Conclusion);
            P_Protocol.Inlines.Add(" " + app.Protocol);
            P_EmployeeName.Inlines.Add(" " + app.EmployeeFirstName + " " + app.EmployeeSurname);
            P_EmployeeSpecialty.Inlines.Add(" " + app.EmployeeSpecialty + ", " + app.EmployeeCategory + " категорія");
            P_StartTime.Inlines.Add(" " + app.StartTime);
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void imagePrint_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
          PrintDocument  doc = new PrintDocument();
          doc.PrintPage += new EventHandler<PrintPageEventArgs>(doc_PrintPage);
          doc.Print("SilverlightDoc");
        }

        void doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.PageVisual = this;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += new EventHandler<PrintPageEventArgs>(doc_PrintPage);
            doc.Print("SilverlightDoc");
        }



    }
}
