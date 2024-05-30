using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Sudoko_wpf
{
    public enum ColorMode
    {
        Dark,
        Light,
        Brown,
        Red,
        Blue,
        LightBlue

    }
    public class Colors
    {
        public Colors (ColorMode mode) 
        {
            SetColors(mode);
        }    

        public static Brush FOCUS; //LightCyan //PaleTurquoise
        public static Brush UNVALID_TEXT;
        public static Brush CONTREDICT;
        public static Brush SAME_TEXT;
        public static Brush SIGN;
        public static Brush BOARD = Brushes.Transparent;
        public static Brush BORDER;
        public static Brush VALID_TEXT;
        public static Brush BACKGROUND;
        public static Brush NOTES_MODE_BG = Brushes.Yellow;
        public static Brush VALID_BORDER = Brushes.Green;
        public static Brush UNVALID_BORDER = Brushes.Red;
        public static void SetColors(ColorMode mode)
        {
            switch (mode)
            {
                case ColorMode.Light:
                    FOCUS = Brushes.LightSteelBlue; //LightCyan //PaleTurquoise
                    UNVALID_TEXT = Brushes.OrangeRed;
                    CONTREDICT = Brushes.Pink;
                    SAME_TEXT = Brushes.LightGray;
                    SIGN = Brushes.Lavender;
                    BORDER = Brushes.Black;
                    VALID_TEXT = Brushes.Black;
                    BACKGROUND = Brushes.White;
                    BOARD = Brushes.Transparent;
                    break;

                case ColorMode.Dark:
                    FOCUS = Brushes.DarkGray; //LightCyan //PaleTurquoise
                    UNVALID_TEXT = Brushes.IndianRed;
                    CONTREDICT = Brushes.DarkRed;
                    SAME_TEXT = Brushes.DimGray;
                    SIGN = Brushes.MidnightBlue;
                    BORDER = Brushes.LightGray;
                    VALID_TEXT = Brushes.White;
                    BACKGROUND = Brushes.DimGray;
                    BOARD = Brushes.Black;
                    break;

                case ColorMode.Brown:
                    FOCUS = Brushes.Green; //LightCyan //PaleTurquoise
                    UNVALID_TEXT = Brushes.Red;
                    CONTREDICT = Brushes.Pink;
                    SAME_TEXT = Brushes.LightGray;
                    SIGN = Brushes.Lavender;
                    BORDER = Brushes.Black;
                    VALID_TEXT = Brushes.Black;
                    BACKGROUND = Brushes.Brown;
                    BOARD = Brushes.Transparent;
                    break;


                case ColorMode.Red:
                    FOCUS = Brushes.LightSteelBlue; //LightCyan //PaleTurquoise
                    UNVALID_TEXT = Brushes.Red;
                    CONTREDICT = Brushes.Pink;
                    SAME_TEXT = Brushes.LightGray;
                    SIGN = Brushes.Lavender;
                    BORDER = Brushes.Black;
                    VALID_TEXT = Brushes.Black;
                    BACKGROUND = Brushes.Red;
                    BOARD = Brushes.Transparent;
                    break;

                case ColorMode.Blue:
                    FOCUS = Brushes.SlateBlue; 
                    UNVALID_TEXT = Brushes.IndianRed;
                    CONTREDICT = Brushes.MediumVioletRed;
                    SAME_TEXT = Brushes.MediumBlue;
                    SIGN = Brushes.DodgerBlue;
                    BORDER = Brushes.Black;
                    VALID_TEXT = Brushes.LightCyan;
                    BACKGROUND = Brushes.Navy;
                    BOARD = Brushes.CornflowerBlue;
                    break;
                case ColorMode.LightBlue:
                    FOCUS = Brushes.MediumTurquoise; 
                    UNVALID_TEXT = Brushes.IndianRed;
                    CONTREDICT = Brushes.IndianRed;
                    SAME_TEXT = Brushes.DeepSkyBlue;
                    SIGN = Brushes.LightBlue;
                    BORDER = Brushes.Indigo;
                    VALID_TEXT = Brushes.MidnightBlue;
                    BACKGROUND = Brushes.LightCyan;
                    BOARD = Brushes.PaleTurquoise;
                    break;
            }

        }
    }
}