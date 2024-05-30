using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Data.Common;

namespace Sudoko_wpf
{
    public class Board
    {
        private static Cell[,] cells;
        private static Puzzle puzzle;
        public Grid mainGrid;

        public Board(Grid mainGrid)
        {
            this.mainGrid = mainGrid;
        }

        public void Generate()
        {
            cells = new Cell[Settings.BOARD_SIDE, Settings.BOARD_SIDE];
            puzzle = new Puzzle();

            CreateSudokuGrid(mainGrid);
            initialize();
        }

        public static void VisualizeState(Cell focusCell, bool validateForeground, string MarkText = "")
        {
            if (MarkText == "")
                MarkText = focusCell.Text;

            focusCell.Foreground = Colors.VALID_TEXT;

            foreach (Cell cell in cells)
            {
                if(cell != focusCell)
                {
                    cell.ColorBy(focusCell, MarkText, validateForeground);
                }   
            }

            focusCell.Background = Colors.FOCUS;
        }

        public static void Check()
        {
            foreach (Cell cell in cells)
            {
                cell.Background = Colors.BOARD;
                cell.BorderThickness = new Thickness(Constants.BORDER_THICKNESS * 2);
                cell.BorderBrush = cell.Text == puzzle.CellValue(cell.row, cell.column).ToString() ? Colors.VALID_BORDER : Colors.UNVALID_BORDER;
            }
        }


        public static void MoveFocusToTextBox(int row, int column)
        {
            Cell nextCell = cells[row, column];
            nextCell.Focus();
        }

        public static bool CellMustValid(int row, int col)
        {
            return cells[row, col].Text == "" || puzzle.CellValue(row, col).ToString() == cells[row, col].Text ;
        }

        public void ShowSolution()
        {
            foreach (Cell cell in cells)
            {
                cell.DettachEventHandlers();
                cell.Text = ((char)puzzle.CellValue(cell.row, cell.column)).ToString();
                cell.IsReadOnly = true;
                cell.Background = Colors.BOARD;
                cell.BorderBrush = Colors.BORDER;
                cell.Foreground = Colors.VALID_TEXT;
                cell.BorderThickness = new Thickness(Constants.BORDER_THICKNESS * 2);
                cell.AttachEventHandlers();
            }
        }

        public void initialize()
        {
            for (int i = 0; i < Settings.BOARD_SIDE; i++)
            {
                for (int j = 0; j < Settings.BOARD_SIDE; j++)
                {
                    Cell cell = cells[i, j];
                    if (puzzle.IsCellInitial(i, j))
                    {
                        cell.Text = ((char)puzzle.CellValue(i, j)).ToString();
                        cell.IsReadOnly = true;
                        cell.FontWeight = FontWeights.Bold;
                    }
                    else
                    {
                        cell.IsReadOnly = false;
                        cell.Text = "";
                    }
                    cell.Background = Colors.BOARD;
                    cell.AttachEventHandlers();
                }
            }
        }

        private void CreateSudokuGrid(Grid mainGrid)
        {
            Grid sudokuGrid = new Grid();
            sudokuGrid.HorizontalAlignment = HorizontalAlignment.Center;
            sudokuGrid.VerticalAlignment = VerticalAlignment.Center;
            sudokuGrid.Height = Constants.BOARD_WIDTH;
            sudokuGrid.Width = Constants.BOARD_WIDTH;
            Grid.SetColumn(sudokuGrid, 1);
            Grid.SetRow(sudokuGrid, 1);

            CreatSeperation(sudokuGrid);
            CreatCells(sudokuGrid);
            AddBorders(sudokuGrid);

            mainGrid.Children.Add(sudokuGrid);
        }

        private void CreatSeperation(Grid sudokuGrid)
        {
            //creating seperations
            for (int i = 0; i < Settings.BOARD_SIDE; i++)
            {
                sudokuGrid.RowDefinitions.Add(new RowDefinition());
                sudokuGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        private void CreatCells(Grid sudokuGrid)
        {
            for (int i = 0; i < Settings.BOARD_SIDE; i++)
            {
                for (int j = 0; j < Settings.BOARD_SIDE; j++)
                {
                    Cell cell = new Cell(i, j);

                    sudokuGrid.Children.Add(cell);

                    //binding grid to metrix
                    cells[i, j] = cell;
                }
            }
        }

        private void AddBorders(Grid sudokuGrid)
        {
            //creating strong borders
            for (int rows = 0; rows < Settings.BOX_HEIGHT; rows++)
            {
                for (int cols = 0; cols < Settings.BOX_WIDTH; cols++)
                {

                    Border internalBorder = new Border();
                    internalBorder.BorderThickness = new Thickness(Constants.BORDER_THICKNESS * 2);
                    internalBorder.BorderBrush = Colors.BORDER;
                    Grid.SetRow(internalBorder, cols * Settings.BOX_HEIGHT);
                    Grid.SetRowSpan(internalBorder, Settings.BOX_HEIGHT);
                    Grid.SetColumn(internalBorder, rows * Settings.BOX_WIDTH);
                    Grid.SetColumnSpan(internalBorder, Settings.BOX_WIDTH);
                    sudokuGrid.Children.Add(internalBorder);

                }
            }

            //making the big border
            Border gridBorder = new Border();
            gridBorder.BorderThickness = new Thickness(Constants.BORDER_THICKNESS * 4);
            gridBorder.BorderBrush = Colors.BORDER;
            Grid.SetRowSpan(gridBorder, Settings.BOARD_SIDE);
            Grid.SetColumnSpan(gridBorder, Settings.BOARD_SIDE);
            sudokuGrid.Children.Add(gridBorder);

        }

        public static List<Cell> GetRelatedCells(int row, int column)
        {
            List<Cell> relatedCells = new List<Cell>();

            for (int c = 0; c < Settings.BOARD_SIDE; c++)
            {
                if (c != column)
                {
                    relatedCells.Add(cells[row, c]);
                }
            }

            for (int r = 0; r < Settings.BOARD_SIDE; r++)
            {
                if (r != r)
                {
                    relatedCells.Add(cells[r, column]);
                }
            }

            // Add cells in the same box
            int boxRowStart = (row / Settings.BOX_HEIGHT) * Settings.BOX_HEIGHT;
            int boxColStart = (column / Settings.BOX_WIDTH) * Settings.BOX_WIDTH;

            for (int r = boxRowStart; r < boxRowStart + Settings.BOX_HEIGHT; r++)
            {
                for (int c = boxColStart; c < boxColStart + Settings.BOX_WIDTH; c++)
                {
                    if (r != row || c != column)
                    {
                        relatedCells.Add(cells[r, c]);
                    }
                }
            }

            return relatedCells;
        }



    }
}
