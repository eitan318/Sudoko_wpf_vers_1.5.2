using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sudoko_wpf
{
    /// <summary>
    /// Interaction logic for OpenningPage.xaml
    /// </summary>
    public partial class OpenningPage : Page
    {
        public OpenningPage()
        {
            InitializeComponent();
            Background = Colors.BACKGROUND;
        }

        private void GameSettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new System.Uri("GameSettingsPage.xaml", UriKind.Relative));
        }

        private void InstructionsBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new System.Uri("InstructionsPage.xaml", UriKind.Relative));
        }

        private void HistoryBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new System.Uri("InstructionsPage.xaml", UriKind.Relative));
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new System.Uri("SettingsPage.xaml", UriKind.Relative));
        }

    }
}
