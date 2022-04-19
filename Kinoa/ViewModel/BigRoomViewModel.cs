using Kinoa.ViewModel.Base;
using Kinoa.ViewModel.Common;
using Kinoa.ViewModel.Models;
using Kinoa.ViewModel.Services;
using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Kinoa.ViewModel
{
    public class BigRoomViewModel : BaseViewModel, IDialogResult
    {
        private readonly Lazy<DelegateCommand> _okCommand;

        public BigRoomViewModel(OrderForm currentOrder, ObservableCollection<ObservableCollection<CellViewModel>> cells)
        {
            _okCommand = new Lazy<DelegateCommand>(() =>
                new DelegateCommand(() =>
                    InvokeRequestCloseDialog(
                        new RequestCloseDialogEventArgs(true))));
            CurrentOrder = currentOrder;
            Cells = cells;
        }

        public ICommand OkCommand
        {
            get
            {
                return _okCommand.Value;
            }
        }

        public OrderForm CurrentOrder { get; set; }
        public ObservableCollection<ObservableCollection<CellViewModel>> Cells { get; set; }

        public event EventHandler<RequestCloseDialogEventArgs> RequestCloseDialog;
        private void InvokeRequestCloseDialog(RequestCloseDialogEventArgs e)
        {
            var handler = RequestCloseDialog;
            if (handler != null)
                handler(this, e);
        }
    }
}
