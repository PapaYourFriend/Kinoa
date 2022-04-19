using Kinoa.Entities;
using Kinoa.Repositories.Interfaces;
using Kinoa.ViewModel.Base;
using Kinoa.ViewModel.Command;
using Kinoa.ViewModel.Common;
using Kinoa.ViewModel.Models;
using Kinoa.ViewModel.Services;
using System;

namespace Kinoa.ViewModel
{
    public class AuthViewModel : BaseViewModel
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMessageBoxService _messageBoxService;
        private RelayCommand _loginCommand;
        private RelayCommand _registerCommand;

        public AuthViewModel(
            IClientRepository clientRepository,
            IMessageBoxService messageBoxService)
        {
            _clientRepository = clientRepository;
            _messageBoxService = messageBoxService;
        }
        public Action CloseAction { get; set; }
        public bool IsLoggedIn { get; set; } = false;

        public LoginForm Login { get; set; } = new LoginForm();
        public RegisterForm Register { get; set; } = new RegisterForm();

        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new RelayCommand((o) =>
                {
                    var client = _clientRepository.GetEntityByConditionOrDefault(c => c.ClientSecret.Login == Login.Login, c => c.ClientSecret);

                    if (client is null)
                    {
                        _messageBoxService.ShowMessageBox(
                                    "Login",
                                    "Логин введен неверно",
                                    System.Windows.Forms.MessageBoxButtons.OK,
                                    System.Windows.Forms.MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (client.IsDeleted)
                        {
                            _messageBoxService.ShowMessageBox(
                                    "Login",
                                    "Аккаунт был удален! Восстановите его повторным прохождением регистрации",
                                    System.Windows.Forms.MessageBoxButtons.OK,
                                    System.Windows.Forms.MessageBoxIcon.Information);
                        }
                        else if (BCrypt.Net.BCrypt.Verify(Login.Password, client.ClientSecret.PasswordEncoded))
                        {
                            GodClient.ClientId = client.ClientId;
                            GodClient.Image = client.Image;
                            GodClient.ClientName = client.FirstName;
                            IsLoggedIn = true;
                            CloseAction();
                        }
                        else
                        {
                            _messageBoxService.ShowMessageBox(
                                    "Login",
                                    "Пароль введен неверно",
                                    System.Windows.Forms.MessageBoxButtons.OK,
                                    System.Windows.Forms.MessageBoxIcon.Information);
                        }
                    }

                }, (o) => !Login.HasErrors));
            }
        }

        public RelayCommand RegisterCommand
        {
            get
            {
                return _registerCommand ?? (_registerCommand = new RelayCommand((o) =>
                {
                    if(!string.IsNullOrEmpty(Register.Login) && !string.IsNullOrEmpty(Register.Email) && !string.IsNullOrEmpty(Register.Name))
                    {
                        var client = _clientRepository.GetEntityByConditionOrDefault(c => c.ClientSecret.Login == Register.Login, c => c.ClientSecret);

                        if (client != null)
                        {

                            if (client.IsDeleted)
                            {
                                if (BCrypt.Net.BCrypt.Verify(Register.Password, client.ClientSecret.PasswordEncoded))
                                {
                                    client.IsDeleted = false;
                                    _clientRepository.Update(client);
                                    _messageBoxService.ShowMessageBox(
                                        "Register",
                                        "Аккаунт восстановлен",
                                        System.Windows.Forms.MessageBoxButtons.OK,
                                        System.Windows.Forms.MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                _messageBoxService.ShowMessageBox(
                                        "Register",
                                        "Пользователь с таким логином уже существует",
                                        System.Windows.Forms.MessageBoxButtons.OK,
                                        System.Windows.Forms.MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            var clientNew = new Client
                            {
                                FirstName = Register.Name,
                                IsDeleted = false,
                                RegistrationDate = DateTime.Now,
                                Image = "/Views/Images/Oxxx.jpg",
                                ClientSecret = new ClientSecret
                                {
                                    Login = Register.Login,
                                    Email = Register.Email,
                                    PasswordEncoded = BCrypt.Net.BCrypt.HashPassword(Register.Password)
                                }
                            };

                            _clientRepository.Add(clientNew);

                            _messageBoxService.ShowMessageBox(
                                        "Register",
                                        "Зарегистрирован",
                                        System.Windows.Forms.MessageBoxButtons.OK,
                                        System.Windows.Forms.MessageBoxIcon.Information);
                        }
                    }
                }, (o) => !Register.HasErrors));
            }
        }
    }
}
