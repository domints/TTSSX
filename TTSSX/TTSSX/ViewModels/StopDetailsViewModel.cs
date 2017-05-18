using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTSSLib.Models.Data;
using TTSSX.Helpers;
using TTSSX.Models;

namespace TTSSX.ViewModels
{
    public class StopDetailsViewModel : BaseViewModel
    {
        StopData _data;

        ObservableRangeCollection<PassageListItem> _passages;

        public ObservableRangeCollection<PassageListItem> Passages {
            get { return _passages; }
            set { SetProperty(ref _passages, value); }
        }

        public StopDetailsViewModel(StopData data)
        {
            _data = data;
            _passages = new ObservableRangeCollection<PassageListItem>();
            Title = data.Name;
        }        
    }
}
