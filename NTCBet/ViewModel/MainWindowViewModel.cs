using Model;
using NTCBet.Command;
using NTCBet.View;
using System.Windows;
using System.Windows.Controls;

namespace NTCBet.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _userName;
        private Page _currentPage;
        private Visibility _visibilityBackButton;
        private ApplicationMain _applicationMain;
        private Manager _manager;

        public string UserName { get { return _userName; } set { _userName = value; } }
        public Page CurrentPage { get { return _currentPage; } set { _currentPage = value; OnPropertyChanged("CurrentPage"); } }
        public Visibility VisibilityBackButton { get { return _visibilityBackButton; } set { _visibilityBackButton = value; OnPropertyChanged("VisibilityBackButton"); } }

        public RelayCommand ExitCommand { get { return new RelayCommand((obj) => { System.Windows.Application.Current.Shutdown(); }); } }
        public RelayCommand BackCommand { get { return new RelayCommand((obj) => { _applicationMain.BackPage(); }); } }

        public MainWindowViewModel(ApplicationMain applicationMain)
        {
            try
            {
                this._applicationMain = applicationMain;
                this._manager = new Manager();

                _applicationMain.PageChanged += Application_PageChanged;
                _applicationMain.NextPage(new Tournaments(_applicationMain, _manager));

                UserActivationWindow userForm = new UserActivationWindow(_applicationMain, _manager);
                userForm.ShowDialog();
            }
            catch { MessageBox.Show("Подключение к серверу с базой данных отсутствует!!!"); }
        }

        private void Application_PageChanged(object sender, System.EventArgs e)
        {
            CurrentPage = _applicationMain.CurrentPage;
            UserName = _applicationMain.UserName;
            VisibilityBackButton = _applicationMain.VisibilityBackButton;
        }
    }
}
