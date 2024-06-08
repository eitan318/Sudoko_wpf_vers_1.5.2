using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Sudoko_wpf.GameClasses;

namespace Sudoko_wpf
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private Game game;
        private bool isLoaded = true;

        // Default constructor for XAML
        public GamePage()
        {
            InitializeComponent();
            game = new Game(GameGrid, timerTxtB);
            this.Loaded += MyPage_Loaded;
            this.Unloaded += MyPage_Unloaded;
        }

        public GamePage(string code)
        {
            InitializeComponent();
            game = new Game(GameGrid, timerTxtB, code);
            this.Loaded += MyPage_Loaded;
            this.Unloaded += MyPage_Unloaded;
        }

        private void MyPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Subscribe to the Navigating event
            if (NavigationService != null)
            {
                NavigationService.Navigating += OnNavigatingFrom;
            }
        }

        private void MyPage_Unloaded(object sender, RoutedEventArgs e)
        {
            // Unsubscribe from the Navigating event
            if (NavigationService != null)
            {
                NavigationService.Navigating -= OnNavigatingFrom;
            }
        }

        private void OnNavigatingFrom(object sender, NavigatingCancelEventArgs e)
        {
            // Check if the current content is GamePage and we're navigating away
            if (NavigationService != null && NavigationService.Content is GamePage && isLoaded)
            {
                isLoaded = false;
                game.Timer.Stop();
            }
            if (e.Content is GamePage)
            {
                isLoaded = true;
                game.Timer.Start();
            }
        }



        private void EndGame_Click(object sender, RoutedEventArgs e)
        {
            game.Board.ShowSolution();
            game.Timer.Stop();
            game.End();

            var window = (MainWindow)Application.Current.MainWindow;
            window.gamePage = null;
            window.Settings_btn.Visibility = Visibility.Visible;
        }

        private void Hint_Click(object sender, RoutedEventArgs e)
        {
            Cell focusCell = Board.focusedCell();
            if (focusCell != null)
            {
                Board.focusedCell().Validate();
            }

        }

        private void ShowPuzzleCode_Click(object sender, RoutedEventArgs e)
        {
            string puzzleCode = Puzzle.GetCurrentCode();
            OutputBoxWindow.Show(puzzleCode);
        }

        private void CheckBoard_Click(object sender, RoutedEventArgs e)
        {
            Board.CheckMyBoard();
            // CHECKS--
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            game.Timer.Stop();
            MessageBox.Show("Verify to continue");
            game.Timer.Start();
        }
    }
}
