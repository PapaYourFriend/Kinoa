using Kinoa.ViewModel.Base;
using Kinoa.ViewModel.Command;
using Kinoa.ViewModel.Models;

namespace Kinoa.ViewModel
{
    public class CellViewModel : BaseViewModel
    {
        private bool _state = false;
        private bool _isEnabled = true;
        private RelayCommand _changeStateCommand;
        private SeatModel _seatModel;

        public CellViewModel(bool isEnabled, int rowNumber, int seatNumber, OrderForm order)
        {
            IsEnabled = isEnabled;
            RowNumber = rowNumber;
            SeatNumber = seatNumber;
            Order = order;
            _seatModel = new SeatModel(RowNumber, SeatNumber);
        }

        public bool State
        {
            get { return _state; }
            set
            {
                _state = value;
                OnPropertyChanged(nameof(State));
            }
        }
        public int RowNumber { get; }
        public int SeatNumber { get; }
        public OrderForm Order { get; set; }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        public RelayCommand ChangeStateCommand
        {
            get
            {
                return _changeStateCommand ?? (_changeStateCommand = new RelayCommand((o) =>
                {
                    if(!State)
                    {
                        Order.Seats.Add(_seatModel);
                        Order.Sum += Order.Price;
                    }
                    else
                    {
                        Order.Seats.Remove(_seatModel);
                        Order.Sum -= Order.Price;
                    }
                    State = !State;
                }));
            }
        }
    }
}
