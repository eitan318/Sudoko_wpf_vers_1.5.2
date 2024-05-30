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
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        Game game;
        public GamePage()
        {
            InitializeComponent();
            Background = Colors.BACKGROUND;
            game = new Game(GameGrid, timerTxtB);
            btn_toGameSettings.Visibility = Visibility.Collapsed;
        }

        private void EndGame_Click(object sender, RoutedEventArgs e)
        {
            btn_endGame.Visibility = Visibility.Collapsed;
            btn_toGameSettings.Visibility = Visibility.Visible;
            game.Board.ShowSolution();
            game.Timer.Stop();
        }

        private void ToGameSetting_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new System.Uri("GameSettingsPage.xaml", UriKind.Relative));
        }
        

        private void Hint_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void ShowPuzzleCode_Click(object sender, RoutedEventArgs e)
        {
            string puzzleCode = Puzzle.GetCurrentCode();
            CopyBox copyB = new CopyBox(puzzleCode);
            copyB.ShowMessageBox();
        }

        private void CheckBoard_Click(object sender, RoutedEventArgs e)
        {
            Board.Check();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            game.Timer.Stop();
            MessageBox.Show("verify to continue");
            game.Timer.Start();
        }




    }

}