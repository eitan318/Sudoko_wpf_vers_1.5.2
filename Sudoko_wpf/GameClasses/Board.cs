using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Data.Common;
using Sudoko_wpf.publico;
using static Sudoko_wpf.publico.Constants;

namespace Sudoko_wpf.GameClasses
{
    public class Board
    {
        private static Cell[,] cells;
        private static Puzzle puzzle;
        public Grid mainGrid;

        public Board(Grid mainGrid, Puzzle Ppuzzle)
        {
            this.mainGrid = mainGrid;
            cells = new Cell[Settings.BOARD_SIDE, Settings.BOARD_SIDE];
            puzzle = Ppuzzle;

            CreateSudokuGrid(mainGrid);
            initialize();
        }

        public static void VisualizeState(Cell focusCell, bool validateForeground, string MarkText = "")
        {
            if (MarkText == "")
                MarkText = focusCell.Text;

            focusCell.Foreground = BrushResources.TextFore;

            foreach (Cell cell in cells)
            {
                if (cell != focusCell)
                {
                    cell.ColorBy(focusCell, MarkText, validateForeground);
                }
            }

            focusCell.Background = BrushResources.Focus;
        }

        private static bool IsSolved()
        {
            foreach (Cell cell in cells)
            {
                if(cell.Text == "" || cell.Foreground == BrushResources.WrongForeground || cell.Background == BrushResources.WrongBackground)
                {
                    return false;
                }
            }
            return true;

        }

        public static void ForSolvedAnimation()
        {
            if (IsSolved())
            {
                Game.TimerStop();
                ShowSolvedAnimation();
            }
        }

        private static void ShowSolvedAnimation()
        {
            foreach (Cell cell in cells)
            {
                cell.Background = BrushResources.RightBackground;
                cell.IsReadOnly = true;
            }
        }


        public static void CheckMyBoard()
        {
            foreach (Cell cell in cells)
            {
                if (cell.Text == puzzle.CellValue(cell.row, cell.column).ToString())
                {
                    cell.IsReadOnly = true;
                    cell.Background = BrushResources.RightBackground;
                }
                else if (cell.Text != "")
                {
                    cell.Background = BrushResources.WrongBackground;
                }
                else
                {
                    cell.BorderBrush = BrushResources.Border;
                }
            }
        }

        public static void MoveFocusToTextBox(int row, int column)
        {
            Cell nextCell = cells[row, column];
            nextCell.Focus();
        }

        public static bool CellMustValid(int row, int col)
        {
            return cells[row, col].Text == "" || puzzle.CellValue(row, col).ToString() == cells[row, col].Text;
        }

        public void ShowSolution()
        {
            foreach (Cell cell in cells)
            {
                cell.DettachEventHandlers();
                cell.Text = puzzle.CellValue(cell.row, cell.column).ToString();
                cell.IsReadOnly = true;
                cell.Background = BrushResources.Board;
                cell.BorderBrush = BrushResources.Border;
                cell.Foreground = BrushResources.TextFore;
                cell.AttachEventHandlers();
            }
        }

        public static Cell focusedCell()
        {
            foreach (Cell cell in cells)
            {
                if (cell.Background == BrushResources.Focus)
                {
                    return cell;
                }
            }
            return null;
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
                        cell.Text = puzzle.CellValue(i, j).ToString();
                        cell.IsReadOnly = true;
                        cell.FontWeight = FontWeights.Bold;
                    }
                    else
                    {
                        cell.IsReadOnly = false;
                        cell.Text = "";
                    }
                    cell.Background = BrushResources.Board;
                    cell.AttachEventHandlers();
                }
            }
        }

        private void CreateSudokuGrid(Grid mainGrid)
        {
            Grid sudokuGrid = mainGrid;
            //sudokuGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            //sudokuGrid.VerticalAlignment = VerticalAlignment.Stretch;
            //sudokuGrid.HorizontalAlignment = HorizontalAlignment.Center;
            //sudokuGrid.VerticalAlignment = VerticalAlignment.Center;
            //sudokuGrid.Height = BoardConstants.BOARD_WIDTH;
            //sudokuGrid.Width = BoardConstants.BOARD_WIDTH;
            //Grid.SetColumn(sudokuGrid, 1);
            //Grid.SetRow(sudokuGrid, 1);

            CreateSeperation(sudokuGrid);
            CreateCells(sudokuGrid);
            AddBorders(sudokuGrid);

            //mainGrid.Children.Add(sudokuGrid);
        }

        private void CreateSeperation(Grid sudokuGrid)
        {
            //creating seperations
            for (int i = 0; i < Settings.BOARD_SIDE; i++)
            {
                sudokuGrid.RowDefinitions.Add(new RowDefinition());
                sudokuGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        private void CreateCells(Grid sudokuGrid)
        {
            for (int i = 0; i < Settings.BOARD_SIDE; i++)
            {
                for (int j = 0; j < Settings.BOARD_SIDE; j++)
                {
                    Cell cell = new Cell(i, j);


                    sudokuGrid.Children.Add(cell);
                    sudokuGrid.Children.Add(cell.notesGrid);

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
                    internalBorder.BorderThickness = new Thickness(BoardConstants.REGULAR_BORDER_THICKNESS * BoardConstants.INTERNAL_BORDER_TO_REGULAR);
                    internalBorder.BorderBrush = BrushResources.Border;
                    Grid.SetRow(internalBorder, cols * Settings.BOX_HEIGHT);
                    Grid.SetRowSpan(internalBorder, Settings.BOX_HEIGHT);
                    Grid.SetColumn(internalBorder, rows * Settings.BOX_WIDTH);
                    Grid.SetColumnSpan(internalBorder, Settings.BOX_WIDTH);
                    sudokuGrid.Children.Add(internalBorder);

                }
            }

            //making the big border
            Border gridBorder = new Border();
            gridBorder.BorderThickness = new Thickness(BoardConstants.REGULAR_BORDER_THICKNESS * BoardConstants.EXTERNAL_BORDER_TO_REGULAR);
            gridBorder.BorderBrush = BrushResources.Border;
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
            int boxRowStart = row / Settings.BOX_HEIGHT * Settings.BOX_HEIGHT;
            int boxColStart = column / Settings.BOX_WIDTH * Settings.BOX_WIDTH;

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
