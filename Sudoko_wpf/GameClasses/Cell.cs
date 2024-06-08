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
using Sudoko_wpf.publico;
using static Sudoko_wpf.publico.Constants;

namespace Sudoko_wpf.GameClasses
{
    public class Cell : TextBox
    {
        public int row, column;
        private List<Cell> relatedCells;
        private string previusText;
        public string solvedValue;
        public Notes notesGrid;
        private bool gridVisible = false;



        public Cell(int row, int column)
        {
            this.row = row;
            this.column = column;

            notesGrid = new Notes { Visibility = Visibility.Collapsed };
            //this.Parent.Add(notesGrid);

            Grid.SetRow(this, row);
            Grid.SetColumn(this, column);

            Grid.SetRow(notesGrid, row);
            Grid.SetColumn(notesGrid, column);

            solvedValue = Puzzle.CellValueS(row, column).ToString();

            InitializeProperties();
        }





        private void InitializeProperties()
        {
            Text = "";
            VerticalContentAlignment = VerticalAlignment.Center;
            HorizontalContentAlignment = HorizontalAlignment.Center;
            FontSize = BoardConstants.BOARD_WIDTH * BoardConstants.RELATIVE_FONT_SIZE / Settings.BOARD_SIDE;
            BorderThickness = new Thickness(BoardConstants.REGULAR_BORDER_THICKNESS);
            BorderBrush = BrushResources.Border;
            Background = BrushResources.Board;
            Foreground = BrushResources.TextFore;
            CaretBrush = Brushes.Transparent;
            //this.HorizontalAlignment = HorizontalAlignment.Stretch;
            //this.VerticalAlignment = VerticalAlignment.Stretch;
            //this.MaxLength = Settings.BOARD_SIDE.ToString().Length;

        }

        public void AttachEventHandlers()
        {
            TextChanged += TextBox_TextChanged;
            PreviewTextInput += TextBox_PreviewTextInput;
            PreviewKeyDown += TextBox_PreviewKeyDown;
            GotFocus += TextBox_GotFocus;
            //SizeChanged += OnSizeChanged;

        }

        public void DettachEventHandlers()
        {
            TextChanged -= TextBox_TextChanged;
            PreviewTextInput -= TextBox_PreviewTextInput;
            PreviewKeyDown -= TextBox_PreviewKeyDown;
            GotFocus -= TextBox_GotFocus;
            //SizeChanged -= OnSizeChanged;
        }

        //THE "" IS IMPORTENT
        public void ColorBy(Cell focusCell, string previewText, bool validateForeground)
        {
            bool isValid = Foreground == BrushResources.TextFore;
            BorderThickness = new Thickness(BoardConstants.REGULAR_BORDER_THICKNESS);
            BorderBrush = BrushResources.Border;

            if (IsRelatedTo(focusCell))
            {
                Background = Text == focusCell.Text && Text != "" ? BrushResources.WrongBackground : BrushResources.Sign;

                if (validateForeground)
                {
                    if (Text == focusCell.previusText && Text != "")
                    {
                        isValid = isRelativelyValid();
                    }
                    else if (Text == focusCell.Text && Text != "")
                    {
                        isValid = false;
                    }
                    if (!IsReadOnly)
                    {
                        Foreground = isValid ? BrushResources.TextFore : BrushResources.WrongForeground;
                    }
                    if (!isValid)
                    {
                        focusCell.Foreground = BrushResources.WrongForeground;
                    }

                }

            }
            else
            {
                Background = Text == previewText && Text != "" ? BrushResources.SameText : BrushResources.Board;
            }


        }

        private bool IsRelatedTo(Cell anotherCell)
        {
            bool sameRow = row == anotherCell.row;
            bool sameColumn = column == anotherCell.column;
            bool sameBox = row / Settings.BOX_HEIGHT == anotherCell.row / Settings.BOX_HEIGHT && column / Settings.BOX_WIDTH == anotherCell.column / Settings.BOX_WIDTH;
            return sameRow || sameColumn || sameBox;
        }

        public void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Validate the text input based on maxValue
            if (IsValidInput(e.Text, Settings.BOARD_SIDE))
            {

                if (IsReadOnly)
                {
                    Board.VisualizeState(this, false, e.Text);
                }
                else
                {
                    if (Text.Length == 1)
                    {
                        ShowNotes(Text, e.Text);
                    }
                    else
                    {
                        if (gridVisible)
                        {
                            SwitchNote(e.Text);
                        }
                        else
                        {
                            Text = e.Text.ToUpper();
                            // Move the caret to the end of the TextBox
                            CaretIndex = Text.Length;
                        }

                    }

                }

            }

            // prevent writing text
            e.Handled = true;
        }

        public void Validate()
        {
            if (!IsReadOnly)
            {
                Text = solvedValue;
                IsReadOnly = true;
                Foreground = BrushResources.TextFore;
                Background = BrushResources.Board;
                //HINTS--
            }
        }


        private void ShowNotes(string firstNumber, string secondNumber)
        {
            if (gridVisible)
                return;

            gridVisible = true;
            notesGrid.Visibility = Visibility.Visible;
            //textBox.Visibility = Visibility.Collapsed;
            Text = "";

            notesGrid.Clear();
            notesGrid.ManipulatNote(firstNumber);
            notesGrid.ManipulatNote(secondNumber);
        }

        private void SwitchNote(string number)
        {
            if (notesGrid != null)
            {
                notesGrid.ManipulatNote(number);
                if (notesGrid.IsLastOne())
                {
                    Text = notesGrid.LastOne();
                    notesGrid.Visibility = Visibility.Collapsed;
                    gridVisible = false;
                }
            }

        }

        public bool IsValidInput(string input, int maxValue)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            int value = HexaToInt(input[0]);

            return value >= 1 && value <= maxValue;
        }


        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            // Handle arrow key navigation
            switch (e.Key)
            {
                case Key.Up:
                    if (row > 0)
                    {
                        Board.MoveFocusToTextBox(row - 1, column);
                    }
                    break;
                case Key.Down:
                    if (row < Settings.BOARD_SIDE - 1)
                    {
                        Board.MoveFocusToTextBox(row + 1, column);
                    }
                    break;
                case Key.Left:
                    if (column > 0)
                    {
                        Board.MoveFocusToTextBox(row, column - 1);
                    }
                    break;
                case Key.Right:

                    if (column < Settings.BOARD_SIDE - 1)
                    {
                        Board.MoveFocusToTextBox(row, column + 1);
                    }
                    break;
            }

            if (e.Key == Key.Delete || e.Key == Key.Space || e.Key == Key.Back)
            {
                Text = "";
                notesGrid.Clear();
                e.Handled = true;
            }
        }

        public bool isRelativelyValid()
        {
            if (relatedCells == null)
                relatedCells = Board.GetRelatedCells(row, column);

            foreach (Cell cell in relatedCells)
            {
                if (this != cell && cell.Text != "" && Text == cell.Text)
                {
                    return false;
                }
            }
            return true;
        }

        public bool isTotalyValid()
        {
            return Text == Puzzle.CellValueS(row, column).ToString();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this != null && !IsReadOnly)
            {
                Board.VisualizeState(this, true, Text);
                Board.ForSolvedAnimation();
                previusText = Text;
            }

        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            CaretIndex = Text.Length;
            Board.VisualizeState(this, false, Text);
        }


        private int HexaToInt(char character)
        {
            if (char.IsDigit(character))
            {
                return character - '0';
            }
            else if (char.IsLetter(character))
            {
                return char.ToUpper(character) - 'A' + Constants.NUM_DIGITS;
            }

            return Constants.ERROR;
        }


    }
}
