#region "    using    "
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using DCS_Hawkeye_Server.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Owin.Security.Cookies;
using Newtonsoft;
using Newtonsoft.Json;
using System.IO;
#endregion

namespace DCS_Hawkeye_Server.DataLayer
{
    public class LocationDataProvider
    {
        public static int SetLocationDataDB(byte[] input)
        {
            int retval = 0;
            //string strInput = Encoding.UTF8.GetString(input);
            var testFile = @"C:\Users\Administrator\Documents\Visual Studio 2015\Projects\dcs-hawkeye\DCS-Hawkeye-Server\DebugData\locationTest - Mod.js";
            string strInput = File.ReadAllText(testFile);
            //convert post to class list
            var listLocationUpdate = JsonConvert.DeserializeObject<List<DCS_Hawkeye_Server.Models.LocationModel.MapBoxPackage>>(strInput);
            //convert list to datatable
            var dtLocationUpdate = ConvertToDataTable(listLocationUpdate);
            //send to SQL
            List<SqlParameter> parameters = new List<SqlParameter>();
            try
            {
                parameters.Add(new SqlParameter("@list", dtLocationUpdate));
                retval = SqlHelper.ExecuteNonQuery("stp_SetUpdatedLocation", parameters.ToArray());
            }
            catch
            {}
            return retval;
        }

        public static int DummySetLocationDataDB()
        {
            int retval = 0;
            //string strInput = Encoding.UTF8.GetString(input);
            var testFile = @"C:\Users\Administrator\Documents\Visual Studio 2015\Projects\dcs-hawkeye\DCS-Hawkeye-Server\DebugData\locationTest - Mod.js";
            string strInput = File.ReadAllText(testFile);
            //convert post to class list
            var listLocationUpdate = JsonConvert.DeserializeObject<List<DCS_Hawkeye_Server.Models.LocationModel.MapBoxPackage>>(strInput);
            //convert list to datatable
            var dtLocationUpdate = ConvertToDataTable(listLocationUpdate);
            //send to SQL
            List<SqlParameter> parameters = new List<SqlParameter>();
            try
            {
                parameters.Add(new SqlParameter("@list", dtLocationUpdate));
                retval = SqlHelper.ExecuteNonQuery("stp_SetUpdatedLocation", parameters.ToArray());
            }
            catch (Exception ex)
            {
                string exception = "Error at DummySetLocationDataDB" + ex.ToString();
            }
            return retval;
        }

        public static string GetLocationDataDB()
        {
            string result = string.Empty;
            var retval = SqlHelper.TSQLExecuteScalarParam("stp_GetTestLocationData", null);
            result = retval.ToString();
            return result;
        }

        public static string GetTestLocationData(byte[] input)
        {
            string result = string.Empty;
            string strInput = Encoding.UTF8.GetString(input);
            switch (strInput)
            {
                case "Nikki":
                    result = "Nikki is great!";
                    break;
                case "Matthew":
                    result = "Matthew is shit!";
                    break;
                default:
                    result = "Not known here";
                    break;
            }
            return result;
        }
        public static string GetLocationData()
        {
            //return value
            string LocationResult = string.Empty;

            //instantiate list
            LocationModel.LocationViewModel LocationList = new LocationModel.LocationViewModel();
            LocationList.ListPoints = new List<LocationModel.LocationPoint>();

            //instantiate object
            var LocModel = SetNewModel((decimal)41.6168, (decimal)41.6367);

            //populate object
            LocModel.FeatureType = "Feature";
            LocModel.LocationGeometry.GeometryType = "Point";
            LocModel.Properties.PropTitle = "Batumi";
            LocModel.Properties.PropIcon = "air_friendly";
            //add to list
            LocationList.ListPoints.Add(LocModel);

            //instantiate object
            LocModel = SetNewModel((decimal)42.2662, (decimal)42.7180);

            //populate object
            LocModel.FeatureType = "Feature";
            LocModel.LocationGeometry.GeometryType = "Point";
            LocModel.Properties.PropTitle = "Kutaisi";
            LocModel.Properties.PropIcon = "air_enemy";
            //add to list
            LocationList.ListPoints.Add(LocModel);

            //convert class to json
            LocationResult = JsonConvert.SerializeObject(LocationList);

            //eliminate listname
            LocationResult = LocationResult.Replace("\"ListPoints\":", "");

            return LocationResult;
        }

        public static DCS_Hawkeye_Server.Models.LocationModel.LocationPoint SetNewModel(decimal longit, decimal latit)
        {
            //instantiate parent
            var result = new LocationModel.LocationPoint();

            //instantiate children
            var properties = new LocationModel.LocationProperties();
            var geometry = new LocationModel.LocationGeometry();
            var coords = new LocationModel.LocationCoordinates();

            //handle coordinates
            coords.Longit = longit;
            coords.Latit = latit;

            //add one to the other
            geometry.LongLat = coords;
            result.LocationGeometry = geometry;
            result.Properties = properties;

            return result;
        }

        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }
}