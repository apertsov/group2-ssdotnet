namespace DCExternalSite
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using DCExternalSite.LoginUI;
    using System;

    /// <summary>
    /// <see cref="UserControl"/> class providing the main UI for the application.
    /// </summary>
    public partial class MainPage : UserControl
    {
        /// <summary>
        /// Creates a new <see cref="MainPage"/> instance.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            this.loginContainer.Child = new LoginStatus();
            WebContext.Current.Authentication.LoggedOut += (se, ev) =>
                {
                    Link3.Visibility = System.Windows.Visibility.Collapsed;
                    ContentFrame.Source = new System.Uri("/Home", UriKind.RelativeOrAbsolute);
                };

            WebContext.Current.Authentication.LoggedIn += (se, ev) =>
                {
                    Link3.Visibility = System.Windows.Visibility.Visible;
                    ContentFrame.Source = new System.Uri("/PatientList", UriKind.RelativeOrAbsolute);//System.Uri.Compare("/PatientList"); "/PatientList"; //"/PatientList"; 
                };   
        
            
            //WebContext.Current.Authentication.LoggedIn += new System.EventHandler<System.ServiceModel.DomainServices.Client.ApplicationServices.AuthenticationEventArgs>(Authentication_LoggedIn);
            //WebContext.Current.Authentication.LoggedOut += new System.EventHandler<System.ServiceModel.DomainServices.Client.ApplicationServices.AuthenticationEventArgs>(Authentication_LoggedOut);
        }
        void Authentication_LoggedOut(object sender, System.ServiceModel.DomainServices.Client.ApplicationServices.AuthenticationEventArgs e)
        {
            // скрыть станицы, недоступные для незарегистрированных пользователей
            this.ContentFrame.Navigate(new Uri("/About", UriKind.Relative));
            this.ContentFrame.Navigate(new Uri("/PatientList", UriKind.Relative));
        }

        void Authentication_LoggedIn(object sender, System.ServiceModel.DomainServices.Client.ApplicationServices.AuthenticationEventArgs e)
        {
            if (WebContext.Current.User.IsAuthenticated)
            {

                // показывать больше страниц

            }

        }

        /// <summary>
        /// After the Frame navigates, ensure the <see cref="HyperlinkButton"/> representing the current page is selected
        /// </summary>
        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            foreach (UIElement child in LinksStackPanel.Children)
            {
                HyperlinkButton hb = child as HyperlinkButton;
                if (hb != null && hb.NavigateUri != null)
                {
                    if (hb.NavigateUri.ToString().Equals(e.Uri.ToString()))
                    {
                        VisualStateManager.GoToState(hb, "ActiveLink", true);
                    }
                    else
                    {
                        VisualStateManager.GoToState(hb, "InactiveLink", true);
                    }
                }
            }
        }

        /// <summary>
        /// If an error occurs during navigation, show an error window
        /// </summary>
        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            e.Handled = true;
            ErrorWindow.CreateNew(e.Exception);
        }

        private void rectangle_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
           LogoStoryboard.Begin();
        }
    }
}