using System.Windows;
using Eraser.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using Eraser.Helper;
using GalaSoft.MvvmLight.Ioc;

namespace Eraser
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
            Messenger.Default.Register<OpenWindowMessage>(
              this,
              message =>
              {
                  if (message.Type == WindowType.MODAL)
                  {
                      var modalWindowVM = SimpleIoc.Default.GetInstance<ModalViewModel>();
                      var modalWindow = new ModalWindow();
                      modalWindow.ShowDialog();
                      var adapter = modalWindowVM.SelectedAdapter;
                      var dns = modalWindowVM.SelectedDNSProvider;
                      Messenger.Default.Send(adapter);
                      Messenger.Default.Send(dns);
                  }
              });
        }

        private void btnChangeDNS_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MainViewModel;
            vm.SetDNS();
        }
    }
}