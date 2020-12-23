using Data;
using Model.Common;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Model.Tournaments
{
    public class TournamentModelData : ModelBase
    {
        private readonly DataManager _dataManager;
        private readonly ObservableCollection<TournamentModel> _tournaments;

        public ReadOnlyObservableCollection<TournamentModel> Tournaments;

        public TournamentModelData()
        {
            this._dataManager = new DataManager();
            _tournaments = new ObservableCollection<TournamentModel>(GetTournaments());
            Tournaments = new ReadOnlyObservableCollection<TournamentModel>(_tournaments);
        }

        public List<TournamentModel> GetTournaments()
        {
            List<Tournament> tournaments = _dataManager.GetTournaments();
            foreach (var tournament in tournaments) { LoadFromDB(tournament.Logo); }
            List<TournamentModel> result = new List<TournamentModel>(tournaments.Select(t => new TournamentModel(t)));
            return result;
        }

        private void LoadFromDB(Logo logo)
        {
            System.IO.Directory.CreateDirectory("temp");
            string path = @".\temp\" + logo.Name;
            if (!File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    fs.Write(logo.Data, 0, logo.Data.Length);
                }
            }
        }
    }
}
