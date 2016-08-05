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
        public static string Autocomplete = @"http://www.ttss.krakow.pl/internetservice/services/lookup/autocomplete/json?query={0}";

        /// <summary>
        /// The passage information service address. Returns list of trams passing by that stop, ans some useful info. {0} indicates stop ID, {1} indicates if "arrival" or "departure". I suggest using "departure".
        /// </summary>
        public static string PassageInfo = @"http://www.ttss.krakow.pl/internetservice/services/passageInfo/stopPassages/stop?stop={0}&mode={1}";
    }
}
