using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTSSLib;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace TTSSX
{
    public class App : Application
    {
        TTSS ttss;
        List<string> m_blSuggestionList;
        ListView lv;

        public App()
        {
            ttss = new TTSS();
            m_blSuggestionList = new List<string>();

            var passageCell = new DataTemplate(typeof(PassageCell));
            passageCell.SetBinding(PassageCell.LineProperty, "PatternText");
            passageCell.SetBinding(PassageCell.DirectionProperty, "Direction");
            passageCell.SetBinding(PassageCell.TimeProperty, "MixedTime");

            lv = new ListView
            {
                ItemTemplate = passageCell
            };

            // The root page of your application
            AutoCompleteView acv = new AutoCompleteView
            {
                Placeholder = "Stopz."
            };
            acv.TextChanged += Acv_TextChanged;
            acv.Suggestions = m_blSuggestionList;
            acv.SearchText = "Pokaż";
            acv.SearchCommand = new Command(async () =>
            {
                List<Stop> stops = await ttss.GetCachedStops(acv.Text);
                StopInfo si = await ttss.StopInfo(stops.FirstOrDefault(s => s.Valid));
                if (si != null)
                {
                    lv.ItemsSource = si.ActualPassages;
                }
            }, () => acv.Text.Length > 0);

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

        private async void Acv_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as AutoCompleteView).Text.Length > 0)
            {
                List<string> list = await ttss.Autocomplete((sender as AutoCompleteView).Text);
                foreach (string s in list)
                {
                    if (!m_blSuggestionList.Contains(s))
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
                                comparation = string.Compare(m_blSuggestionList[changeIndex], s, StringComparison.OrdinalIgnoreCase);
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

                List<string> sugList = m_blSuggestionList.ToList();
                foreach (string s in sugList)
                {
                    if (!list.Contains(s))
                    {
                        m_blSuggestionList.Remove(s);
                    }
                }
            }
            else
            {
                m_blSuggestionList.Clear();
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
