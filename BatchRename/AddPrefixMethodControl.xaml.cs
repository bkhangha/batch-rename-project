using System.Windows;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for RenameMethod.xaml
    /// </summary>
    public partial class AddPrefixMethodControl : Window
    {
        private AddPrefixArgs newArgs;

        public AddPrefixMethodControl(StringArgs args)
        {
            InitializeComponent();

            newArgs = args as AddPrefixArgs;
            PrefixTextBox.Text = newArgs.text;
        }

        private void BtnApplyArgs_Click(object sender, RoutedEventArgs e)
        {
            newArgs.text = PrefixTextBox.Text;

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