using Data;

namespace Model.Tours
{
    public class TourModel
    {
        internal Tour Tour { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }

        public TourModel(Tour tour)
        {
            Id = tour.Id;
            Name = tour.TourName;
            Tour = tour;
        }
    }
}
