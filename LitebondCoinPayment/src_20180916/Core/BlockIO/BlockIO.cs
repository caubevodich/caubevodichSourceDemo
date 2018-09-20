﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace Core.BlockIO
{
    public class BlockIO
    {
        //private string ApiKey = "9223-2fca-1673-f1ed";
        //private string Pin = "0976060432";
        //private string Uri = "https://block.io/api/v2/";
        private string ApiKey = "6ea0-8536-dc63-acd3"; //best of info.litebond@gmail.com
        private string Pin = "12345678";
        private string Uri = "https://block.io/api/v2/";

        private APIResponse apiCall(string Method, NameValueCollection Parameters)
        {
            WebClient Client = new WebClient();
            Uri url = new Uri(Uri + Method + "/");
            JavaScriptSerializer ser = new JavaScriptSerializer();
            string JsonString = string.Empty;
            APIResponse Response = new APIResponse();
            Parameters.Add("api_key", this.ApiKey);
            try
            {
                string urlWithParams = url + "?" + Parameters.ToString();
                JsonString = Client.DownloadString(urlWithParams);
            }
            catch (WebException ex)
            {
                JsonString = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
            }
            Response = ser.Deserialize<APIResponse>(JsonString);
            if (Response.Status == "success")
            {
                if (Response.Data.ContainsKey("user_id"))
                {
                    Response.Data["user_id"] = Convert.ToInt32(Response.Data["user_id"]);
                }
            }
            else
            {
                throw new Exception("Block.io API Error: " + Response.Data["error_message"]);
            }
            return Response;
        }

        private string CombineString(string DestinationString, string Item)
        {
            if (DestinationString == string.Empty)
            {
                DestinationString = Item;
            }
            else
            {
                DestinationString = DestinationString + "," + Item;
            }
            return DestinationString;
        }

        /// <summary>
        /// Returns a newly generated address, and its unique(!) label generated by Block.io. You can optionally specify a custom label.
        /// </summary>
        /// <param name="getNewAddress"></param>
        /// <returns>APIResponse</returns>
        public APIResponse getNewAddress(string label = null)
        {
            NameValueCollection Params = System.Web.HttpUtility.ParseQueryString(string.Empty);
            if (label != null)
            {
                Params.Add("label", label);
            }
            return apiCall("get_new_address", Params);
        }

        /// <summary>
        /// Returns the balance of your entire Bitcoin, Litecoin, or Dogecoin account (i.e., the sum of balances of all addresses/users within it) as numbers to 8 decimal points, as strings.
        /// </summary>
        /// <returns>APIResponse</returns>
        public APIResponse getBalance()
        {
            NameValueCollection Params = System.Web.HttpUtility.ParseQueryString(string.Empty);
            return apiCall("get_balance", Params);
        }
        public APIResponse getAddressBalance(string address)
        {
            NameValueCollection Params = System.Web.HttpUtility.ParseQueryString(address);            
            if (address != null)
            {
                Params.Add("addresses", address);
            }
            return apiCall("get_address_balance", Params);
        }
        /// <summary>
        /// Returns the balance of the specified addresses, or labels.
        /// </summary>
        /// <returns></returns>
        public APIResponse getMyAddresses()
        {
            NameValueCollection Params = System.Web.HttpUtility.ParseQueryString(string.Empty);
            return apiCall("get_my_addresses", Params);
        }

        /// <summary>
        /// Returns all the addresses, their labels, and user ids on your account. must specify addresses or labels.
        /// </summary>
        /// <returns>APIResponse</returns>
        public APIResponse getAddressBalance(string Type, List<string> Values)
        {
            NameValueCollection Params = System.Web.HttpUtility.ParseQueryString(string.Empty);
            string CombinedValues = string.Empty;
            if (Type != null)
            {
                foreach (string Item in Values)
                {
                    CombinedValues = CombineString(CombinedValues, Item);
                }
                Params.Add(Type, CombinedValues);
            }
            else
            {
                throw new Exception("Block.io API Error: you must enter a Type");
            }

            return apiCall("get_my_addresses", Params);
        }

        /// <summary>
        /// Returns the address specified by a label.
        /// </summary>
        /// <param name="Label"></param>
        /// <returns></returns>
        public APIResponse getAddressByLabel(string Label)
        {
            NameValueCollection Params = System.Web.HttpUtility.ParseQueryString(string.Empty);
            Params.Add("label", Label);

            return apiCall("get_address_by_label", Params);
        }

        /// <summary>
        /// Withdraws amount of coins from any addresses in your account to up to 100 destination addresses.
        /// </summary>
        /// <returns></returns>
        public APIResponse withdraw(List<string> Amounts, List<string> Addresses )
        {
            NameValueCollection Params = System.Web.HttpUtility.ParseQueryString(string.Empty);
            string CombinedAmounts = string.Empty;
            string CombinedAddresses = string.Empty;
            foreach (string Item in Amounts)
            {
                CombinedAmounts = CombineString(CombinedAmounts, Item);
            }
            foreach (string Item in Addresses)
            {
                CombinedAddresses = CombineString(CombinedAddresses, Item);
            }
            Params.Add("amounts", CombinedAmounts);
            Params.Add("to_addresses", CombinedAddresses);
            Params.Add("pin", Pin);

            return apiCall("withdraw", Params);
        }

        /// <summary>
        /// Withdraws AMOUNT coins from upto 100 addresses at a time, and deposits it to up to 100 destination addresses.
        /// </summary>
        /// <returns></returns>
        public APIResponse withdrawFromAddresses(List<string> FromAddresses, List<string> ToAddresses, List<string> Amounts)
        {
            NameValueCollection Params = System.Web.HttpUtility.ParseQueryString(string.Empty);
            string CombinedFromAddresses = string.Empty;
            string CombinedToAddresses = string.Empty;
            string CombinedAmounts = string.Empty;
            foreach (string Item in FromAddresses)
            {
                CombinedFromAddresses = CombineString(CombinedFromAddresses, Item);
            }
            foreach (string Item in ToAddresses)
            {
                CombinedToAddresses = CombineString(CombinedToAddresses, Item);
            }
            foreach (string Item in Amounts)
            {
                CombinedAmounts = CombineString(CombinedAmounts, Item);
            }
            Params.Add("from_addresses", CombinedFromAddresses);
            Params.Add("to_addresses", CombinedToAddresses);
            Params.Add("amounts", CombinedAmounts);
            Params.Add("pin", Pin);
            return apiCall("withdraw_from_addresses", Params);
        }

        /// <summary>
        /// Withdraws AMOUNT coins from upto 100 labels at a time, and deposits it to upto 100 destination addresses, or labels.
        /// </summary>
        /// <returns></returns>
        public APIResponse withdrawFromLabels(List<string> FromLabels, List<string> Amounts,  List<string> ToAddresses = null, List<string> ToLabels = null)
        {
            NameValueCollection Params = System.Web.HttpUtility.ParseQueryString(string.Empty);
            string CombinedFromLabels = string.Empty;
            string CombinedToLabels = string.Empty;
            string CombinedToAddresses = string.Empty;
            string CombinedAmounts = string.Empty;
            foreach (string Item in FromLabels)
            {
                CombinedFromLabels = CombineString(CombinedFromLabels, Item);
            }
            if (ToAddresses != null || ToLabels == null)
            {
                foreach (string Item in ToAddresses)
                {
                    CombinedToAddresses = CombineString(CombinedToAddresses, Item);
                }
                Params.Add("to_addresses", CombinedToAddresses);
            }
            if (ToLabels != null || ToAddresses == null)
            {
                foreach (string Item in ToLabels)
                {
                    CombinedToLabels = CombineString(CombinedToLabels, Item);
                }
                Params.Add("to_labels", CombinedToLabels);
            }

            foreach (string Item in Amounts)
            {
                CombinedAmounts = CombineString(CombinedAmounts, Item);
            }
            Params.Add("from_labels", CombinedFromLabels);
            Params.Add("amounts", CombinedAmounts);
            Params.Add("pin", Pin);
            return apiCall("withdraw_from_labels", Params);
        }

        /// <summary>
        /// Returns the prices from the largest exchanges for Bitcoin, Dogecoin, or Litecoin, specified by the API Key. Specifying the base currency is optional.
        /// </summary>
        /// <returns></returns>
        public APIResponse getCurrentPrices(string BaseCurrency = null)
        {
            NameValueCollection Params = System.Web.HttpUtility.ParseQueryString(string.Empty);
            if (BaseCurrency != null)
            {
                Params.Add("price_base", BaseCurrency);
            }

            return apiCall("get_current_prices", Params);
        }

        /// <summary>
        /// Returns an array of Block.io Green Addresses. Funds sent from Green Addresses are guaranteed by Block.io, and can be used immediately on receipt with zero network confirmations. This API call does not need an API Key.
        /// </summary>
        /// <returns></returns>
        public APIResponse isGreenAddress(List<string> Addresses)
        {
            NameValueCollection Params = System.Web.HttpUtility.ParseQueryString(string.Empty);
            string CombinedAddresses = string.Empty;
            foreach (string Item in Addresses)
            {
                CombinedAddresses = CombineString(CombinedAddresses, Item);
            }
            Params.Add("addresses", CombinedAddresses);

            return apiCall("is_green_address", Params);
        }

        /// <summary>
        /// Returns an array of transactions that were sent by Block.io Green Addresses. Funds sent from Green Addresses are guaranteed by Block.io, and can be used immediately on receipt with zero network confirmations. This API call does not need an API Key.
        /// </summary>
        /// <returns></returns>
        public APIResponse isGreenTransaction(List<string> Transactions)
        {
            NameValueCollection Params = System.Web.HttpUtility.ParseQueryString(string.Empty);
            string CombinedTransactions = string.Empty;
            foreach (string Item in Transactions)
            {
                CombinedTransactions = CombineString(CombinedTransactions, Item);
            }
            Params.Add("transaction_ids", CombinedTransactions);

            return apiCall("is_green_transaction", Params);
        }

        /// <summary>
        /// Returns various data for the last 100 transactions spent or received. You can optionally specify a before_tx parameter to get earlier transactions.
        /// </summary>
        /// <returns></returns>
        public APIResponse getTransactions(string Type)
        {
            NameValueCollection Params = System.Web.HttpUtility.ParseQueryString(string.Empty);
            Params.Add("type", Type);

            return apiCall("get_transactions", Params);
        }

        /// <summary>
        /// Returns various data for the last 100 transactions spent or received. You can optionally specify a before_tx parameter to get earlier transactions.
        /// </summary>
        /// <returns></returns>
        public APIResponse getTransactions(string Type, string BeforeTX)
        {
            NameValueCollection Params = System.Web.HttpUtility.ParseQueryString(string.Empty);
            Params.Add("type", Type);
            Params.Add("before_tx", BeforeTX);

            return apiCall("get_transactions", Params);
        }

        /// <summary>
        /// Returns various data for the last 100 transactions spent or received. You can optionally specify a before_tx parameter to get earlier transactions.
        /// </summary>
        /// <returns></returns>
        public APIResponse getTransactions(string Type, string ListType, List<string> List)
        {
            NameValueCollection Params = System.Web.HttpUtility.ParseQueryString(string.Empty);
            string CombinedList = string.Empty;
            foreach (string Item in List)
            {
                CombinedList = CombineString(CombinedList, Item);
            }
            Params.Add("type", Type);
            Params.Add(ListType, CombinedList);

            return apiCall("get_transactions", Params);
        }

        /// <summary>
        /// Returns various data for the last 100 transactions spent or received. You can optionally specify a before_tx parameter to get earlier transactions.
        /// </summary>
        /// <returns></returns>
        public APIResponse getTransactions(string Type, string BeforeTX, List<string> Addresses, string SETNULL)
        {
            NameValueCollection Params = System.Web.HttpUtility.ParseQueryString(string.Empty);
            string CombinedAddresses = string.Empty;
            foreach (string Item in Addresses)
            {
                CombinedAddresses = CombineString(CombinedAddresses, Item);
            }
            Params.Add("type", Type);
            Params.Add("before_tx", BeforeTX);
            Params.Add("addresses", CombinedAddresses);

            return apiCall("get_transactions", Params);
        }


        public APIResponse getAllTransactionDeposit(string address)
        {
            NameValueCollection Params = System.Web.HttpUtility.ParseQueryString(string.Empty);
            Params.Add("type", "received");            
            Params.Add("addresses", address);
            return apiCall("get_transactions", Params);
        }
    }
}