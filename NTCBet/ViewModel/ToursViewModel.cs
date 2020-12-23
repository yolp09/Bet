using Model;
using Model.Tours;
using NTCBet.Command;
using NTCBet.View;
using System.Collections.ObjectModel;

namespace NTCBet.ViewModel
{
    public class ToursViewModel : ViewModelBase
    {
        private ToursModelData _toursModelData;
        private ApplicationMain _applicationMain;
        private Manager _manager;

        public ObservableCollection<TourModel> Tours { get; set; }

        public RelayCommand OpeningGamesCommand { get { return new RelayCommand((obj) => { OpeningGames(obj); }); } }

        public ToursViewModel(ApplicationMain applicationMain, Manager manager)
        {
            this._applicationMain = applicationMain;
            this._manager = manager;
            _toursModelData = this._manager.ToursModelData;
            _toursModelData.PropertyChanged += (s, e) => { OnPropertyChanged(e.PropertyName); };

            Tours = new ObservableCollection<TourModel>(_toursModelData.Tours);
        }

        private void OpeningGames(object obj)
        {
            if(obj != null)
            {
                int id = (int)obj;
                using (Loading lw = new Loading(() => { _manager.GetGames(id); }))
                {
                    if (System.Windows.Application.Current.Windows.Count > 0)
                    {
                        var w = System.Windows.Application.Current.Windows[0];
                        lw.Owner = w;
                    }
                    lw.ShowDialog();
                }

                _applicationMain.NextPage(new Games(_applicationMain, _manager));
            }
        }
    }
}
