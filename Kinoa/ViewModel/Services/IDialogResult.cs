using Kinoa.ViewModel.Common;
using System;

namespace Kinoa.ViewModel.Services
{
    public interface IDialogResult
    {
        event EventHandler<RequestCloseDialogEventArgs> RequestCloseDialog;
    }
}
