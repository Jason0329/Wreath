using Nop.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.SMS_SendText
{
    public class SendTextSMS
    {
        private readonly String USER_AGENT = "Mozilla/5.0";
        private readonly String SMS_URL = "http://smexpress.mitake.com.tw:7002/SpLmGet?";
        private readonly String username = "69486892";
        private readonly String password = "Champion";

        public string PrepareSendText(Order OrderPlaced)
        {
            string SendText = SMS_URL;

            SendText += "username=" + username;
            SendText += "&password=" + password;
            SendText += "&dstaddr=" + OrderPlaced.BillingAddress.PhoneNumber;
            SendText += "&smbody=" + OrderPlacedItemQuotedPrice(OrderPlaced) + ((char)6) + OrderPlacedItemLink(OrderPlaced);

            SendText += "&CharsetURL=utf8";

            return SendText;
        }

     

        public int SendMessage(string SendText)
        {
            try{
                HttpWebRequest request = null;
                string responseMessage = string.Empty;
                byte[] data = new System.Text.ASCIIEncoding().GetBytes(SendText.ToString());

                request = (HttpWebRequest)WebRequest.Create(SendText);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                // Write data to stream 
                using (Stream newStream = request.GetRequestStream())
                {
                    newStream.Write(data, 0, data.Length);
                }

                //Send the request and get a response 
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    // Read the response 
                    using (StreamReader srResponse = new StreamReader(response.GetResponseStream()))
                    {
                        responseMessage = srResponse.ReadToEnd();
                    }

                    // Logic to interpret response from your gateway goes here 
                    // Response.Write(String.Format("Response from gateway: {0}", responseMessage));
                }

                return 0;

            }
            catch (Exception eee)
            {
                Console.WriteLine(eee.Message);
                return -1;
            }

            
        }



        public string OrderPlacedItemQuotedPrice(Order OrderPlaced)
        {
            string ReturnQuotedPrice =  "詢價日期:";
            ReturnQuotedPrice += OrderPlaced.CreatedOnUtc.ToShortDateString();

            foreach (OrderItem item in OrderPlaced.OrderItems)
            {
                if (item.Product.IsDownload) continue;

                ReturnQuotedPrice += ((char)6).ToString() + item.Product.Name;

                if (item.AttributeDescription.Contains("商品顏色"))
                {
                    string[] AttributeValue = item.AttributeDescription.Split(new string[] { "<br />", "[" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string CarAttribute in AttributeValue)
                    {
                        if (CarAttribute.Contains("商品顏色"))
                        {
                            ReturnQuotedPrice += ((char)6).ToString() + CarAttribute.Trim();
                            break;
                        }
                    }

                }

                ReturnQuotedPrice += ((char)6).ToString() + "官方售價:" + (item.Product.OldPrice / 10000).ToString().Trim('0').Trim('.') + "萬";

                if (!(item.Product.Price==0) && item.Product.Price!=null)
                {
                    ReturnQuotedPrice += ((char)6).ToString() + "新車成交價:";
                    ReturnQuotedPrice += (item.UnitPriceExclTax / 10000).ToString().Trim('0').Trim('.') + "萬";
                }

                if (item.Product.AdminComment != null && item.Product.AdminComment.CompareTo("")!=0)
                {
                    ReturnQuotedPrice += ((char)6).ToString() + "平台成交加贈:";
                    ReturnQuotedPrice += item.Product.AdminComment;
                }
            }


            return ReturnQuotedPrice;
        }

        public string OrderPlacedItemLink(Order OrderPlaced)
        {
            string ReturnQuotedLink = "";

            string _EncryptString = EncryptString(OrderPlaced.Id.ToString()).Replace("+","99999");

            ReturnQuotedLink += "點選連結，※獲取此價格經銷商資訊※:http://gotruecar.com/AskRequire?id=" + _EncryptString + ((char)6).ToString() + " 專業GoTRUECAR提供嚴選經銷商議價";

            return ReturnQuotedLink;
        }


        public string EncryptString(string Message, string Passphrase = "69486892Champion")
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(Message);
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results).Trim('=');
        }

        public string DecryptString(string Message, string Passphrase = "69486892Champion")
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToDecrypt = Convert.FromBase64String(Message + "=");
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return UTF8.GetString(Results);
        }

    }
}
