using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTSSLib
{
    /// <summary>
    /// Class containing things about stop, from autocomplete service.
    /// </summary>
    public class Stop
    {
        /// <summary>
        /// Gets or sets the stop identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty("id")]
        public int ID { get; set; }


        /// <summary>
        /// Gets or sets the stop name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Stop"/> is valid. Not existing in JSON (as you can see by JsonIgnoreAttribute).
        /// </summary>
        /// <value>
        ///   <c>true</c> if valid; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool Valid { get; set; }
    }
}
