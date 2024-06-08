
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Sudoko_wpf.Themes;

namespace Sudoko_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Page? gamePage = null;
        public MainWindow()
        {
            InitializeComponent();
            ThemeControl.SetColors(ColorMode.Light);
            MainFrame.Navigate(new OpenningPage());
            Resize.Visibility = Visibility.Visible;
        }


        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TglSizeBtn_Checked(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void TglSizeBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void IconButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton menuBtn = sender as RadioButton;
            string content = menuBtn.Content.ToString();

            switch (content)
            {
                case "Home":
                    MainFrame.Navigate(new OpenningPage());
                    break;
                case "History":
                    MainFrame.Navigate(new HistoryPage());
                    break;
                case "Settings":
                    MainFrame.Navigate(new SettingsPage());
                    break;
                case "Game":
                    if(gamePage == null)
                    {
                        Page gsp = new GameSettingsPage();
                        MainFrame.Navigate(gsp);
                    }
                    else
                    {
                        MainFrame.Navigate(gamePage);
                    }
                    break;
                case "Instructions":
                    MainFrame.Navigate(new InstructionsPage());
                    break;
                default:
                    break;
            }
        }

















    }
}