using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTSSLib.Models.Data;
using TTSSLib.Services;
using TTSSX.Models;

namespace TTSSX.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public List<StopData> Stops { get; set; }
        //public List<string> Stops { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item.Text;
            Item = item;
        }

        public async Task LoadStops()
        {
            var ss = new StopService();
            Stops = await ss.GetAllStops();
        }

        int quantity = 1;
        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }
    }
}