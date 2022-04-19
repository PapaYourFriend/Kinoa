using Kinoa.Repositories.Interfaces;
using Kinoa.ViewModel.Base;
using Kinoa.ViewModel.Command;
using Kinoa.ViewModel.Common;
using Kinoa.ViewModel.Services;
using System;
using System.Windows.Controls;

namespace Kinoa.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IFilmRoomRepository _filmRoomRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMovieSessionRepository _movieSessionRepository;
        private readonly IUploadPicture _uploadPicture;
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IOrderDataRepository _orderDataRepository;
        private readonly IMessageBoxService _messageBoxService;
        private readonly IOpenDialog _openDialog;
        private BaseViewModel _showingViewModel;
        public Action CloseAction { get; set; }
        public MainWindowViewModel(
            IClientRepository clientRepository,
            IMovieRepository movieRepository,
            IFilmRoomRepository filmRoomRepository,
            IMovieSessionRepository movieSessionRepository,
            IMessageBoxService messageBoxService,
            IUploadPicture uploadPicture,
            IOrderRepository orderRepository,
            IPaymentMethodRepository paymentMethodRepository,
            IOrderDataRepository orderDataRepository,
            IOpenDialog openDialog)
        {
            _clientRepository = clientRepository;
            _movieRepository = movieRepository;
            _filmRoomRepository = filmRoomRepository;
            _movieSessionRepository = movieSessionRepository;
            _messageBoxService = messageBoxService;
            _uploadPicture = uploadPicture;
            _orderRepository = orderRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _orderDataRepository = orderDataRepository;
            _openDialog = openDialog;

            ShowingViewModel = new MainControlViewModel(this);
            ChangeCommand = new RelayCommand((selectedItem) =>
            {
                ListBoxItem item = selectedItem as ListBoxItem;
                if (item != null)
                {
                    switch (item.Name)
                    {
                        case "main":
                            ShowingViewModel = new MainControlViewModel(this);
                            break;
                        case "account":
                            ShowingViewModel = new ProfileViewModel(this);
                            break;
                    }
                }
            });

            UpdateOrders();
        }

        private void UpdateOrders()
        {
            var orders = _orderRepository.GetAllByCondition(o => o.ClientId == GodClient.ClientId, o => o.MovieSession);
            foreach (var order in orders)
            {
                if(order.MovieSession.SessionDate.Date < DateTime.Now.Date)
                {
                    order.OrderStatusId = (int)OrderStatuses.Completed;
                    _orderRepository.Update(order);
                }
            }
        }

        public string ClientName { get; set; } = GodClient.ClientName;
        public string Image { get; set; } = GodClient.Image;
        public RelayCommand ChangeCommand { get; set; }
        public BaseViewModel ShowingViewModel
        {
            get { return _showingViewModel; }
            set
            {
                _showingViewModel = value;
                OnPropertyChanged(nameof(ShowingViewModel));
            }
        }
        public IMovieRepository MovieRepository { get { return _movieRepository; } }
        public IClientRepository ClientRepository { get { return _clientRepository; } }
        public IFilmRoomRepository FilmRoomRepository { get { return _filmRoomRepository; } }
        public IMovieSessionRepository MovieSessionRepository { get { return _movieSessionRepository; } }
        public IOrderRepository OrderRepository { get { return _orderRepository; } }
        public IPaymentMethodRepository PaymentMethodRepository { get { return _paymentMethodRepository; } }
        public IOrderDataRepository OrderDataRepository { get { return _orderDataRepository; } }
        public IMessageBoxService MessageBoxService { get { return _messageBoxService; } }
        public IOpenDialog OpenDialog { get { return _openDialog; } }
        public IUploadPicture UploadPicture { get { return _uploadPicture; } }
    }
}
