using Kinoa.ViewModel;
using System;
using System.Windows;

namespace Kinoa.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel vm)
        {
            DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(() => Close());
            InitializeComponent();
        }
    }
}
