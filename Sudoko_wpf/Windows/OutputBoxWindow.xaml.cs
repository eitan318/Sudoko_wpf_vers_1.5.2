using Sudoko_wpf;
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
using System.Windows.Shapes;

namespace Sudoko_wpf
{
    /// <summary>
    /// Interaction logic for OutputBoxWindow.xaml
    /// </summary>
    public partial class OutputBoxWindow : Window
    {
        public OutputBoxWindow(string outputText)
        {
            InitializeComponent();
            OutputTextBlock.Text = outputText;
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(OutputTextBlock.Text);
            MessageBox.Show("Text copied to clipboard.");
        }

        public static void Show(string outputText)
        {
            OutputBoxWindow window = new OutputBoxWindow(outputText);
            window.ShowDialog();
        }
    }
}

