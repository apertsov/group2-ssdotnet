namespace DCExternalSite
{
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using System;
    using System.Collections.Generic;
    using System.Windows.Browser;
    using DCExternalSite.Web.Services;
    //using DCExternalSite.BLL;

    /// <summary>
    /// <see cref="Page"/> class to present information about the current application.
    /// </summary>
    public partial class About : Page
    {
        /// <summary>
        /// Creates a new instance of the <see cref="About"/> class.
        /// </summary>
        public About()
        {
            if (WebContext.Current.User.IsAuthenticated)
            {
                InitializeComponent();
            }

            this.Title = ApplicationStrings.AboutPageTitle;
        }

        /// <summary>
        /// Executes when the user navigates to this page.
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!WebContext.Current.User.IsAuthenticated)
            {
                this.NavigationService.Navigate(new Uri("/Home", UriKind.Relative));
                return;
            }
            if (!WebContext.Current.User.IsAuthenticated)
            {
                NavigationService.Navigate(new Uri("/Home", UriKind.Relative));
            }
        }
    }
}