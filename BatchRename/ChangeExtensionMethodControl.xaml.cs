using System.Windows;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for RenameMethod.xaml
    /// </summary>
    public partial class ChangeExtensionMethodControl : Window
    {
        private ChangeExtensionArgs newArgs;

        public ChangeExtensionMethodControl(StringArgs args)
        {
            InitializeComponent();

            newArgs = args as ChangeExtensionArgs;
            FromTextBox.Text = newArgs.From;
            ToTextbox.Text = newArgs.To;
        }

        private void BtnApplyArgs_Click(object sender, RoutedEventArgs e)
        {
            newArgs.From = FromTextBox.Text;
            newArgs.To = ToTextbox.Text;
            DialogResult = true;
            Close();
        }
    }
}