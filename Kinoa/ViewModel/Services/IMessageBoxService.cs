using System.Windows.Forms;

namespace Kinoa.ViewModel.Services
{
    public interface IMessageBoxService
    {
        bool ShowMessageBox(string title, string text, MessageBoxButtons button, MessageBoxIcon image);
    }
}
