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

namespace Sudoko_wpf
{
    /// <summary>
    /// Interaction logic for InputBoxWindow.xaml
    /// </summary>
    public partial class InputBoxWindow : Window
    {
        public InputBoxWindow(string prompt, string defaultText = "")
        {
            InitializeComponent();
            PromptTextBlock.Text = prompt;
            InputTextBox.Text = defaultText;
        }

        public string InputText
        {
            get { return InputTextBox.Text; }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        public static string Show(string prompt, string defaultText = "")
        {
            InputBoxWindow window = new InputBoxWindow(prompt, defaultText);
            if (window.ShowDialog() == true)
            {
                return window.InputText;
            }
            return null;
        }
    }
}
