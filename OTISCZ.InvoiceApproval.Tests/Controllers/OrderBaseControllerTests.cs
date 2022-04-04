using Microsoft.VisualStudio.TestTools.UnitTesting;
using OTISCZ.InvoiceApproval.Controllers;
using OTISCZ.InvoiceApproval.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace OTISCZ.InvoiceApproval.Controllers.Tests {
    [TestClass()]
    public class OrderBaseControllerTests {
        [TestMethod()]
        public void GetOrderStatusTest() {
            //Arrange


            //Act
            using (StreamReader sr = new StreamReader(@"c:\temp\ClcPur.csv")) {
                string strLine = null;
                while ((strLine = sr.ReadLine()) != null) {
                    string orderNr = strLine;
                    InvoiceApproval.Tests.WcfOrder.OrderWcfClient wcfOrder = new InvoiceApproval.Tests.WcfOrder.OrderWcfClient();
                    var wcfOrderStatus = wcfOrder.GetOrderStatus(orderNr);
                    Console.WriteLine(wcfOrderStatus.Status);
                    if (wcfOrderStatus.Status == 50) {
                        Assert.Fail();
                    }

                    InvoiceApproval.Tests.WsOrder.OrderWcf wsfOrder = new InvoiceApproval.Tests.WsOrder.OrderWcf();
                    var wsOrderStatus = wcfOrder.GetOrderStatus(orderNr);
                    if (wcfOrderStatus.Status == 50) {
                        Assert.Fail();
                    }

                    Order orderWebApi = GetWebApiOrderStatus(orderNr).Result;
                    if (orderWebApi.Status == 50) {
                        Assert.Fail();
                    }
                }
            }

            //Assert
            
        }

        private async Task<Order> GetWebApiOrderStatus(string orderNr) {
            HttpClient client = new HttpClient();
            var serializer = new DataContractJsonSerializer(typeof(Order));

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            //var streamTask = client.GetStreamAsync("http://localhost:65010/api/OrderApi/GetOrderStatus?orderNr=70939642");
            Task<System.IO.Stream> streamTask = client.GetStreamAsync("http://intranetcz.cz.eu.otis.utc.com/OtisOrder/api/OrderApi/GetOrderStatus?orderNr=" + orderNr);
            Order orderStatus = serializer.ReadObject(await streamTask) as Order;
            return orderStatus;
        }
    }
}