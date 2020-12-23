using Model;
using NTCBet.ViewModel;
using System.Windows.Controls;

namespace NTCBet.View
{
    /// <summary>
    /// Логика взаимодействия для Tours.xaml
    /// </summary>
    public partial class Tours : Page
    {
        public Tours(ApplicationMain applicationMain, Manager manager)
        {
            InitializeComponent();
            this.DataContext = new ToursViewModel(applicationMain, manager);
        }
    }
}
