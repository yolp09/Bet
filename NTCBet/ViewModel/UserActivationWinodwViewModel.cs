using Model;
using NTCBet.Command;
using System.Windows;

namespace NTCBet.ViewModel
{
    public class UserActivationWinodwViewModel : ViewModelBase
    {
        private string _userName;
        private string _userPassword;
        private ApplicationMain _applicationMain;
        private Manager _manager;

        public string UserName { get { return _userName; } set { _userName = value; } }
        public string UserPassword { get { return _userPassword; } set { _userPassword = value; } }

        public RelayCommand ExitCommand { get { return new RelayCommand((obj) => { System.Windows.Application.Current.Shutdown(); }); } }
        public RelayCommand OkCommand { get { return new RelayCommand((obj) => { Ok(obj); }); } }

        public UserActivationWinodwViewModel(ApplicationMain applicationMain, Manager manager)
        {
            this._applicationMain = applicationMain;
            this._manager = manager;
            UserName = Properties.Settings.Default.SettingName;
            UserPassword = Properties.Settings.Default.SettingPassword;
        }

        private void Ok(object obj)
        {
            if(obj != null)
            {
                var window = (Window)obj;
                if (_manager.UserActivation(UserName, UserPassword))
                {
                    Properties.Settings.Default.SettingName = UserName;
                    Properties.Settings.Default.SettingPassword = UserPassword;
                    Properties.Settings.Default.Save();
                    _applicationMain.ChangeUserName(UserName);
                    if (window != null)
                        window.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка!!!");
                }
            }
        }
    }
}
