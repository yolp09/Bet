using Data;
using Model.Common;
using System.Reflection;

namespace Model.Games
{
    public class GameModel : ModelBase
    {
        private bool _isRead;

        internal int Id { get; set; }

        public string NameH { get; set; }
        public string NameA { get; set; }
        public string IconH { get; set; }
        public string IconA { get; set; }
        public int? ValueScoreH { get; set; }
        public int? ValueScoreA { get; set; }
        public bool IsRead { get { return _isRead; } set { _isRead = value; OnPropertyChanged("IsRead"); } }
        public string DateM { get; set; }

        public GameModel(Bet match)
        {
            string pathExecutiveFile = Assembly.GetExecutingAssembly().Location;

            Id = match.Id;
            NameH = match.Match.CommandH.Name;
            NameA = match.Match.CommandA.Name;
            IconH = pathExecutiveFile.Substring(0, pathExecutiveFile.LastIndexOf("\\")) + "\\temp\\" + match.Match.CommandH.Logo.Name;
            IconA = pathExecutiveFile.Substring(0, pathExecutiveFile.LastIndexOf("\\")) + "\\temp\\" + match.Match.CommandA.Logo.Name;
            ValueScoreH = match.ScoreH;
            ValueScoreA = match.ScoreA;
            IsRead = (match.IsSet == true || match.Match.Played == true) ? false : true;
            DateM = match.Match.Date.Value.ToString("dd.MM.yyyy");
        }
    }
}
