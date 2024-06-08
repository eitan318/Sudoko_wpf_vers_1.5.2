using System;
using System.Windows.Controls;
using System.Windows.Threading;
using static Sudoko_wpf.publico.Constants;

namespace Sudoko_wpf.GameClasses
{
    public class Game
    {
        private Board board;
        private TextBlock timerTxtB;
        private static DispatcherTimer timer;
        private TimeSpan elapsedTime;
        private Puzzle puzzle;
        private static bool newPuzzle = true;

        public Game(Grid gameGrid, TextBlock timerTxtB)
        {
            timerTxtB.Text = TimerConstants.DEFAULT_TIME;
            initTimer();
            puzzle = new Puzzle();
            this.timerTxtB = timerTxtB;
            timer.Start();
            board = new Board(gameGrid, puzzle);
        }

        public Game(Grid gameGrid, TextBlock timerTxtB, string code)
        {
            timerTxtB.Text = TimerConstants.DEFAULT_TIME;
            initTimer();
            puzzle = new Puzzle(code);
            this.timerTxtB = timerTxtB;
            timer.Start();
            board = new Board(gameGrid, puzzle);
        }


        public void End()
        {
            Board.ShowSolution();
            Timer.Stop();
        }

        private void initTimer()
        {
            // Initialize the timer
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Tick every second
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Update the elapsed time and display it
            elapsedTime = elapsedTime.Add(TimeSpan.FromSeconds(1));
            timerTxtB.Text = GetTime();
        }

        private string GetTime()
        {
            return elapsedTime.ToString(TimerConstants.FORMAT);
        }

        public void Resume()
        {
            // Resume the game timer
            timer.Start();
        }

        // Accessor for the specific game items
        public Board Board => board;

        public DispatcherTimer Timer => timer;

        public static void TimerStop()
        {
            timer.Stop();
        }
    }
}
