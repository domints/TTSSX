using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TTSSLib;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace TTSSX
{
    public class App : Application
    {
        TTSS ttss;
        List<AutoCompleteStop> m_blSuggestionList;
        ListView lv;
        AutoCompleteView acv;
        Stop showedStop;
        List<TramGetItem> tramsCache;

        public App()
        {
            ttss = new TTSS();
            m_blSuggestionList = new List<AutoCompleteStop>();
            tramsCache = new List<TramGetItem>();

            var passageCell = new DataTemplate(typeof(PassageCell));
            passageCell.SetBinding(PassageCell.LineProperty, "Line");
            passageCell.SetBinding(PassageCell.DirectionProperty, "Direction");
            passageCell.SetBinding(PassageCell.TimeProperty, "Time");
            passageCell.SetBinding(PassageCell.OldProperty, "Old");
            passageCell.SetBinding(PassageCell.RelativeTimeProperty, "RelativeTime");
            passageCell.SetBinding(PassageCell.TramDescProperty, "TramDesc");

            lv = new ListView
            {
                ItemTemplate = passageCell
            };

            lv.ItemTapped += Lv_ItemTapped;
            lv.IsPullToRefreshEnabled = true;
            lv.RefreshCommand = new Command(async () =>
            {
                if(showedStop != null)
                {
                    StopInfo si = await ttss.StopInfo(showedStop);
                    var passages = si.OldPassages.Concat(si.ActualPassages);
                    var trams = await GetTramInfos(passages.Select(p => p.VehicleID).ToArray());

                    lv.ItemsSource = passages.Select(p =>
                    {
                        TramGetItem item = trams.FirstOrDefault(t => t.TramId == p.VehicleID);
                        string tramDesc = item == null ? null : $"{item.TramNo} - {item.Name}";
                        return new PassageCellModel
                        {
                            Direction = p.Direction,
                            Line = p.PatternText,
                            Old = p.ActualRelativeTime < 0,
                            RelativeTime = p.ActualRelativeTime,
                            Time = p.MixedTime,
                            TramDesc = tramDesc,
                            Passage = p
                        };
                    });
                    lv.EndRefresh();
                }
            });

            // The root page of your application
            acv = new AutoCompleteView
            {
                Placeholder = "Przystanek (min. 2 znaki)"
            };
            acv.ExecuteOnSuggestionClick = true;
            acv.ShowSearchButton = false;
            acv.TextChanged += Acv_TextChanged;
            acv.Suggestions = m_blSuggestionList;
            acv.SearchText = "Pokaż";
            acv.SearchCommand = new Command(async (e) =>
            {
                lv.IsRefreshing = true;
                List<Stop> stops = ((e as SelectedItemChangedEventArgs).SelectedItem as AutoCompleteStop).Stops;
                foreach(Stop stop in stops.OrderByDescending(s => s.ID))
                {
                    StopInfo si = await ttss.StopInfo(stops.FirstOrDefault(s => s.Valid));
                    if (si != null && (si.ActualPassages.Any() || si.OldPassages.Any()))
                    {
                        var passages = si.OldPassages.Concat(si.ActualPassages);
                        var trams = await GetTramInfos(passages.Select(p => p.VehicleID).ToArray());

                        var source = passages.Select(p =>
                        {
                            TramGetItem item = trams.FirstOrDefault(t => t.TramId == p.VehicleID);
                            string tramDesc = item == null ? null : $"{item.TramNo} - {item.Name}";
                            return new PassageCellModel
                            {
                                Direction = p.Direction,
                                Line = p.PatternText,
                                Old = p.ActualRelativeTime < 0,
                                RelativeTime = p.ActualRelativeTime,
                                Time = p.MixedTime,
                                TramDesc = tramDesc,
                                Passage = p
                            };
                        });
                        lv.ItemsSource = source.ToList();
                        /*PassageCellModel pcm = source.FirstOrDefault(cell => !cell.Old);
                        if (pcm != null)
                        {
                            lv.ScrollTo(pcm, ScrollToPosition.Start, false);
                        }*/

                        showedStop = stop;
                        break;
                    }
                    else
                    {
                        stop.Valid = false;
                    }       
                }

                lv.IsRefreshing = false;
            }, (e) =>  acv.Text.Length > 0);

            MainPage = new ContentPage
            {
                Padding = Device.OnPlatform(new Thickness(5, 25, 5, 5), new Thickness(5), new Thickness(5)),
                Content = new ScrollView
                {
                    Content = new StackLayout
                    {
                        Children = {
                            new Label {
                                Text = "Przystanek:"
                            },
                            acv,
                            lv
                        }
                    }
                }
            };
        }

        private async void Lv_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            StopPassage i = ((PassageCellModel)e.Item).Passage;

            var model = await PassageDialog(MainPage.Navigation, i);
            if (model == null)
            {
                return;
            }            

            PassagePostRq rq = new PassagePostRq
            {
                TramNo = model.TramNo,
                CompositionNo = model.CompositionNo,
                Line = i.PatternText,
                PlannedTime = i.PlannedTime,
                TheirId = i.Passageid,
                VehicleId = i.VehicleID,
                RouteId = i.RouteID,
                TripId = i.TripID,
                Direction = i.Direction
            };

            HttpClient hc = new HttpClient();
            HttpResponseMessage response = await hc.PostAsJsonAsync("http://ttssx.dszymanski.pl/api/app", rq);

#if DEBUG
            string message = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            IEnumerable<string> encoding = new List<string>();
            if (response.Content.Headers.TryGetValues("Content-Encoding", out encoding) && (string.Join(" ", encoding).Contains("gzip") || (string.Join(" ", encoding).Contains("deflate"))))
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                using (var decompressedStream = new GZipStream(responseStream, CompressionMode.Decompress))
                using (var streamReader = new StreamReader(decompressedStream))
                {
                    message = await streamReader.ReadToEndAsync();
                }
            }
            else
            {
                message = await response.Content.ReadAsStringAsync();
            }

            if(message != null)
            {
                PassagePostRs rs = JsonConvert.DeserializeObject<PassagePostRs>(message);

                await MainPage.DisplayAlert("Rezultat", $"PassageId: {rs.PassageInstances}, VehicleId: {rs.VehicleInstances}", "Zamknij");
            }
#else
            await MainPage.DisplayAlert("Powodzenie", "Dzięki!", "Zamknij");
#endif
        }

        private async void Acv_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length > 1 && (e.OldTextValue == null || e.OldTextValue.Length < e.NewTextValue.Length))
            {
                lv.IsRefreshing = true;
                List<Stop> RsList = await ttss.AutocompleteObj(e.NewTextValue);
                List<AutoCompleteStop> list = RsList.GroupBy(s => s.Name).Select(s => new AutoCompleteStop { Name = s.Key, Stops = s.ToList() }).ToList();
                foreach (var s in list)
                {
                    if (!m_blSuggestionList.Any(sug => sug.Name == s.Name))
                    {
                        if (m_blSuggestionList.Count == 0)
                        {
                            m_blSuggestionList.Add(s);
                        }
                        else
                        {
                            int comparation = -1;
                            int changeIndex = 0;
                            while (comparation < 0 && changeIndex < m_blSuggestionList.Count)
                            {
                                comparation = string.Compare(m_blSuggestionList[changeIndex].Name, s.Name, StringComparison.OrdinalIgnoreCase);
                                changeIndex++;
                            }

                            if (changeIndex < m_blSuggestionList.Count)
                            {
                                m_blSuggestionList.Insert(changeIndex, s);
                            }
                            else
                            {
                                m_blSuggestionList.Add(s);
                            }
                        }
                    }
                }

                List<AutoCompleteStop> sugList = m_blSuggestionList.ToList();
                foreach (var s in sugList)
                {
                    if (!list.Any(sug => sug.Name == s.Name))
                    {
                        m_blSuggestionList.RemoveAll(sug => sug.Name == s.Name);
                    }
                }
                lv.IsRefreshing = false;
            }
            else
            {
                m_blSuggestionList.Clear();
            }
        }

        public async Task<TramGetItem[]> GetTramInfos(string[] tramids)
        {
            TramGet rq = new TramGet
            {
                Trams = tramids.Where(i => i != null && !tramsCache.Any(t => t.TramId == i)).ToArray()
            };

            HttpClient hc = new HttpClient();
            HttpResponseMessage response = await hc.PostAsJsonAsync("http://ttssx.dszymanski.pl/api/app/trams", rq);

            if(response.IsSuccessStatusCode)
            {
                TramGetRs rs = await response.Content.ReadAsAsync<TramGetRs>();
                return rs.Items.Concat(tramsCache.Where(t => tramids.Any(ti => ti == t.TramId))).ToArray();
            }
            else
            {
                return null;
            }
        }

        protected override async void OnStart()
        {
            await ttss.Autocomplete("wesele");
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static Task<PassageInputModel> PassageDialog(INavigation navigation, StopPassage passage)
        {
            // wait in this proc, until user did his input 
            var tcs = new TaskCompletionSource<PassageInputModel>();

            var lblTitle = new Label { Text = $"{passage.PatternText} -> {passage.Direction}", HorizontalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold };
            var lblTramMessage = new Label { Text = "Nr boczny:" };
            var tramInput = new Entry { Text = "", Placeholder = "np. RY801" };
            var lblCompositionMessage = new Label { Text = "Nr składu:" };
            var compositionInput = new Entry { Text = "", Placeholder = $"np. {passage.PatternText}-03" };

            var btnOk = new Button
            {
                Text = "Ok",
                WidthRequest = 100,
                BackgroundColor = Color.FromRgb(0.8, 0.8, 0.8),
            };
            btnOk.Clicked += async (s, e) =>
            {
                // close page
                var result = new PassageInputModel(tramInput.Text, compositionInput.Text);
                await navigation.PopModalAsync();
                // pass result
                tcs.SetResult(result);
            };

            var btnCancel = new Button
            {
                Text = "Cancel",
                WidthRequest = 100,
                BackgroundColor = Color.FromRgb(0.8, 0.8, 0.8)
            };
            btnCancel.Clicked += async (s, e) =>
            {
                // close page
                await navigation.PopModalAsync();
                // pass empty result
                tcs.SetResult(null);
            };

            var slButtons = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { btnOk, btnCancel },
            };

            var layout = new StackLayout
            {
                Padding = new Thickness(0, 40, 0, 0),
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = { lblTitle, lblTramMessage, tramInput, lblCompositionMessage, compositionInput, slButtons },
            };

            // create and show page
            var page = new ContentPage();
            page.Content = layout;
            navigation.PushModalAsync(page);
            // open keyboard
            tramInput.Focus();

            // code is waiting her, until result is passed with tcs.SetResult() in btn-Clicked
            // then proc returns the result
            return tcs.Task;
        }
    }
}
