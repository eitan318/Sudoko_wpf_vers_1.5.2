using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System.Drawing;
using System.Diagnostics.Eventing.Reader;

namespace Sudoko_wpf
{
    public class Cell : TextBox
    {
        private bool isSplited, ignoreMouseover;
        public int row, column;
        private List<Cell> relatedCells;
        private string previusText;

        public Cell(int row, int column)
        {
            this.row = row;
            this.column = column;

            Grid.SetRow(this, row);
            Grid.SetColumn(this, column);
            
            InitializeProperties();
            this.isSplited = false;
        }

        private void InitializeProperties()
        {
            this.Text = "";
            this.VerticalContentAlignment = VerticalAlignment.Center;
            this.HorizontalContentAlignment = HorizontalAlignment.Center;
            this.FontSize = Constants.BOARD_WIDTH * Constants.RELATIVE_FONT_SIZE / Settings.BOARD_SIDE;
            this.BorderThickness = new Thickness(Constants.BORDER_THICKNESS);
            this.BorderBrush = Colors.BORDER;
            this.Background = Colors.BOARD;
            this.Foreground = Colors.VALID_TEXT;
            this.CaretBrush = Brushes.Transparent;
            this.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.VerticalAlignment = VerticalAlignment.Stretch;
            this.MaxLength = Settings.BOARD_SIDE.ToString().Length;
            this.row = Grid.GetRow(this);
            this.column = Grid.GetColumn(this);
            
        }

        public void AttachEventHandlers()
        {
            TextChanged += TextBox_TextChanged;
            PreviewTextInput += TextBox_PreviewTextInput;
            PreviewKeyDown += TextBox_PreviewKeyDown;
            GotFocus += TextBox_GotFocus;
        }

        public void DettachEventHandlers()
        {
            TextChanged -= TextBox_TextChanged;
            PreviewTextInput -= TextBox_PreviewTextInput;
            PreviewKeyDown -= TextBox_PreviewKeyDown;
            GotFocus -= TextBox_GotFocus;
        }

        public void Merge()
        {
            
        }


        public void Split()
        { }



        //THE "" IS IMPORTENT
        public void ColorBy(Cell focusCell, string previewText, bool validateForeground)
        {
            bool isValid = this.Foreground == Colors.VALID_TEXT;
            this.BorderThickness = new Thickness(Constants.BORDER_THICKNESS);
            this.BorderBrush = Colors.BORDER;

            if (this.IsRelatedTo(focusCell))
            {
                this.Background = this.Text == focusCell.Text && this.Text != "" ? Colors.CONTREDICT : Colors.SIGN;

                if (validateForeground)
                {
                    if (this.Text == focusCell.previusText )
                    {
                        isValid = this.isRelativelyValid();
                    }
                    else if (this.Text == focusCell.Text && this.Text != "")
                    {
                        isValid = false;
                    }
                    if (!this.IsReadOnly)
                    {
                        this.Foreground = isValid ? Colors.VALID_TEXT : Colors.UNVALID_TEXT;
                    }
                    if(!isValid)
                    {
                        focusCell.Foreground = Colors.UNVALID_TEXT;
                    }
                        
                }

            }  
            else
            {
                this.Background = this.Text == previewText && this.Text != "" ? Colors.SAME_TEXT : Colors.BOARD;
            }

            
        }





        private bool IsRelatedTo(Cell anotherCell)
        {
            bool sameRow = this.row == anotherCell.row;
            bool sameColumn = this.column == anotherCell.column;
            bool sameBox = this.row / Settings.BOX_HEIGHT == anotherCell.row / Settings.BOX_HEIGHT && this.column / Settings.BOX_WIDTH == anotherCell.column / Settings.BOX_WIDTH;
            return sameRow || sameColumn || sameBox;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Validate the text input based on maxValue
            if (IsValidInput(e.Text, Settings.BOARD_SIDE))
            {
                if (this.IsReadOnly)
                {
                    Board.VisualizeState(this, false, e.Text);
                }
                else
                {
                    this.Text = e.Text.ToUpper();
                    // Move the caret to the end of the TextBox
                    this.CaretIndex = this.Text.Length;
                }
                
            }

            // prevent writing text
            e.Handled = true;
        }

        private bool IsValidInput(string input, int maxValue)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            char character = input[0];

            int value = HexaToInt(character);

            return value >= 1 && value <= maxValue;
        }


        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            // Handle arrow key navigation
            switch (e.Key)
            {
                case Key.Up:
                    if (this.row > 0)
                    {
                        Board.MoveFocusToTextBox(this.row - 1, this.column);
                    }
                    break;
                case Key.Down:
                    if (this.row < Settings.BOARD_SIDE - 1)
                    {
                        Board.MoveFocusToTextBox(this.row + 1, this.column);
                    }
                    break;
                case Key.Left:
                    if (this.column > 0)
                    {
                        Board.MoveFocusToTextBox(this.row, this.column - 1);
                    }
                    break;
                case Key.Right:

                    if (this.column < Settings.BOARD_SIDE - 1)
                    {
                        Board.MoveFocusToTextBox(this.row, this.column + 1);
                    }
                    break;
                case Key.Space:
                    if (this.isSplited)
                        Merge();
                    else
                        Split();
                    break;
            }
        }



        public bool isRelativelyValid()
        {
            if (relatedCells == null)
                relatedCells = Board.GetRelatedCells(row, column);

            foreach (Cell cell in relatedCells)
            {
                if (this != cell && cell.Text != "" && this.Text == cell.Text)
                {
                    return false;
                }
            }
            return true;
        }

        public bool isTotalyValid()
        {
            return this.Text == Puzzle.CellValueS(row, column).ToString();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this != null && !this.IsReadOnly)
            {
                Board.VisualizeState(this, true, this.Text);
                previusText = this.Text;
            }

        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            this.CaretIndex = this.Text.Length;
            Board.VisualizeState(this, false, this.Text);
        }

        
        private int HexaToInt(char character)
        {
            if (char.IsDigit(character))
            {
                return character - '0';
            }
            else if (char.IsLetter(character))
            {
                return char.ToUpper(character) - 'A' + 10;
            }

            return -1;
        }


    }
}
