using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TTSSLib
{
    public class TTSS
    {
        /// <summary>
        /// List of all cached stops.
        /// </summary>
        Dictionary<string, List<Stop>> m_dictStops;

        /// <summary>
        /// The list of checked stops
        /// </summary>
        Dictionary<string, CheckDate> m_dictCheckedStops;

        /// <summary>
        /// The thread lock.
        /// </summary>
        object m_lockStopsCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="TTSS"/> class.
        /// </summary>
        public TTSS()
        {
            m_dictStops = new Dictionary<string, List<Stop>>();
            m_dictCheckedStops = new Dictionary<string, CheckDate>();
            m_lockStopsCache = new object();
        }

        /// <summary>
        /// Gets list of autocomplete items. Max 20 items (sometimes lower, b'coz of doubled values filtration).
        /// </summary>
        /// <param name="name">Entered part.</param>
        /// <returns>List of stringz.</returns>
        public async Task<List<string>> Autocomplete(string name)
        {
            if(string.IsNullOrWhiteSpace(name) || name.Length <= 1)
            {
                return new List<string>();
            }

            int count = 0;
            string response = await GetString(string.Format(Req.Autocomplete, name));
            List<Stop> preparsed = new List<Stop>();
            JArray resp = JArray.Parse(response);
            foreach(JObject obj in resp)
            {
                if(obj.Value<string>("type") != "stop")
                {
                    count = obj.Value<int>("count");
                }
                else
                {
                    preparsed.Add(JsonConvert.DeserializeObject<Stop>(obj.ToString()));
                }
            }

            List<string> ret = new List<string>();
            foreach(Stop st in preparsed)
            {
                st.Name = WebUtility.HtmlDecode(st.Name);
                if(m_dictStops.ContainsKey(st.Name.ToLower()))
                {
                    if(!m_dictStops[st.Name.ToLower()].Any(s => s.ID == st.ID))
                    {
                        m_dictStops[st.Name.ToLower()].Add(st);
                    }
                }
                else
                {
                    m_dictStops.Add(st.Name.ToLower(), new List<Stop>() { st });
                }

                if(!ret.Any(r => r.ToLower() == st.Name.ToLower()))
                {
                    ret.Add(st.Name);
                }
            }

            return ret;
        }

        public async Task<List<Stop>> AutocompleteObj(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length <= 1)
            {
                return new List<Stop>();
            }

            int count = 0;
            string response = await GetString(string.Format(Req.Autocomplete, name));
            List<Stop> preparsed = new List<Stop>();
            JArray resp = JArray.Parse(response);
            foreach (JObject obj in resp)
            {
                if (obj.Value<string>("type") != "stop")
                {
                    count = obj.Value<int>("count");
                }
                else
                {
                    preparsed.Add(JsonConvert.DeserializeObject<Stop>(obj.ToString()));
                }
            }

            foreach(Stop s in preparsed)
            {
                s.Name = WebUtility.HtmlDecode(s.Name);
                s.Valid = true;
            }

            return preparsed;
        }

        /// <summary>
        /// Gets the stop information.
        /// </summary>
        /// <param name="stop">The stop object.</param>
        /// <param name="type">The stop info type.</param>
        /// <returns>Stop info</returns>
        public async Task<StopInfo> StopInfo(Stop stop, StopInfoType type = StopInfoType.Departure)
        {
            if (stop != null)
            {
                return await StopInfo(stop.ID, type);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the stop information.
        /// </summary>
        /// <param name="stopID">The stop identifier.</param>
        /// <param name="type">The type.</param>
        /// <returns>Stop info.</returns>
        public async Task<StopInfo> StopInfo(int stopID, StopInfoType type = StopInfoType.Departure)
        {
            string stype = string.Empty;
            switch(type)
            {
                case StopInfoType.Arrival:
                    stype = "arrival";
                    break;
                case StopInfoType.Departure:
                    stype = "departure";
                    break;
            }

            string data = await GetString(string.Format(Req.PassageInfo, stopID, stype));
            return JsonConvert.DeserializeObject<StopInfo>(data);
        }

        /// <summary>
        /// Gets the cached stops. Correction check is quite useful, because some of stops are duplicated and contain no passages.
        /// </summary>
        /// <param name="StopName">Name of the stop.</param>
        /// <param name="checkCorrection">if set to <c>true</c> checks correction.</param>
        /// <returns>List of stop objects for given name.</returns>
        public async Task<List<Stop>> GetCachedStops(string StopName, bool checkCorrection = true)
        {
            return await Task.Run(() =>
                {
                    lock (m_lockStopsCache)
                    {
                        if (!m_dictStops.ContainsKey(StopName.ToLower()))
                        {
                            return new List<Stop>();
                        }
                        else
                        {
                            if (checkCorrection
                                && (!m_dictCheckedStops.ContainsKey(StopName.ToLower())
                                    || m_dictCheckedStops[StopName.ToLower()].Checked == false
                                    || m_dictCheckedStops[StopName.ToLower()].Date.AddMinutes(60) < DateTime.Now))
                            {
                                foreach (Stop s in m_dictStops[StopName.ToLower()])
                                {
                                    StopInfo si = StopInfo(s.ID, StopInfoType.Departure).Result;
                                    if (si.ActualPassages != null && si.ActualPassages.Count > 0)
                                    {
                                        s.Valid = true;
                                    }
                                    else
                                    {
                                        s.Valid = false;
                                    }
                                }

                                if (m_dictCheckedStops.ContainsKey(StopName.ToLower()))
                                {
                                    m_dictCheckedStops[StopName.ToLower()] = new CheckDate(true);
                                }
                                else
                                {
                                    m_dictCheckedStops.Add(StopName.ToLower(), new CheckDate(true));
                                }
                            }

                            return m_dictStops[StopName.ToLower()];
                        }
                    }
                });
        }

        /// <summary>
        /// Gets the string from given URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        private static async Task<string> GetString(string url)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");

            var response = await httpClient.GetAsync(new Uri(url));

            response.EnsureSuccessStatusCode();
            IEnumerable<string> encoding = new List<string>();
            if (response.Content.Headers.TryGetValues("Content-Encoding", out encoding) && (string.Join(" ", encoding).Contains("gzip") || (string.Join(" ", encoding).Contains("deflate"))))
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                using (var decompressedStream = new GZipStream(responseStream, CompressionMode.Decompress))
                using (var streamReader = new StreamReader(decompressedStream))
                {
                    return await streamReader.ReadToEndAsync();
                }
            }
            else
            {
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
