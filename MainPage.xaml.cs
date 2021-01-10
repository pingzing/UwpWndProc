using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace WndProcTest
{
    public sealed partial class MainPage : Page
    {       
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            myButton.Content = "Clicked";
        }
    }
}
