using OTISCZ.InvoiceApproval.Controllers;
using OTISCZ.InvoiceApproval.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace OTISCZ.InvoiceApproval.Wcf {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OrderWcf" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select OrderWcf.svc or OrderWcf.svc.cs at the Solution Explorer and start debugging.
    public class OrderWcf : IOrderWcf {
        public Order GetOrderStatus(string orderNr) {
            return new OrderBaseController().GetOrderStatus(orderNr);
        }
    }
}
