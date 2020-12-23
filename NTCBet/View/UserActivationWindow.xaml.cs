using Model;
using NTCBet.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace NTCBet.View
{
    /// <summary>
    /// Логика взаимодействия для UserActivationWindow.xaml
    /// </summary>
    public partial class UserActivationWindow : Window
    {
        public UserActivationWindow(ApplicationMain applicationMain, Manager manager)
        {
            InitializeComponent();
            this.MouseLeftButtonDown += titleBar_MouseLeftButtonDown;
            this.DataContext = new UserActivationWinodwViewModel(applicationMain, manager);
        }

        private void titleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch { }
        }
    }
}
