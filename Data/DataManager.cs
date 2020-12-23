using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public class DataManager
    {
        private static readonly NTCBetBDContext _context = new NTCBetBDContext();

        public List<Tournament> GetTournaments()
        {
            var query = from t in _context.Tournaments
                        select t;
            List<Tournament> tournaments = query.ToList();
            return tournaments;
        }

        public void BetUser(int id, int? scoreH, int? scoreA)
        {
            Bet bet = _context.Bets.FirstOrDefault(b => b.Id == id);
            bet.ScoreH = scoreH;
            bet.ScoreA = scoreA;
            bet.IsSet = true;

            _context.SaveChanges();
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }
    }
}
