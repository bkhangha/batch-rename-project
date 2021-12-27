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
            DialogResult = true;
            Close();
        }
    }
}