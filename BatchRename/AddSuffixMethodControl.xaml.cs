﻿using System;
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

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for RenameMethod.xaml
    /// </summary>
    public partial class AddSuffixMethodControl : Window
    {
        AddSuffixArgs newArgs;
        public AddSuffixMethodControl(StringArgs args)
        {
            InitializeComponent();

            newArgs = args as AddSuffixArgs;
            SuffixTextBox.Text = newArgs.text;
        }

        private void BtnApplyArgs_Click(object sender, RoutedEventArgs e)
        {
            newArgs.text = SuffixTextBox.Text;

            //var style = StyleCombobox.SelectedIndex;
            //if (style == 0)
            //{
            //    newArgs.Area = 1;
            //}
            //else
            //{
            //    newArgs.Area = 2;
            //}
            DialogResult = true;
            Close();
        }
    }
}
