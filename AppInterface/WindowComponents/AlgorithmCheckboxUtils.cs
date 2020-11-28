using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AppInterface.WindowComponents
{
    class AlgorithmCheckboxUtils
    {
        readonly CheckBox cbSelectAll;
        readonly ItemsControl listAlgorithms;

        public AlgorithmCheckboxUtils(ItemsControl listAlgorithms, CheckBox cbSelectAll)
        {
            this.listAlgorithms = listAlgorithms;
            this.cbSelectAll = cbSelectAll;
        }

        public CheckBox CreateCheckbox(AlgorithmType algorithmType)
        {
            var cb = new CheckBox
            {
                Content = CreateTextBox(algorithmType),
                IsEnabled = algorithmType.IsEnabled,
                Tag = algorithmType.Algorithm,
                Margin = new Thickness(3),
            };

            cb.Checked += OneChecked;
            cb.Unchecked += OneUnchecked;

            return cb;
        }

        private TextBlock CreateTextBox(AlgorithmType algorithmType)
        {
            return new TextBlock
            {
                Text = algorithmType.Text,
                Background = Brushes.Transparent,
                TextWrapping = TextWrapping.Wrap,
                FontSize = 14,
                IsEnabled = algorithmType.IsEnabled
            };
        }

        private void OneUnchecked(object sender, RoutedEventArgs e)
        {
            if (!cbSelectAll.IsChecked ?? false) { return; }

            if (listAlgorithms.Items.Cast<CheckBox>().Any(cb => cb.IsChecked ?? false))
            {
                cbSelectAll.IsChecked = null;
            }
            else
            {
                cbSelectAll.IsChecked = false;
            }
        }

        private void OneChecked(object sender, RoutedEventArgs e)
        {
            cbSelectAll.IsChecked = null;

            if (listAlgorithms.Items.Cast<CheckBox>().All(cb => !cb.IsEnabled || (cb.IsChecked ?? false)))
            {
                cbSelectAll.IsChecked = true;
            }
        }

        public void ToggleSelectAll(object sender, RoutedEventArgs e)
        {
            ActionWithoutClickHandler(() =>
            {
                if (cbSelectAll.IsChecked == null) { cbSelectAll.IsChecked = false; }
            });

            foreach (CheckBox cb in listAlgorithms.Items)
            {
                ActionWithoutCheckedHandler(
                    cb,
                    () => cb.IsChecked = (cbSelectAll.IsChecked ?? false) && cb.IsEnabled
                );
            }
        }

        private void ActionWithoutClickHandler(Action action)
        {
            cbSelectAll.Click -= ToggleSelectAll;

            action();

            cbSelectAll.Click += ToggleSelectAll;
        }

        private void ActionWithoutCheckedHandler(CheckBox cb, Action action)
        {
            cb.Checked -= OneChecked;
            cb.Unchecked -= OneUnchecked;

            action();

            cb.Checked += OneChecked;
            cb.Unchecked += OneUnchecked;
        }
    }
}
