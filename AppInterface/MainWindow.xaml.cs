using AppInterface.Algorithms;
using AppInterface.WindowComponents;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
        public string CodeIn { get; private set; } = "";
        public string CodeOut { get; private set; } = "";

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
            {
                CodeIn = File.ReadAllText(openFileDialog.FileName);
                tboxCodeIn.Text = CodeIn;
            }
        }

        private void output_path_btn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = FILE_FILTER;
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, CodeOut);
            }
        }

        private void start_obfuscation_btn_Click(object sender, RoutedEventArgs e)
        {
            ObfuscationManager om = new ObfuscationManager(CodeIn);

            var algorithms = TakeCheckedAlgorithms();

            foreach (var algorithm in algorithms)
            {
                om.Obfuscate(algorithm);
            }

            CodeOut = om.GetSourceCode();
            tboxCodeOut.Text = CodeOut;
        }

        private void start_deobfuscation_btn_Click(object sender, RoutedEventArgs e)
        {
            DeobfuscationManager dm = new DeobfuscationManager(CodeIn);

            var algorithms = TakeCheckedAlgorithms();

            foreach(var algorithm in algorithms)
            {
                dm.Deobfuscate(algorithm);
            }

            CodeOut = Regex.Replace(dm.GetSourceCode(), @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline).TrimEnd(); ;
            tboxCodeOut.Text = CodeOut;
        }

        private IEnumerable<Algorithm> TakeCheckedAlgorithms()
        {
            return from cb in listAlgorithms.Items.Cast<CheckBox>()
                   where cb.IsEnabled && (cb.IsChecked ?? false)
                   select (Algorithm) cb.Tag;
        }

        public void ClearCodeOut(object sender, RoutedEventArgs e)
        {
            CodeOut = "";
            tboxCodeOut.Text = CodeOut;
        }

        public void MoveObfuscatedCode(object sender, RoutedEventArgs e)
        {
            CodeIn = CodeOut;
            tboxCodeIn.Text = CodeIn;
            ClearCodeOut(null, null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            input_path_btn_Click(sender, e);

        }
    }
}
