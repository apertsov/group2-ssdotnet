﻿namespace DCExternalSite
{
    using System;
    using System.Runtime.Serialization;
    using System.ServiceModel.DomainServices.Client.ApplicationServices;
    using System.Windows;
    using System.Windows.Controls;
    using DCExternalSite.Controls;

    /// <summary>
    /// Main <see cref="Application"/> class.
    /// </summary>
    public partial class App : Application
    {
        private BusyIndicator busyIndicator;

        //for Passing Values between Pages
       
        private string firstName;
        private string surname;
        private string birthDate;        
        private string consultation;
        private string recommendation;
        private string conclusion;
        private string protocol;
        private string startTime;
        private string examinationTypeName;
        private string examinationTypePrice;
        private string employeeFirstName;
        private string employeeSurname;        
        private string employeeSpecialty;
        private string employeeCategory;
        
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
            }
        }
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
            }
        }
        public string BirthDate
        {
            get
            {
                return birthDate;
            }
            set
            {
                birthDate = value;
            }
        }        
        public string Consultation
        {
            get
            {
                return consultation;
            }
            set
            {
                consultation = value;
            }
        }
        public string Recommendation
        {
            get
            {
                return recommendation;
            }
            set
            {
                recommendation = value;
            }
        }
        public string Conclusion
        {
            get
            {
                return conclusion;
            }
            set
            {
                conclusion = value;
            }
        }
        public string Protocol
        {
            get
            {
                return protocol;
            }
            set
            {
                protocol = value;
            }
        }
        public string StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                startTime = value;
            }
        }
        public string ExaminationTypeName
        {
            get
            {
                return examinationTypeName;
            }
            set
            {
                examinationTypeName = value;
            }
        }
        public string ExaminationTypePrice
        {
            get
            {
                return examinationTypePrice;
            }
            set
            {
                examinationTypePrice = value;
            }
        }
        public string EmployeeFirstName
        {
            get
            {
                return employeeFirstName;
            }
            set
            {
                employeeFirstName = value;
            }
        }
        public string EmployeeSurname
        {
            get
            {
                return employeeSurname;
            }
            set
            {
                employeeSurname = value;
            }
        }
        public string EmployeeSpecialty
        {
            get
            {
                return employeeSpecialty;
            }
            set
            {
                employeeSpecialty = value;
            }
        }
        public string EmployeeCategory
        {
            get
            {
                return employeeCategory;
            }
            set
            {
                employeeCategory = value;
            }
        }

        //
        
        /// <summary>
        /// Creates a new <see cref="App"/> instance.
        /// </summary>
        public App()
        {
            InitializeComponent();

            // Create a WebContext and add it to the ApplicationLifetimeObjects
            // collection.  This will then be available as WebContext.Current.
            WebContext webContext = new WebContext();
            webContext.Authentication = new FormsAuthentication();
            //webContext.Authentication = new WindowsAuthentication();
            this.ApplicationLifetimeObjects.Add(webContext);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // This will enable you to bind controls in XAML files to WebContext.Current
            // properties
            this.Resources.Add("WebContext", WebContext.Current);

            // This will automatically authenticate a user when using windows authentication
            // or when the user chose "Keep me signed in" on a previous login attempt
            WebContext.Current.Authentication.LoadUser(this.Application_UserLoaded, null);

            // Show some UI to the user while LoadUser is in progress
            this.InitializeRootVisual();

            //for Passing Values between Pages
            //this.RootVisual = new Home();
        }

        /// <summary>
        /// Invoked when the <see cref="LoadUserOperation"/> completes. Use this
        /// event handler to switch from the "loading UI" you created in
        /// <see cref="InitializeRootVisual"/> to the "application UI"
        /// </summary>
        private void Application_UserLoaded(LoadUserOperation operation)
        {
        }

        /// <summary>
        /// Initializes the <see cref="Application.RootVisual"/> property. The
        /// initial UI will be displayed before the LoadUser operation has completed
        /// (The LoadUser operation will cause user to be logged automatically if
        /// using windows authentication or if the user had selected the "keep
        /// me signed in" option on a previous login).
        /// </summary>
        protected virtual void InitializeRootVisual()
        {
            this.busyIndicator = new BusyIndicator();
            this.busyIndicator.Content = new MainPage();
            this.busyIndicator.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            this.busyIndicator.VerticalContentAlignment = VerticalAlignment.Stretch;

            this.RootVisual = this.busyIndicator;
        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // a ChildWindow control.
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                ErrorWindow.CreateNew(e.ExceptionObject);
            }
        }
    }
}