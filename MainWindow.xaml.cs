using Microsoft.Win32;

using System;
using System.Windows;
using System.Windows.Input;

namespace LogViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly LineReader lineReader;

        public MainWindow()
        {
            InitializeComponent();
            lineReader = new LineReader();
        }

        private void OpenFile(string filename)
        {
            if (lineReader.FileOpened)
            {
                lineReader.CloseFile();
            }

            lineReader.OpenFile(filename);
        }

        private void CloseFile()
        {
            if (!lineReader.FileOpened) return;

            lineReader.CloseFile();
            LogTextBox.Clear();
        }

        private void ReadLines()
        {
            try
            {
                var lines = lineReader.ReadLines(10);
                LogTextBox.Text = string.Join(Environment.NewLine, lines);
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show("Try opening a file first.", "Error reading lines", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClickOnOpenFile(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|Log files (*.log)|*.log|All files (*.*)|*.*",
                Title = "Open Log File"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filename = openFileDialog.FileName;
                OpenFile(filename);
            }
        }

        private void ClickOnCloseFile(object sender, RoutedEventArgs e)
        {
            CloseFile();
        }

        private void ClickOnCut(object sender, RoutedEventArgs e)
        {
            ApplicationCommands.Cut.Execute(null, Keyboard.FocusedElement);
        }

        private void ClickOnCopy(object sender, RoutedEventArgs e)
        {
            ApplicationCommands.Copy.Execute(null, Keyboard.FocusedElement);
        }

        private void ClickOnPaste(object sender, RoutedEventArgs e)
        {
            ApplicationCommands.Paste.Execute(null, Keyboard.FocusedElement);
        }

        private void ClickOnDelete(object sender, RoutedEventArgs e)
        {
            ApplicationCommands.Delete.Execute(null, Keyboard.FocusedElement);
        }

        private void ClickOnSelectAll(object sender, RoutedEventArgs e)
        {
            ApplicationCommands.SelectAll.Execute(null, Keyboard.FocusedElement);
        }

        private void KeyDownOnInputText(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ReadLines();
            }
        }

        private void ClickOnInputEnterButton(object sender, RoutedEventArgs e)
        {
            ReadLines();
        }
    }
}
