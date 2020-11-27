using AppInterface.Algorithms;
using AppInterface.WindowComponents;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace AppInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // -----------------------------------------------------
        // Display

        private readonly AlgorithmCheckboxUtils checkboxUtils;

        // -----------------------------------------------------
        // Logic

        private const string FILE_FILTER = "C# file (*.cs)|*.cs";
        private String fileContent;
        private String outputPath;

        public MainWindow()
        {
            InitializeComponent();

            this.checkboxUtils = new AlgorithmCheckboxUtils(listAlgorithms, cbSelectAll);

            PopulateAlgorithmList();
            cbSelectAll.Click += this.checkboxUtils.ToggleSelectAll;
        }

        private void PopulateAlgorithmList()
        {
            listAlgorithms.Items.Clear();

            foreach (var algorithmType in AlgorithmType.Values)
            {
                listAlgorithms.Items.Add(this.checkboxUtils.CreateCheckbox(algorithmType));
            }
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
            ObfuscationManager om = new ObfuscationManager(fileContent);

            if (cbInsertDeadCode.IsChecked ?? false)
            {
                om.InsertDeadCodeIntoMethods();
            }

            if (cbNumericTypeChange.IsChecked ?? false)
            {
                om.ChangeNumericTypes();
            }

            if(cbChangeNames.IsChecked ?? false)
            {
                om.ChangeMethodNames();
                om.ChangeClassNames();
            }

            fileContent = om.GetSourceCode();
            File.WriteAllText(outputPath, fileContent);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            input_path_btn_Click(sender, e);

        }
    }
}
