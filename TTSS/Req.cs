namespace TTSSLib
{
    /// <summary>
    /// Class containing services URIs.
    /// </summary>
    internal class Req
    {
        /// <summary>
        /// The autocomplete service address. Provides list of matching stop names. {0} indicates value, that you have already.
        /// </summary>
        public static readonly string Autocomplete = @"http://www.ttss.krakow.pl/internetservice/services/lookup/autocomplete/json?query={0}";

        /// <summary>
        /// The autocomplete service address. Provides list of stops nearby specified coords. {0} is latitude, {1} is longtitude.
        /// </summary>
        public static readonly string GpsAutocomplete = @"http://www.ttss.krakow.pl/internetservice/services/lookup/autocomplete/nearStops/json?lat={0}&lon={1}";

        /// <summary>
        /// The passage information service address. Returns list of trams passing by that stop, ans some useful info. {0} indicates stop ID, {1} indicates if "arrival" or "departure". I suggest using "departure".
        /// </summary>
        public static readonly string PassageInfo = @"http://www.ttss.krakow.pl/internetservice/services/passageInfo/stopPassages/stop?stop={0}&mode={1}";

        /// <summary>
        /// The route information containing list of stops (unfortunately not in order). {0} is stop ID.
        /// </summary>
        public static readonly string RouteInfo = @"http://www.ttss.krakow.pl/internetservice/services/routeInfo/routeStops?routeId={0}";
    }
}
