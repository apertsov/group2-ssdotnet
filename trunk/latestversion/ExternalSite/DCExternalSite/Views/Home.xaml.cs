namespace DCExternalSite
{
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using DCExternalSite.Views;
    using System;
    using System.Windows;
    using System.Windows.Input;

    using System.Collections.Generic;
    using System.Net;
    using System.Windows.Documents;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;

    /// <summary>
    /// Home page for the application.
    /// </summary>
    public partial class Home : Page
    {
        Point lastMousePos = new Point();
        Point lastMouseLogicalPos = new Point();
        Point lastMouseViewPort = new Point();
        bool duringDrag = false;
        double zoom = 1;
        double minzoom = 0.001;
        bool duringOpen = false;
        
        

        public Home()
        {
            InitializeComponent();

            this.Title = ApplicationStrings.HomePageTitle;

            this.MouseMove += delegate(object sender, MouseEventArgs e)
            {
                this.lastMousePos = e.GetPosition(this.ZoomImage);
            };

            new MouseWheelHelper(this).Moved += delegate(object sender, MouseWheelEventArgs e)
            {

                double newzoom = zoom;
                if (e.Delta > 0)
                    newzoom /= 1.3;
                else
                    newzoom *= 1.3;
                Point logicalPoint = this.ZoomImage.ElementToLogicalPoint(this.lastMousePos);

                this.ZoomImage.ZoomAboutLogicalPoint(zoom / newzoom, logicalPoint.X, logicalPoint.Y);
                zoom = newzoom;
            };

         }

        private void Home_Click(object sender, MouseButtonEventArgs e)
        {
            this.ZoomImage.ViewportOrigin = new Point(0, 0);
            this.ZoomImage.ViewportWidth = 1;
            zoom = 1;
        }

        private void ZoomIn_Click(object sender, MouseButtonEventArgs e)
        {
            double newzoom = zoom / 1.3;
            if (newzoom < minzoom) { newzoom = minzoom; }
            Point logicalPoint = this.ZoomImage.ElementToLogicalPoint(new Point(this.ActualWidth / 2, this.ActualHeight / 2));
            this.ZoomImage.ZoomAboutLogicalPoint(zoom / newzoom, logicalPoint.X, logicalPoint.Y);
            zoom = newzoom;
        }

        private void ZoomOut_Click(object sender, MouseButtonEventArgs e)
        {
            double newzoom = zoom * 1.3;
            if (newzoom > 1) { newzoom = 1; }
            Point logicalPoint = this.ZoomImage.ElementToLogicalPoint(new Point(this.ActualWidth / 2, this.ActualHeight / 2));
            this.ZoomImage.ZoomAboutLogicalPoint(zoom / newzoom, logicalPoint.X, logicalPoint.Y);
            zoom = newzoom;
        }

        private void ZoomImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            lastMouseLogicalPos = e.GetPosition(this.ZoomImage);
            lastMouseViewPort = this.ZoomImage.ViewportOrigin;
            duringDrag = true;
        }

        private void ZoomImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            duringDrag = false;
            this.ZoomImage.UseSprings = true;
        }

        private void ZoomImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (duringDrag)
            {
                Point newPoint = lastMouseViewPort;
                Point thisMouseLogicalPos = e.GetPosition(this.ZoomImage);
                newPoint.X += (lastMouseLogicalPos.X - thisMouseLogicalPos.X) / this.ZoomImage.ActualWidth * this.ZoomImage.ViewportWidth;
                newPoint.Y += (lastMouseLogicalPos.Y - thisMouseLogicalPos.Y) / this.ZoomImage.ActualWidth * this.ZoomImage.ViewportWidth;
                this.ZoomImage.ViewportOrigin = newPoint;
            }
        }

        private void ZoomImage_ImageOpenSucceeded(object sender, RoutedEventArgs e)
        {
            duringOpen = true;

        }

        private void ZoomImage_MotionFinished(object sender, RoutedEventArgs e)
        {
            if (duringOpen)
            {
                duringOpen = false;

                // zoom out a tad bit.
                this.ZoomImage.ViewportOrigin = new Point(-0.4, -0.1);
                this.ZoomImage.ViewportWidth = 1.8;
            }
        }             

        
        private void ZoomImage_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            PageScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
        }

        private void ZoomImage_MouseLeave(object sender, MouseEventArgs e)
        {
            PageScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
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
    
