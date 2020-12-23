using Data;
using Model.Common;
using System.Collections.ObjectModel;
using System.Linq;

namespace Model.Games
{
    public class GamesModelData : ModelBase
    {
        private readonly DataManager _dataManager;

        public bool EnabledButtonBet { get { return Games.FirstOrDefault(g => g.IsRead == true) == null ? false : true; } }
        public bool EnabledButtonResults { get { return Games.Count > 0 ? true : false; } }
        public ObservableCollection<GameModel> Games;

        public GamesModelData()
        {
            this._dataManager = new DataManager();

            Games = new ObservableCollection<GameModel>();
            int s = Games.Select(g => g.IsRead == true).ToList().Count;
        }

        public void BetUser()
        {
            for(int i=0;i<Games.Count;i++)
            {
                if (Games[i].ValueScoreH != null && Games[i].ValueScoreA != null && Games[i].IsRead == true)
                {
                    _dataManager.BetUser(Games[i].Id, Games[i].ValueScoreH, Games[i].ValueScoreA);
                    Games[i].IsRead = false;
                    OnPropertyChanged("EnabledButtonBet");
                }
            }
        }
    }
}
