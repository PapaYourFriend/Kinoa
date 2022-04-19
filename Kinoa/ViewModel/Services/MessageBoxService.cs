using System.Windows.Forms;

namespace Kinoa.ViewModel.Services
{
    public class MessageBoxService : IMessageBoxService
    {
        public bool ShowMessageBox(string title, string text, MessageBoxButtons button, MessageBoxIcon image)
        {
            DialogResult result = MessageBox.Show(text, title, button, image);
            return DialogResult.Yes == result;
        }
    }
}
