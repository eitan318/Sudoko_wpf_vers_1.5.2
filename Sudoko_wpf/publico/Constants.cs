using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sudoko_wpf.publico
{
    public class Constants
    {
        public static int NUM_DIGITS = 10;
        public static int ERROR = -1;
        public class BoardConstants 
        {
            public static int BOARD_WIDTH = 600;
            public static double RELATIVE_FONT_SIZE = 0.65;
            public static string SHOW_SOLUTION = "Show a solution";
            public static double REGULAR_BORDER_THICKNESS = 6;
            public static double INTERNAL_BORDER_TO_REGULAR = 2;
            public static double EXTERNAL_BORDER_TO_REGULAR = 4;
            public static double DEFAULT_SIDE = 9.0;
            public static double DEFAULT_WIDTH = 500.0;
        }
        public class HistoryConstants
        {
            public static double RELATIVE_FONT_SIZE = 0.4;
            public static int MARGIN = 10;
            public static int CORNER_RADIUS = 20;
            public static int HEIGHT = 60;
        }

        public class TimerConstants
        {
            public static string DEFAULT_TIME = "00:00:00";
            public static string FORMAT = @"hh\:mm\:ss";
        }
    }
}
