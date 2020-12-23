using Model.Common;
using System.Collections.ObjectModel;

namespace Model.Tours
{
    public class ToursModelData : ModelBase
    {
        public ObservableCollection<TourModel> Tours;

        public ToursModelData()
        {
            Tours = new ObservableCollection<TourModel>();
        }
    }
}
