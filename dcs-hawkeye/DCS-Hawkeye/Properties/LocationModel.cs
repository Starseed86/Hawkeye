using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DCS_Hawkeye_Server.Models
{
    public class LocationModel
    {
        //DCS package model
        public class MapBoxPackage
        {
            public float latitude { get; set; }
            public float longitude { get; set; }
            public string coalitionName { get; set; }
            public int countryID { get; set; }
            public int groupID { get; set; }
            public string groupName { get; set; }
            public string type { get; set; }
            public int unitID { get; set; }
            public string unitName { get; set; }
        }

        //view model
        public class LocationViewModel
        {
            [JsonProperty(PropertyName = "type")] public string LocationType = "FeatureCollection";
            [JsonProperty(PropertyName = "features")]
            public List<LocationPoint> ListPoints { get; set; }
        }

        //intermediate classes
        public class LocationPoint
        {
            [JsonProperty(PropertyName = "type")]
            public string FeatureType { get; set; }
            [JsonProperty(PropertyName = "geometry")]
            public LocationGeometry LocationGeometry { get; set; }
            [JsonProperty(PropertyName = "properties")]
            public LocationProperties Properties { get; set; }
        }

        public class LocationGeometry
        {
            [JsonProperty(PropertyName = "type")]
            public string GeometryType { get; set; }
            [JsonProperty(PropertyName = "coordinates")]
            public LocationCoordinates LongLat { get; set; }
        }

        //atomic classes
        
        public class LocationCoordinates : IEnumerable<object>
        {
            public decimal Longit;
            public decimal Latit;

            IEnumerator<object> IEnumerable<object>.GetEnumerator() => EnumerateFields().GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => EnumerateFields().GetEnumerator();
            IEnumerable<object> EnumerateFields()
            {
                yield return Longit;
                yield return Latit;
            }
        }

        public class LocationProperties
        {
            [JsonProperty(PropertyName = "title")]
            public string PropTitle { get; set; }
            [JsonProperty(PropertyName = "icon")]
            public string PropIcon { get; set; }
        }

    }
}