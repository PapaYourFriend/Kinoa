using Kinoa.Entities;
using Kinoa.ViewModel.Base;
using Kinoa.ViewModel.Command;
using Kinoa.ViewModel.Common;
using Kinoa.ViewModel.Models;
using System.Collections.ObjectModel;
using System.IO;

namespace Kinoa.ViewModel
{
    internal class ProfileViewModel : BaseViewModel
    {
        private MainWindowViewModel _main;
        private ProfileModel _profile;
        private RelayCommand _changePicCommand;
        private RelayCommand _deleteCardCommand;
        private RelayCommand _updateProfileCommand;
        private RelayCommand _logOutCommand;
        private RelayCommand _cancelOrderCommand;
        private bool _select = true;
        private Client _cl;
        private bool _method = true;
        private Order _selectedOrder;

        public ProfileViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _main = mainWindowViewModel;
            ActiveOrders = new ObservableCollection<Order>(
                _main.OrderRepository.GetAllByCondition(o => o.ClientId == GodClient.ClientId && o.OrderStatusId == (int)OrderStatuses.Active,
                o => o.MovieSession, o => o.Data, o => o.MovieSession.Movie));
            CancelledOrders = new ObservableCollection<Order>(
                _main.OrderRepository.GetAllByCondition(o => o.ClientId == GodClient.ClientId && o.OrderStatusId == (int)OrderStatuses.Cancelled,
                o => o.MovieSession, o => o.Data, o => o.MovieSession.Movie));

            CompletedOrders = new ObservableCollection<Order>(
                _main.OrderRepository.GetAllByCondition(o => o.ClientId == GodClient.ClientId && o.OrderStatusId == (int)OrderStatuses.Completed,
                o => o.MovieSession, o => o.Data, o => o.MovieSession.Movie));

            _cl = _main.ClientRepository.GetEntityByConditionOrDefault(c => c.ClientId == GodClient.ClientId, c => c.ClientSecret);
            var pm = _main.PaymentMethodRepository.GetEntityByConditionOrDefault(c => c.ClientId == GodClient.ClientId);
            Profile = new ProfileModel(_cl, pm);
            if(pm is null)
            {
                Method = false;
            }
            else
            {
                Method = true;
            }

            ChangeCommand = new RelayCommand((selectedItem) =>
            {
                Select = !Select;
            });
        }
        public RelayCommand ChangeCommand { get; set; }
        public Order SelectedOrder
        {
            get { return _selectedOrder; }
            set
            {
                _selectedOrder = value;
                OnPropertyChanged(nameof(SelectedOrder));
            }
        }
        public ObservableCollection<Order> ActiveOrders { get; set; }
        public ObservableCollection<Order> CancelledOrders { get; set; }
        public ObservableCollection<Order> CompletedOrders { get; set; }
        public bool Method
        {
            get { return _method; }
            set
            {
                _method = value;
                OnPropertyChanged(nameof(Method));
            }
        }
        public bool Select
        {
            get { return _select; }
            set
            {
                _select = value;
                OnPropertyChanged(nameof(Select));
            }
        }

        public ProfileModel Profile
        {
            get { return _profile; }
            set
            {
                _profile = value;
                OnPropertyChanged(nameof(Profile));
            }
        }

        public RelayCommand ChangePicCommand
        {
            get
            {
                return _changePicCommand ??
                    (_changePicCommand = new RelayCommand(async (o) =>
                    {
                        var path = _main.UploadPicture.OpenFileDialog();
                        if (!string.IsNullOrEmpty(path))
                        {
                            try
                            {
                                var endPath = await _main.UploadPicture.AddClientImageAsync(path, GodClient.ClientId);
                                Profile.Image = endPath;
                            }
                            catch (IOException ex)
                            {
                                _main.MessageBoxService.ShowMessageBox(
                                    "Kinoa",
                                    ex.Message,
                                    System.Windows.Forms.MessageBoxButtons.OK,
                                    System.Windows.Forms.MessageBoxIcon.Warning);
                            }

                        }
                    }));
            }
        }

        public RelayCommand DeleteCardCommand
        {
            get
            {
                return _deleteCardCommand ??
                    (_deleteCardCommand = new RelayCommand((o) =>
                    {
                        if(_main.MessageBoxService.ShowMessageBox(
                                    "Kinoa",
                                    "Хотите удалить карту?",
                                    System.Windows.Forms.MessageBoxButtons.YesNo,
                                    System.Windows.Forms.MessageBoxIcon.Warning))
                        {
                            var card = _main.PaymentMethodRepository.GetEntityByConditionOrDefault(p => p.CardNumber == Profile.CardNumber);
                            if(card != null)
                            {
                                _main.PaymentMethodRepository.Delete(card);
                                Method = false;
                                _main.MessageBoxService.ShowMessageBox(
                                    "Kinoa",
                                    "Карта удалена",
                                    System.Windows.Forms.MessageBoxButtons.OK,
                                    System.Windows.Forms.MessageBoxIcon.Information);
                            }
                        }
                    }));
            }
        }
        public RelayCommand UpdateProfileCommand
        {
            get
            {
                return _updateProfileCommand ??
                    (_updateProfileCommand = new RelayCommand((o) =>
                    {
                        _cl.FirstName = Profile.FirstName;
                        _cl.LastName = Profile.LastName;
                        _cl.Image = Profile.Image;
                        _cl.ClientSecret.Email = Profile.Email;
                        _main.ClientRepository.Update(_cl);

                    }, (o) => !Profile.HasErrors));
            }
        }
        public RelayCommand CancelOrderCommand
        {
            get
            {
                return _cancelOrderCommand ??
                    (_cancelOrderCommand = new RelayCommand((o) =>
                    {
                        var order = o as Order;
                        if(order != null)
                        {
                            if(_main.MessageBoxService.ShowMessageBox(
                                    "Kinoa",
                                    "Хотите вернуть билеты?",
                                    System.Windows.Forms.MessageBoxButtons.YesNo,
                                    System.Windows.Forms.MessageBoxIcon.Warning))
                            {
                                order.OrderStatusId = (int)OrderStatuses.Cancelled;
                                _main.OrderRepository.Update(order);
                                ActiveOrders.Remove(order);
                                CancelledOrders.Add(order);
                            }
                        }
                        
                    }));
            }
        }

        public RelayCommand LogOutCommand
        {
            get
            {
                return _logOutCommand ??
                    (_logOutCommand = new RelayCommand((obj) =>
                    {
                        if (_main.MessageBoxService.ShowMessageBox(
                                    "Kinoa",
                                    "Хотите выйти?",
                                    System.Windows.Forms.MessageBoxButtons.YesNo,
                                    System.Windows.Forms.MessageBoxIcon.Question))
                        {
                            GodClient.ClientName = string.Empty;
                            _main.CloseAction();
                        }
                    }));
            }
        }
    }
}