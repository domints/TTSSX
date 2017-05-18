using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTSSLib.Models.Data;
using TTSSX.Helpers;

namespace TTSSX.ViewModels
{
    public class FavouritesViewModel : BaseViewModel
    {
        private ObservableRangeCollection<StopData> _stops;
        public FavouritesViewModel(List<StopData> stops)
        {
            Title = "Przystanki";
            _stops = new ObservableRangeCollection<StopData>(stops);
        }

        public ObservableRangeCollection<StopData> Stops
        {
            get { return _stops; }
            set { _stops = value; }
        }
        //public List<StopData> AllStops { get; set; }
    }
}
