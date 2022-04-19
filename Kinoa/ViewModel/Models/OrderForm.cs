using Kinoa.ViewModel.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Kinoa.ViewModel.Models
{
    public class OrderForm : ValidateModel
    {
        public int ClientId { get; set; }
        public int SessionId { get; set; }
        public int OrderStatusId { get; set; }
        public decimal Price { get; set; }
        private ObservableCollection<SeatModel> _seats = new ObservableCollection<SeatModel>();
        public DateTime CreatedDate { get; set; }
        private decimal _sum = 0;
        private string _cardNumber;
        private string _cardHolderName;
        private string _CVV;
        private string _expireMonth;
        private string _expireYear;

        public decimal Sum
        {
            get { return _sum; }
            set
            {
                _sum = value;
                OnPropertyChanged(nameof(Sum));
            }
        }

        public ObservableCollection<SeatModel> Seats
        {
            get { return _seats; }
            set
            {
                _seats = value;
                OnPropertyChanged(nameof(Seats));
            }
        }
        [Required(ErrorMessage = "Обязательно")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Только цифры")]
        [MinLength(16, ErrorMessage = "16 цифр")]
        public string CardNumber
        {
            get { return _cardNumber; }
            set
            {
                _cardNumber = value;
                OnPropertyChanged(nameof(CardNumber));
            }
        }
        [Required(ErrorMessage = "Обязательно")]
        public string CardHolderName
        {
            get { return _cardHolderName; }
            set
            {
                _cardHolderName = value.ToUpper();
                OnPropertyChanged(nameof(CardHolderName));
            }
        }
        [Required(ErrorMessage = "Обязательно")]
        public string CVV
        {
            get { return _CVV; }
            set
            {
                _CVV = value;
                int p;
                if (!int.TryParse(value, out p))
                {
                    _CVV = string.Empty;
                }
                OnPropertyChanged(nameof(CVV));
            }
        }
        [Required(ErrorMessage = "Обязательно")]
        public string ExpireMonth
        {
            get { return _expireMonth; }
            set
            {
                _expireMonth = value;
                int p;
                if (!int.TryParse(value, out p) || int.Parse(_expireMonth) > 12)
                {
                    _expireMonth = string.Empty;
                }
                OnPropertyChanged(nameof(ExpireMonth));
            }
        }
        [Required(ErrorMessage = "Обязательно")]
        public string ExpireYear
        {
            get { return _expireYear; }
            set
            {
                _expireYear = value;
                int p;
                if (!int.TryParse(value, out p) || int.Parse(_expireYear) > 50)
                {
                    _expireYear = string.Empty;
                }
                OnPropertyChanged(nameof(ExpireYear));
            }
        }
    }
}
