using ControlLib;
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
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class GameSettingsPage : Page
    {
        public GameSettingsPage()
        {
            InitializeComponent();
            Background = Colors.BACKGROUND;
            NUD_boxHeight.Value = 3;
            NUD_boxWidth.Value = 3;

            NUD_boxHeight.MaxValue = 4;
            NUD_boxWidth.MaxValue = 4;

            NUD_boxHeight.Value = Settings.BOX_HEIGHT;
            NUD_boxWidth.Value = Settings.BOX_WIDTH;

            NUD_boxWidth.ValueChanged += NUD_ValueChanged;
            NUD_boxHeight.ValueChanged += NUD_ValueChanged;
        }

        private void NUD_ValueChanged(object sender, EventArgs e)
        {
            if (NUD_boxWidth.Value == 5)
            {
                NUD_boxHeight.MaxValue = 4;
            }
            else if (NUD_boxHeight.Value == 5)
            {
                NUD_boxWidth.MaxValue = 4;
            }
            else
            {
                NUD_boxWidth.MaxValue = 5;
                NUD_boxHeight.MaxValue = 5;
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new System.Uri("OpenningPage.xaml", UriKind.Relative));
        }
        
        private void EnterPuzzleCodeBtn_Click(object sender, RoutedEventArgs e)
        {
            InputBox inputB = new InputBox("enter puzzle code: ");
            Puzzle.ImportPuzzleCode(inputB.ShowDialog());
            NavigationService.Navigate(new System.Uri("GamePage.xaml", UriKind.Relative));
        }
    

        private void GameStarterBtn_Click(object sender, RoutedEventArgs e)
        {
            Settings.BOX_HEIGHT = Convert.ToInt32(NUD_boxHeight.Value);
            Settings.BOX_WIDTH = Convert.ToInt32(NUD_boxWidth.Value);
            Settings.BOARD_SIDE = Settings.BOX_WIDTH * Settings.BOX_HEIGHT;
            Constants.BORDER_THICKNESS = (Constants.BOARD_WIDTH / 500.0) * 9.0 / Settings.BOARD_SIDE;
            NavigationService.Navigate(new System.Uri("GamePage.xaml", UriKind.Relative));
        }


    }
}
