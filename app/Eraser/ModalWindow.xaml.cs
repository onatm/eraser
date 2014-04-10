using Eraser.ViewModel;
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

namespace Eraser
{
    /// <summary>
    /// Interaction logic for ModalWindow.xaml
    /// </summary>
    public partial class ModalWindow : Window
    {
        public ModalWindow()
        {
            InitializeComponent();
        }

        private void btnGetDNSList_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ModalViewModel;
            vm.GetDNSList();
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
