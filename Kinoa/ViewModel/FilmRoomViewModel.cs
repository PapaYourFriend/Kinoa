using Kinoa.Entities;
using Kinoa.ViewModel.Base;
using Kinoa.ViewModel.Command;
using Kinoa.ViewModel.Common;
using Kinoa.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace Kinoa.ViewModel
{
    public class FilmRoomViewModel : BaseViewModel
    {
        private readonly MainWindowViewModel _main;
        private readonly BaseViewModel _back;
        private RelayCommand _backCommand;
        private FilmRoom _selectedRoom;
        private DateModel _selectedDate;
        private DateModel _selectedSession;
        private Order[] _orders;
        private ObservableCollection<ObservableCollection<CellViewModel>> _cells;
        private OrderForm _currentOrder;
        private RelayCommand _buyTicketsCommand;
        private RelayCommand _expandCommand;

        public FilmRoomViewModel(
            MainWindowViewModel main,
            BaseViewModel back,
            Movie movie)
        {
            _main = main;
            _back = back;
            Showing = movie;
            MovieSessions = _main.MovieSessionRepository.GetAllByCondition(s => s.MovieId == movie.MovieId, s => s.FilmRoom)
                .OrderBy(s => s.SessionStartTime)
                .ToArray();
            SessionTimes = new ObservableCollection<DateModel>();
            CurrentOrder = new OrderForm { ClientId = GodClient.ClientId };
            FilmRooms = new List<FilmRoom>();
            Dates = new List<DateModel>();
            DateTime start = DateTime.Now;
            DateTime end = start.AddDays(7);

            for (var dt = start; dt <= end; dt = dt.AddDays(1))
            {
                Dates.Add(new DateModel { Date = dt });
                if (SelectedDate is null)
                {
                    _selectedDate = Dates.First();
                }
            }

            foreach (var item in MovieSessions)
            {
                if (FilmRooms.FirstOrDefault(f => f.FilmRoomId == item.FilmRoomId) is null)
                {
                    FilmRooms.Add(item.FilmRoom);
                    if (SelectedRoom is null)
                    {
                        SelectedRoom = item.FilmRoom;
                        CurrentOrder.Price = SelectedRoom.Price;
                    }
                }
            }
        }

        private ObservableCollection<ObservableCollection<CellViewModel>> CreateCells(int rows, int columns, Order[] orders)
        {
            var cells = new ObservableCollection<ObservableCollection<CellViewModel>>();
            for (var posRow = 0; posRow < rows; posRow++)
            {
                var row = new ObservableCollection<CellViewModel>();
                for (var posCol = 0; posCol < columns; posCol++)
                {
                    if (orders.Any(o => o.Data.Any(d => d.RowNumber == posRow + 1 && d.SeatNumber == posCol + 1)))
                    {
                        var cellViewModel = new CellViewModel(false, posRow + 1, posCol + 1, _currentOrder);
                        row.Add(cellViewModel);
                    }
                    else
                    {
                        var cellViewModel = new CellViewModel(true, posRow + 1, posCol + 1, _currentOrder);
                        row.Add(cellViewModel);
                    }
                }
                cells.Add(row);
            }
            return cells;
        }
        public OrderForm CurrentOrder
        {
            get { return _currentOrder; }
            set
            {
                _currentOrder = value;
                OnPropertyChanged(nameof(CurrentOrder));
            }
        }
        public ObservableCollection<ObservableCollection<CellViewModel>> Cells
        {
            get { return _cells; }
            set
            {
                _cells = value;
                OnPropertyChanged(nameof(Cells));
            }
        }

        public FilmRoom SelectedRoom
        {
            get { return _selectedRoom; }
            set
            {
                _selectedRoom = value;
                CurrentOrder.Price = _selectedRoom.Price;
                SessionTimes.Clear();
                SelectedSession = null;
                foreach (var item in MovieSessions)
                {
                    if (item.SessionDate.Date.Equals(_selectedDate.Date.Date) && _selectedRoom.FilmRoomId == item.FilmRoomId)
                    {
                        SessionTimes.Add(new DateModel { Date = item.SessionStartTime, SessionId = item.MovieSessionId });
                        SelectedSession = SessionTimes.First();
                    }
                }
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }
        public DateModel SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                SessionTimes.Clear();
                SelectedSession = null;
                foreach (var item in MovieSessions)
                {
                    if (item.SessionDate.Date.Equals(_selectedDate.Date.Date) && _selectedRoom.FilmRoomId == item.FilmRoomId)
                    {
                        SessionTimes.Add(new DateModel { Date = item.SessionStartTime, SessionId = item.MovieSessionId });
                        SelectedSession = SessionTimes.First();
                    }
                }
                OnPropertyChanged(nameof(SelectedDate));
            }
        }
        public DateModel SelectedSession
        {
            get { return _selectedSession; }
            set
            {
                _selectedSession = value;
                CurrentOrder.Seats.Clear();
                CurrentOrder.Sum = 0;
                if (_selectedSession != null)
                {
                    _orders = _main.OrderRepository.GetAllByCondition(o => o.MovieSessionId == _selectedSession.SessionId 
                        && o.OrderStatusId == (int)OrderStatuses.Active, o => o.Data);
                    Cells = CreateCells(_selectedRoom.Rows, SelectedRoom.SeatsInRow, _orders);
                    CurrentOrder.SessionId = _selectedSession.SessionId;
                }
                else
                {
                    Cells = null;
                }
                OnPropertyChanged(nameof(SelectedSession));
            }
        }
        public List<DateModel> Dates { get; set; }
        public ObservableCollection<DateModel> SessionTimes { get; set; }
        public MovieSession[] MovieSessions { get; set; }
        public List<FilmRoom> FilmRooms { get; set; }

        public Movie Showing { get; set; }

        public RelayCommand BackCommand
        {
            get
            {
                return _backCommand ??
                    (_backCommand = new RelayCommand((o) =>
                    {
                        _main.ShowingViewModel = _back;
                    }));
            }
        }

        public RelayCommand BuyTicketsCommand
        {
            get
            {
                return _buyTicketsCommand ??
                    (_buyTicketsCommand = new RelayCommand((o) =>
                    {
                        MakeOrder();
                    }));
            }
        }

        public RelayCommand ExpandCommand
        {
            get
            {
                return _expandCommand ??
                    (_expandCommand = new RelayCommand((o) =>
                    {
                        DialogViewModel dialog = new DialogViewModel(new BigRoomViewModel(CurrentOrder, Cells));
                        bool result = (bool)_main.OpenDialog.ShowDialog(dialog);
                        if(result)
                        {
                            MakeOrder();
                        }
                    }));
            }
        }

        private void MakeOrder()
        {
            if (CurrentOrder.Seats.Count > 0)
            {
                var method = _main.PaymentMethodRepository.GetEntityByConditionOrDefault(p => p.ClientId == GodClient.ClientId);
                if (method != null)
                {
                    CurrentOrder.CardNumber = method.CardNumber;
                    CurrentOrder.ExpireMonth = method.ExpireMonth.ToString();
                    CurrentOrder.ExpireYear = method.ExpireYear.ToString();
                    CurrentOrder.CardHolderName = method.CardHolderName;
                }
                DialogViewModel dialog = new DialogViewModel(new CreateOrderViewModel(_main, CurrentOrder));
                bool result = (bool)_main.OpenDialog.ShowDialog(dialog);
                if (result)
                {
                    var paymentMethod = new PaymentMethod
                    {
                        CardNumber = CurrentOrder.CardNumber,
                        CardHolderName = CurrentOrder.CardHolderName,
                        ClientId = CurrentOrder.ClientId,
                        CVVEncoded = BCrypt.Net.BCrypt.HashPassword(CurrentOrder.CVV),
                        ExpireMonth = int.Parse(CurrentOrder.ExpireMonth),
                        ExpireYear = int.Parse(CurrentOrder.ExpireYear)
                    };
                    if (method is null)
                    {
                        method = _main.PaymentMethodRepository.Add(paymentMethod);
                    }
                    if (BCrypt.Net.BCrypt.Verify(CurrentOrder.CVV, method.CVVEncoded))
                    {
                        var newOrder = new Order
                        {
                            ClientId = CurrentOrder.ClientId,
                            CreatedDate = DateTime.Now,
                            MovieSessionId = CurrentOrder.SessionId,
                            OrderStatusId = (int)OrderStatuses.Active,
                            Sum = CurrentOrder.Sum
                        };
                        _main.OrderRepository.Add(newOrder);
                        foreach (var item in CurrentOrder.Seats)
                        {
                            var orderData = new OrderData
                            {
                                OrderId = newOrder.OrderId,
                                RowNumber = item.Row,
                                SeatNumber = item.SeatInRow
                            };
                            Cells[item.Row - 1][item.SeatInRow - 1].IsEnabled = false;
                            _main.OrderDataRepository.Add(orderData);
                        }
                        _main.MessageBoxService.ShowMessageBox(
                            "Kinoa",
                            "Заказ оформлен",
                            System.Windows.Forms.MessageBoxButtons.OK,
                            System.Windows.Forms.MessageBoxIcon.Information);
                    }
                    else
                    {
                        _main.MessageBoxService.ShowMessageBox(
                            "Kinoa",
                            "Данные карты неверны",
                            System.Windows.Forms.MessageBoxButtons.OK,
                            System.Windows.Forms.MessageBoxIcon.Warning);
                    }
                }
            }
        }
    }
}
