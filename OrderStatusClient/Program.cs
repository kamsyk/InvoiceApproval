using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace OrderStatusClient {
    class Program {
        static void Main(string[] args) {
            Console.Write("Enter Order Number:");
            string orderNr = Console.ReadLine();

            #region Wcf Client
            WcfOrder.OrderWcfClient wcfOrder = new WcfOrder.OrderWcfClient();
            var wcfOrderStatus = wcfOrder.GetOrderStatus(orderNr);

            Console.WriteLine("**************** Wcf Client **********************************");
            Console.WriteLine("Order Nr: " + wcfOrderStatus.OrderNr);
            Console.WriteLine("Source Application: " + wcfOrderStatus.OrderNr);
            Console.WriteLine("Approval Status: " + wcfOrderStatus.Status);
            Console.WriteLine("Approval Status Text: " + wcfOrderStatus.StatusText);
            Console.WriteLine("Application: " + wcfOrderStatus.ApplicationName);
            Console.WriteLine("**************************************************");
            #endregion

            Console.WriteLine();

            #region Ws Client
            WsOrder.OrderWcf wsfOrder = new WsOrder.OrderWcf();
            var wsOrderStatus = wcfOrder.GetOrderStatus(orderNr);

            Console.WriteLine("*************** Web Service Client ***********************************");
            Console.WriteLine("Order Nr: " + wsOrderStatus.OrderNr);
            Console.WriteLine("Source Application: " + wsOrderStatus.OrderNr);
            Console.WriteLine("Approval Status: " + wsOrderStatus.Status);
            Console.WriteLine("Approval Status Text: " + wsOrderStatus.StatusText);
            Console.WriteLine("Application: " + wcfOrderStatus.ApplicationName);
            Console.WriteLine("**************************************************");
            #endregion

            Console.WriteLine();

            #region Http REST API Client
            Order orderWebApi = GetWebApiOrderStatus(orderNr).Result;

            Console.WriteLine("*************** Http REST API Client ***********************************");
            Console.WriteLine("Order Nr: " + orderWebApi.OrderNr);
            Console.WriteLine("Source Application: " + orderWebApi.OrderNr);
            Console.WriteLine("Approval Status: " + orderWebApi.Status);
            Console.WriteLine("Approval Status Text: " + orderWebApi.StatusText);
            Console.WriteLine("Application: " + orderWebApi.ApplicationName);
            Console.WriteLine("**************************************************");
            #endregion

        }

        private static async Task<Order> GetWebApiOrderStatus(string orderNr) {
            HttpClient client = new HttpClient();
            var serializer = new DataContractJsonSerializer(typeof(Order));
                        
            Task<System.IO.Stream> streamTask = client.GetStreamAsync("http://intranetcz.cz.eu.otis.utc.com/OtisOrder/api/OrderApi/GetOrderStatus?orderNr=" + orderNr);
            Order orderStatus = serializer.ReadObject(await streamTask) as Order;
            return orderStatus;
        }
    }

    public class Order {
        public string OrderNr = null;
        public int Status = 0;
        public string StatusText = null;
        public string ApplicationName = null;
    }
}
