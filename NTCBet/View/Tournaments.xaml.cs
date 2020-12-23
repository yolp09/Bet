using Model;
using NTCBet.ViewModel;
using System.Windows.Controls;

namespace NTCBet.View
{
    /// <summary>
    /// Логика взаимодействия для Tournaments.xaml
    /// </summary>
    public partial class Tournaments : Page
    {
        public Tournaments(ApplicationMain applicationMain, Manager manager)
        {
            InitializeComponent();
            this.DataContext = new TournamentsViewModel(applicationMain, manager);
        }
    }
}
