using Kinoa.Entities;
using Kinoa.ViewModel.Base;
using Kinoa.ViewModel.Command;
using Kinoa.ViewModel.Common;
using Kinoa.ViewModel.Models;
using Kinoa.ViewModel.Services;
using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Kinoa.ViewModel
{
    public class CreateOrderViewModel : BaseViewModel, IDialogResult
    {
        private readonly Lazy<DelegateCommand> _okCommand;
        private readonly MainWindowViewModel _main;
        private OrderForm _order;

        public CreateOrderViewModel(MainWindowViewModel main, OrderForm order)
        {
            _main = main;
            _order = order;
            _okCommand = new Lazy<DelegateCommand>(() =>
                new DelegateCommand(() =>
                    InvokeRequestCloseDialog(
                        new RequestCloseDialogEventArgs(true)), () => !Order.HasErrors));
        }
        public OrderForm Order
        {
            get { return _order; }
            set
            {
                _order = value;
                OnPropertyChanged(nameof(Order));
            }
        }
        public ICommand OkCommand
        {
            get
            {
                return _okCommand.Value;
            }
        }

        public event EventHandler<RequestCloseDialogEventArgs> RequestCloseDialog;
        private void InvokeRequestCloseDialog(RequestCloseDialogEventArgs e)
        {
            var handler = RequestCloseDialog;
            if (handler != null)
                handler(this, e);
        }
    }
}
