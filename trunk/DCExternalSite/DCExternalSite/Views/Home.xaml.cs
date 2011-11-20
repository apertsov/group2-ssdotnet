namespace DCExternalSite
{
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using DCExternalSite.Views;
    using System;
    using System.Windows;

    /// <summary>
    /// Home page for the application.
    /// </summary>
    public partial class Home : Page
    {
        /// <summary>
        /// Creates a new <see cref="Home"/> instance.
        /// </summary>
        public Home()
        {
            InitializeComponent();

            this.Title = ApplicationStrings.HomePageTitle;
        }
        /// <summary>
        /// Executes when the user navigates to this page.
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void dataGrid1_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NewsViewWindow window = new NewsViewWindow();
            window.Show();
            window.Loaded += new RoutedEventHandler(delegate
            {
                window.dataForm.CurrentItem = dataGrid1.SelectedItem;
            });
        }
        }
    }
