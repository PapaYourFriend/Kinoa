using Kinoa.Views.Windows;

namespace Kinoa.ViewModel.Services
{
    public class OpenDialog : IOpenDialog
    {
        public bool? ShowDialog(object DataContext)
        {
            var win = new DialogWindow();
            win.DataContext = DataContext;
            return win.ShowDialog();
        }
    }
}
