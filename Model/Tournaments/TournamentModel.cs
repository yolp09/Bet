using Data;
using System.Reflection;

namespace Model.Tournaments
{
    public class TournamentModel
    {
        internal Tournament Tournament { get; set; }

        public int Id { get; set; }
        public string Icon { get; set; }
        public string Name { get; set; }

        public TournamentModel(Tournament tournament)
        {
            string pathExecutiveFail = Assembly.GetExecutingAssembly().Location;

            Id = tournament.Id;
            Icon = pathExecutiveFail.Substring(0, pathExecutiveFail.LastIndexOf("\\")) + "\\temp\\" + tournament.Logo.Name;
            Name = tournament.Name;
            Tournament = tournament;
        }
    }
}
