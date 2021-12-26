using System.Windows;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for NewCaseMethod.xaml
    /// </summary>
    public partial class NewCaseMethodControl : Window
    {
        private NewCaseArgs newArgs;

        public NewCaseMethodControl(StringArgs args)
        {
            InitializeComponent();
            newArgs = args as NewCaseArgs;
            NewCaseStyleCombobox.SelectedIndex = newArgs.style - 1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int style = NewCaseStyleCombobox.SelectedIndex + 1;
            newArgs.style = style;
            DialogResult = true;
            Close();
        }
    }
}