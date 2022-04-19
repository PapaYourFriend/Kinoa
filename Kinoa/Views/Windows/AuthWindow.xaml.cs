using Kinoa.ViewModel;
using System;
using System.Windows;

namespace Kinoa.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        private readonly AuthViewModel _vm;
        public AuthWindow(AuthViewModel vm)
        {
            DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(() => Close());
            _vm = vm;
            InitializeComponent();
        }
        public AuthViewModel VM { get { return _vm; } }
    }
}
