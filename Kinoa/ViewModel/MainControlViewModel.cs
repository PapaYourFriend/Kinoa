using Kinoa.Entities;
using Kinoa.ViewModel.Base;
using Kinoa.ViewModel.Command;
using Kinoa.ViewModel.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Kinoa.ViewModel
{
    internal class MainControlViewModel : BaseViewModel
    {
        private MainWindowViewModel _main;
        private Movie _showingHot;
        private DispatcherTimer _timer;
        private bool _changed = false;
        private int _showingIndex;
        private Movie _selectedItem;
        private DateTime _selectedDate;
        private RelayCommand _butTicketsCommand;

        public MainControlViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _main = mainWindowViewModel;
            HotMovies = _main.MovieRepository.GetAllByCondition(m => m.Hot, m => m.Genre);
            AllMovies = _main.MovieRepository.GetAll(m => m.AgeLimit, m => m.Genre);
            SelectedDate = DateTime.Now;
            if(HotMovies.Length > 0)
            {
                _showingIndex = 0;
                ShowingHot = HotMovies[_showingIndex];
                _timer = new DispatcherTimer();
                _timer.Interval = TimeSpan.FromSeconds(10);
                _timer.Tick += _timer_Tick;
                _timer.Start();
            }
        }

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(_selectedDate));
            }
        } 

        public bool Changed
        {
            get { return _changed; }
            set
            {
                _changed = value;
                OnPropertyChanged(nameof(Changed));
            }
        }

        public Movie[] HotMovies { get; set; }
        public Movie[] AllMovies { get; set; }
        public Movie ShowingHot
        {
            get { return _showingHot; }
            set
            {
                _showingHot = value;
                OnPropertyChanged(nameof(ShowingHot));
            }
        }

        public Movie SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public RelayCommand BuyTicketsCommand
        {
            get
            {
                return _butTicketsCommand ?? (_butTicketsCommand = new RelayCommand((o) =>
                {
                    var movie = o as Movie;

                    if(movie != null)
                    {
                        _main.ShowingViewModel = new FilmRoomViewModel(_main, this, movie);
                    }
                }));
            }
        }


        private async void _timer_Tick(object sender, EventArgs e)
        {
            if (_showingIndex == HotMovies.Length - 1
                || HotMovies[_showingIndex] != ShowingHot)
            {
                _showingIndex = 0;
            }
            else
            {
                _showingIndex++;
            }
            Changed = true;
            await Task.Delay(800);
            ShowingHot = HotMovies[_showingIndex];
            Changed = false;
        }
    }
}