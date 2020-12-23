using Model;
using Model.Games;
using NTCBet.Command;
using NTCBet.View;
using System.Collections.ObjectModel;

namespace NTCBet.ViewModel
{
    public class GamesViewModel : ViewModelBase
    {
        private GamesModelData _gamesModelData;
        private ApplicationMain _applicationMain;
        private Manager _manager;

        public bool EnabledButtonBet { get { return _gamesModelData.EnabledButtonBet; } }
        public bool EnabledButtonResults { get { return _gamesModelData.EnabledButtonResults; } }
        public ObservableCollection<GameModel> Games { get; set; }

        public RelayCommand BetCommand { get { return new RelayCommand((obj) => { _manager.BetUser(); }); } }
        public RelayCommand ResultCommand { get { return new RelayCommand((obj) => { Result(); }); } }

        public GamesViewModel(ApplicationMain applicationMain, Manager manager)
        {
            this._applicationMain = applicationMain;
            this._manager = manager;
            _gamesModelData = this._manager.GamesModelData;
            _gamesModelData.PropertyChanged += (s, e) => { OnPropertyChanged(e.PropertyName); };

            Games = new ObservableCollection<GameModel>(_gamesModelData.Games);
        }

        private void Result()
        {
            using (Loading lw = new Loading(() => { _manager.PDF(); }))
            {
                if (System.Windows.Application.Current.Windows.Count > 0)
                {
                    var w = System.Windows.Application.Current.Windows[0];
                    lw.Owner = w;
                }
                lw.ShowDialog();
            }
        }
    }
}
