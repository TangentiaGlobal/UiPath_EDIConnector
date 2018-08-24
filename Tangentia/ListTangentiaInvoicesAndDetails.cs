using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Security;
using System.Text;

namespace Tangentia
{
    public class ListTangentiaInvoicesAndDetails : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> Email { get; set; }

        [Category("Input")]
        [RequiredArgument]
        //[PasswordPropertyText(true)]
        public InArgument<SecureString> Password { get; set; }

        [Category("Input")]
        public InArgument<DateTime> FromDate { get; set; } = null;

        [Category("Input")]
        public InArgument<DateTime> ToDate { get; set; } = null;

        [Category("Output")]
        public OutArgument<List<invoice_header>> Invoices { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            string password = new NetworkCredential(string.Empty, Password.Get(context)).Password;
            string AT = "";
            WebRequest request = WebRequest.Create("http://104.197.60.212/token");
            request.Method = "POST";
            string postData = "grant_type=password&username=" + Email.Get(context) + "&password=" + password;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "text/plain";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            using (WebResponse response = request.GetResponse())
            {
                dataStream = response.GetResponseStream();
                using (StreamReader reader = new StreamReader(dataStream))
                {
                    var at = JObject.Parse(reader.ReadToEnd());
                    AT = at["access_token"].ToString();
                }
                dataStream.Close();
            }
            //////////////////////////////////////////////////////////////////////////////////////////////
            DateTime fromDate = FromDate.Get(context);
            DateTime toDate = ToDate.Get(context);
            string data = string.Empty;
            string url = string.Empty;
            if (FromDate.Get(context) == null && ToDate.Get(context) == null)
                url = @"http://104.197.60.212/api/invoice_header";
            else if (FromDate.Get(context) != null && ToDate.Get(context) != null)
                url = @"http://104.197.60.212/api/invoice_header?from=" + fromDate.ToString("yyyy-MM-dd") + "&to=" + toDate.ToString("yyyy-MM-dd");
            else if (FromDate.Get(context) != null && ToDate.Get(context) == null)
                url = @"http://104.197.60.212/api/invoice_header?from=" + fromDate.ToString("yyyy-MM-dd");
            else if (FromDate.Get(context) == null && ToDate.Get(context) != null)
                url = @"http://104.197.60.212/api/invoice_header?to=" + toDate.ToString("yyyy-MM-dd");
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(url);
            request1.Headers.Add("Authorization", "Bearer " + AT);
            request1.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request1.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                data = reader.ReadToEnd();
            }
            List<int> InvoiceNumbers = JsonConvert.DeserializeObject<List<int>>(data);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            List<invoice_header> output1 = new List<invoice_header>();
            foreach (int InvoiceNumber in InvoiceNumbers)
            {
                string IH = GetInvoiceH(AT, InvoiceNumber.ToString());
                string II = GetInvoiceI(AT, InvoiceNumber.ToString());
                invoice_header invoiceheader = JsonConvert.DeserializeObject<invoice_header>(IH);
                invoiceheader.invoice_details_list = JsonConvert.DeserializeObject<List<invoice_details>>(II);
                output1.Add(invoiceheader);
            }
            Invoices.Set(context, output1);
        }

        static string GetInvoiceH(string access_token, string docnum)
        {
            string data = string.Empty;
            string url = @"http://104.197.60.212/api/invoice_header/" + docnum;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("Authorization", "Bearer " + access_token);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                data = reader.ReadToEnd();
            }
            return data;
        }

        static string GetInvoiceI(string access_token, string docnum)
        {
            string data = string.Empty;
            string url = @"http://104.197.60.212/api/invoice_details/" + docnum;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("Authorization", "Bearer " + access_token);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                data = reader.ReadToEnd();
            }
            return data;
        }
    }
}