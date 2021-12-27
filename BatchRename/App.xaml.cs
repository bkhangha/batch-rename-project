using System.Windows;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            BatchRename.Properties.Settings.Default.Save();
        }
    }
}