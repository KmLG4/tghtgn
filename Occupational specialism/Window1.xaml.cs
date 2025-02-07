using System.Windows;

namespace Occupational_specialism
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int screenWidth = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
            int screenHeight = (int)System.Windows.SystemParameters.PrimaryScreenHeight;

            this.Width = screenWidth * 0.8; // Set window width to 80% of screen width
            this.Height = screenHeight * 0.8; // Set window height to 80% of screen height
        }
    }
}
