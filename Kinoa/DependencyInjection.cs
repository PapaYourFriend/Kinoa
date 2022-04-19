using Kinoa.DataContext;
using Kinoa.Repositories.Implem;
using Kinoa.Repositories.Interfaces;
using Kinoa.ViewModel;
using Kinoa.ViewModel.Services;
using Ninject.Modules;

namespace Kinoa
{
    public class DependencyInjection : NinjectModule
    {
        public override void Load()
        {
            Bind<KinoaDataContext>().ToSelf().InSingletonScope();
            Bind<IClientRepository>().To<ClientRepository>().InSingletonScope();
            Bind<IPaymentMethodRepository>().To<PaymentMethodRepository>().InSingletonScope();
            Bind<IOrderDataRepository>().To<OrderDataRepository>().InSingletonScope();
            Bind<IOrderRepository>().To<OrderRepository>().InSingletonScope();
            Bind<IMovieSessionRepository>().To<MovieSessionRepository>().InSingletonScope();
            Bind<IFilmRoomRepository>().To<FilmRoomRepository>().InSingletonScope();
            Bind<IOpenDialog>().To<OpenDialog>().InSingletonScope();
            Bind<IUploadPicture>().To<UploadPicture>().InSingletonScope();
            Bind<IMovieRepository>().To<MovieRepository>().InSingletonScope();
            Bind<IMessageBoxService>().To<MessageBoxService>().InSingletonScope();
            Bind<AuthViewModel>().ToSelf().InTransientScope();
            Bind<MainWindowViewModel>().ToSelf().InTransientScope();
        }
    }
}
