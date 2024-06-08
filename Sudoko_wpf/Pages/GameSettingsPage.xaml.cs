using ControlLib;
using Sudoko_wpf.publico;
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
using static Sudoko_wpf.publico.Constants;

namespace Sudoko_wpf
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class GameSettingsPage : Page
    {
        public Page gamePage;



        public GameSettingsPage()
        {
            InitializeComponent();
            NUD_boxHeight.Value = 3;
            NUD_boxWidth.Value = 3;

            NUD_boxHeight.MaxValue = 4;
            NUD_boxWidth.MaxValue = 4;

            NUD_boxHeight.Value = Settings.BOX_HEIGHT;
            NUD_boxWidth.Value = Settings.BOX_WIDTH;

            NUD_boxWidth.ValueChanged += NUD_ValueChanged;
            NUD_boxHeight.ValueChanged += NUD_ValueChanged;

            gamePage = null;
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

        private void GameStarterBtn_Click(object sender, RoutedEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            if (CodeTXTBox.Text == "")
            {
                Settings.BOX_HEIGHT = Convert.ToInt32(NUD_boxHeight.Value);
                Settings.BOX_WIDTH = Convert.ToInt32(NUD_boxWidth.Value);
                Settings.BOARD_SIDE = Settings.BOX_WIDTH * Settings.BOX_HEIGHT;
                BoardConstants.REGULAR_BORDER_THICKNESS = (BoardConstants.BOARD_WIDTH / BoardConstants.DEFAULT_WIDTH) * (BoardConstants.DEFAULT_SIDE / Settings.BOARD_SIDE);
                window.gamePage = new GamePage();
                NavigationService.Navigate(window.gamePage);
                window.Settings_btn.Visibility = Visibility.Collapsed;
            }
            else if (IsValidCode(CodeTXTBox.Text))
            {
                window.gamePage = new GamePage(CodeTXTBox.Text);
                NavigationService.Navigate(window.gamePage);
                window.Settings_btn.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Puzzle code invalid! Enter it again");
                this.CodeTXTBox.Text = "";
            }
            
            
        }


        private bool IsValidCode(string code)
        {
            // Check for the presence of the colon and commas
            int settingEnd = code.IndexOf(":");
            int firstSeparator = code.IndexOf(",");

            if (settingEnd == -1 || firstSeparator == -1 || firstSeparator >= settingEnd)
            {
                return false;
            }

            // Check if box height and width are integers and positive
            if (!int.TryParse(code.Substring(0, firstSeparator), out int boxHeight) || boxHeight <= 0)
            {
                return false;
            }

            if (!int.TryParse(code.Substring(firstSeparator + 1, settingEnd - firstSeparator - 1), out int boxWidth) || boxWidth <= 0)
            {
                return false;
            }

            int boardSide = boxWidth * boxHeight;

            // Check the puzzle data length and format
            string puzzleData = code.Substring(settingEnd + 1);
            string[] puzzleParts = puzzleData.Split('|');

            if (puzzleParts.Length != boardSide * boardSide)
            {
                return false;
            }

            // Check each puzzle part for valid characters and format
            foreach (string part in puzzleParts)
            {
                int commaIndex = part.IndexOf(',');
                if (commaIndex == -1 || commaIndex != 1 || (part[commaIndex + 1] != 'V' && part[commaIndex + 1] != 'X'))
                {
                    return false;
                }

                char puzzleChar = part[0];
                if (!char.IsDigit(puzzleChar) && puzzleChar != '.')
                {
                    return false;
                }
            }

            return true;
        }


    }
}
