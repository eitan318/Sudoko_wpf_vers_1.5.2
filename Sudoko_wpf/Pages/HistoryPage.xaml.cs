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
    /// Interaction logic for HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        List<Border> Items = new List<Border>();
        public HistoryPage()
        {
            InitializeComponent();
            AddItemToList(3, 5, "00:10:00", "2024-06-08", true);
            AddItemToList(3, 5, "00:10:00", "2024-06-08", true);
            AddItemToList(3, 5, "00:10:00", "2024-06-08", true);
            AddItemToList(3, 5, "00:10:00", "2024-06-08", true);
            AddItemToList(3, 5, "00:10:00", "2024-06-08", true);
            AddItemToList(3, 5, "00:10:00", "2024-06-08", true);
            AddItemToList(3, 5, "00:10:00", "2024-06-08", true);
            AddItemToList(3, 5, "00:10:00", "2024-06-08", true);
            AddItemToList(3, 5, "00:10:00", "2024-06-08", true);



        }


        private void AddItemToList(int hints, int checks, string time, string date, bool solved)
        {
            Border border = new Border
            {
                Margin = new Thickness(HistoryConstants.MARGIN),
                Background = BrushResources.HistoryItem_BG,
                Height = HistoryConstants.HEIGHT,
                CornerRadius = new CornerRadius(HistoryConstants.CORNER_RADIUS),
                Tag = ItemsPanel.Children.Count // Assuming ItemsPanel is a StackPanel
            };

            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            // Add TextBlocks for each parameter
            TextBlock hintsTextBlock = new TextBlock
            {
                Text = hints.ToString(),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = HistoryConstants.RELATIVE_FONT_SIZE * border.Height,
                Foreground = BrushResources.TextFore
            };
            Grid.SetColumn(hintsTextBlock, 0);

            TextBlock checksTextBlock = new TextBlock
            {
                Text = checks.ToString(),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = HistoryConstants.RELATIVE_FONT_SIZE * border.Height,
                Foreground = BrushResources.TextFore
            };
            Grid.SetColumn(checksTextBlock, 1);

            TextBlock timeTextBlock = new TextBlock
            {
                Text = time,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = HistoryConstants.RELATIVE_FONT_SIZE * border.Height,
                Foreground = BrushResources.TextFore
            };
            Grid.SetColumn(timeTextBlock, 2);

            TextBlock dateTextBlock = new TextBlock
            {
                Text = date,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = HistoryConstants.RELATIVE_FONT_SIZE * border.Height,
                Foreground = BrushResources.TextFore
            };
            Grid.SetColumn(dateTextBlock, 3);

            TextBlock solvedTextBlock = new TextBlock
            {
                Text = solved ? "solved" : "failed",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = HistoryConstants.RELATIVE_FONT_SIZE * border.Height,
                Foreground = BrushResources.TextFore
            };
            Grid.SetColumn(solvedTextBlock, 4);

            // Add TextBlocks to the Grid
            grid.Children.Add(hintsTextBlock);
            grid.Children.Add(checksTextBlock);
            grid.Children.Add(timeTextBlock);
            grid.Children.Add(dateTextBlock);
            grid.Children.Add(solvedTextBlock);

            // Add Grid to the Border
            border.Child = grid;

            // Add Border to the ItemsPanel
            ItemsPanel.Children.Add(border);
        }
    }
}
