using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace WcfService.Utility
{
    public class UtilNotification
    {
        public static void SendMessage(string identifier, string title, string message)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes("{"
                                        + "\"app_id\": \"" + System.Configuration.ConfigurationManager.AppSettings["OneSignalAppId"] + "\","
                                        + "\"contents\": {\"en\": \"" + message + "\"},"
                                        + "\"headings\": {\"en\": \"" + title + "\"},"
                                        + "\"include_player_ids\": [\"" + identifier + "\"]}");

            sendMsg(byteArray);
        }

        public static void BroadCastMessage(string[] identifiers, string title, string message)
        {
            string identifierList = string.Empty;
            foreach(string identifier in identifiers)
            {
                if(identifierList != string.Empty)
                {
                    identifierList += ",";
                }

                identifierList += ("\"" + identifier + "\"");
            }

            byte[] byteArray = Encoding.UTF8.GetBytes("{"
                            + "\"app_id\": \"" + System.Configuration.ConfigurationManager.AppSettings["OneSignalAppId"] + "\","
                            + "\"contents\": {\"en\": \"" + message + "\"},"
                            + "\"headings\": {\"en\": \"" + title + "\"},"
                            + "\"include_player_ids\": [" + identifierList  + "]}");

            sendMsg(byteArray);
        }

        private static void sendMsg(byte[] byteArray)
        {
            // send push notification if location was within boundary 
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json";

            request.Headers.Add("authorization", "Basic " + System.Configuration.ConfigurationManager.AppSettings["OneSignalAppAPI"]);

            string responseContent = null;
            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }
        }
    }
}