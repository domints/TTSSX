using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TTSSLib.Models.Data;
using TTSSLib.Services;
using TTSSX.Models;
using TTSSX.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TTSSX.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StopDetailsPage : ContentPage
    {
        StopData _data;
        StopDetailsViewModel vm;

        public StopDetailsPage()
        {
            InitializeComponent();
        }

        public StopDetailsPage(StopData data)
            : this()
        {
            _data = data;
            Title = data.Name;
            BindingContext = vm = new StopDetailsViewModel(data);
            this.Appearing += StopDetailsPage_Appearing;
        }

        private void StopDetailsPage_Appearing(object sender, EventArgs e)
        {
            var _passageService = new PassageService();
            var passages = _passageService.GetPassages(_data).Result;
            var merged = passages.OldPassages.Concat(passages.ActualPassages).Select(p => new PassageListItem(p)).ToList();
            vm.Passages = new Helpers.ObservableRangeCollection<PassageListItem>(merged);
        }
    }
}
