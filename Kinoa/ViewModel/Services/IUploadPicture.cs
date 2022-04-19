using System.Threading.Tasks;

namespace Kinoa.ViewModel.Services
{
    public interface IUploadPicture
    {
        string OpenFileDialog();
        Task<string> AddClientImageAsync(string filePath, int clientId);
    }
}
