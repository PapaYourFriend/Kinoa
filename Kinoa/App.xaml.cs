using Kinoa.ViewModel.Common;
using Kinoa.Views.Windows;
using Ninject;
using System.Windows;

namespace Kinoa
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private KernelBase _kernel;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _kernel = new StandardKernel();

            _kernel.Load(new DependencyInjection());

            Current.MainWindow = _kernel.Get<AuthWindow>();
            Current.MainWindow.Closing += AuthWindow_Closing;
            Current.MainWindow.Show();
        }

        private void AuthWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var view = Current.MainWindow as AuthWindow;
            if(view is null || !view.VM.IsLoggedIn)
            {
                return;
            }
            Current.MainWindow = _kernel.Get<MainWindow>();
            Current.MainWindow.Closing -= AuthWindow_Closing;
            Current.MainWindow.Closing += MainWindow_Closing;
            Current.MainWindow.Show();
        }
        
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var view = Current.MainWindow as MainWindow;
            if(view is null || !string.IsNullOrEmpty(GodClient.ClientName)) 
            {
                return;
            }
            Current.MainWindow = _kernel.Get<AuthWindow>();
            Current.MainWindow.Closing -= MainWindow_Closing;
            Current.MainWindow.Closing += AuthWindow_Closing;
            Current.MainWindow.Show();
        }
    }
}
