using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Core.BlockIO
{
    public class ChainSO
    {
        public string apiUrl = "https://chain.so/api/v2/";

        public int GetTxtConfirmed(string network, string txId)
        {
            string url = apiUrl + "is_tx_confirmed/" + network + "/" + txId;
            WebClient Client = new WebClient();
           
            JavaScriptSerializer ser = new JavaScriptSerializer();
            string JsonString = string.Empty;
            APIResponse Response = new APIResponse();
           
            try
            {                
                JsonString = Client.DownloadString(url);
            }
            catch (WebException ex)
            {
                JsonString = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
            }
            Response = ser.Deserialize<APIResponse>(JsonString);
            int confirmations = int.Parse(Response.Data["confirmations"].ToString());
            return confirmations;
        }
       
    }
}
