using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;

namespace Sudoko_wpf
{
    public class Game
    {
        private Board board;
        private TextBlock timerTxtB;
        private DispatcherTimer timer;
        private TimeSpan elapsedTime;

        public Game(Grid mainGrid, TextBlock timerTxtB)
        {
            this.timerTxtB = timerTxtB;
            this.board = new Board(mainGrid);

            initTimer();
            this.timer.Start();
            this.board.Generate();
        }

        private void initTimer()
        {
            // Initialize the timer
            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromSeconds(1); // Tick every second
            this.timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Update the elapsed time and display it
            this.elapsedTime = elapsedTime.Add(TimeSpan.FromSeconds(1));
            this.timerTxtB.Text = elapsedTime.ToString(@"hh\:mm\:ss");
        }


        //acces for a specific game items
        public Board Board => board;

        public DispatcherTimer Timer => timer;
    }
}
