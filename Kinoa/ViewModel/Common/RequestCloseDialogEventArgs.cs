using System;

namespace Kinoa.ViewModel.Common
{
    public class RequestCloseDialogEventArgs : EventArgs
    {
        public bool DialogResult { get; set; }
        public RequestCloseDialogEventArgs(bool DialogResult)
        {
            this.DialogResult = DialogResult;
        }
    }
}