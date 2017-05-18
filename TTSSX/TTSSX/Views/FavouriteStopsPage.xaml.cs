using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TTSSLib.Models.Data;
using TTSSX.Models;
using TTSSX.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TTSSX.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavouriteStopsPage : ContentPage
    {
        FavouritesViewModel viewModel;

        public FavouriteStopsPage(List<StopData> stops)
        {
            InitializeComponent();
            BindingContext = viewModel = new FavouritesViewModel(stops);
        }

        async void OnItemTapped(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as StopData;
            if (item == null)
                return;

            await Navigation.PushAsync(new StopDetailsPage(item));
            listView.SelectedItem = null;

        }

        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (listView.DataSource != null)
            {
                this.listView.DataSource.Filter = FilterStops;
                this.listView.DataSource.RefreshFilter();
            }
        }

        private bool FilterStops(object obj)
        {
            if (searchBar == null || searchBar.Text == null)
                return true;

            var taskInfo = obj as StopData;
            if (taskInfo.Name.ToLower().Contains(searchBar.Text.ToLower())
                || taskInfo.Name.ToLower().Contains(searchBar.Text.ToLower()))
                return true;
            else
                return false;
        }
    }
}
