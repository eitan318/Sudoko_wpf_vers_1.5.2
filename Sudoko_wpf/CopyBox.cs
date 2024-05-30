using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Sudoko_wpf
{
    public class CopyBox
    {
        private string msg;
        public CopyBox(string msg)
        {
            this.msg = msg;
        }

        public void ShowMessageBox()
        {
            var messageBox = new Window
            {
                Title = "Copy puzzle code:",
                Height = 150,
                Width = 300,
                Content = new Grid
                {
                    Margin = new Thickness(10),
                    Children =
                    {
                        new TextBox
                        {
                            Text = msg,
                            IsReadOnly = true,
                            Margin = new Thickness(0, 0, 0, 30)
                        },
                        new Button
                        {
                            Content = "Copy",
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Bottom,
                            Margin = new Thickness(0, 0, 0, 10)
                        }
                    }
                }
            };

            var copyButton = ((Grid)messageBox.Content).Children[1] as Button;
            copyButton.Click += (sender, e) =>
            {
                Clipboard.SetText(msg);
            };

            messageBox.ShowDialog();

        }
    }
    
}
