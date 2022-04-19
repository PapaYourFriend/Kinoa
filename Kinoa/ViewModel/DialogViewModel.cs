using Kinoa.ViewModel.Base;

namespace Kinoa.ViewModel
{
    public class DialogViewModel : BaseViewModel
    {
        public BaseViewModel ShowingViewModel { get; set; }
        public DialogViewModel(BaseViewModel vm)
        {
            ShowingViewModel = vm;
        }
    }
}
