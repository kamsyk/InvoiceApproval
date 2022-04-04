using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace BulkTest {
    class Program {
        static void Main(string[] args) {
            int iCount = 0;
            using (StreamReader sr = new StreamReader(@"c:\temp\ClcPur.csv")) {
                string strLine = null;
                while ((strLine = sr.ReadLine()) != null) {
                    iCount++;
                    Console.WriteLine(iCount);

                    string orderNr = strLine;
#if DEBUG
                    WcfOrderDebug.OrderWcfClient wcfOrder = new WcfOrderDebug.OrderWcfClient();
#else
                    WcfOrder.OrderWcfClient wcfOrder = new WcfOrder.OrderWcfClient();
#endif
                    var wcfOrderStatus = wcfOrder.GetOrderStatus(orderNr);
                    if (wcfOrderStatus != null) {
                        Console.WriteLine("**************** Wcf Client **********************************");
                        Console.WriteLine("Order Nr: " + wcfOrderStatus.OrderNr);
                        Console.WriteLine("Source Application: " + wcfOrderStatus.OrderNr);
                        if (wcfOrderStatus.Price != null) {
                            Console.WriteLine("Price: " + wcfOrderStatus.Price);
                            Console.WriteLine("Currency: " + wcfOrderStatus.Currency);
                        } else {
                            Console.WriteLine("Price: ");
                        }
                        if (wcfOrderStatus.BuyerMail != null) {
                            Console.WriteLine("Buyer: " + wcfOrderStatus.BuyerMail);
                        } else {
                            Console.WriteLine("Buyer: ");
                        }
                        Console.WriteLine("Approval Status: " + wcfOrderStatus.Status);
                        Console.WriteLine("Approval Status Text: " + wcfOrderStatus.StatusText);
                        Console.WriteLine("**************************************************");
                        Console.WriteLine(wcfOrderStatus.Status);
                        if (wcfOrderStatus.Status == 50) {
                            Console.WriteLine("Error");
                            Console.ReadLine();
                        }
                    }

#if DEBUG
                    WsOrderDebug.OrderWcf wsfOrder = new WsOrderDebug.OrderWcf();
#else
                    WsOrder.OrderWcf wsfOrder = new WsOrder.OrderWcf();
#endif
                    var wsOrderStatus = wcfOrder.GetOrderStatus(orderNr);
                    if (wsOrderStatus != null) {
                        Console.WriteLine("*************** Web Service Client ***********************************");
                        Console.WriteLine("Order Nr: " + wsOrderStatus.OrderNr);
                        Console.WriteLine("Source Application: " + wsOrderStatus.OrderNr);
                        if (wsOrderStatus.Price != null) {
                            Console.WriteLine("Price: " + wsOrderStatus.Price);
                            Console.WriteLine("Currency: " + wsOrderStatus.Currency);
                        } else {
                            Console.WriteLine("Price: ");
                        }
                        if (wsOrderStatus.BuyerMail != null) {
                            Console.WriteLine("Buyer: " + wsOrderStatus.BuyerMail);
                        } else {
                            Console.WriteLine("Buyer: ");
                        }
                        Console.WriteLine("Approval Status: " + wsOrderStatus.Status);
                        Console.WriteLine("Approval Status Text: " + wsOrderStatus.StatusText);
                        Console.WriteLine("**************************************************");
                        if (wcfOrderStatus.Status == 50) {
                            Console.WriteLine("Error");
                            Console.ReadLine();
                        }
                    }

                    Order orderWebApi = GetWebApiOrderStatus(orderNr).Result;
                    if (orderWebApi != null) {
                        Console.WriteLine("*************** Http REST API Client ***********************************");
                        Console.WriteLine("Order Nr: " + orderWebApi.OrderNr);
                        Console.WriteLine("Source Application: " + orderWebApi.OrderNr);
                        if (orderWebApi.Price != null) {
                            Console.WriteLine("Price: " + orderWebApi.Price);
                            Console.WriteLine("Currency: " + orderWebApi.Currency);
                        } else {
                            Console.WriteLine("Price: ");
                        }
                        if (orderWebApi.BuyerMail != null) {
                            Console.WriteLine("Buyer: " + orderWebApi.BuyerMail);
                        } else {
                            Console.WriteLine("Buyer: ");
                        }
                        Console.WriteLine("Approval Status: " + orderWebApi.Status);
                        Console.WriteLine("Approval Status Text: " + orderWebApi.StatusText);
                        Console.WriteLine("**************************************************");
                        if (orderWebApi.Status == 50) {
                            Console.WriteLine("Error");
                            Console.ReadLine();
                        }
                    }
                }
            }
        }

        private static async Task<Order> GetWebApiOrderStatus(string orderNr) {
            HttpClient client = new HttpClient();
            var serializer = new DataContractJsonSerializer(typeof(Order));

            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            //client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

#if DEBUG
            Task<System.IO.Stream> streamTask = client.GetStreamAsync("http://localhost:65010/api/OrderApi/GetOrderStatus?orderNr=" + orderNr);
#else
            Task<System.IO.Stream> streamTask = client.GetStreamAsync("http://intranetcz.cz.eu.otis.utc.com/OtisOrder/api/OrderApi/GetOrderStatus?orderNr=" + orderNr);
#endif
            Order orderStatus = serializer.ReadObject(await streamTask) as Order;
            return orderStatus;
        }
    }

    public class Order {
        public string OrderNr = null;
        public int Status = 0;
        public string StatusText = null;
        public string ApplicationName = null;
        public decimal? Price = null;
        public string Currency = null;
        public string BuyerMail = null;
    }
}
