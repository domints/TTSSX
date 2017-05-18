using Syncfusion.ListView.XForms.UWP;

namespace TTSSX.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            SfListViewRenderer.Init();
            LoadApplication(new TTSSX.App());
        }
    }
}