using System.Windows;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for PresetControl.xaml
    /// </summary>
    public partial class PresetControl : Window
    {
        public static string presetName;

        public PresetControl()
        {
            InitializeComponent();
        }

        private void btnOk(object sender, RoutedEventArgs e)
        {
            presetName = txtName.Text;
            DialogResult = true;
            Close();
        }
    }
}