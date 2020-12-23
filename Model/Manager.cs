using Data;
using Model.Games;
using Model.Tournaments;
using Model.Tours;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Model
{
    public class Manager
    {
        private readonly DataManager _dataManager;
        private Tour _tourSelect;
        private IEnumerable<Data.User> _allUsers;
        private User _currentUser;

        public TournamentModelData TournamentModelData { get; set;} 
        public ToursModelData ToursModelData { get; set;} 
        public GamesModelData GamesModelData { get; set;}

        public Manager()
        {
            this._dataManager = new DataManager();
            TournamentModelData = new TournamentModelData();
            ToursModelData = new ToursModelData();
            GamesModelData = new GamesModelData();
            _allUsers = _dataManager.GetUsers().OrderBy(u => u.Name);
        }

        public bool UserActivation(string name, string password)
        {
            foreach(var user in _allUsers)
            {
                if (user.Name.Equals(name) && user.Password.Equals(password))
                {
                    _currentUser = user;
                    return true;
                }
            }
            return false;
        }

        public void PDF()
        {
            ResultPdf pdf = new ResultPdf();
            List<Bet> bet = _tourSelect.Bets.ToList();
            List<Match> matches = _tourSelect.Matches.ToList();
            pdf.NewSaveP1(bet, _allUsers, matches);
        }

        public void GetTours(int idTournament)
        {
            Tournament tournament = TournamentModelData.Tournaments.FirstOrDefault(t => t.Id == idTournament).Tournament;

            List<Tour> tours = tournament.Tours.ToList();

            List<TourModel> result = new List<TourModel>(tours.Select(t => new TourModel(t)));
            ToursModelData.Tours = new ObservableCollection<TourModel>(result);
        }

        public void BetUser()
        {
            GamesModelData.BetUser();
        }

        public void GetGames(int idTour)
        {
            _tourSelect = ToursModelData.Tours.FirstOrDefault(t => t.Id == idTour).Tour;

            List<Bet> bets = _tourSelect.Bets.Where(b => b.User == _currentUser).ToList();
            foreach (var match in bets) { LoadFromDB(match.Match.CommandH.Logo); LoadFromDB(match.Match.CommandA.Logo); }

            List<GameModel> result = new List<GameModel>(bets.Select(b => new GameModel(b)));
            GamesModelData.Games = new ObservableCollection<GameModel>(result);
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
