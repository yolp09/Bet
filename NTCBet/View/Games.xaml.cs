using Model;
using NTCBet.ViewModel;
using System.Windows.Controls;

namespace NTCBet.View
{
    /// <summary>
    /// Логика взаимодействия для Games.xaml
    /// </summary>
    public partial class Games : Page
    {
        public Games(ApplicationMain applicationMain, Manager manager)
        {
            InitializeComponent();
            this.DataContext = new GamesViewModel(applicationMain, manager);
        }
    }
}
