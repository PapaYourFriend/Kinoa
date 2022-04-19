using Kinoa.Entities;
using Kinoa.ViewModel.Validation;
using System.ComponentModel.DataAnnotations;

namespace Kinoa.ViewModel.Models
{
    public class ProfileModel : ValidateModel
    {
        private string _firstName;
        private string _lastName;
        private string _image;
        private string _email;
        public ProfileModel(Client client, PaymentMethod method = null)
        {
            ClientId = client.ClientId;
            Email = client.ClientSecret.Email;
            Login = client.ClientSecret.Login;
            FirstName = client.FirstName;
            LastName = client.LastName;
            Image = client.Image;
            if(method != null)
            {
                CardNumber = method.CardNumber;
                Month = method.ExpireMonth;
                Year = method.ExpireYear;
            }
        }
        public int ClientId { get; }
        public string Login { get; }
        public int Month { get; }
        public int Year { get; }
        public string CardNumber { get; }
        [Required(ErrorMessage = "Обязательно")]
        [RegularExpression(@"^([a-z0-9_\.-]+)@([a-z0-9_\.-]+)\.([a-z\.]{2,6})$", ErrorMessage = "Невалидный Email")]
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        [Required(ErrorMessage = "Обязательно")]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        [Required(ErrorMessage = "Обязательно")]
        public string Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }
    }
}
