using System.Windows;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for RenameMethod.xaml
    /// </summary>
    public partial class AddCounterMethodControl : Window
    {
        private AddCounterArgs newArgs;

        public AddCounterMethodControl(StringArgs args)
        {
            InitializeComponent();

            newArgs = args as AddCounterArgs;
            StartTextBox.Text = newArgs.start;
            StepTextBox.Text = newArgs.step;
            NumDigitTextBox.Text = newArgs.num;
        }

        private void BtnApplyArgs_Click(object sender, RoutedEventArgs e)
        {
            newArgs.start = StartTextBox.Text;
            newArgs.step = StepTextBox.Text;
            newArgs.num = NumDigitTextBox.Text;
            DialogResult = true;
            Close();
        }
    }
}