using System.Windows;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for RenameMethod.xaml
    /// </summary>
    public partial class AddSuffixMethodControl : Window
    {
        private AddSuffixArgs newArgs;

        public AddSuffixMethodControl(StringArgs args)
        {
            InitializeComponent();

            newArgs = args as AddSuffixArgs;
            SuffixTextBox.Text = newArgs.text;
        }

        private void BtnApplyArgs_Click(object sender, RoutedEventArgs e)
        {
            newArgs.text = SuffixTextBox.Text;
            DialogResult = true;
            Close();
        }
    }
}