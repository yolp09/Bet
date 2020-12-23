using Model;
using Model.Tournaments;
using NTCBet.Command;
using NTCBet.View;
using System.Collections.ObjectModel;

namespace NTCBet.ViewModel
{
    public class TournamentsViewModel : ViewModelBase
    {
        private TournamentModelData _tournamentModelData;
        private ApplicationMain _applicationMain;
        private Manager _manager;

        public ObservableCollection<TournamentModel> Tournaments { get; set; }

        public RelayCommand OpeningTourCommand { get { return new RelayCommand((obj) => { OpeningTour(obj); }); } }

        public TournamentsViewModel(ApplicationMain applicationMain, Manager manager)
        {
            this._applicationMain = applicationMain;
            this._manager = manager;
            _tournamentModelData = _manager.TournamentModelData;
            _tournamentModelData.PropertyChanged += (s, e) => { OnPropertyChanged(e.PropertyName); };

            Tournaments = new ObservableCollection<TournamentModel>(_tournamentModelData.Tournaments);
        }

        private void OpeningTour(object obj)
        {
            if(obj != null)
            {
                int id = (int)obj;
                using (Loading lw = new Loading(() => { _manager.GetTours(id); }))
                {
                    if (System.Windows.Application.Current.Windows.Count > 0)
                    {
                        var w = System.Windows.Application.Current.Windows[0];
                        lw.Owner = w;
                    }
                    lw.ShowDialog();
                }
                _applicationMain.NextPage(new Tours(_applicationMain, _manager));
            }
        }
    }
}
