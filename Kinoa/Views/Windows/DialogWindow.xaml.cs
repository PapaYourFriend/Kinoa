using Kinoa.ViewModel.Common;
using Kinoa.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kinoa.Views.Windows
{
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        private bool isClosed = false;
        public DialogWindow()
        {
            InitializeComponent();
            DialogPresenter.DataContextChanged += DialogPresenterDataContextChanged;
            Closed += DialogWindowClosed;
        }
        void DialogWindowClosed(object sender, EventArgs e)
        {
            isClosed = true;
        }

        private void DialogPresenterDataContextChanged(object sender,
                                  DependencyPropertyChangedEventArgs e)
        {
            var d = e.NewValue as IDialogResult;

            if (d == null)
                return;

            d.RequestCloseDialog += new EventHandler<RequestCloseDialogEventArgs>(DialogResultTrueEvent).MakeWeak(
                                        eh => d.RequestCloseDialog -= eh);
        }

        private void DialogResultTrueEvent(object sender,
                                  RequestCloseDialogEventArgs eventargs)
        {
            // Important: Do not set DialogResult for a closed window
            // GC clears windows anyways and with MakeWeak it
            // closes out with IDialogResultVMHelper
            if (isClosed) return;

            DialogResult = eventargs.DialogResult;
        }
    }
}
