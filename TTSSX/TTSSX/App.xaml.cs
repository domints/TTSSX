using System.Linq;
using System.Threading.Tasks;
using TTSSLib.Models.Enums;
using TTSSLib.Services;
using TTSSX.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TTSSX
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SetMainPage();
        }

        public static void SetMainPage()
        {
            var ss = new StopService();
            var stops = Task.Run(() => ss.GetAllStops(StopType.Tram)).Result;
            Current.MainPage = new TabbedPage
            {
                Children =
                {
                    new NavigationPage(new FavouriteStopsPage(stops))
                    {
                        Title = "Przystanki",
                        Icon = Device.OnPlatform("tab_feed.png",null,null)
                    },
                }
            };
        }
    }
}
