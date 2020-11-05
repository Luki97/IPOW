using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace AppInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string FILE_FILTER = "C# file (*.cs)|*.cs";
        private String fileContent;
        private String outputPath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void input_path_btn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = FILE_FILTER;
            if (openFileDialog.ShowDialog() == true)
                fileContent = File.ReadAllText(openFileDialog.FileName);
        }

        private void output_path_btn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = FILE_FILTER;
            if (saveFileDialog.ShowDialog() == true)
                outputPath = saveFileDialog.FileName;
        }

        private void start_btn_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(outputPath, fileContent);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            input_path_btn_Click(sender, e);

        }
    }
}
